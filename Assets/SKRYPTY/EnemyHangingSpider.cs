using System;
using UnityEngine;
using TMPro;

public class EnemyHangingSpider : MonoBehaviour
{
    private EnemiesManager enemiesManager;
    private RaycastHit2D raycastHit;

    public Transform raycastOrigin;
    public HingeJoint2D webStringAnchor;
    public TextMeshPro healthLeftText;
    public GameObject healthBar;
    public CircleCollider2D childCricleCollider;
    public CapsuleCollider2D childCapsuleCollider;
    public Animator spriteAnimator;

    public Transform rotatingObject;

    public bool isFalling;

    private float animationSpeed;                 //param

    private float raycastRange;
    private bool isTransitionToBallDone;
    private float projectileSpeed;                     
    private Vector3 targetPosition;

    private float cooldownTimer; 

    private float actualLifePool;                    
    private float startingLifePool; 

    private float moveSpeed;                             
    private float scaleMultiplier;                    

    private Vector3 locScale;

    private Rigidbody2D rb;
    private LevelManager levelManager;

    public PlatformComponent platformComponentThisSpiderIsStandingOn;
    private float bloodEffectTimer;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        rb = GetComponent<Rigidbody2D>();

        actualLifePool = enemiesManager.enemyHangingSpiderLifePool;
        startingLifePool = actualLifePool;

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemyHangingSpiderScaleMultiplierMin, enemiesManager.enemyHangingSpiderScaleMultiplierMax);

        transform.localScale = new Vector3(1f * scaleMultiplier, 1f * scaleMultiplier, 1f);


        animationSpeed = enemiesManager.enemyHangingSpiderAnimationSpeed;
        spriteAnimator.SetFloat("animSpeed", animationSpeed);

        moveSpeed = UnityEngine.Random.Range(enemiesManager.enemyHangingSpiderMoveSpeedMin, enemiesManager.enemyHangingSpiderMoveSpeedMax);

        cooldownTimer = 0.5f; // chyba dobrze jak zostanie stała...

        projectileSpeed = enemiesManager.enemyHangingSpiderProjectileSpeed;
        raycastRange = 24.0f;  // chyba dobrze jak zostanie stała...

        bloodEffectTimer = 0;

        rb.AddForce(transform.right * UnityEngine.Random.Range(0, 30), ForceMode2D.Impulse);
    }


    void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;
        cooldownTimer -= Time.fixedDeltaTime;
      
        if (!isTransitionToBallDone)
        {
            raycastHit = Physics2D.CircleCast(raycastOrigin.position, 0.5f, Vector2.down, raycastRange, (1 << 8) | (1 << 25));

            if (raycastHit)
            {
                if (raycastHit.collider.gameObject.layer == 25 && cooldownTimer <= 0.0f)
                {
                    cooldownTimer = 0.5f;
                    spriteAnimator.SetTrigger("attack");
                    targetPosition = raycastHit.collider.gameObject.transform.position;
                }
            }
        }

        if (isFalling && !isTransitionToBallDone)
        {
            SpiderBallActivation();
        }

        if (platformComponentThisSpiderIsStandingOn != null && isTransitionToBallDone)
        {
            if (platformComponentThisSpiderIsStandingOn.isHeroOnThisPlatform)
            {
                if (levelManager.cameraAndEnemiesTargetPoint.position.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                    rotatingObject.Rotate(0, 0, 5, Space.Self);
                }
                else
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                    rotatingObject.Rotate(0, 0, -5, Space.Self);
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
             //   rb.rotation = 0;
            }
        }
    }

    public void SpawnProjectile()
    {
        levelManager.audioManager.Play("EnemyHangingSpiderProjectile", transform.position);
        GameObject spawnedProjectile = enemiesManager.enemyHangingSiderProjectilePrefab;

        spawnedProjectile = Instantiate(spawnedProjectile);
        spawnedProjectile.transform.position = raycastOrigin.position;
       

        Vector3 direction = spawnedProjectile.transform.position - targetPosition;

        spawnedProjectile.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;

        Destroy(spawnedProjectile, 10.0f);

    }

    public void SpiderBallActivation()
    {
        spriteAnimator.SetTrigger("ball");
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
        childCapsuleCollider.enabled = false;
        childCricleCollider.enabled = true;
        GetComponent<HingeJoint2D>().enabled = false;

        isTransitionToBallDone = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SpiderBallActivation();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        string coliderTag = other.tag;

    
        if (coliderTag.Equals("AttackHero1") || coliderTag.Equals("AttackHero2") || coliderTag.Equals("AttackHero3") || coliderTag.Equals("AttackHero6"))
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
            enemiesManager.HitBloodEffect(enemiesManager.enemyHangingSpiderHitBloodEffect, transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-1.2f, -0.5f), 0));
            bloodEffectTimer = enemiesManager.bloodHitEffectReload;
        }

        if (actualLifePool <= 0)
            Death();
        else
            levelManager.audioManager.Play("EnemyHangingSpiderHit", transform.position);
    }

    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            DamageTaken(other.gameObject, 1);
        }

    }

    public void Death()
    {
        levelManager.audioManager.Play("EnemyHangingSpiderDeath", transform.position);

        levelManager.ExpGain("EnemyHangingSpider");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyHangingSpiderDeathBloodEffect, transform.position);

        enemiesManager.DropOfEssence(enemiesManager.enemyHangingSpiderEssenceDropMinQuantity, enemiesManager.enemyHangingSpiderEssenceDropMaxQuantity, transform.position);

        Destroy(gameObject);
    }
}
