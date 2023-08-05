using TMPro;
using UnityEngine;
using System;

public class EnemyBossSpider: MonoBehaviour
{
    private EnemiesManager enemiesManager;
    private HeroesManager heroesManager;
    private SkillsUpgradeManager skillsUpgradeManager;

    [HideInInspector] public LevelManager levelManager;
    private Rigidbody2D rb;
    public float eventTimer;
    [HideInInspector] public bool isWalking;
    [HideInInspector] public Animator anim;
    public float fallMultiplier = 10f;
    public GameObject poruszanie;
  //  public Transform groundCheck;
   // public float groundCheckRadius;
  //  public LayerMask WhatIsGround;
  //  public bool grounded;
    public float moveSpeed;
    public bool afterJump;
    [HideInInspector] public float Hero2overallKnockback;

    [HideInInspector] public float coinTimeToDestruction = 10f;
    [HideInInspector] public float heartTimeToDestruction = 20f;

    [HideInInspector] public int chanceToDropCoin = 4;   // szansa = 1/chanceToDropCoin
    [HideInInspector] public int chanceToDropHeart = 20;
    public bool right;

    public Transform[] bulletSpawnPoint;

    public float lifePool;

    private float lifePoolStala;

    public GameObject healthBar;
    [System.NonSerialized]
    public Vector3 locScale;

   // public ItemsManager itemsManager;

    public GameObject obracac;
    public float scaleMultiplier;
    private Vector3 scalePlus;
    private Vector3 scaleMinus;

    public TextMeshPro healthLeftText;
    public Transform groundDetection;
    [HideInInspector] public int layer_mask;
    public float distance;
    public float stregthX;
    public float stregthY;
   // public Collider2D firstCollider;
  //  public Collider2D secondCollider;
    public Collider2D[] colliders;
    public Transform parentPosition;
    public GameObject paretnGameObject;
    public PlatformEffector2D effector2D;
    public Transform bossStopPoint1;
    public Transform bossStopPoint2;



    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();

        levelManager = FindObjectOfType<LevelManager>();
      
        layer_mask = LayerMask.GetMask("Ground");
        scaleMultiplier = 1f;
      //  Debug.Log(effector2D.colliderMask);
       // scaleMultiplier = UnityEngine.Random.Range(0.85f, 1.1f);
        scalePlus = new Vector3(3.2f* scaleMultiplier, 3.2f * scaleMultiplier, 1f);
        scaleMinus = new Vector3(-3.2f * scaleMultiplier, 3.2f * scaleMultiplier, 1f);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        eventTimer = 0f;

      //  lifePool = LevelManager.lifePoolSpiderBoss;
        lifePoolStala = lifePool;
        moveSpeed = 1.5f;
        locScale = healthBar.transform.localScale;
        healthLeftText.text = lifePool + " / " + lifePoolStala;

