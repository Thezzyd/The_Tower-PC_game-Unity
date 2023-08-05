
using UnityEngine;
using TMPro;
using System;
/*
public class Patrol : MonoBehaviour
{

    [HideInInspector] public float coinTimeToDestruction = 10f;
    [HideInInspector] public float heartTimeToDestruction = 20f;

    public Transform patrolEnemyBulletSpawnerPrefab;

    [HideInInspector] public int chanceToDropCoin = 13; // szansa = 1/chanceToDropCoin
    [HideInInspector] public int chanceToDropHeart = 15;
    [HideInInspector] public float Hero2overallKnockback;

    public GameObject obracac;

    [HideInInspector] private float lifePool = 75f;

    [HideInInspector] private float lifePoolStala;
    public GameObject healthBar;
    [HideInInspector] public Vector3 locScale;
    [HideInInspector] public Vector3 pudelko;

    [HideInInspector] public Vector2 swordPosition;

    public TextMeshPro healthLeftText;
    [HideInInspector] public ItemsManager itemsManager;
    [HideInInspector] public float speed;
    [HideInInspector] public float distance;
    [HideInInspector] public int layer_mask;
    [HideInInspector] public Animator Anim;
    [HideInInspector] private bool movingRight = true;
    [HideInInspector] public float shootingTimer = 0f;
    [HideInInspector] public bool attacking;
    [HideInInspector] public bool canIAttack = true;

    [HideInInspector] public float range = 20f;
    [HideInInspector] public float fireRate;
    [HideInInspector] public float nextFire;

    [HideInInspector] public LevelManager levelManager;

    public Transform groundDetection;

    public void Start()
    {
        layer_mask = LayerMask.GetMask("Ground");
        Anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        itemsManager = FindObjectOfType<ItemsManager>();

        lifePool = LevelManager.lifePooPatrolEnemy;
        lifePoolStala = lifePool;

        locScale = healthBar.transform.localScale;
        pudelko = locScale;
        healthLeftText.text = lifePool + " / " + lifePoolStala;

       Hero2overallKnockback = levelManager.knockbackStrengthHERO2 + itemsManager.itemSwordKnockbackHERO2;

        fireRate = levelManager.patrolFireRate;
        fireRate = 4f;
        nextFire = Time.time;
    }

    private void FixedUpdate()
    {

        if (Vector3.Distance(levelManager.cameraTarget.position, transform.position) <= range)
        {
            CheckOfTimeToFire();
            attacking = true;

            if (levelManager.cameraTarget.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                obracac.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                obracac.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;

            }

        }

        else attacking = false;


             if (attacking == false)
             {
                 transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
                 

                 RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, layer_mask);
                 if (groundInfo.collider == false)
                 {
                     if (movingRight == true)
                     {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    obracac.transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = false;
                    }

                     else
                     {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    obracac.transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;

                }
                 }
             }

            Anim.SetBool("Attacking", attacking);
    }

  

    public void CheckOfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            for (float i = 0.1f; i < 0.4f; i += 0.1f)
            {
                Invoke("Attack2", i * 2f);
            }

            nextFire = Time.time + fireRate;
        }
    }


    public void Attack2()
    {
        Instantiate(levelManager.patrolEnemyBulletPrefab, patrolEnemyBulletSpawnerPrefab.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("patrolEnemyShoot");

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("POCISK"))
        {
            

            FindObjectOfType<AudioManager>().Play("EnemyHit");

            if (levelManager.ulepszenieKnockbackHERO1 == true)
            {
                if (other.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
                {
                     gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2((levelManager.knockbackStrengthHERO1 + itemsManager.itemKnockbackPociskuHERO1) / 5f, 0);
                }
                else
                {   
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-(levelManager.knockbackStrengthHERO1 + itemsManager.itemKnockbackPociskuHERO1) / 5f, 0f);
                }
            }


            float damageAfterMultiplier = (levelManager.damagePociskuHERO1 + itemsManager.itemDamagePociskuHERO1) * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;

            locScale.x -= (0.125f * ((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;

            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;


           // Debug.Log("Trafiloooooooooooooooo" + lifePool);
            Blood();

        }


        else if (other.CompareTag("MIECZ"))
        {

            FindObjectOfType<AudioManager>().Play("EnemyHit");

            if (levelManager.ulepszenieKnockbackHERO2 == true)
            {
                if (levelManager.cameraTarget.position.x >= gameObject.transform.position.x)

                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Hero2overallKnockback /3f, 0);

                else gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Hero2overallKnockback /3f, 0f);


            }

            FindObjectOfType<AudioManager>().Play("SwordHit");


            float damageAfterMultiplier = levelManager.critOverallDamageHERO2 * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;

            locScale.x -= (0.125f * ((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;

            if (levelManager.czyCritHERO2)
            {
                Vector2 pozycja = gameObject.transform.position;
                pozycja.y += 3f;
                var crit = Instantiate(levelManager.critTextPrefabHERO2, pozycja, gameObject.transform.rotation);
                Destroy(crit, 0.2f);
            }

           // Debug.Log("Trafiloooooooooooooooo" + lifePool);
            Blood();

        }
        

        else if (other.CompareTag("CANONBULLET"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("EnemyHit");

            float damageAfterMultiplier = (levelManager.canonDamage + itemsManager.itemCanoonDamageHERO3) * levelManager.damagePlayerMultiplier;
            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;

            locScale.x -= (0.125f *((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);

            healthLeftText.text = lifePool + " / " + lifePoolStala;

           // Debug.Log("Trafilo" + lifePool);
            Blood();

        }


      


    }
   

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("LightingHERO4"))
        {
            // Debug.Log("LIffffffffffffGHTING!!!!!!!!!!!");

            float damageAfterMultiplier = (levelManager.damageHERO4 + itemsManager.itemDamageHERO4) * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;


            locScale.x -= (0.125f *((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;


            Blood();
        }
        else if (other.CompareTag("AttackHERO5"))
        {

            float damageAfterMultiplier = (levelManager.damageHERO5 + itemsManager.itemDamageHERO5) * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;


            locScale.x -= (0.125f * ((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;


            Blood();
        }

        else if (other.CompareTag("MIECZ"))
        {

            FindObjectOfType<AudioManager>().Play("EnemyHit");

            if (levelManager.ulepszenieKnockbackHERO2 == true)
            {
                if (levelManager.cameraTarget.position.x >= gameObject.transform.position.x)

                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Hero2overallKnockback / 3f, 0);

                else gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Hero2overallKnockback / 3f, 0f);


            }

            FindObjectOfType<AudioManager>().Play("SwordHit");


            float damageAfterMultiplier = levelManager.critOverallDamageHERO2 * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;

            locScale.x -= (0.125f * ((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;

            if (levelManager.czyCritHERO2)
            {
                Vector2 pozycja = gameObject.transform.position;
                pozycja.y += 3f;
                var crit = Instantiate(levelManager.critTextPrefabHERO2, pozycja, gameObject.transform.rotation);
                Destroy(crit, 0.2f);
            }

            // Debug.Log("Trafiloooooooooooooooo" + lifePool);
            Blood();

        }

    }

    public void Blood()
    {

        var blood1 = Instantiate(levelManager.prefebBlood, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(blood1, 3f);
        Death();

    }
    public void DeathBlood()
    {

        var blood2 = Instantiate(levelManager.prefebBlood2, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(blood2, 3f);

    }



    public void DropCoinow(int maxChance)
    {
        var coinChanceNumber = UnityEngine.Random.Range(4, maxChance);

        for (int i = coinChanceNumber; i >= 0; i--)
        {
            var tak = gameObject.transform.position;
            tak.x += (float)i / 2;
            tak.y += (float)i / 2;
            var coin = Instantiate(levelManager.coinx2Prefab, tak, gameObject.transform.rotation);
            Destroy(coin, coinTimeToDestruction);
        }

    }
    public void DropSerc(int maxChance)
    {
        var heartChanceNumber = UnityEngine.Random.Range(0, maxChance);

        if (heartChanceNumber == 0)
        {
            var heart = Instantiate(levelManager.prefabHeart, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(heart, heartTimeToDestruction);
        }

    }

    public void Death()
    {
        if (lifePool <= 0)
        {

            FindObjectOfType<AudioManager>().Play("MedusaDeath");

           // Debug.Log("ZNISZCZONO!");

            chanceToDropCoin = 9; // szansa = 1/chanceToDropCoin
            chanceToDropHeart = 16;
            // chanceToDropKey = 6;

            DropCoinow(chanceToDropCoin);
            DropSerc(chanceToDropHeart);
            //   KeyDrop(chanceToDropKey);
           // itemsManager.ItemDrop(gameObject.transform.position, 200f * levelManager.itemDropChanceMultiplier);
            itemsManager.GemDrop(gameObject.transform.position, 200f * levelManager.itemDropChanceMultiplier);


            levelManager.ExpGain("Patrol");

         //   levelManager.killCounter += 1;
           // levelManager.KillCounter();
            DeathBlood();

            Destroy(gameObject);
        }
    }
}
*/