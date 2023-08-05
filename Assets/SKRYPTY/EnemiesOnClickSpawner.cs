using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesOnClickSpawner : MonoBehaviour
{

    private EnemiesManager enemiesManager;
    //public 
    private GameObject mouseClickEnemies = null;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnEnemy("enemySkeletalDragon");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnEnemy("enemyJumpingSpider");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnEnemy("enemyBat");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnEnemy("enemyFirefly");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SpawnEnemy("enemyEggSpider");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SpawnEnemy("enemyNormalSpider");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SpawnEnemy("enemyBabySpider");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SpawnEnemy("enemyBigSpider");
        }

    }

    public void SpawnEnemy(string whichEnemy)
    {

        if (!mouseClickEnemies)
        {
            mouseClickEnemies = Instantiate(new GameObject());
            mouseClickEnemies.name = "mouseClickEnemies";
        }

        float distanceFromCamera = Random.Range(-2.0f, 0.0f);
        Vector3 mousePosition = Input.mousePosition;
       // mousePosition.z = distanceFromCamera;

        Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseScreenToWorld = new Vector3(mouseScreenToWorld.x, mouseScreenToWorld.y, distanceFromCamera);

        GameObject spawnedEnemy = null;
        Vector3 enemyPosition = mouseScreenToWorld;

        switch (whichEnemy) 
        {
            case "enemySkeletalDragon" : spawnedEnemy = enemiesManager.enemySkeletalDragonPrefab as GameObject; break;
            case "enemyBat": spawnedEnemy = enemiesManager.enemyBatPrefab as GameObject; break;
            case "enemyFirefly": spawnedEnemy = enemiesManager.enemyFireflyHivePrefab as GameObject;
                spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
                spawnedEnemy.transform.SetParent(mouseClickEnemies.transform);
                spawnedEnemy.transform.position = enemyPosition;
                spawnedEnemy.GetComponent<EnemyFireflyHive>().howManyHiveFirelfies = Random.Range(0, 5);
                return;
                
            case "enemyEggSpider": spawnedEnemy = enemiesManager.enemyEggEmptyPrefab[Random.Range(0, enemiesManager.enemyEggEmptyPrefab.Length)] as GameObject; break;
            case "enemyNormalSpider": spawnedEnemy = enemiesManager.enemyNormalSpiderPrefab as GameObject; break;
            case "enemyBabySpider": spawnedEnemy = enemiesManager.enemyBabySpiderPrefab as GameObject; break;
            case "enemyBigSpider": spawnedEnemy = enemiesManager.enemyBigSpiderPrefab as GameObject; break;
            case "enemyJumpingSpider": spawnedEnemy = enemiesManager.enemyJumpingSpiderPrefab as GameObject; break;
           // case "enemyPortal": spawnedEnemy = enemiesManager.enemyPortalPrefab as GameObject; break;
          //  case "enemyTentacleWorm": spawnedEnemy = enemiesManager.enemyTentacleWormPrefab as GameObject; break;
           // case "enemyHangingEnemy": spawnedEnemy = enemiesManager.enemyHangingSpiderPrefab as GameObject; break;
        }

        spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
        spawnedEnemy.transform.SetParent(mouseClickEnemies.transform);
        spawnedEnemy.transform.position = enemyPosition;



    }
}
