
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Pathfinding;

public class TowerSpawnManager : MonoBehaviour
{

    public LevelManager levelManager;
    // private GraphManager graphManager;

    [Header("Wall spawn")]
    public GameObject[] leftTowerTilePrefabs;
    public GameObject[] rightTowerTilePrefabs;


    public float spawnWall_Y = -50.0f;
    public float wallSafeZone = 80.0f;
    public float spawnLeftWall_X = -50.0f;
    public float spawnRightWall_X = 50.0f;
    public float wallTileLength = 30;
    public int ammountOfWallTilesOnScreen;
    public Transform leftWallParent;
    public Transform rightWallParent;
    private List<GameObject> listOfLeftWallObjects;
    private List<GameObject> listOfRightWallObjects;

    [Header("Platform spawn")]
    public GameObject[] floatingPlatformPrefabs;
    public GameObject[] towerPlatformPrefabs;
    public float platformPresetSafeZone = 80.0f;
    public int ammountOfPlatformPresetsOnScreen;
    public Transform platformPresetsParent;
    public Transform platformExtraPresetsParent;
    public List<GameObject> listOfPlatformPresetObjects;
    public List<GameObject> listOfExtraPlatformPresetObjects;
    public List<GameObject> listOfAllPlatformPresetObjects;

    public float maxLeftSidePlatformX = -50;
    public float maxRightSidePlatformX = 50;

    [Header("Decorations spawn")]
    public GameObject[] decorationTreePrefabs;
    public GameObject[] decorationTreeFireflyPrefabs;
    public GameObject[] decorationTreeStonePrefabs;
    public GameObject[] decorationStonePrefabs;
    public GameObject[] lanternLightPrefabs;
    public GameObject[] lanternPrefabs;
    public GameObject[] campFirePrefabs;
    public List<GameObject> decorationObjectsList;
    public List<AudioSource> fireCampAudioSourceList;

    [Header("Stars spawn")]
    public GameObject[] starPrefabs;
    private Vector3 leftSpawnPoint;
    private Vector3 rightSpawnPoint;
    private Transform decorationsParent;
    public GameObject starCollectedParentPlatform;
    private GameObject starCollectedParentPlatformReference;
    public int previousStarplatformIndex;

    public List<GameObject> starObjectsList;

    [Header("Chest spawn")]
    public GameObject chestSmallPrefab;
    public GameObject chestNormalPrefab;
    public GameObject chestBigPrefab;
    public Material chestSpriteMaterial;
    public Material chestParticlesMaterial;
    public List<GameObject> chestObjectsList;
    public Transform chestParent;

    [Header("Aurora spawn")]
    public GameObject auroraPrefab;
    public Transform auroraParent;
    public List<GameObject> auroraObjectsList;

    [Header("Web string spawn")]
    public GameObject webStringPrefab;
    public List<GameObject> webStringObjectsList;

    [Header("Water - back travel bound")]
    public GameObject waterInGameObject;

    [Header("First platform")]
    public GameObject firstInGamePlatform;


    private bool isStartSequenceOver;
    private bool isPreviousSequenceOnFixedUpdateOver;

    private void SetStartingValues()
    {
        wallSafeZone = 80.0f;
        platformPresetSafeZone = 80.0f;
        leftSpawnPoint = new Vector3(0, -300, 0);
        rightSpawnPoint = new Vector3(0, -300, 0);
        maxLeftSidePlatformX = -50;
        maxRightSidePlatformX = 50;
        ammountOfPlatformPresetsOnScreen = 14;
        //  graphManager = FindObjectOfType<GraphManager>();
        ammountOfWallTilesOnScreen = 7;

        listOfLeftWallObjects = new List<GameObject>();
        listOfRightWallObjects = new List<GameObject>();
        starObjectsList = new List<GameObject>();
        listOfPlatformPresetObjects = new List<GameObject>();
        listOfExtraPlatformPresetObjects = new List<GameObject>();
        chestObjectsList = new List<GameObject>();
        decorationObjectsList = new List<GameObject>();
        auroraObjectsList = new List<GameObject>();
        webStringObjectsList = new List<GameObject>();
        listOfAllPlatformPresetObjects = new List<GameObject>();
        fireCampAudioSourceList = new List<AudioSource>();

    }

