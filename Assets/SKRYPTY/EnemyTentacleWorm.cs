using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class EnemyTentacleWorm : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
    private Animator anim;

    public GameObject healthBar;
    public Transform projectileSpwan;
    public TextMeshPro healthLeftText;
    public GameObject scalableObject;


    private float detectionRange;
    private bool movingRight = false;

    private float actualLifePool;
    private float startingLifePool;

    private Vector3 locScale;
    private Vector3 pudelko;

    private Vector2 moveDirection;
    private Vector3 target;
    private bool isAttacking;
    private float scaleMultiplier;
    private Vector3 scalePlus;
    private Vector3 scaleMinus;
    private RaycastHit2D raycastHit;
    private int layerMask;
    private float moveSpeed;
    private float bloodEffectTimer;
    private float radiusOfProjectile;
    Vector2 directionOfRaycast;

    public void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponentInChildren<Animator>();

        actualLifePool = enemiesManager.enemyTentacleWormLifePool;
        startingLifePool = actualLifePool;

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

        anim.SetFloat("AttackSpeed", enemiesManager.enemyTentacleWormFireRate);

        detectionRange = UnityEngine.Random.Range(enemiesManager.enemyTentacleWormDetectionRangeMin, enemiesManager.enemyTentacleWormDetectionRangeMax);
        bloodEffectTimer = 0;
        radiusOfProjectile = 0.204f;
        layerMask = ((1 << 8) | (1 << 24) | (1 << 25)); //ground | webString | hero
    }

    void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;

        if (Vector3.Distance(levelManager.cameraAndEnemiesTargetPoint.position, transform.position) <= detectionRange)
        {   
            Vector2 directionOfRaycast = levelManager.cameraAndEnemiesTargetPoint.position - projectileSpwan.position;
            raycastHit = Physics2D.CircleCast(projectileSpwan.position, radiusOfProjectile, directionOfRaycast, detectionRange, layerMask);
            
            if(raycastHit && raycastHit.transform.gameObject.layer == 25){
                //Debug.Log("Layer of enemyTentacle raycast hit: "+raycastHit.transform.gameObject.layer);
                anim.SetBool("isAttacking", true);
                target = levelManager.cameraAndEnemiesTargetPoint.position;
            }
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

    }

    public void SetStartingDirectionScale(bool isLeftWall) {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemyTentacleWormScaleMultiplierMin, enemiesManager.enemyTentacleWormScaleMultiplierMax);

        if (!isLeftWall)
        {
            transform.localScale = new Vector3(-1f * scaleMultiplier, 1f * scaleMultiplier, 1f);
            healthLeftText.gameObject.GetComponent<RectTransform>().localScale = new Vector2( healthLeftText.gameObject.transform.localScale.x, -healthLeftText.gameObject.transform.localScale.y);
            healthLeftText.gameObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 270);
        }
        else 
        {
            transform.localScale = new Vector3(1f * scaleMultiplier, 1f * scaleMultiplier, 1f);
            healthLeftText.gameObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 90);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        string coliderTag = other.tag;
       
        if (coliderTag.Equals("AttackHero1") || coliderTag.Equals("AttackHero2") || coliderTag.Equals("AttackHero3")
             || coliderTag.Equals("AttackHero6"))
        {
            DamageTaken(other.gameObject, 1);
        }

    }
    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.layer == 8){
            Destroy(gameObject);
        }
        Debug.Log("ajajjajaja");
    }

    public void DamageTaken(GameObject colliderGameObject, float dmageMultiplier)
    {
        actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(colliderGameObject.tag, gameObject, colliderGameObject, dmageMultiplier);
        actualLifePool = (float)Math.Round(actualLifePool, 2);

        locScale.x = (actualLifePool / startingLifePool);

        if (locScale.x >= 0f)
            healthBar.transform.localScale = locScale;
        else
        {
            locScale.x = 0f;
            healthBar.transform.localScale = locScale;
        }

        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";


        if (bloodEffectTimer <= 0)
        {
            enemiesManager.HitBloodEffect(enemiesManager.enemyTentacleWormHitBloodEffect, projectileSpwan.position);
            bloodEffectTimer = enemiesManager.bloodHitEffectReload;
        }

        if (actualLifePool <= 0)
            Death();
        else
            levelManager.audioManager.Play("EnemyBigSpiderHit", transform.position);
    }

    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            DamageTaken(other, 1);
        }

    }


    public void SpawnProjectile()
    {

        GameObject projectile = Instantiate(enemiesManager.enemyTentacleWormProjectilePrefeb, projectileSpwan.position, Quaternion.identity);
        projectile.GetComponent<EnemyTentacleWormProjectile>().target = target;

        levelManager.audioManager.Play("EnemyBigSpiderProjectile", projectileSpwan.position);
       
    }

    public void Death()
    {
        levelManager.audioManager.Play("EnemyBigSpiderDeath", transform.position);

        levelManager.ExpGain("EnemyTentacleWorm");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyTentacleWormDeathBloodEffect, projectileSpwan.position);

        enemiesManager.DropOfEssence(enemiesManager.enemyTentacleWormEssenceDropMinQuantity, enemiesManager.enemyTentacleWormEssenceDropMaxQuantity, projectileSpwan.position);

        Destroy(gameObject);

    }

}


    