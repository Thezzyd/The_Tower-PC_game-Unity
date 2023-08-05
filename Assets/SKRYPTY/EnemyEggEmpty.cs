using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyEggEmpty : MonoBehaviour
{
    private EnemiesManager enemiesManager;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        enemiesManager = FindObjectOfType<EnemiesManager>();

        float scaleMultiplier = Random.Range(enemiesManager.enemyEggEmptyScaleMultiplierMin, enemiesManager.enemyEggEmptyScaleMultiplierMax);

        transform.localScale = new Vector3(transform.localScale.x * scaleMultiplier, transform.localScale.y * scaleMultiplier, 1f);

    }

    public void HatchEgg()
    {
        GameObject effect = Instantiate(enemiesManager.enemyEggSpiderExplozionEffect, transform.position + new Vector3(0,0.2f,0), enemiesManager.enemyEggSpiderExplozionEffect.transform.rotation);
        
        switch (Random.Range(1,6))
        {
            case 1: FindObjectOfType<AudioManager>().Play("EggCrack_V1", transform.position); break;
            case 2: FindObjectOfType<AudioManager>().Play("EggCrack_V2", transform.position); break;
            case 3: FindObjectOfType<AudioManager>().Play("EggCrack_V3", transform.position); break;
            case 4: FindObjectOfType<AudioManager>().Play("EggCrack_V4", transform.position); break;
            case 5: FindObjectOfType<AudioManager>().Play("EggCrack_V5", transform.position); break;
        }


        Destroy(effect, 4f);
      
        float chance = Random.Range(1, 101);

        if (chance <= 80)
        {
            enemiesManager.DropOfEssence(enemiesManager.enemyEgEmptyEssenceDropMinQuantity, enemiesManager.enemyEgEmptyEssenceDropMaxQuantity, transform.position);
        }
        else
        {
            GameObject enemyBabySpider =  Instantiate(enemiesManager.enemyBabySpiderPrefab, transform.position, transform.rotation);
            enemyBabySpider.transform.SetParent(transform.parent);
            enemyBabySpider.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, Random.Range(10f, 11.5f)), ForceMode2D.Impulse);

        }

        CinemachineCameraShake.Instance.ShakeCamera(1.7f, 0.1f);
        levelManager.ExpGain("EnemyEgg");

          var damageText = Instantiate(enemiesManager.EnemyDamageTextPrefab, transform.position +
         new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(2.5f,3.5f), 0), enemiesManager.EnemyDamageTextPrefab.transform.rotation);
        damageText.GetComponent<TextMeshPro>().SetText("!!!");
        Destroy(damageText, 0.5f);

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero1") || coliderTag.Equals("AttackHero2") || coliderTag.Equals("AttackHero3")
            || coliderTag.Equals("AttackHero6"))
        {
            if (coliderTag.Equals("AttackHero3"))
                Destroy(other.gameObject);

            if (coliderTag.Equals("AttackHero2"))
                levelManager.audioManager.Play("Hero2SlashHit", transform.position);

            if (coliderTag.Equals("AttackHero6"))
                levelManager.audioManager.Play("Hero6ProjectileHit", transform.position);

          
            HatchEgg();
        }

    }

    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            if (coliderTag.Equals("AttackHero4"))
                levelManager.audioManager.Play("Hero4TendrilsHit");

            if (coliderTag.Equals("AttackHero5"))
                levelManager.audioManager.Play("Hero5ProjectileHit", transform.position);

          
            HatchEgg();
        }
    }
}