    void Start()
    {
        isStartSequenceOver = false;
        isPreviousSequenceOnFixedUpdateOver = true;
        StartCoroutine(OnStartSequence());

         //  Invoke("UpdateCollidersOfExtraPlatformsToPathfindingGraph", 0.5f);
      //  StarSpawner();
    }

    private IEnumerator OnStartSequence(){
        levelManager = FindObjectOfType<LevelManager>();
        SetStartingValues();

        listOfPlatformPresetObjects.Add(firstInGamePlatform);


        for (int i = 0; i < ammountOfWallTilesOnScreen; i++)
        {
            SpawnWallTiles();
        }

        for (int i = 0; i < ammountOfPlatformPresetsOnScreen; i++)
        {
          yield return StartCoroutine(SpawnPlatform());
        }
         
        
        for (int i = 0; i < listOfPlatformPresetObjects.Count; i++)
        {
            BoxCollider2D[] colliders = listOfPlatformPresetObjects[i].GetComponents<BoxCollider2D>();

            for (int j = 0; j < colliders.Length; j++)
            {
                var guo = new GraphUpdateObject(colliders[j].bounds);
                AstarPath.active.UpdateGraphs(guo);

            }
        }

        for (int i = 0; i < listOfExtraPlatformPresetObjects.Count; i++)
        {
            BoxCollider2D[] colliders = listOfExtraPlatformPresetObjects[i].GetComponents<BoxCollider2D>();

            for (int j = 0; j < colliders.Length; j++)
            {
                var guo = new GraphUpdateObject(colliders[j].bounds);
                 AstarPath.active.UpdateGraphs(guo);

            }
        }

        starCollectedParentPlatform = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count / 2 - 5];
         
