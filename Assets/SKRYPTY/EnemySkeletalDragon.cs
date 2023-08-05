using UnityEngine;
using Pathfinding;
using TMPro;
using System.Collections;
using System;

public class EnemySkeletalDragon : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;

    private float detectionRange;
    //private float fireRate;
    public GameObject fireballSpawner;

    private float startingLifePool;
    private float actualLifePool;

    public TextMeshPro healthLeftText;
    public GameObject healthBar;
    public Transform scalableObject;

    private float scaleMultiplier;
    private Vector3 locScale;
    private Vector3 scalePlus;
    private Vector3 scaleMinus;

    private AIPath aiPath;
    private Animator anim;
    //  private AIDestinationSetter skeletalDragonTarget;

    private bool isChangingColor;
    public SpriteRenderer skeletalDragonSprite;
    private Color baseColorSkeletalDragonSprite;
    private Color attackEndColorSkeletalDragonSprite;
    private float colorChangeTimeLeft;


    private int animationCyclesToAttack = 0;
    private float bloodEffectTimer;
    private void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponentInChildren<Animator>();
        aiPath = GetComponent<AIPath>();
     //   skeletalDragonTarget = GetComponent<AIDestinationSetter>();

        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemySkeletalDragonScaleMultiplierMin, enemiesManager.enemySkeletalDragonScaleMultiplierMax);
        transform.localScale = new Vector3(transform.localScale.x * scaleMultiplier, transform.localScale.y * scaleMultiplier, 1f);

        scalePlus = new Vector3(1f, 1f, 1f);
        scaleMinus = new Vector3(-1f, 1f, 1f);

        detectionRange = enemiesManager.enemySkeletalDragonDetectionRange;
        //fireRate = enemiesManager.enemySkeletalDragonFireRate;

        aiPath.maxSpeed = UnityEngine.Random.Range( enemiesManager.enemySkeletalDragonMoveSpeedMin, enemiesManager.enemySkeletalDragonMoveSpeedMax);
       
        actualLifePool = enemiesManager.enemySkeletalDragonLifePool;
        startingLifePool = actualLifePool;

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

        anim.SetFloat("dragonFlySpeed", 0.33f * aiPath.maxSpeed);

        baseColorSkeletalDragonSprite = skeletalDragonSprite.color;
        attackEndColorSkeletalDragonSprite = new Vector4(0.15f, 0.15f, 1.0f, 1.0f);
        colorChangeTimeLeft = 0.0f;
        bloodEffectTimer = 0;
    }

    public void BeginAttack()
    {
        animationCyclesToAttack++;

        if (animationCyclesToAttack >= 4 && Vector3.Distance(levelManager.cameraAndEnemiesTargetPoint.position, transform.position) <= detectionRange)
        {
            anim.SetBool("isAttacking", true);
            animationCyclesToAttack = 0;
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

    }

    public void SpawnProjectile()
    {
        Instantiate(enemiesManager.enemySkeletalDragonStage1Projectile, fireballSpawner.transform.position, Quaternion.identity);
        levelManager.audioManager.Play("EnemySkeletalDragonProjectile");
    }
   

    public void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;
       // fireRate -= Time.fixedDeltaTime;

       // if (Vector3.Distance(levelManager.cameraAndEnemiesTargetPoint.position, transform.position) <= detectionRange)
      //  { 
       //     CheckOfTimeToFire();
      //  }

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            scalableObject.localScale = scalePlus;
        }
        else
        {
            scalableObject.localScale = scaleMinus;
        }

        if (transform.position.y < levelManager.highestTowerYaxisValueReached - 30 - 100)
        {
            Destroy(gameObject);
        }

        aiPath.destination = levelManager.cameraAndEnemiesTargetPoint.position;

       /* if (isChangingColor && colorChangeTimeLeft < 1.0f)
        {
            skeletalDragonSprite.color = Color.Lerp(baseColorSkeletalDragonSprite, attackEndColorSkeletalDragonSprite,  colorChangeTimeLeft);
            colorChangeTimeLeft += Time.fixedDeltaTime / 2.0f;
        }
        else if(!isChangingColor && colorChangeTimeLeft < 1.0f)
        {
            skeletalDragonSprite.color = Color.Lerp(attackEndColorSkeletalDragonSprite, baseColorSkeletalDragonSprite, colorChangeTimeLeft);
            colorChangeTimeLeft += Time.fixedDeltaTime;
        }*/

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

        actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(colliderGameObject.tag, gameObject, colliderGameObject, damageMultiplier); ;
        actualLifePool = (float)Math.Round(actualLifePool, 2);


        if (colliderGameObject.Equals("AttackHero1") || colliderGameObject.Equals("AttackHero2"))
        {
            aiPath.canMove = false;
            StartCoroutine(TurnOnMovement(0.15f));
        }


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
            enemiesManager.HitBloodEffect(enemiesManager.enemySkeletalDragonHitBloodEffect, transform.position);
            bloodEffectTimer = enemiesManager.bloodHitEffectReload;
        }


        if (actualLifePool <= 0)
            Death();
        else
            levelManager.audioManager.Play("EnemySkeletalDragonHit", transform.position);

    }

    private IEnumerator TurnOnMovement(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        aiPath.canMove = true;

    }

    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            DamageTaken(other.gameObject, 1);
        }

    }

    public void MoveReturn()
    {
        gameObject.GetComponent<AIPath>().canMove = true;
    }


  /*  public void CheckOfTimeToFire()
    {
        if (fireRate <= 0f)
        {
            isChangingColor = true;
            StartCoroutine(SpawnProjectile(2.0f));
            fireRate = enemiesManager.enemySkeletalDragonFireRate;
            colorChangeTimeLeft = 0.0f; 
        }
    }*/

   /* private IEnumerator SpawnProjectile(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        Instantiate(enemiesManager.enemySkeletalDragonStage1Projectile, fireballSpawner.transform.position, Quaternion.identity);
        levelManager.audioManager.Play("EnemySkeletalDragonProjectile");
        isChangingColor = false;
        colorChangeTimeLeft = 0.0f;
    }*/


    public void Death()
    {

        levelManager.audioManager.Play("EnemySkeletalDragonHit", transform.position);

        levelManager.ExpGain("EnemySkeletalDragon");

        enemiesManager.DeathBloodEffect(enemiesManager.enemySkeletalDragonDeathBloodEffect, transform.position);

        enemiesManager.DropOfEssenceMiniBoss(enemiesManager.enemySkeletalDragonEssenceDropMinQuantity, enemiesManager.enemySkeletalDragonEssenceDropMaxQuantity, transform.position);

        Destroy(gameObject);
    }


}
