using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireflyHive : MonoBehaviour
{
    private EnemiesManager enemiesManager;
    private LevelManager levelManager;

    public int howManyHiveFirelfies;
    public bool isTriggered;

    public List<GameObject> hiveFirefliesMembers;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();

        SpawnFireflies();
    }

    private void SpawnFireflies()
    {
        for (int i = 0; i < howManyHiveFirelfies; i++)
        {
            GameObject firefly = Instantiate(enemiesManager.enemyFireflyPrefab);
            firefly.transform.SetParent(transform);
            firefly.transform.position = new Vector3( Random.Range( transform.position.x - 1.0f, transform.position.x + 1.0f) , Random.Range(transform.position.y - 1.0f, transform.position.y + 1.0f), 0);
            hiveFirefliesMembers.Add(firefly);

            firefly.GetComponentInChildren<EnemyFirefly>().enemyFireflyHive = this;
        }
    }
    
    
    void FixedUpdate()
    {
        ChechIfHeroInRadius();
    }

    private void ChechIfHeroInRadius()
    {
        if (!isTriggered)
        {
            isTriggered = Physics2D.OverlapCircle(transform.position, enemiesManager.enemyFireflyDetectionRadius, (1 << 25));
            if (isTriggered)
            {
                TriggerAllMembersInHive();
            }
        }
    }

    public void TriggerAllMembersInHive()
    {
        foreach (GameObject firefly in hiveFirefliesMembers)
        {
            EnemyFirefly fireflyComponent = firefly.GetComponentInChildren<EnemyFirefly>();
            if(fireflyComponent){
                fireflyComponent.isTriggered = true;
            }
            //fireflyComponent.FixPositionWhenTriggered();  
        }

        isTriggered = true;
    }

}