        Transform temporaryLeftSpawnPoint = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count / 2 - 5].transform.Find("SpawnPoints").Find("SpawnPointLeftEdge");
        Transform temporaryrightSpawnPoint = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count / 2 - 5].transform.Find("SpawnPoints").Find("SpawnPointRightEdge");
        decorationsParent = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count / 2 - 5].transform.Find("SpawnPoints").Find("DecorationsParent");

        Vector3 leftSpawnPointTemp = temporaryLeftSpawnPoint.position;
        Vector3 rightSpawnPointTemp = temporaryrightSpawnPoint.position;

        GenerateStar(leftSpawnPointTemp.x, rightSpawnPointTemp.x, leftSpawnPointTemp.y);
        isStartSequenceOver = true;

    }

   // private void UpdateCollidersOfExtraPlatformsToPathfindingGraph(){

  //  }


    void FixedUpdate()
    {
        if (isStartSequenceOver && levelManager.highestTowerPointReached.position.y - wallSafeZone * 2 > (spawnWall_Y - ammountOfWallTilesOnScreen * wallTileLength))
        {
            SpawnWallTiles();
            DeleteWallTile();

        }

        if (isStartSequenceOver && isPreviousSequenceOnFixedUpdateOver && listOfPlatformPresetObjects.Count <= ammountOfPlatformPresetsOnScreen)
        {
            isPreviousSequenceOnFixedUpdateOver = false;
            StartCoroutine(SpawnPlatform());
        }

        if (levelManager.highestTowerPointReached.position.y - 1 * platformPresetSafeZone > listOfPlatformPresetObjects[0].transform.position.y)
        {
            DeletePlatformPresets();

        }

    }

    private void SpawnWallTiles()
    {
        GameObject leftWallSpawn = Instantiate(leftTowerTilePrefabs[0]) as GameObject;
        GameObject rightWallSpawn = Instantiate(rightTowerTilePrefabs[0]) as GameObject;
        leftWallSpawn.transform.SetParent(leftWallParent);
        rightWallSpawn.transform.SetParent(rightWallParent);
        leftWallSpawn.transform.position = new Vector3(spawnLeftWall_X, spawnWall_Y, 0);
        rightWallSpawn.transform.position = new Vector3(spawnRightWall_X, spawnWall_Y, 0);
        spawnWall_Y += wallTileLength;

        listOfLeftWallObjects.Add(leftWallSpawn);
        listOfRightWallObjects.Add(rightWallSpawn);
    }

    private void DeleteWallTile()
    {
        Destroy(listOfLeftWallObjects[0]);
        listOfLeftWallObjects.RemoveAt(0);

        Destroy(listOfRightWallObjects[0]);
        listOfRightWallObjects.RemoveAt(0);
    }

    public void StarSpawner(bool waterDestructionSpawn)
    {

      previousStarplatformIndex = listOfPlatformPresetObjects.IndexOf(starCollectedParentPlatform);

   //     bool isSpawnedStarGoingToBeOnExtraPlatform =  Random.Range(0.0f, 1.0f) > 0.5f;
      GameObject platformToSpawnStar = null;


        if (waterDestructionSpawn)
        {
            for (int i = 0; i < listOfPlatformPresetObjects.Count; i++)
            {
                if (listOfPlatformPresetObjects[i] != null)
                {
                    if (listOfPlatformPresetObjects[i].transform.position.y > levelManager.cameraAndEnemiesTargetPoint.position.y)
                    {
                        platformToSpawnStar = listOfPlatformPresetObjects[i + 1];
                     //   starCollectedParentPlatform = listOfPlatformPresetObjects[i + 1];
                        break;
                    }
                }
            }
        }
        else
        {
            platformToSpawnStar = listOfPlatformPresetObjects[previousStarplatformIndex + 2];
           // starCollectedParentPlatform = listOfPlatformPresetObjects[previousStarplatformIndex + 2];
        }
   
         Transform temporaryLeftSpawnPoint = platformToSpawnStar.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge");
         Transform temporaryrightSpawnPoint = platformToSpawnStar.transform.Find("SpawnPoints").Find("SpawnPointRightEdge");
         decorationsParent = platformToSpawnStar.transform.Find("SpawnPoints").Find("DecorationsParent");

        
         leftSpawnPoint = temporaryLeftSpawnPoint.position;
         rightSpawnPoint = temporaryrightSpawnPoint.position;

         GenerateStar(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);

        starCollectedParentPlatform = platformToSpawnStar;



    }

    public void GenerateStar(float leftPositionX, float rightPositionX, float worldPositionY)
    {
        float distanceOfSpawningArea = rightPositionX - leftPositionX;

        int loop = 0;
        bool ifSucceded = false;

        while (ifSucceded == false)
        {
            int whichArrayIndex = Random.Range(0, starPrefabs.Length);
            GameObject spawnedStar = null;
            Vector3 starPosition = new Vector3(0, 0, 0);

            spawnedStar = starPrefabs[whichArrayIndex] as GameObject;

            starPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY, 0);


            spawnedStar = Instantiate(spawnedStar) as GameObject;
            spawnedStar.transform.position = starPosition;

            if (spawnedStar.transform.position.x > maxRightSidePlatformX || spawnedStar.transform.position.x < maxLeftSidePlatformX)
            {
                ifSucceded = false;
                Destroy(spawnedStar);
            }
            else
            {
                ifSucceded = true;
                starObjectsList.Add(spawnedStar);

                //  levelManager.activeStarCounter++;
            }

            loop += 1;
            if (loop >= 100) { Debug.Log("Zapetlilo star..."); break; }

        }

    }

    public Transform GenerateTowerPlatform(float leftPositionX, float rightPositionX, float worldPositionY, Transform parent)
    {

        float distanceOfSpawningArea = rightPositionX - leftPositionX;
        int roundedDistanceOfSpawningArea = (int)distanceOfSpawningArea;
        bool ifSucceded = false;
        Transform spawnedTowerPlatformSpawnPointsTransform = null;

        while (!ifSucceded)
        {
            int whichArrayIndex = Random.Range(0, towerPlatformPrefabs.Length);
            GameObject spawnedPlatform = null;
            Vector3 platformPosition = new Vector3(0, 0, 0);

            spawnedPlatform = towerPlatformPrefabs[whichArrayIndex] as GameObject;
            platformPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY, 0);
            spawnedPlatform = Instantiate(spawnedPlatform) as GameObject;
            spawnedPlatform.transform.SetParent(parent);
            spawnedPlatform.transform.position = platformPosition;

            if (spawnedPlatform.transform.position.x < maxLeftSidePlatformX || spawnedPlatform.transform.position.x > maxRightSidePlatformX)
            {
                ifSucceded = false;
                Destroy(spawnedPlatform);
            }
            else
            {
                ifSucceded = true;
                spawnedTowerPlatformSpawnPointsTransform = spawnedPlatform.transform.Find("SpawnPoints");
                spawnedPlatform.GetComponentInChildren<DecorationSpawnScript>().isThisATowerPlatform = true;
                listOfPlatformPresetObjects.Add(spawnedPlatform);     

            }
        }
        return spawnedTowerPlatformSpawnPointsTransform;
    }

    private IEnumerator SpawnPlatform()
    {
        GameObject platform = null;
        bool ifSucceded = false;
        bool ifCorrectPlatformRolled = false;
        int loop = 0;
        int whichIndex;
        bool isRgightDirection = false;
        Vector3 previousPlatformLeftEdgeWorldPosition = Vector3.zero;
        Vector3 previousPlatformRightEdgeWorldPosition = Vector3.zero;

        while (ifSucceded == false)
        {
            string previousPlatformTag = "";

            bool isTowerAllowed = false;
            if (listOfPlatformPresetObjects.Count >= 1)
                previousPlatformTag = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count - 1].tag;

            ifCorrectPlatformRolled = false;

            while (!ifCorrectPlatformRolled)
            {
                whichIndex = Random.Range(0, floatingPlatformPrefabs.Length);
                platform = floatingPlatformPrefabs[whichIndex];

                switch (platform.tag)
                {
                    case "Platform_1_0": ifCorrectPlatformRolled = true; break;
                    case "Platform_1_5": ifCorrectPlatformRolled = true; break;
                    case "Platform_2_0": ifCorrectPlatformRolled = true; break;
                    case "Platform_2_5": ifCorrectPlatformRolled = true; break;
                    case "Platform_3_0": ifCorrectPlatformRolled = true; break;
                    case "Platform_4_0": if (previousPlatformTag.Equals("Platform_5_0") || previousPlatformTag.Equals("Platform_6_0")) ifCorrectPlatformRolled = false; else ifCorrectPlatformRolled = true; break;
                    case "Platform_5_0": if (previousPlatformTag.Equals("Platform_4_0") || previousPlatformTag.Equals("Platform_5_0") || previousPlatformTag.Equals("Platform_6_0")) ifCorrectPlatformRolled = false; else ifCorrectPlatformRolled = true; break;
                    case "Platform_6_0": if (previousPlatformTag.Equals("Platform_3_0") || previousPlatformTag.Equals("Platform_4_0") || previousPlatformTag.Equals("Platform_5_0") || previousPlatformTag.Equals("Platform_6_0")) ifCorrectPlatformRolled = false; else ifCorrectPlatformRolled = true; break;
                    case "StartingPlatform": ifCorrectPlatformRolled = true; break;
                }

            }

            string platformTag = platform.tag;
            //     float max_X_Jump = 2.4f;
            //  float max_Y_Jump = 1.6f;
            //    float max_X_Y_vector_length = Mathf.Sqrt(Mathf.Pow(max_X_Jump, 2) + Mathf.Pow(max_Y_Jump, 2)); //dlugosc vektora skoku...
            isRgightDirection = false;
            previousPlatformLeftEdgeWorldPosition = new Vector3(Random.Range(maxLeftSidePlatformX, maxRightSidePlatformX), 10, 0);
            previousPlatformRightEdgeWorldPosition = new Vector3(Random.Range(maxLeftSidePlatformX, maxRightSidePlatformX), 10, 0);

            float X_distance_to_spawn_platform = 0.0f;
            float Y_distance_to_spawn_platform = 0.0f;

            if (Random.Range(0, 2) == 1)
            {
                isRgightDirection = true;
            }

            if (listOfPlatformPresetObjects.Count >= 1)
            {
                previousPlatformLeftEdgeWorldPosition = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count - 1].transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position;
                previousPlatformRightEdgeWorldPosition = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count - 1].transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position;
            }
            yield return null;

            if (isRgightDirection)
            {

                Y_distance_to_spawn_platform = Random.Range(6.8f, 10f);
                float minXvalue = 4f;
                float maxXvalue = 18f;
                switch (platformTag)
                {
                    case "Platform_1_0": X_distance_to_spawn_platform = Random.Range(minXvalue + 5f, maxXvalue + 5f); break;
                    case "Platform_1_5": X_distance_to_spawn_platform = Random.Range(minXvalue + 7.5f, maxXvalue + 7.5f); break;
                    case "Platform_2_0": X_distance_to_spawn_platform = Random.Range(minXvalue + 10f, maxXvalue + 10f); break;
                    case "Platform_2_5": X_distance_to_spawn_platform = Random.Range(minXvalue + 12.5f, maxXvalue + 12.5f); break;
                    case "Platform_3_0": X_distance_to_spawn_platform = Random.Range(minXvalue + 15f, maxXvalue + 15f); isTowerAllowed = true; break;
                    case "Platform_4_0": X_distance_to_spawn_platform = Random.Range(minXvalue + 20f, maxXvalue + 20f); isTowerAllowed = true; break;
                    case "Platform_5_0": X_distance_to_spawn_platform = Random.Range(minXvalue + 25f, maxXvalue + 25f); isTowerAllowed = true; break;
                    case "Platform_6_0": X_distance_to_spawn_platform = Random.Range(minXvalue + 30f, maxXvalue + 30f); isTowerAllowed = true; break;
                }

                X_distance_to_spawn_platform += previousPlatformRightEdgeWorldPosition.x;
                Y_distance_to_spawn_platform += previousPlatformRightEdgeWorldPosition.y;
            }
            else
            {

                Y_distance_to_spawn_platform = Random.Range(6.8f, 10f);
                float minXvalue = -18f;
                float maxXvalue = -4f;
                switch (platformTag)
                {
                    case "Platform_1_0": X_distance_to_spawn_platform = Random.Range(minXvalue - 5f, maxXvalue - 5f); break;
                    case "Platform_1_5": X_distance_to_spawn_platform = Random.Range(minXvalue - 7.5f, maxXvalue - 7.5f); break;
                    case "Platform_2_0": X_distance_to_spawn_platform = Random.Range(minXvalue - 10f, maxXvalue - 10f); break;
                    case "Platform_2_5": X_distance_to_spawn_platform = Random.Range(minXvalue - 12.5f, maxXvalue - 12.5f); break;
                    case "Platform_3_0": X_distance_to_spawn_platform = Random.Range(minXvalue - 15f, maxXvalue - 15f); isTowerAllowed = true; break;
                    case "Platform_4_0": X_distance_to_spawn_platform = Random.Range(minXvalue - 20f, maxXvalue - 20f); isTowerAllowed = true; break;
                    case "Platform_5_0": X_distance_to_spawn_platform = Random.Range(minXvalue - 25f, maxXvalue - 25f); isTowerAllowed = true; break;
                    case "Platform_6_0": X_distance_to_spawn_platform = Random.Range(minXvalue - 30f, maxXvalue - 30f); isTowerAllowed = true; break;
                }

                X_distance_to_spawn_platform += previousPlatformLeftEdgeWorldPosition.x;
                Y_distance_to_spawn_platform += previousPlatformLeftEdgeWorldPosition.y;
            }

            platform = Instantiate(platform);
            platform.transform.SetParent(platformPresetsParent);
            platform.transform.position = new Vector3(X_distance_to_spawn_platform, Y_distance_to_spawn_platform, 0);
            yield return null;

            if (platform.transform.position.x > maxRightSidePlatformX || platform.transform.position.x < maxLeftSidePlatformX)
            {
                ifSucceded = false;
                Destroy(platform);
            }
            else
            {
                ifSucceded = true;

                if(platform.transform.position.x < 0){
                    CheckIsPlatformOnCollision(platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position);
                }else{
                    CheckIsPlatformOnCollision(platform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position);
                }

                listOfPlatformPresetObjects.Add(platform);
            //    listOfAllPlatformPresetObjects.Add(platform);

                platform.GetComponentInChildren<DecorationSpawnScript>().isSpawnedToRight = isRgightDirection;
             //   platform.GetComponentInChildren<DecorationSpawnScript>().indexInAllPlatformList = listOfPlatformPresetObjects.Count - 1;

                if (isTowerAllowed)
                {
                    int isTowerPlatformChance = Random.Range(1, 11);

                    if (isTowerPlatformChance >= 8)
                    {
                        float leftSpawnPointPositionX = platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.x;
                        float rightSpawnPointPositionX = platform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position.x;
                        float spawnPointPositionY = platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.y;


                        leftSpawnPointPositionX += 7f;
                        rightSpawnPointPositionX -= 7f;

                        Transform spawnedTowerPlatformSpawnPointsTransform = GenerateTowerPlatform(leftSpawnPointPositionX, rightSpawnPointPositionX, spawnPointPositionY, platformPresetsParent);


                        if (spawnedTowerPlatformSpawnPointsTransform != null)
                        {
                            Vector3 leftEdgeTowerPlatformPoint = spawnedTowerPlatformSpawnPointsTransform.Find("SpawnPointLeftEdge").position;
                            Vector3 rightEdgeTowerPlatformPoint = spawnedTowerPlatformSpawnPointsTransform.Find("SpawnPointRightEdge").position;

                            DecorationSpawnScript decorationSpawnScript = platform.transform.Find("SpawnPoints").GetComponent<DecorationSpawnScript>();
                            decorationSpawnScript.isTowerOnThisPlatform = true;
                            decorationSpawnScript.leftEdgeTowerPlatformPoint = leftEdgeTowerPlatformPoint;
                            decorationSpawnScript.rightEdgeTowerPlatformPoint = rightEdgeTowerPlatformPoint;
                        }

                    }

                }

            }
            yield return null;
            loop += 1;
            if (loop >= 100) { Debug.Log("Zapetliło"); break; }

        }
       // StartCoroutine(platform.transform.Find("SpawnPoints").GetComponent<DecorationSpawnScript>().PlatformSetupSequence_1());

        isPreviousSequenceOnFixedUpdateOver = true;
        
        if (isRgightDirection)
            StartCoroutine(SpawnExtraPlatform(platform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position.x + Random.Range(3.0f, 6.0f), platform.transform.position.y + Random.Range(-3.0f, -1.0f), isRgightDirection, platform.transform, platform.tag));
        else
            StartCoroutine(SpawnExtraPlatform(platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.x + Random.Range(-6.0f, -3.0f), platform.transform.position.y + Random.Range(-3.0f, -1.0f), isRgightDirection, platform.transform, platform.tag));


    }

    private IEnumerator SpawnExtraPlatform(float positionXToSpawn, float positionYToSpawn, bool isSpawningToRight, Transform parent, string parentPlatformTag)
    {
        yield return null;
        //Debug.Log("1 _O ty kurla to jednak dziala!");
        GameObject platform = null;
        bool ifSucceded = false;
        int whichIndex;
        float rolledPlatformLength = 0.0f;
        float leftEdgePositionX = 0.0f;
        float rightEdgePositionX = 0.0f;

        //   while (!ifSucceded)
        //    {
        while (!ifSucceded)
        {
            whichIndex = Random.Range(0, floatingPlatformPrefabs.Length);
            platform = floatingPlatformPrefabs[whichIndex];

            switch (platform.tag)
            {
                case "Platform_1_0": ifSucceded = true; break;
                case "Platform_1_5": ifSucceded = true; break;
                case "Platform_2_0": ifSucceded = true; break;
                case "Platform_2_5": if(parentPlatformTag.Equals("Platform_6_0")) ifSucceded = false; else ifSucceded = true; break;
                case "Platform_3_0": if(parentPlatformTag.Equals("Platform_6_0") || parentPlatformTag.Equals("Platform_5_0")) ifSucceded = false; else ifSucceded = true; break;
                case "Platform_4_0": ifSucceded = false; break;
                case "Platform_5_0": ifSucceded = false; break;
                case "Platform_6_0": ifSucceded = false; break;
            }

        }
        yield return null;
       // Debug.Log("2 _O ty kurla to jednak dziala!");

        leftEdgePositionX = platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.x;
        rightEdgePositionX = platform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position.x;

        rolledPlatformLength = Mathf.Abs((rightEdgePositionX - leftEdgePositionX) / 2f);

        if (isSpawningToRight)
            positionXToSpawn += rolledPlatformLength;
        else
            positionXToSpawn -= rolledPlatformLength;

        float failedSpawnXMovement = 0.0f;


        if (positionXToSpawn > maxRightSidePlatformX || positionXToSpawn < maxLeftSidePlatformX)
        {

            failedSpawnXMovement += Random.Range(5.0f, rolledPlatformLength + 3f);

            if (isSpawningToRight)
            {
                positionXToSpawn = maxLeftSidePlatformX + failedSpawnXMovement;
            }
            else
            {
                positionXToSpawn = maxRightSidePlatformX - failedSpawnXMovement;
            }

        }

        platform = Instantiate(platform);
        platform.transform.SetParent(parent);
        platform.transform.position = new Vector3(positionXToSpawn, positionYToSpawn, 0);
        platform.name = "extraPlatform";

        leftEdgePositionX = platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.x;
        rightEdgePositionX = platform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position.x;

        float leftEdgeParentPlatformX = platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.x;
        float rightEdgeParentPlatformX = platform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position.x;

        if (Mathf.Abs(leftEdgeParentPlatformX - rightEdgePositionX) < 3.0f || Mathf.Abs(rightEdgeParentPlatformX - leftEdgePositionX) < 3.0f)
        {
            Destroy(platform);
            StartCoroutine(parent.Find("SpawnPoints").GetComponent<DecorationSpawnScript>().PlatformSetupSequence_1());
            yield break;
            //return
        }
        yield return null;
       // Debug.Log("3 _O ty kurla to jednak dziala!");

        GameObject previousPlatformGameObject;

        if (listOfPlatformPresetObjects.Count >= 2)
        {
            if (listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count - 1].name.Contains("Tower") && listOfPlatformPresetObjects.Count >= 3)
            {
                previousPlatformGameObject = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count - 3];
            }
            else
            {
                previousPlatformGameObject = listOfPlatformPresetObjects[listOfPlatformPresetObjects.Count - 2];
            }

            float leftEdgePositionXPrevious = previousPlatformGameObject.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.x;
            float rightEdgePositionXPrevious = previousPlatformGameObject.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position.x;

            if ((leftEdgePositionXPrevious < leftEdgePositionX && rightEdgePositionXPrevious < leftEdgePositionX) || (leftEdgePositionXPrevious > rightEdgePositionX && rightEdgePositionXPrevious > rightEdgePositionX))
            {
                // do nothing its ok
                //  Debug.Log("Porownanie wczesniejszy: " + previousPlatformGameObject.name + " ---- z obecnym: " + platform.name + "WARTOSCI: "+ leftEdgePositionXPrevious + "<" + leftEdgePositionX + "&&" +rightEdgePositionXPrevious+ "<"+ leftEdgePositionX+ "||" +leftEdgePositionXPrevious +">" +rightEdgePositionX +"&&"+ rightEdgePositionXPrevious+ ">"+ rightEdgePositionX);

            }
            else
            {


                if (platform.transform.position.y - previousPlatformGameObject.transform.position.y < 15.0f)
                {
                    Destroy(platform);
                    StartCoroutine(parent.Find("SpawnPoints").GetComponent<DecorationSpawnScript>().PlatformSetupSequence_1());
                    yield break;
                    //return
                 //   listOfExtraPlatformPresetObjects.RemoveAt(listOfExtraPlatformPresetObjects.Count - 1);
                }
              
            }

          
        }

        yield return null;
       // Debug.Log("4 _O ty kurla to jednak dziala!");
        if (listOfExtraPlatformPresetObjects.Count >= 2)
        {
            GameObject previousExtraPlatform = listOfExtraPlatformPresetObjects[listOfExtraPlatformPresetObjects.Count - 2];

            float leftEdgePositionXPreviousExtra = previousExtraPlatform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position.x;
            float rightEdgePositionXPreviousExtra = previousExtraPlatform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position.x;

            if ((leftEdgePositionXPreviousExtra < leftEdgePositionX && rightEdgePositionXPreviousExtra < leftEdgePositionX) || (leftEdgePositionXPreviousExtra > rightEdgePositionX && rightEdgePositionXPreviousExtra > rightEdgePositionX))
            {
                // do nothing its ok
                //  Debug.Log("Porownanie wczesniejszy extra: " + previousExtraPlatform.name + " ---- z obecnym: " + platform.name + "WARTOSCI: " + leftEdgePositionXPreviousExtra + "<" + leftEdgePositionX + "&&" + rightEdgePositionXPreviousExtra + "<" + leftEdgePositionX + "||" + leftEdgePositionXPreviousExtra + ">" + rightEdgePositionX + "&&" + rightEdgePositionXPreviousExtra + ">" + rightEdgePositionX);
            }
            else
            {
                if (platform.transform.position.y - previousExtraPlatform.transform.position.y < 15.0f)
                {
                    Destroy(platform);
                    StartCoroutine(parent.Find("SpawnPoints").GetComponent<DecorationSpawnScript>().PlatformSetupSequence_1());
                    yield break;
                    //return
                    //listOfExtraPlatformPresetObjects.RemoveAt(listOfExtraPlatformPresetObjects.Count - 1);
                }
               
            }
        }

        if(platform.transform.position.x < 0){
            CheckIsPlatformOnCollision(platform.transform.Find("SpawnPoints").Find("SpawnPointLeftEdge").position);
        }else{
            CheckIsPlatformOnCollision(platform.transform.Find("SpawnPoints").Find("SpawnPointRightEdge").position);
        }

        listOfExtraPlatformPresetObjects.Add(platform);
        StartCoroutine(parent.GetComponentInChildren<DecorationSpawnScript>().PlatformSetupSequence_1());

        //listOfAllPlatformPresetObjects.Add(platform);

    //    platform.GetComponentInChildren<DecorationSpawnScript>().isSpawnedToRight = isSpawningToRight;
        platform.GetComponentInChildren<DecorationSpawnScript>().isThisAExtraPlatform = true;
        parent.Find("SpawnPoints").GetComponent<DecorationSpawnScript>().extraPlatformTransform = platform.transform;
      //  platform.GetComponentInChildren<DecorationSpawnScript>().indexInAllPlatformList = listOfAllPlatformPresetObjects.Count - 1;

    }

    private void CheckIsPlatformOnCollision(Vector2 centerPoint){
        var layerMask = (1 << 17); //enemy layer
        Collider2D[] cols = Physics2D.OverlapCircleAll(centerPoint, 4f, layerMask);
        foreach(Collider2D col in cols){
            if(col.tag.Equals("EnemyTentacleWorm")){
               // Debug.Log("Usunieto EnemyTentacleWorm: "+col.transform.position);
                Destroy(col.gameObject);
              
            }
        }

    }
    /*private void RemoveTentacleWormOnColision(){
        var layerMask = (1 << 8); //ground layer
        Collider2D groundColider = Physics2D.OverlapCircle(spawnedEnemy.transform.position, 4f, layerMask);
    }*/

    private void DeletePlatformPresets()
    {
        
        Destroy(listOfPlatformPresetObjects[0]);
        listOfPlatformPresetObjects.RemoveAt(0);

        waterInGameObject.transform.position = new Vector3(0, listOfPlatformPresetObjects[0].transform.position.y - 1.0f, 0);
        try
        {
            GameObject extraPlatform = listOfPlatformPresetObjects[0].transform.Find("extraPlatform").gameObject;
            if (extraPlatform != null)
            {
                Destroy(extraPlatform);
            }
        }
        catch (System.NullReferenceException e)
        { 
            // do nothing 
            // it means platform set do not have extra platform that we need to destroy 
        }

    }

}
