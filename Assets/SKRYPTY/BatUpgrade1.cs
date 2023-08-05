using UnityEngine;
using Pathfinding;
using TMPro;
using System;
/*
public class BatUpgrade1 : MonoBehaviour
{

    [HideInInspector] public Player2 player2;

    //COIN DROP
    [System.NonSerialized]
    // public int coinChanceNumber;

    [HideInInspector] public float coinTimeToDestruction = 10f;
    [HideInInspector] public float heartTimeToDestruction = 30f;

    [HideInInspector] public int chanceToDropCoin; // szansa = 1/chanceToDropCoin
    [HideInInspector] public int chanceToDropHeart; // szansa = 1/chanceToDropCoin

    [HideInInspector] public bool isDestroyed;

    [System.NonSerialized]
    private float lifePoolStala;
    [System.NonSerialized]
    public float lifePool = 50f;

    [System.NonSerialized]
    public LevelManager levelManager;
    [System.NonSerialized]
    public ShopPanel shopPanel;

    // public Strzelanie strzelanie;

    public TextMeshPro healthLeftText;
    public GameObject healthBar;

    [System.NonSerialized]
    public Vector3 locScale;
    [System.NonSerialized]
    public Vector3 pudelko;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public float Hero2overallKnockback;

    [HideInInspector] public AIPath aiPath;

    [System.NonSerialized]
    [HideInInspector] public bool ulepszenieKnockback = false;
    [HideInInspector] public ItemsManager itemsManager;

    private void Start()
    {
        isDestroyed = false;
        itemsManager = FindObjectOfType<ItemsManager>();

        lifePool = LevelManager.lifePoolBat1;
        lifePoolStala = lifePool;

        //Debug.Log("LIFFFFFFFFE POOOOL" + lifePool);

        float maxMoveSpeed = UnityEngine.Random.Range(1.2f, 2f);
        maxMoveSpeed = (float)Math.Round(maxMoveSpeed, 3);

        GetComponent<AIPath>().maxSpeed = maxMoveSpeed;

        shopPanel = FindObjectOfType<ShopPanel>();
        levelManager = FindObjectOfType<LevelManager>();
        // aiPath = FindObjectOfType<AIPath>();

        locScale = healthBar.transform.localScale;
        pudelko = locScale;
        healthLeftText.text = lifePool + " / " + lifePoolStala;

        if (ItemsManager.knockbackWeaknessHERO2 == true)
            Hero2overallKnockback = ((levelManager.knockbackStrengthHERO2 + itemsManager.itemSwordKnockbackHERO2) / 2f);
        else Hero2overallKnockback = levelManager.knockbackStrengthHERO2 + itemsManager.itemSwordKnockbackHERO2;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {



        if (other.CompareTag("POCISK"))
        {
            ///////

            //  strzelanie = FindObjectOfType<Strzelanie>();

            FindObjectOfType<AudioManager>().Play("EnemyHit");


            if (levelManager.ulepszenieKnockbackHERO1 == true)
            {

                gameObject.GetComponent<AIPath>().canMove = false;

                if (levelManager.cameraTarget.position.x >= gameObject.transform.position.x)

                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-(levelManager.knockbackStrengthHERO1 + itemsManager.itemKnockbackPociskuHERO1), 0f);

                else

                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2((levelManager.knockbackStrengthHERO1 + itemsManager.itemKnockbackPociskuHERO1), 0f);


                Invoke("MoveReturn", 0.1f);

            }

            /////////  

            //  Debug.Log("ZYCIEEEEEEEEEEEEEEEEEE" + lifePool);
            // Debug.Log("DAMAGEEEEEEEEEEEEEEEEE" + levelManager.damagePociskuHERO1);

            float damageAfterMultiplier = (levelManager.damagePociskuHERO1 + itemsManager.itemDamagePociskuHERO1) * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;



            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;

            //Debug.Log("Trafilo" + lifePool);
            Blood();



        }


        else if (other.CompareTag("MIECZ"))
        {
            // Debug.Log("MIECZZZZZZZZZZ YOOOOOOO" + levelManager.knockbackStrengthHERO2);
            // Debug.Log("MIECZZZZZZZZZZ YOOOOOOOooooooooo" + levelManager.ulepszenieKnockbackHERO2);
            FindObjectOfType<AudioManager>().Play("EnemyHit");
            player2 = FindObjectOfType<Player2>();

            if (levelManager.ulepszenieKnockbackHERO2 == true)
            {
                // Debug.Log("MIECZZZZZZZZZZ KNOCKBACKKKKK" + levelManager.knockbackStrengthHERO2);
                gameObject.GetComponent<AIPath>().canMove = false;

                if (levelManager.cameraTarget.position.x >= gameObject.transform.position.x)

                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-Hero2overallKnockback, 0);

                else gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Hero2overallKnockback, 0f);

                Invoke("MoveReturn", 0.1f);

            }


            FindObjectOfType<AudioManager>().Play("SwordHit");


         //   Debug.Log("MIECZZZZZZZZZZ" + levelManager.damageMiecza);


            float damageAfterMultiplier = levelManager.critOverallDamageHERO2 * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;

            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
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

            Blood();

        }

        else if (other.CompareTag("CANONBULLET"))
        {
            ///////



            FindObjectOfType<AudioManager>().Play("EnemyHit");

            float damageAfterMultiplier = (levelManager.canonDamage + itemsManager.itemCanoonDamageHERO3) * levelManager.damagePlayerMultiplier;
            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;


          

            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;

          //  Debug.Log("Trafilo" + lifePool);
            Blood();

            Destroy(other.gameObject);


        }

        Death();

       
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("LightingHERO4"))
        {
           // Debug.Log("LIffffffffffffGHTING!!!!!!!!!!!");

            float damageAfterMultiplier = (levelManager.damageHERO4 + itemsManager.itemDamageHERO4) * levelManager.damagePlayerMultiplier;

            damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);

            lifePool -= damageAfterMultiplier;


            locScale.x -= (((damageAfterMultiplier) / lifePoolStala));
            healthBar.transform.localScale = locScale;
            lifePool = (float)Math.Round(lifePool, 2);
            healthLeftText.text = lifePool + " / " + lifePoolStala;


            Blood();
        }

        Death();
    }

    public void Blood()
    {

        var blood1 = Instantiate(levelManager.prefebBlood, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(blood1, 3f);

    }

    public void DeathBlood()
    {

        var blood2 = Instantiate(levelManager.prefebBlood2, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(blood2, 3f);

    }

    public void MoveReturn()
    {
        gameObject.GetComponent<AIPath>().canMove = true;
    }

    public void DropCoinow(int maxChance)
    {
        var coinChanceNumber = UnityEngine.Random.Range(3, maxChance);

        for (int i = coinChanceNumber; i >= 0; i--)
        {
           
            
                var tak = gameObject.transform.position ;
                tak.x += (float)i / 2;
                tak.y += (float)i / 2;
                var coin = Instantiate(levelManager.coinx2Prefab, tak, gameObject.transform.rotation);
                Destroy(coin, coinTimeToDestruction);
            
           
        }

    }

    public void DropSerc(int maxChance)
    {
        var heartChanceNumber = UnityEngine.Random.Range(0, maxChance);
        Debug.Log("LOSOWANIE SERCAAA:" + heartChanceNumber);
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
            FindObjectOfType<AudioManager>().Play("BatDeath");

           // Debug.Log("ZNISZCZONO!");

            if (gameObject.tag == "Nietoperz1")
                levelManager.licznikNietoperza1--;

            if (gameObject.tag == "Nietoperz2")
                levelManager.licznikNietoperza2--;

            if (gameObject.tag == "Nietoperz3")
                levelManager.licznikNietoperza3--;

            ///DROP COINÓW
            ///
            isDestroyed = true;

            chanceToDropCoin = 10;
            chanceToDropHeart = 18;

            levelManager.ExpGain("Bat1");


            DropCoinow(chanceToDropCoin);
            DropSerc(chanceToDropHeart);
           // itemsManager.ItemDrop(gameObject.transform.position, 200f * levelManager.itemDropChanceMultiplier);
            itemsManager.GemDrop(gameObject.transform.position, 200f * levelManager.itemDropChanceMultiplier);
            DeathBlood();


            Destroy(gameObject);

           // levelManager.killCounter += 1;
           // levelManager.KillCounter();



        }
    }

}
*/