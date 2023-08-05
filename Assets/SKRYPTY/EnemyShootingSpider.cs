using UnityEngine;
using TMPro;
using System;

public class EnemyShootingSpider : MonoBehaviour
{



    private EnemiesManager enemiesManager;
    private HeroesManager heroesManager;
    private SkillsUpgradeManager skillsUpgradeManager;

    [HideInInspector] public float Hero2overallKnockback;

    [HideInInspector] public float coinTimeToDestruction = 10f;
    [HideInInspector] public float heartTimeToDestruction = 20f;

    [HideInInspector] public int chanceToDropCoin = 4;   // szansa = 1/chanceToDropCoin
    [HideInInspector] public int chanceToDropHeart = 20;

    public TextMeshPro healthLeftText;

    [HideInInspector] public bool movingRight = true;

    
    [System.NonSerialized]
    private float lifePool;
    [System.NonSerialized]
    private float lifePoolStala;

    [System.NonSerialized]
    public LevelManager levelManager;

    //HealthBar
    public GameObject healthBar;

    [System.NonSerialized]
    public Vector3 locScale;

 //   public ItemsManager itemsManager;

    public GameObject obracac;
    public GameObject poruszanie;
    public float moveSpeed;
    public float scaleMultiplier;

    private Vector3 scalePlus;
    private Vector3 scaleMinus;
    private Animator anim;
    public bool stojPanie;
    public Transform shootingPoint;

    private float attackTimer;


    public void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();

        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponentInChildren<Animator>();

       

        attackTimer = 0.5f;

        lifePool = enemiesManager.lifePoolShootingSpider;
        lifePoolStala = lifePool;

        
        locScale = healthBar.transform.localScale;
        healthLeftText.text = lifePool + " / " + lifePoolStala;


        scaleMultiplier = UnityEngine.Random.Range(0.85f, 1.1f);
        scalePlus = new Vector3(1f * scaleMultiplier, 1f * scaleMultiplier, 1f);
        scaleMinus = new Vector3(-1f * scaleMultiplier, 1f * scaleMultiplier, 1f);

        if (!movingRight)
            transform.localScale = scaleMinus;
        else transform.localScale = scalePlus;

        moveSpeed = UnityEngine.Random.Range(enemiesManager.moveSpeedShootingSpider - 0.2f, enemiesManager.moveSpeedShootingSpider + 0.2f);
      //  moveSpeed = 1.2f;
        anim.SetFloat("walkingSpeedMultiplier", 0.5f * moveSpeed);
    }

    public void Attack()
    {
      //  Invoke("AllowWalking", 0.5f);


        for (float i = 0.1f; i < 0.4f; i += 0.1f)
        {
            Invoke("shootProjectile", i);
        }

    }

   /* public void AllowWalking()
    {
        stojPanie = false;
    }*/

    public void shootProjectile()
    {
      var pocisk =  Instantiate(enemiesManager.enemyEggSpiderPrefab, shootingPoint.position, enemiesManager.enemyEggSpiderPrefab.transform.rotation);
      /*  if (transform.localScale.x > 0f)
            pocisk.GetComponent<EnemyEggSpider>().right = true;
        else
            pocisk.GetComponent<EnemyEggSpider>().right = false;*/

    }

    void FixedUpdate()
    {
        attackTimer -= Time.fixedDeltaTime;

        if (attackTimer <= 0f)
        {
            stojPanie = true;
           // anim.SetBool("isAttacking", true);
            anim.SetTrigger("isAttacking");
            Invoke("Attack", 1.4f);
           //Attack();
            attackTimer = enemiesManager.shootingSpiderAttackTimer;


        }


        if (movingRight && !stojPanie)
        {
            poruszanie.transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);
        }
        else if(!movingRight && !stojPanie)
        {
            poruszanie.transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("turnEnemy"))
        {

            if (movingRight)
            {
                movingRight = false;
                transform.localScale = scaleMinus;
            }
            else
            {
                movingRight = true;
                transform.localScale = scalePlus;
            }
        }

        else if (other.CompareTag("POCISK"))
        {


            FindObjectOfType<AudioManager>().Play("EnemyHit");
            if (OptionsManager.screenShake)
                CinemachineCameraShake.Instance.ShakeCamera(1.4f, 0.1f);
       //     if (skillsUpgradeManager.Hero1ProjectileKnockbackLevel > 1)
        //    {
                if (other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2((heroesManager.Hero1LaserKnockbackValue), 0);
                else gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-(heroesManager.Hero1LaserKnockbackValue ), 0f);
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
            float damageAfterMultiplier = (heroesManager.Hero5ProjectileDamageValue ) * heroesManager.HeroesDamageMultiplier;

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
        var blood1 = Instantiate(enemiesManager.enemyEggSpiderHitBloodEffect, obracac.transform.position, obracac.transform.rotation);
        Destroy(blood1, 3f);
    }


    public void DeathBlood()
    {

        var blood2 = Instantiate(enemiesManager.enemyEggSpiderDeathBloodEffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(blood2, 3f);

    }



    public void Death()
    {
        if (lifePool <= 0)
        {

            FindObjectOfType<AudioManager>().Play("MedusaDeath");


            chanceToDropCoin = 3; // szansa = 1/chanceToDropCoin
            chanceToDropHeart = 40;

        


            levelManager.ExpGain("ShootingSpider");

            //  levelManager.killCounter += 1;
            // levelManager.KillCounter();
          //  itemsManager.GemDrop(gameObject.transform.position, 180f * levelManager.itemDropChanceMultiplier);

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