        if (levelManager.cameraAndEnemiesTargetPoint.position.x >= transform.position.x)
            obracac.transform.localScale = scalePlus;
        else obracac.transform.localScale = scaleMinus;

   
      //  colliders = GetComponentsInParent<CapsuleCollider2D>();
        colliders = GetComponents<CapsuleCollider2D>();
    }


    void FixedUpdate()
    {

        if (parentPosition.position.y > levelManager.cameraAndEnemiesTargetPoint.position.y + 3 && !afterJump)
        {

            //   paretnGameObject.layer = 19;
            //   effector2D.useColliderMask = false;
            // effector2D.colliderMask = (1 << 18);
            //  effector2D.colliderMask = (1 << 0);
            colliders[0].enabled = false;
            colliders[1].enabled = false;
        }
        else if(!afterJump)
        {
            //  paretnGameObject.layer = 17;




            //effector2D.useColliderMask = true;

            colliders[0].enabled = true;
            colliders[1].enabled = true;

        }

        eventTimer += Time.fixedDeltaTime;
        if (parentPosition.position.x < bossStopPoint1.position.x)
        {
            rb.velocity = new Vector2(1, rb.velocity.y);
        }
        else if(parentPosition.position.x > bossStopPoint2.position.x)
        {
            rb.velocity = new Vector2(-1, rb.velocity.y);
        }
        //   grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);

          

        if (eventTimer >= 4f)
            {
                  
                var ranNum = UnityEngine.Random.Range(0,2);
                if (ranNum == 0)
                {
               // anim.SetBool("isWalking", false);
                anim.SetTrigger("isJumping");
                }

                else if (ranNum == 1)
                {
               // anim.SetBool("isWalking", false);
                anim.SetTrigger("isAttacking");

                    if (levelManager.cameraAndEnemiesTargetPoint.position.x >= transform.position.x)
                    {  obracac.transform.localScale = scalePlus;  }
                    else
                    { obracac.transform.localScale = scaleMinus;  }

              //  float ranNum2 = UnityEngine.Random.Range(2f, 3f);
             //   Invoke("WalkingDeactivation", ranNum2);

                }

                else if (ranNum == 2)
                {
                anim.SetTrigger("isWalking");
               /* isWalking = true;

                if (levelManager.cameraTarget.position.x >= transform.position.x)
                {
                    obracac.transform.localScale = scalePlus;
                    right = true;
                }
                else
                {
                    obracac.transform.localScale = scaleMinus;
                    right = false;
                }*/

            }
                

                eventTimer = 0f;

            }

        if (isWalking)
        {
            if(right)
            {
                RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, layer_mask);
                poruszanie.transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                poruszanie.transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
            }

        }
        

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

    }

    public void Attack()
    {
        if (levelManager.cameraAndEnemiesTargetPoint.position.x >= transform.position.x)
        {
            obracac.transform.localScale = scalePlus;
            right = true;
        }
        else
        {
            obracac.transform.localScale = scaleMinus;
            right = false;
        }

        Instantiate(enemiesManager.spiderBossBulletPrefab, bulletSpawnPoint[0].position, enemiesManager.spiderBossBulletPrefab.transform.rotation);
        Instantiate(enemiesManager.spiderBossBulletPrefab, bulletSpawnPoint[1].position, enemiesManager.spiderBossBulletPrefab.transform.rotation);
        Instantiate(enemiesManager.spiderBossBulletPrefab, bulletSpawnPoint[2].position, enemiesManager.spiderBossBulletPrefab.transform.rotation);
        Instantiate(enemiesManager.spiderBossBulletPrefab, bulletSpawnPoint[3].position, enemiesManager.spiderBossBulletPrefab.transform.rotation);
        Instantiate(enemiesManager.spiderBossBulletPrefab, bulletSpawnPoint[4].position, enemiesManager.spiderBossBulletPrefab.transform.rotation);
        
    }

    public void WalkingDeactivation()
    {
     //   anim.SetBool("isWalking", false);
        isWalking = false;
    }
    public void WalkingActivation()
    {
        //   anim.SetBool("isWalking", false);
        isWalking = true;

        if (levelManager.cameraAndEnemiesTargetPoint.position.x >= transform.position.x)
        {
            obracac.transform.localScale = scalePlus;
            right = true;
        }
        else
        {
            obracac.transform.localScale = scaleMinus;
            right = false;
        }
    }



    public void Skakanie()
    {
        afterJump = false;
        float jumpX = Vector2.Distance(transform.position, levelManager.cameraAndEnemiesTargetPoint.position) * stregthX;
        float jumpY = Vector2.Distance(transform.position, levelManager.cameraAndEnemiesTargetPoint.position) * stregthY;
        jumpY = 300;

        if (levelManager.cameraAndEnemiesTargetPoint.position.x >= transform.position.x)
        {
            obracac.transform.localScale = scalePlus;
            if (levelManager.cameraAndEnemiesTargetPoint.position.y >= transform.position.y)
            {
                    //rb.AddForce(new Vector2(UnityEngine.Random.Range(110f, 140f), UnityEngine.Random.Range(200f, 250f)), ForceMode2D.Impulse); 

                    Debug.Log("JumpPower:"+ jumpX +" && "+ jumpY );
                rb.AddForce(new Vector2( jumpX, jumpY), ForceMode2D.Impulse); 
            }
            else 
            {
                //  rb.AddForce(new Vector2(UnityEngine.Random.Range(110f, 140f), UnityEngine.Random.Range(140f, 170f)), ForceMode2D.Impulse); 
                Debug.Log("JumpPower:" + jumpX + " && " + jumpY);

                rb.AddForce(new Vector2(jumpX, jumpY), ForceMode2D.Impulse);
            }
        }
        else
        {
            obracac.transform.localScale = scaleMinus;
            if (levelManager.cameraAndEnemiesTargetPoint.position.y >= transform.position.y)
            {
                Debug.Log("JumpPower:" + jumpX + " && " + jumpY);

                rb.AddForce(new Vector2(-jumpX, jumpY), ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("JumpPower:" + jumpX + " && " + jumpY);

                rb.AddForce(new Vector2(-jumpX, jumpY), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("POCISK"))
        {


            FindObjectOfType<AudioManager>().Play("EnemyHit");
            if (OptionsManager.screenShake)
                CinemachineCameraShake.Instance.ShakeCamera(1.6f, 0.1f);
      //      if (skillsUpgradeManager.Hero1ProjectileKnockbackLevel > 1)
      //      {
                if (other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2((heroesManager.Hero1LaserKnockbackValue), 0);
                else gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-(heroesManager.Hero1LaserKnockbackValue), 0f);
         //   }


            float damageAfterMultiplier = (heroesManager.Hero1LaserDamageValue) * heroesManager.HeroesDamageMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;



            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;

            Blood();

        }


        else if (other.gameObject.CompareTag("MIECZ"))
        {


            FindObjectOfType<AudioManager>().Play("EnemyHit");
            if (OptionsManager.screenShake)
                CinemachineCameraShake.Instance.ShakeCamera(1.5f, 0.1f);
            if (skillsUpgradeManager.Hero2WeaponKnockbackLevel > 1)
            {

                if (levelManager.cameraAndEnemiesTargetPoint.position.x >= gameObject.transform.position.x)

                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Hero2overallKnockback, 0);

                else gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Hero2overallKnockback, 0f);
            }

            FindObjectOfType<AudioManager>().Play("SwordHit");


            float damageAfterMultiplier = heroesManager.Hero2WeaponDamageValue * heroesManager.Hero2WeaponCriticalStrikeMultiplierValue;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;

            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;

            if (UnityEngine.Random.Range(0.0f, 1.0f) <= heroesManager.Hero2WeaponCriticalStrikeChanceValue)
            {
                Vector2 pozycja = gameObject.transform.position;
                pozycja.y += 2f;
                var crit = Instantiate(heroesManager.critTextPrefabHERO2, pozycja, gameObject.transform.rotation);
                Destroy(crit, 0.2f);
            }

            Blood();

        }

        else if (other.CompareTag("CANONBULLET"))
        {
            ///////

            if (OptionsManager.screenShake)
                CinemachineCameraShake.Instance.ShakeCamera(1.1f, 0.1f);
            FindObjectOfType<AudioManager>().Play("EnemyHit");

            float damageAfterMultiplier = (heroesManager.Hero3ProjectileDamageValue ) * heroesManager.HeroesDamageMultiplier;
            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;




            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;



            /// Debug.Log("Trafilo" + lifePool);
            Blood();

            Destroy(other.gameObject);


        }
        else if (other.CompareTag("AttackHero6"))
        {
            ///////

            if (OptionsManager.screenShake)
                CinemachineCameraShake.Instance.ShakeCamera(1.1f, 0.1f);
            FindObjectOfType<AudioManager>().Play("EnemyHit");

            float damageAfterMultiplier = heroesManager.Hero6ProjectileDamageValue * heroesManager.HeroesDamageMultiplier;
            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;

            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;

            //  Debug.Log("Trafilo" + lifePool);
            Blood();

            // Destroy(other.gameObject);


        }
        //   Debug.Log(other.tag);


        Death();


    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("LightingHERO4"))
        {
            if (OptionsManager.screenShake)
                CinemachineCameraShake.Instance.ShakeCamera(1.3f, 0.1f);
            //  Debug.Log("LIffffffffffffGHTING!!!!!!!!!!!");

            float damageAfterMultiplier = (heroesManager.Hero4BeamDamageValue) * heroesManager.HeroesDamageMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;


            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;


            Blood();

        }
        else if (other.CompareTag("AttackHERO5"))
        {
            //   Debug.Log("LIffffffffffffGHTING!!!!!!!!!!!");
            float damageAfterMultiplier = (heroesManager.Hero5ProjectileDamageValue) * heroesManager.HeroesDamageMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;
            if (OptionsManager.screenShake)
                CinemachineCameraShake.Instance.ShakeCamera(2f, 0.1f);


            locScale.x -= ((damageAfterMultiplier) / lifePoolStala);
            if (locScale.x >= 0f)
                healthBar.transform.localScale = locScale;
            else
            {
                locScale.x = 0f;
                healthBar.transform.localScale = locScale;
            }
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;



            Blood();

        }

        Death();

    }

    public void Blood()
    {
        var blood1 = Instantiate(enemiesManager.enemyEggSpiderHitBloodEffect, transform.position, enemiesManager.enemyEggSpiderHitBloodEffect.transform.rotation);
        Destroy(blood1, 3f);
    }


    public void DeathBlood()
    {

        var blood2 = Instantiate(enemiesManager.enemyEggSpiderDeathBloodEffect, transform.position, enemiesManager.enemyEggSpiderDeathBloodEffect.transform.rotation);
        Destroy(blood2, 3f);

    }


    public void Death()
    {
        if (lifePool <= 0)
        {

            FindObjectOfType<AudioManager>().Play("MedusaDeath");


            chanceToDropCoin = 4; // szansa = 1/chanceToDropCoin
            chanceToDropHeart = 32;

        
          
            levelManager.ExpGain("JumpingSpider");

            // levelManager.killCounter += 1;
            // levelManager.KillCounter();
          //  itemsManager.GemDrop(gameObject.transform.position, 100f * levelManager.itemDropChanceMultiplier);

            DeathBlood();

            Destroy(gameObject);
        }
    }

    public void DestroyDeath()
    {
        FindObjectOfType<AudioManager>().Play("BatDeath");
        // isDestroyed = true;
        DeathBlood();
        Destroy(gameObject);
    }


}




