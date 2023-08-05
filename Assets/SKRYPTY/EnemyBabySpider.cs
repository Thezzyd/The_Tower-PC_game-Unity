using UnityEngine;
using TMPro;
using System;

public class EnemyBabySpider : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
    private Animator anim;

    public TextMeshPro healthLeftText;

    public bool movingRight = true;

    private float actualLifePool;
    private float startingLifePool;

    public GameObject healthBar;

    public GameObject scalableObject;

    private float moveSpeed;
    private float scaleMultiplier;

    private Vector3 locScale;
    private Vector3 scalePlus;
    private Vector3 scaleMinus;

    private float bloodEffectTimer;
    public void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponentInChildren<Animator>();

        actualLifePool = enemiesManager.enemyBabySpiderLifePool;
        startingLifePool = actualLifePool;

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool + " / " + startingLifePool+" HP";

        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemyBabySpiderScaleMultiplierMin, enemiesManager.enemyBabySpiderScaleMultiplierMax);

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

        moveSpeed = UnityEngine.Random.Range(enemiesManager.enemyBabySpiderMoveSpeedMin, enemiesManager.enemyBabySpiderMoveSpeedMax);

        anim.SetFloat("walkingSpeedMultiplier", 0.7f * moveSpeed);
        bloodEffectTimer = 0;
    }

    void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;

        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
        }
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


    public void DamageTaken(GameObject colliderGameObject, float damagemultiplier)
    {
        if (enemiesManager == null)
            enemiesManager = FindObjectOfType<EnemiesManager>();

       actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(colliderGameObject.tag, gameObject, colliderGameObject, damagemultiplier);
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
            enemiesManager.HitBloodEffect(enemiesManager.enemyBabySpiderHitBloodEffect, transform.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0, 0));
            bloodEffectTimer = enemiesManager.bloodHitEffectReload;
        }


        if (actualLifePool <= 0)
            Death();
        else
            levelManager.audioManager.Play("EnemyBabySpiderHit", transform.position);
    }

    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            DamageTaken(other, 1);
        }

    }

    public void Death()
    {
        if (levelManager == null)
            levelManager = FindObjectOfType<LevelManager>();

        levelManager.audioManager.Play("EnemyBabySpiderDeath", transform.position);

        levelManager.ExpGain("EnemyBabySpider");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyBabySpiderDeathBloodEffect, transform.position);

        enemiesManager.DropOfEssence(enemiesManager.enemyBabySpiderEssenceDropMinQuantity, enemiesManager.enemyBabySpiderEssenceDropMaxQuantity, transform.position);

        Destroy(gameObject);
    }

}


