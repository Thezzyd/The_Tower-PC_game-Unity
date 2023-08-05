using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyPortalComponent : MonoBehaviour
{
    private EnemiesManager enemiesManager;
    private Transform parent;
    private bool isActivated;
   // private ParticleSystem activationEffect;
    private Animator anim;
    private float activationSpeed;
    private float timeToStartSpawningEnemies;
    public TextMeshPro enemiesCounter;
    private int enemiesQuantity;
    private float maxTimeOfSpawning;

    public float portalDifficultyMultiplier;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        anim = GetComponent<Animator>();

        timeToStartSpawningEnemies = 2.0f;
        maxTimeOfSpawning = Random.Range(enemiesManager.enemyPortaTimeOfSpawningMin, enemiesManager.enemyPortaTimeOfSpawningMax);

        //   activationEffect = GetComponentInChildren<ParticleSystem>();
        activationSpeed = 1f;
        anim.SetFloat("activationSpeedMultiplier", activationSpeed);

        enemiesQuantity = (int)(Random.Range(enemiesManager.enemyPortalMonstersQuantityMin, enemiesManager.enemyPortalMonstersQuantityMax) * portalDifficultyMultiplier);

        enemiesCounter.text = "x " + enemiesQuantity;
        parent = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isActivated && collision.CompareTag("PLAYER"))
        {
            ActivatePortal();
            isActivated = true;
        }
    }


    private void ActivatePortal()
    {
        anim.SetTrigger("portalActivated");
        StartCoroutine(StartSpawningEnemies(timeToStartSpawningEnemies));
   //     StartCoroutine(DeactivatePortal(timeToDeactivation));
    }

    private IEnumerator DeactivatePortal(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        anim.SetTrigger("portalDeactivated");
    }

    private IEnumerator StartSpawningEnemies(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        for (int i = 0; i < enemiesQuantity; i++)
        {
            StartCoroutine(SpawnEnemy(Random.Range(0.0f, maxTimeOfSpawning))); 
        }

    }

    private IEnumerator SpawnEnemy(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        int whichEnemyPrefab = Random.Range(0, 5); // 0 babySpider,  1 normalSpider, 2 bigSpider, 3 bat, 4 hanging- ball spider
        GameObject spawnedEnemy = null;
        bool isHangingSpider = false;

        switch (whichEnemyPrefab)
        {
            case 0: spawnedEnemy = enemiesManager.enemyBabySpiderPrefab; break;
            case 1: spawnedEnemy = enemiesManager.enemyNormalSpiderPrefab; break;
            case 2: spawnedEnemy = enemiesManager.enemyBigSpiderPrefab; break;
            case 3: spawnedEnemy = enemiesManager.enemyBatPrefab; break;
            case 4: spawnedEnemy = enemiesManager.enemyHangingSpiderPrefab; isHangingSpider = true; break;
            case 5: spawnedEnemy = enemiesManager.enemyFireflyPrefab; break;
            case 6: spawnedEnemy = enemiesManager.enemyJumpingSpiderPrefab; break;
                
        }

        spawnedEnemy = Instantiate(spawnedEnemy);

        spawnedEnemy.transform.SetParent(parent);
        spawnedEnemy.transform.position = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), Random.Range(-1.0f, 1.0f)) ;
        spawnedEnemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(5f, 9f)), ForceMode2D.Impulse);

        if (isHangingSpider)
        {
            spawnedEnemy.GetComponent<EnemyHangingSpider>().SpiderBallActivation();
        }

        enemiesQuantity -= 1;

        enemiesCounter.text = "x " + enemiesQuantity;

        if (enemiesQuantity <= 0)
            StartCoroutine(DeactivatePortal(1.0f));
    }

}
