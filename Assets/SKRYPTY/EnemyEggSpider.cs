using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyEggSpider : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
//    private HeroesManager heroesManager;
 //   private SkillsUpgradeManager skillsUpgradeManager;
 //   private Rigidbody2D rb;

  //  public bool right;
    private Vector3 locScale;
    private float eggGrowTimer;
    private float durationOfGrowing;

    public TextMeshPro healthLeftText;

    private float actualLifePool;
    private float startingLifePool;

    public GameObject healthBar;


    private bool isGrowingFinalized;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
     //   heroesManager = FindObjectOfType<HeroesManager>();
     //   skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();
        levelManager = FindObjectOfType<LevelManager>();
      
        GetComponent<SpriteRenderer>().material = enemiesManager.enemyEggSpiderHatchingMaterials[UnityEngine.Random.Range(0, 3)];

        actualLifePool = enemiesManager.enemyEggSpiderLifePool;
        startingLifePool = actualLifePool;

        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

        transform.localScale  = new Vector3(0f, 0f, 1f);
        locScale = new Vector3(0f, 0f, 1f);

        eggGrowTimer = 0f;

        durationOfGrowing = enemiesManager.enemyEggSpiderGrowTime;
 
    //    rb = GetComponent<Rigidbody2D>();
/*
        if(right)
            rb.AddForce(new Vector2(UnityEngine.Random.Range(1f, 10f), UnityEngine.Random.Range(5f, 20f)), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(UnityEngine.Random.Range(-1f, -10f), UnityEngine.Random.Range(5f, 20f)), ForceMode2D.Impulse);
*/
    }

    public void HatchEgg()
    {
        var effect = Instantiate(enemiesManager.enemyEggSpiderExplozionEffect, transform.position, enemiesManager.enemyEggSpiderExplozionEffect.transform.rotation);
      /*  var babySpider = Instantiate(enemiesManager.babySpiderPrefab, transform.position, enemiesManager.babySpiderPrefab.transform.rotation);
   
        if (right)
            babySpider.GetComponent<BabySpider>().movingRight = true;
        else
            babySpider.GetComponent<BabySpider>().movingRight = false;*/

        Destroy(effect, 4f);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (locScale.x < 1f)
        {
            eggGrowTimer += Time.fixedDeltaTime / durationOfGrowing;
            locScale = new Vector3( Mathf.Lerp(0.0f, 1f, eggGrowTimer), Mathf.Lerp(0.0f, 1f, eggGrowTimer), 1f);
            transform.localScale = locScale;
        }
        else if (!isGrowingFinalized)
        {
            HatchEgg();
            isGrowingFinalized = true;
        }
    
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero1") || coliderTag.Equals("AttackHero2") || coliderTag.Equals("AttackHero3")
            || coliderTag.Equals("AttackHero6"))
        {
            actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(coliderTag, gameObject, other.gameObject, 1);
            actualLifePool = (float)Math.Round(actualLifePool, 2);

            locScale.x = (actualLifePool / startingLifePool);

            healthBar.transform.localScale = locScale;
            healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

            enemiesManager.HitBloodEffect(enemiesManager.enemyEggSpiderHitBloodEffect, transform.position);

            if (actualLifePool <= 0)
            {
                Death();
            }
        }
    }


    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(other.tag, gameObject, other, 1); ;
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

            enemiesManager.HitBloodEffect(enemiesManager.enemyEggSpiderHitBloodEffect, transform.position);

            if (actualLifePool <= 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        enemiesManager.DropOfEssence(enemiesManager.enemyEggSpiderEssenceDropMinQuantity, enemiesManager.enemyEggSpiderEssenceDropMaxQuantity, transform.position);

        levelManager.ExpGain("Egg");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyEggSpiderDeathBloodEffect, transform.position);

        Destroy(gameObject);  
    }

}
