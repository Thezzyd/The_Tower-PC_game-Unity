using TMPro;
using UnityEngine;
using System;

public class EnemyJumpingSpider : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
    private Animator anim;
    private Rigidbody2D rb;

    private float jumpTimer;
    private bool isJumping;

    public float fallMultiplier;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    public bool grounded;
    public bool isHeroInRadius;

    private float actualLifePool;
    private float startingLifePool;

    public GameObject healthBar;
    private Vector3 locScale;

    public GameObject scalableObject;
    private float scaleMultiplier;

    private Vector3 scalePlus;
    private Vector3 scaleMinus;

    public TextMeshPro healthLeftText;

    private float spiderTriggeredMultiplier;

    private float bloodEffectTimer;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();

        rb = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        actualLifePool = enemiesManager.enemyJumpingSpiderLifePool;
        startingLifePool = actualLifePool;

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderScaleMultiplierMin, enemiesManager.enemyJumpingSpiderScaleMultiplierMax);

        transform.localScale = new Vector3(1f * scaleMultiplier, 1f * scaleMultiplier, 1f);

        scalePlus = new Vector3(scalableObject.transform.localScale.x, scalableObject.transform.localScale.y, 1f);
        scaleMinus = new Vector3(-scalableObject.transform.localScale.x, scalableObject.transform.localScale.y, 1f);

        spiderTriggeredMultiplier = 1.0f;

        jumpTimer = enemiesManager.enemyJumpingSpiderJumpPauseTime;

        if (levelManager.cameraAndEnemiesTargetPoint.position.x >= transform.position.x)
            scalableObject.transform.localScale = scalePlus;
        else scalableObject.transform.localScale = scaleMinus;
        bloodEffectTimer = 0;

        fallMultiplier = 3f;
    }


    void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;

        jumpTimer -= Time.fixedDeltaTime;

        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, WhatIsGround);
       
        isHeroInRadius = Physics2D.OverlapCircle(transform.position, enemiesManager.enemyJumpingSpiderSerachRadius, (1 << 25));

        if (grounded && isHeroInRadius)
        {
            SpiderJump();
        }

        if (grounded)
        {
            anim.SetBool("isJumping", false);

        }
        else
        {
            anim.SetBool("isJumping", true);
        }


        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

    }

    private void SpiderJump()
    {
        float jumpSpeedMultiplier = 1.0f;

    //    if (isSpiderTriggered)
     //       jumpSpeedMultiplier = 0.5f;

        if (jumpTimer <= 0f)
        {
            if (levelManager.cameraAndEnemiesTargetPoint.position.x >= transform.position.x)
            {
                scalableObject.transform.localScale = scalePlus;

             //   if (levelManager.cameraAndEnemiesTargetPoint.position.y - 2f >= transform.position.y)
             //   {
                    rb.AddForce(new Vector2(UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 0.9f, enemiesManager.enemyJumpingSpiderJumpPower * 1.1f), UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 1.3f, enemiesManager.enemyJumpingSpiderJumpPower * 1.4f)), ForceMode2D.Impulse);
              //  } //11 i 14 || 20 i 25
             //   else
             //   {
              //      rb.AddForce(new Vector2(UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 0.9f, enemiesManager.enemyJumpingSpiderJumpPower * 1.1f), UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 1.1f, enemiesManager.enemyJumpingSpiderJumpPower * 1.2f)), ForceMode2D.Impulse);
             //   }//11 i 14 || 14 i 17

                jumpTimer = enemiesManager.enemyJumpingSpiderJumpPauseTime * jumpSpeedMultiplier;
            }
            else
            {
                scalableObject.transform.localScale = scaleMinus;

             //   if (levelManager.cameraAndEnemiesTargetPoint.position.y - 2f >= transform.position.y)
             //   {
                    rb.AddForce(new Vector2(-UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 0.9f, enemiesManager.enemyJumpingSpiderJumpPower * 1.1f), UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 1.3f, enemiesManager.enemyJumpingSpiderJumpPower * 1.4f)), ForceMode2D.Impulse);
              //  } //11 i 14 || 20 i 25
              //  else
              //  {
              //      rb.AddForce(new Vector2(-UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 0.9f, enemiesManager.enemyJumpingSpiderJumpPower * 1.1f), UnityEngine.Random.Range(enemiesManager.enemyJumpingSpiderJumpPower * 1.1f, enemiesManager.enemyJumpingSpiderJumpPower * 1.2f)), ForceMode2D.Impulse);
              //  }//11 i 14 || 14 i 17

                jumpTimer = enemiesManager.enemyJumpingSpiderJumpPauseTime * jumpSpeedMultiplier;
            }
         //   isSpiderTriggered = false;
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

    public void DamageTaken(GameObject colliderGameObject, float damageMultiplier)
    {
        actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(colliderGameObject.tag, gameObject, colliderGameObject, damageMultiplier);
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
            enemiesManager.HitBloodEffect(enemiesManager.enemyJumpingSpiderHitBloodEffect, transform.position + new Vector3(UnityEngine.Random.Range(-0.35f, 0.35f), 0, 0));
            bloodEffectTimer = enemiesManager.bloodHitEffectReload;
        }

        if (actualLifePool <= 0)
            Death();
        else
            levelManager.audioManager.Play("EnemyJumpingSpiderHit", transform.position);

      //  spiderTriggeredMultiplier = 0.5f;

        SpiderJump();
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
        levelManager.audioManager.Play("EnemyJumpingSpiderDeath", transform.position);

        levelManager.ExpGain("EnemyJumpingSpider");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyJumpingSpiderDeathBloodEffect, transform.position);

        enemiesManager.DropOfEssence(enemiesManager.enemyJumpingSpiderEssenceDropMinQuantity, enemiesManager.enemyJumpingSpiderEssenceDropMaxQuantity, transform.position);

        Destroy(gameObject);
    }
}




