
using UnityEngine;
using System.Collections;

public class RopeBehaviourComponent : MonoBehaviour
{
    private TowerSpawnManager towerSpawnManager;

    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks;

    private int indexInPlatformList;
    public bool isOnRightEdge;
    public string webStringType;

    private float distanceBetweenPlatforms;
    private EnemiesManager enemiesManager;

    public GameObject enemyOnThisWebString;

    void Start()
    {
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
        indexInPlatformList = towerSpawnManager.listOfPlatformPresetObjects.IndexOf(transform.parent.parent.gameObject);

        if(indexInPlatformList >= 2)
        {
            GameObject hostPlatform = towerSpawnManager.listOfPlatformPresetObjects[indexInPlatformList];
            GameObject previousPlatform = towerSpawnManager.listOfPlatformPresetObjects[indexInPlatformList - 1];

            Transform previousPlatformLeftEdge = previousPlatform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge");
            Transform previousPlatformRightEdge = previousPlatform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge");


            if (hostPlatform.transform.position.x < previousPlatform.transform.position.x && isOnRightEdge)
            {
                if (webStringType.Equals("Edge"))
                {
                    distanceBetweenPlatforms = Vector3.Distance(transform.position, previousPlatformLeftEdge.position);
                    StartCoroutine(GenerateEmptyRope(previousPlatformLeftEdge.position));
                }
                else if (webStringType.Equals("Inner"))
                {
                    Vector3 whereToSpawn = previousPlatformLeftEdge.position + new Vector3(Random.Range(0.5f, 5f), 0, 0);
                    distanceBetweenPlatforms = Vector3.Distance(transform.position, whereToSpawn);
                    StartCoroutine(GenerateEmptyRope(whereToSpawn));

                }
            }
            else if (hostPlatform.transform.position.x > previousPlatform.transform.position.x && !isOnRightEdge)
            {
                if (webStringType.Equals("Edge"))
                {
                    distanceBetweenPlatforms = Vector3.Distance(transform.position, previousPlatformRightEdge.position);
                    StartCoroutine(GenerateEmptyRope(previousPlatformRightEdge.position));
                }
                else if(webStringType.Equals("Inner"))
                {
                    Vector3 whereToSpawn = previousPlatformRightEdge.position - new Vector3(Random.Range(0.5f, 5f), 0, 0);
                    distanceBetweenPlatforms = Vector3.Distance(transform.position, whereToSpawn);
                    StartCoroutine(GenerateEmptyRope(whereToSpawn));

                }
            }
        }

        if (webStringType.Equals("Enemy"))
        {
            enemiesManager = FindObjectOfType<EnemiesManager>();
            float yAxisDistanceFromRoot = Random.Range(3.2f, 6.0f);
            Vector3 enemyPosition = new Vector3(transform.position.x, transform.position.y - yAxisDistanceFromRoot, 0);

           StartCoroutine(GenerateEnemyRope(enemyPosition, yAxisDistanceFromRoot));
        }

        if (distanceBetweenPlatforms > 30 && webStringType.Equals("Edge"))
        Destroy(gameObject);
    }

    private IEnumerator GenerateEnemyRope(Vector3 enemyPosition, float yAxisDistanceFromRoot)
    {
        Rigidbody2D prevBod = hook;
        numLinks = (int) yAxisDistanceFromRoot * 2;

        for (int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.SetParent(transform);
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            newSeg.GetComponent<RopeSegmentBehaviourComponent>().isEnemyWebStringType = true;

            if (i < numLinks - 1)
            {
                prevBod = newSeg.GetComponent<Rigidbody2D>();
            }
            else
            {

                GameObject spawnedEnemy = enemiesManager.enemyHangingSpiderPrefab;
                

                spawnedEnemy = Instantiate(spawnedEnemy);
                spawnedEnemy.transform.SetParent(transform);
                spawnedEnemy.transform.position = enemyPosition;

                spawnedEnemy.GetComponent<EnemyHangingSpider>().webStringAnchor.connectedBody = newSeg.GetComponent<Rigidbody2D>();

                enemyOnThisWebString = spawnedEnemy;

                enemiesManager.enemiesHangingSpiderObjectsList.Add(spawnedEnemy);

            }
            yield return null;
        }
    }

    private IEnumerator GenerateEmptyRope(Vector3 whereToAttach)
    {
        Rigidbody2D prevBod = hook;
        numLinks = Random.Range((int) (distanceBetweenPlatforms * 2.2f), (int)(distanceBetweenPlatforms * 3.0f));

        for (int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.SetParent(transform);
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            if (i < numLinks - 1)
            {
                prevBod = newSeg.GetComponent<Rigidbody2D>();
            }
            else
            {
                newSeg.transform.position = whereToAttach;
                newSeg.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                int chanceForBreak = Random.Range(1, 101);
                if (chanceForBreak <= 30)
                    Destroy(newSeg);
            }

            yield return null;
        }
    }


}
