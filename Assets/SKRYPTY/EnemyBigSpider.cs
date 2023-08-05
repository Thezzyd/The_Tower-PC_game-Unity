using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class EnemyBigSpider : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
    private Animator anim;

    public GameObject healthBar;
    public Transform projectileSpwan;
    public TextMeshPro healthLeftText;
    public GameObject scalableObject;

    private bool isChangeDirectionAllowed;

    private float detectionRange;
    private bool movingRight = false;

    private float actualLifePool;
    private float startingLifePool;

    private Vector3 locScale;
    private Vector3 pudelko;

    private bool movePause;

    private Vector2 moveDirection;
    private Vector3 target;
    private bool isAttacking;
    private float scaleMultiplier;
    private Vector3 scalePlus;
    private Vector3 scaleMinus;

    private float moveSpeed;
    private float bloodEffectTimer;

    public void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponentInChildren<Animator>();

        actualLifePool = enemiesManager.enemyBigSpiderLifePool;
        startingLifePool = actualLifePool;

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemyBigSpiderScaleMultiplierMin, enemiesManager.enemyBigSpiderScaleMultiplierMax);

        transform.localScale = new Vector3(1f * scaleMultiplier, 1f * scaleMultiplier, 1f);
        
        scalePlus = new Vector3(1f , 1f , 1f);
        scaleMinus = new Vector3(-1f , 1f , 1f);

        if (transform.position.x < levelManager.cameraAndEnemiesTargetPoint.position.x)
            movingRight = true;
        else
            movingRight = false;

        if (!movingRight)
            scalableObject.transform.localScale = scaleMinus;
        else
            scalableObject.transform.localScale = scalePlus;

      
        moveSpeed = UnityEngine.Random.Range(enemiesManager.enemyBigSpiderMoveSpeedMin, enemiesManager.enemyBigSpiderMoveSpeedMax);
      
        anim.SetFloat("walkingSpeedMultiplier", 0.85f * moveSpeed);
        anim.SetFloat("attackSpeed", enemiesManager.enemyBigSpiderFireRate);

        isChangeDirectionAllowed = true;

        detectionRange = UnityEngine.Random.Range(enemiesManager.enemyBigSpiderDetectionRangeMin, enemiesManager.enemyBigSpiderDetectionRangeMax);
        bloodEffectTimer = 0;
    }

    void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;

        if (Vector3.Distance(levelManager.cameraAndEnemiesTargetPoint.position, transform.position) <= detectionRange)
        {
            //isAttacking = true;
            anim.SetBool("isAttacking", true);
            //  ForbidToMove();
            //  movePause = true;

            target = levelManager.cameraAndEnemiesTargetPoint.position;

            moveDirection = (target - transform.position).normalized;

            if (moveDirection.x < 0)
            {
                movingRight = false;
                scalableObject.transform.localScale = scaleMinus;
            }
            else
            {
                movingRight = true;
                scalableObject.transform.localScale = scalePlus;
            }

        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    

        if (movingRight && !movePause)
        {
            scalableObject.transform.localScale = scalePlus;
            transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
        }
        else if (!movingRight && !movePause)
        {
            scalableObject.transform.localScale = scaleMinus;
            transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
        }

       
    }

    IEnumerator ChangeDirection(float timer)
    {
        yield return new WaitForSeconds(timer);

        isChangeDirectionAllowed = true;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("LeftEdge") || coliderTag.Equals("LEFTWALL"))
        {
            movingRight = true;
            scalableObject.transform.localScale = scalePlus;
        }
        else if (coliderTag.Equals("RightEdge") || coliderTag.Equals("RIGHTWALL"))
        {
            movingRight = false;
            scalableObject.transform.localScale = scaleMinus;
        }

        else if (coliderTag.Equals("AttackHero1") || coliderTag.Equals("AttackHero2") || coliderTag.Equals("AttackHero3")
             || coliderTag.Equals("AttackHero6"))
        {
            DamageTaken(other.gameObject, 1);
        }

    }

    public void DamageTaken(GameObject colliderGameObject, float dmageMultiplier)
    {
        actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(colliderGameObject.tag, gameObject, colliderGameObject, dmageMultiplier); ;
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
            enemiesManager.HitBloodEffect(enemiesManager.enemyBigSpiderHitBloodEffect, transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0.2f, 0));
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

        GameObject projectile = Instantiate(enemiesManager.enemyBigSpiderProjectilePrefeb, projectileSpwan.position, Quaternion.identity);
        projectile.GetComponent<EnemyBigSpiderProjectile>().target = target;

        levelManager.audioManager.Play("EnemyBigSpiderProjectile", projectileSpwan.position);
       
    }

    public void AllowToMove()
    {
        movePause = false;
    }

    public void ForbidToMove()
    {
        movePause = true;
    }

    public void Death()
    {
        levelManager.audioManager.Play("EnemyBigSpiderDeath", transform.position);

        levelManager.ExpGain("EnemyBigSpider");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyBigSpiderDeathBloodEffect, transform.position + new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, 0));

        enemiesManager.DropOfEssence(enemiesManager.enemyBigSpiderEssenceDropMinQuantity, enemiesManager.enemyBigSpiderEssenceDropMaxQuantity, transform.position);

        Destroy(gameObject);

    }

}


    