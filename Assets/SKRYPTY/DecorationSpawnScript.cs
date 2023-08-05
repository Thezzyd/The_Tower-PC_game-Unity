
using UnityEngine;
using System.Collections;


public class DecorationSpawnScript : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
    private Vector2 leftSpawnPoint;
    private Vector2 rightSpawnPoint;

    private Vector2 leftSpawnPoint_Enemies;
    private Vector2 rightSpawnPoint_Enemies;

    private Transform decorationsParent;

    private TowerSpawnManager towerSpawnManager;

    private Transform spawnedTowerPlatformSpawnPointsTransform;

    public bool isTowerOnThisPlatform;
    public bool isThisATowerPlatform;
    public bool isThisAExtraPlatform;
    public Transform extraPlatformTransform;
    public Vector3 leftEdgeTowerPlatformPoint;
    public Vector3 rightEdgeTowerPlatformPoint;

    public bool isSpawnedToRight;

   // private bool isThisPlatformFull;
    private int enemiesPacksCountOnThisPlatform;
    // public int indexInAllPlatformList;
    //levelManager.towerHightReachedFinalCounter > 000

    public int chanceForAurora, chanceForChest, chnceForEnemyBat, chanceForEnemyEgg, chanceForNormalSpider,
    chanceForBigSpider, chanceForEnemyPortal, chanceForJumpingSpider, chanceForWebString, GenerateEnemySkeletalDragon,
    chnceForEnemyTentacleWorm, chanceForEnemyFirefly, chanceForHangingEnemy;

    void Start()
    {   
        //StartCoroutine(PlatformSetupSequence_1());
        //GenerateDecorations(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);

        //aurora
       // int chanceForAurora = Random.Range(1, 101);
       
        // end aurora

       // int chanceForChest = Random.Range(1, 101);
        

        //int chnceForEnemyBat = Random.Range(1, 101);
        

        //int chanceForEnemyEgg = Random.Range(1, 101);
       
        
        
        

        //int chanceForNormalSpider = Random.Range(1, 101);
        

        //int chanceForBigSpider = Random.Range(1, 101);
        

       // float chanceForEnemyPortal = Random.Range(0f, 100f);
        

       // float chanceForJumpingSpider = Random.Range(0f, 100f);
        

      //  float chanceForWebString = Random.Range(0f, 100f);
        

       /* float chanceForSkeletalDragon = Random.Range(0f, 100f);
        if (chanceForSkeletalDragon <= 0.2f && levelManager.towerHightReachedFinalCounter > 600)   //parametryzacja 
        {
            GenerateEnemySkeletalDragon(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);
        }*/

        //int chnceForEnemyTentacleWorm = Random.Range(1, 101);


       // int chanceForEnemyFirefly = Random.Range(1, 101);
        

        //int chanceForHangingEnemy = Random.Range(1, 101);
        
    }

    public IEnumerator PlatformSetupSequence_1(){
        enemiesPacksCountOnThisPlatform = 0;
        enemiesManager = FindObjectOfType<EnemiesManager>();
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
        levelManager = FindObjectOfType<LevelManager>();
        decorationsParent = transform;
        leftSpawnPoint = gameObject.transform.Find("SpawnPointLeftEdge").position;
        rightSpawnPoint = gameObject.transform.Find("SpawnPointRightEdge").position;
        leftSpawnPoint_Enemies = gameObject.transform.Find("SpawnPointLeftEdge").position + new Vector3(1.5f, 0, 0);
        rightSpawnPoint_Enemies = gameObject.transform.Find("SpawnPointRightEdge").position + new Vector3(-1.5f, 0, 0);

        yield return null;
        StartCoroutine(PlatformSetupSequence_2());
    }
    
    public IEnumerator PlatformSetupSequence_2(){
        GenerateDecorations(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);
        chanceForAurora = Random.Range(1, 101);
        chanceForChest = Random.Range(1, 101);
        chnceForEnemyBat = Random.Range(1, 101);
        chanceForEnemyEgg = Random.Range(1, 101);
        chanceForNormalSpider = Random.Range(1, 101);
        chanceForBigSpider = Random.Range(1, 101);
        chanceForEnemyPortal = Random.Range(0, 100);
        chanceForJumpingSpider = Random.Range(0, 100);
        chanceForWebString = Random.Range(0, 100);
        //chanceForSkeletalDragon = Random.Range(0f, 100f);
        chnceForEnemyTentacleWorm = Random.Range(1, 101);
        chanceForEnemyFirefly = Random.Range(1, 101);
        chanceForHangingEnemy = Random.Range(1, 101);

        yield return null;
        StartCoroutine(PlatformSetupSequence_3());
    }

    public IEnumerator PlatformSetupSequence_3(){
        if (chanceForAurora <= 100){
            GenerateAurora(towerSpawnManager.maxLeftSidePlatformX, towerSpawnManager.maxRightSidePlatformX, leftSpawnPoint.y - 10f, leftSpawnPoint.y + 10f);
        }if (chanceForChest <= 12){
            GenerateChest(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);
        }if (chnceForEnemyBat <= 25 && levelManager.towerHightReachedFinalCounter > 5){
            GenerateEnemyBat(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);
        } if (chanceForEnemyEgg <= 15){
            GenerateEnemyEggSpider(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y, 1, 1);  // pojedyncze jajka
        }else if (chanceForEnemyEgg <= 25){
            GenerateEnemyEggSpider(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y, 2, 3);  // male grupki
        }else if (chanceForEnemyEgg <= 32){
            GenerateEnemyEggSpider(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y, 3, 6);  // wieksze grupki
        }else if (chanceForEnemyEgg <= 34){
            GenerateEnemyEggSpider(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y, 6, 14); // duze grupki
        }

        yield return null;
        StartCoroutine(PlatformSetupSequence_4());
    }

    public IEnumerator PlatformSetupSequence_4(){
        if (chanceForNormalSpider <= 25){ 
            GenerateEnemyNormalSpider(leftSpawnPoint_Enemies.x, rightSpawnPoint_Enemies.x, leftSpawnPoint_Enemies.y);
        }if (chanceForBigSpider <= 12){
            GenerateEnemyBigSpider(leftSpawnPoint_Enemies.x, rightSpawnPoint_Enemies.x, leftSpawnPoint_Enemies.y);
        }if (chanceForEnemyPortal <= 20f && levelManager.towerHightReachedFinalCounter > 1000){
            GenerateEnemyPortal(leftSpawnPoint_Enemies.x, rightSpawnPoint_Enemies.x, leftSpawnPoint_Enemies.y);
        }if (chanceForJumpingSpider <= 9 && levelManager.towerHightReachedFinalCounter > 40){
            GenerateEnemyJumpingSpider(leftSpawnPoint_Enemies.x, rightSpawnPoint_Enemies.x, leftSpawnPoint_Enemies.y);
        }if (chanceForEnemyFirefly <= 10 && levelManager.towerHightReachedFinalCounter > 5){
            GenerateEnemyFirefly(towerSpawnManager.maxLeftSidePlatformX + 4.5f, towerSpawnManager.maxRightSidePlatformX - 4.5f, leftSpawnPoint.y - 10, leftSpawnPoint.y + 5);
        }

        yield return null;
        StartCoroutine(PlatformSetupSequence_5());
    }

    public IEnumerator PlatformSetupSequence_5(){
        if (chnceForEnemyTentacleWorm <= 20 && levelManager.towerHightReachedFinalCounter > 5)   //parametryzacja
        {
            var layerMask = (1 << 18); //wall layer
            int sideOfWall = Random.Range(0, 2);
            if (sideOfWall == 0)
            {
                RaycastHit2D leftWall = Physics2D.Raycast(rightSpawnPoint, Vector2.left, 400, layerMask);
                if (leftWall.collider)
                {
                    leftWall.collider.GetComponent<WallComponent>().TentacleEnemySpawn();
                }
            }
            else 
            {
                RaycastHit2D rightWall = Physics2D.Raycast(leftSpawnPoint, Vector2.right, 400, layerMask);
                if (rightWall.collider)
                {
                    rightWall.collider.GetComponent<WallComponent>().TentacleEnemySpawn();
                }
            }
        }
        if (chanceForHangingEnemy <= 10 && !isThisATowerPlatform && levelManager.towerHightReachedFinalCounter > 5){
            GenerateHangingEnemy(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);
        }

        yield return null;
        StartCoroutine(PlatformSetupSequence_6());
    }

    public IEnumerator PlatformSetupSequence_6(){
       if (chanceForWebString <= 60 && !isThisATowerPlatform)
        {
            if (chanceForWebString <= 25)
            {
                if (!isSpawnedToRight)
                    GenerateWebStringOnEdges(rightSpawnPoint, true);
                else
                    GenerateWebStringOnEdges(leftSpawnPoint, false);
            }
            else
            {
                GenerateWebStringBetweenPlatforms(leftSpawnPoint.x, rightSpawnPoint.x, leftSpawnPoint.y);
            }
        }

        if(extraPlatformTransform){
            yield return null;
            StartCoroutine(extraPlatformTransform.GetComponentInChildren<DecorationSpawnScript>().PlatformSetupSequence_1());
        }else{
            yield break;
        }
    }

    public void GenerateDecorations(float leftPositionX, float rightPositionX, float worldPositionY) {
        float distanceOfSpawningArea = rightPositionX - leftPositionX;
        int roundedDistanceOfSpawningArea = (int)distanceOfSpawningArea;

        int decorationsNumber = Random.Range(0, roundedDistanceOfSpawningArea/2);
        int lanternsCount = 0;
        int campFireCount = 0;
        int sortingLayerStartNumber = -47;

        for (int i = 0; i <= decorationsNumber; i++) {

            int whichArrayOfPrefabs = Random.Range(1, 25);
            int whichArrayIndex = 0;
            GameObject spawnedDecoration = null;
            Vector3 decorationPosition = new Vector3(0,0,0);
            int ifMirror = 0;
            float locScaleMultiplier = 1f;
            bool isStone = false;
            bool isLantern = false;
            bool isCampFire = false;
            bool isTree = false;

            switch (whichArrayOfPrefabs) {
                case int n when (n <= 10): whichArrayIndex = Random.Range(0, towerSpawnManager.decorationTreePrefabs.Length); spawnedDecoration = towerSpawnManager.decorationTreePrefabs[whichArrayIndex] as GameObject; isTree = true; break;
                case int n when (n <= 14): whichArrayIndex = Random.Range(0, towerSpawnManager.decorationTreeFireflyPrefabs.Length); spawnedDecoration = towerSpawnManager.decorationTreeFireflyPrefabs[whichArrayIndex] as GameObject; isTree = true; break;
                case int n when (n <= 15): whichArrayIndex = Random.Range(0, towerSpawnManager.campFirePrefabs.Length); spawnedDecoration = towerSpawnManager.campFirePrefabs[whichArrayIndex] as GameObject; isCampFire = true; break;
                case int n when (n <= 23): whichArrayIndex = Random.Range(0, towerSpawnManager.decorationStonePrefabs.Length); spawnedDecoration = towerSpawnManager.decorationStonePrefabs[whichArrayIndex] as GameObject; isStone = true; break; 
                case int n when (n <= 24): whichArrayIndex = Random.Range(0, towerSpawnManager.lanternPrefabs.Length); spawnedDecoration = towerSpawnManager.lanternPrefabs[whichArrayIndex] as GameObject; isLantern = true; break; 
            }

            ifMirror = Random.Range(0,2);
            locScaleMultiplier = Random.Range(0.6f, 1.4f);
            decorationPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY, Random.Range(0.0f, 1.0f));

            if(isCampFire || isStone)
                decorationPosition = new Vector3(Random.Range(leftPositionX + 1.5f, rightPositionX - 1.5f), worldPositionY, Random.Range(0.0f, 1.0f));


            if (isLantern && lanternsCount >= 1)
            {
                i -= 1;
                continue;
            }

            if (isCampFire && campFireCount >= 1)
            {
                i -= 1;
                continue;
            }

            spawnedDecoration = Instantiate(spawnedDecoration) as GameObject;
            spawnedDecoration.GetComponentInChildren<SpriteRenderer>().sortingOrder = sortingLayerStartNumber;
            sortingLayerStartNumber += 1;
            if (sortingLayerStartNumber > -2) sortingLayerStartNumber = -47;
            spawnedDecoration.transform.SetParent(decorationsParent);
            spawnedDecoration.transform.position = decorationPosition;


            if (isCampFire)
            {
                if (spawnedDecoration.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedDecoration.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
                {
                    Destroy(spawnedDecoration);
                    continue;
                }

                locScaleMultiplier = Random.Range(0.92f, 1.08f);
                spawnedDecoration.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
                campFireCount += 1;
                towerSpawnManager.fireCampAudioSourceList.Add(spawnedDecoration.GetComponent<AudioSource>());

                spawnedDecoration.GetComponent<AudioSource>().volume = OptionsManager.sfxVolume * OptionsManager.fireCampBaseVolume;

            }

            if (isLantern)
            {
                if (spawnedDecoration.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedDecoration.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
                {
                    Destroy(spawnedDecoration);
                    continue;
                }
                locScaleMultiplier = Random.Range(0.6f, 1.05f);
                spawnedDecoration.GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;
                lanternsCount += 1;
            }

            if (isTowerOnThisPlatform && spawnedDecoration.transform.position.x > leftEdgeTowerPlatformPoint.x - 1.5f && spawnedDecoration.transform.position.x < rightEdgeTowerPlatformPoint.x + 1.5f)
            {
                if (isTree)
                {
                    locScaleMultiplier = Random.Range(0.2f, 0.38f);
                }
                else if (isStone)
                {
                    locScaleMultiplier = Random.Range(0.6f, 0.8f);
                }
                else if (isLantern)
                {
                    i -= 1;
                    Destroy(spawnedDecoration);
                    continue;
                }
                
            }

            if (ifMirror == 1)
            {
                spawnedDecoration.transform.localScale = new Vector3(-spawnedDecoration.transform.localScale.x * locScaleMultiplier, spawnedDecoration.transform.localScale.y * locScaleMultiplier, spawnedDecoration.transform.localScale.z);
            }
            else
            {
                spawnedDecoration.transform.localScale = new Vector3(spawnedDecoration.transform.localScale.x * locScaleMultiplier, spawnedDecoration.transform.localScale.y * locScaleMultiplier, spawnedDecoration.transform.localScale.z);
            }

            if (isStone)
            {
                spawnedDecoration.transform.localEulerAngles = new Vector3(spawnedDecoration.transform.localEulerAngles.x, spawnedDecoration.transform.localEulerAngles.y, spawnedDecoration.transform.localEulerAngles.z + Random.Range(-30.0f, 30.0f));
            }

            towerSpawnManager.decorationObjectsList.Add(spawnedDecoration);

        }

    
    }


    public void GenerateChest(float leftPositionX, float rightPositionX, float worldPositionY)
    {
        int whichChest = Random.Range(1, 101);
        GameObject spawnedChest = null;
        Vector3 chestPosition = new Vector3(0, 0, 0);

        switch (whichChest)
        {
            case int n when (n <= 80): spawnedChest = towerSpawnManager.chestSmallPrefab as GameObject; break;
            case int n when (n <= 95): spawnedChest = towerSpawnManager.chestNormalPrefab as GameObject; break;
            case int n when (n <= 100): spawnedChest = towerSpawnManager.chestBigPrefab as GameObject; break;
              
        }

        chestPosition = new Vector3(Random.Range(leftPositionX + 1.5f, rightPositionX - 1.5f), worldPositionY, Random.Range(0.0f, 1.0f));
        spawnedChest = Instantiate(spawnedChest) as GameObject;
        spawnedChest.transform.SetParent(transform);
        spawnedChest.transform.position = chestPosition;

        if (spawnedChest.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedChest.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
        {
            Destroy(spawnedChest);
            return;
        }

        towerSpawnManager.chestObjectsList.Add(spawnedChest);

    }


    public void GenerateAurora(float leftBoundX, float rightBoundX, float topBoundY, float bottomBoundY)
    {
       
        GameObject spawnedAurora = towerSpawnManager.auroraPrefab;
        Vector3 auroraPosition = new Vector3(0, 0, 0);

        auroraPosition = new Vector3(Random.Range(leftBoundX, rightBoundX), Random.Range(bottomBoundY, topBoundY), Random.Range(0.0f, 1.0f));

        spawnedAurora = Instantiate(spawnedAurora) as GameObject;
        spawnedAurora.transform.SetParent(transform);
        spawnedAurora.transform.localScale = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.8f, 1.2f), 1);
        spawnedAurora.transform.position = auroraPosition;

        towerSpawnManager.auroraObjectsList.Add(spawnedAurora);

    }

    public void GenerateEnemyBat(float leftPositionX, float rightPositionX, float worldPositionY)
    {
        int howManyEnemies = Random.Range(1, enemiesManager.enemyBatMaxQuanityParameter);   

        if (howManyEnemies > enemiesManager.enemyBatMaxQuantityPerPlatform)
            howManyEnemies = enemiesManager.enemyBatMaxQuantityPerPlatform;

        for (int i = 1; i <= howManyEnemies; i++)
        {
            GameObject spawnedEnemy = null;
            Vector3 enemyPosition = new Vector3(0, 0, 0);

            spawnedEnemy = enemiesManager.enemyBatPrefab as GameObject;

            enemyPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY + 3f, 0);
            spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
            spawnedEnemy.transform.SetParent(enemiesManager.enemiesParent);
            spawnedEnemy.transform.position = enemyPosition;

            if (spawnedEnemy.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedEnemy.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
            {
                Destroy(spawnedEnemy);
                continue;
            }

            enemiesManager.enemiesBatObjectsList.Add(spawnedEnemy);
        }
    }

   

    public void GenerateEnemyEggSpider(float leftPositionX, float rightPositionX, float worldPositionY, int eggsOneGroupMinQuantity, int eggsOneGroupMaxQuantity)
    {
        int howManyGroups = 0;
        switch (transform.parent.tag)
        {
            case "Platform_1_0": howManyGroups = Random.Range(1, 2); break;
            case "Platform_1_5": howManyGroups = Random.Range(1, 3); break;
            case "Platform_2_0": howManyGroups = Random.Range(1, 4); break;
            case "Platform_2_5": howManyGroups = Random.Range(1, 4); break;
            case "Platform_3_0": howManyGroups = Random.Range(1, 5); break;
            case "Platform_4_0": howManyGroups = Random.Range(1, 6); break;
            case "Platform_5_0": howManyGroups = Random.Range(1, 7); break;
            case "Platform_6_0": howManyGroups = Random.Range(1, 8); break;
        }

        for (int i = 1; i <= howManyGroups; i++)
        {
            int howManyEnemiesInOneGroup = Random.Range(eggsOneGroupMinQuantity, eggsOneGroupMaxQuantity + 1);
            Vector3 eggGroupPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY + 3f, 0);

            for (int j = 1; j <= howManyEnemiesInOneGroup; j++)
            {
                int whichEggPrefab = Random.Range(0, enemiesManager.enemyEggEmptyPrefab.Length);
                GameObject spawnedEgg = enemiesManager.enemyEggEmptyPrefab[whichEggPrefab] as GameObject;

                Vector3 eggPosition = eggGroupPosition + new Vector3(Random.Range(0f, 0.4f), Random.Range(0f, 3f), 0);

                spawnedEgg = Instantiate(spawnedEgg) as GameObject;
                spawnedEgg.transform.SetParent(transform);
                spawnedEgg.transform.position = eggPosition;

                if (spawnedEgg.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedEgg.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
                {
                    Destroy(spawnedEgg);
                    continue;
                }

                enemiesManager.enemiesEggSpiderObjectsList.Add(spawnedEgg);
            }
        }
    }

    public void GenerateEnemyNormalSpider(float leftPositionX, float rightPositionX, float worldPositionY)
    {
        int howManyEnemies = 0;

        switch (transform.parent.tag)
        {
            case "Platform_1_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 1) + 1); if (enemiesPacksCountOnThisPlatform >= 1) return; break;
            case "Platform_1_5": howManyEnemies = Random.Range(1, ((int) (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 1.5f)) + 1); if (enemiesPacksCountOnThisPlatform >= 2) return; break;
            case "Platform_2_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 2) + 1); break;
            case "Platform_2_5": howManyEnemies = Random.Range(1, ((int) (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 2.5f)) + 1); break;
            case "Platform_3_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 3) + 1); break;
            case "Platform_4_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 4) + 1); break;
            case "Platform_5_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 5) + 1); break;
            case "Platform_6_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyNormalSpiderMaxQuanityParameter * 6) + 1); break;
        }

        for (int i = 1; i <= howManyEnemies; i++)
        {
            GameObject spawnedEnemy = null;
            Vector3 enemyPosition = new Vector3(0, 0, 0);

            spawnedEnemy = enemiesManager.enemyNormalSpiderPrefab as GameObject;
            enemyPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY, Random.Range(-2.0f, 0.0f));

            spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
            spawnedEnemy.transform.SetParent(transform);
            spawnedEnemy.transform.position = enemyPosition;

            if (spawnedEnemy.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedEnemy.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
            {
                Destroy(spawnedEnemy);
                continue;
            }

            enemiesManager.enemiesNormalSpiderObjectsList.Add(spawnedEnemy);
        }

        enemiesPacksCountOnThisPlatform++;
    }

    public void GenerateEnemyBigSpider(float leftPositionX, float rightPositionX, float worldPositionY)
    {
        int howManyEnemies = 0;

        switch (transform.parent.tag)
        {
            case "Platform_1_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyBigSpiderMaxQuanityParameter * 1) + 1); if (enemiesPacksCountOnThisPlatform >= 1) return; break;
            case "Platform_1_5": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyBigSpiderMaxQuanityParameter * 1f)) + 1); if (enemiesPacksCountOnThisPlatform >= 2) return; break;
            case "Platform_2_0": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyBigSpiderMaxQuanityParameter * 1.5f)) + 1); break;
            case "Platform_2_5": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyBigSpiderMaxQuanityParameter * 1.5f)) + 1); break;
            case "Platform_3_0": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyBigSpiderMaxQuanityParameter * 1.5f)) + 1); break;
            case "Platform_4_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyBigSpiderMaxQuanityParameter * 2) + 1); break;
            case "Platform_5_0": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyBigSpiderMaxQuanityParameter * 2.5f)) + 1); break;
            case "Platform_6_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyBigSpiderMaxQuanityParameter * 3) + 1); break;
        }

        for (int i = 1; i <= howManyEnemies; i++)
        {
            GameObject spawnedEnemy = null;
            Vector3 enemyPosition = new Vector3(0, 0, 0);

            spawnedEnemy = enemiesManager.enemyBigSpiderPrefab as GameObject;
            enemyPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY +1f, Random.Range(0.0f, 0.2f));

            spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
            spawnedEnemy.transform.SetParent(transform);
            spawnedEnemy.transform.position = enemyPosition;

            if (spawnedEnemy.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedEnemy.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
            {
                Destroy(spawnedEnemy);
                continue;
            }

            enemiesManager.enemiesBigSpiderObjectsList.Add(spawnedEnemy);
        }
        enemiesPacksCountOnThisPlatform++;

    }

    public void GenerateEnemyPortal(float leftPositionX, float rightPositionX, float worldPositionY)
    {
        float portalDifficulty = 0;
      
        if (!transform.parent.CompareTag("Platform_1_0") && !transform.parent.CompareTag("Platform_1_5"))
        {
            switch (transform.parent.tag)
            {
                case "Platform_2_0": portalDifficulty = 1.0f; break;
                case "Platform_2_5": portalDifficulty = Random.Range(1.0f, 1.3f); break;
                case "Platform_3_0": portalDifficulty = Random.Range(1.3f, 1.8f); break;
                case "Platform_4_0": portalDifficulty = Random.Range(1.8f, 2.2f); break;
                case "Platform_5_0": portalDifficulty = Random.Range(2.2f, 2.8f); break;
                case "Platform_6_0": portalDifficulty = Random.Range(2.8f, 3.4f); break;
            }

            GameObject spawnedPortal = null;
            Vector3 portalPosition = new Vector3(0, 0, 0);

            spawnedPortal = enemiesManager.enemyPortalPrefab as GameObject;
            portalPosition = new Vector3(Random.Range(leftPositionX + 3.5f, rightPositionX - 3.5f), worldPositionY + 4f, Random.Range(0.0f, 1.0f));

            spawnedPortal = Instantiate(spawnedPortal) as GameObject;
            spawnedPortal.transform.SetParent(transform);
            spawnedPortal.transform.position = portalPosition;

            if (spawnedPortal.transform.position.x < (towerSpawnManager.maxLeftSidePlatformX + 2f) || spawnedPortal.transform.position.x > (towerSpawnManager.maxRightSidePlatformX - 2f))
            {
                Destroy(spawnedPortal);
                return;
            }

            if (isTowerOnThisPlatform && spawnedPortal.transform.position.x > leftEdgeTowerPlatformPoint.x - 3f && spawnedPortal.transform.position.x < rightEdgeTowerPlatformPoint.x + 3f)
            {
                Destroy(spawnedPortal);
                return;
            }

            spawnedPortal.GetComponent<EnemyPortalComponent>().portalDifficultyMultiplier = portalDifficulty;

            enemiesManager.enemiesPortalObjectsList.Add(spawnedPortal);
            
        }
    }

    public void GenerateWebStringOnEdges(Vector2 edgePosition, bool isOnRightEdge)
    {

        GameObject spawnedWebString = null;
        Vector3 webStringPosition = new Vector3(0, 0, 0);

        spawnedWebString = towerSpawnManager.webStringPrefab;

        webStringPosition = edgePosition;

        spawnedWebString = Instantiate(spawnedWebString) as GameObject;
        spawnedWebString.transform.SetParent(transform);
        spawnedWebString.transform.position = webStringPosition;

         
        spawnedWebString.GetComponent<RopeBehaviourComponent>().isOnRightEdge = isOnRightEdge;
        spawnedWebString.GetComponent<RopeBehaviourComponent>().webStringType = "Edge";
      //  spawnedWebString.GetComponent<RopeBehaviourComponent>().indexInPlatformList = indexInAllPlatformList;

        towerSpawnManager.webStringObjectsList.Add(spawnedWebString);

    }

    public void GenerateWebStringBetweenPlatforms(float leftPositionX, float rightPositionX, float worldPositionY)
    {

        GameObject spawnedWebString = null;
        Vector3 webStringPosition = new Vector3(0, 0, 0);

        spawnedWebString = towerSpawnManager.webStringPrefab;

        webStringPosition = new Vector3(Random.Range(leftPositionX + 1.5f, rightPositionX - 1.5f), worldPositionY - 1.5f, 0);

        spawnedWebString = Instantiate(spawnedWebString) as GameObject;
        spawnedWebString.transform.SetParent(transform);
        spawnedWebString.transform.position = webStringPosition;

        spawnedWebString.GetComponent<RopeBehaviourComponent>().webStringType = "Edge";

        towerSpawnManager.webStringObjectsList.Add(spawnedWebString);

        
    }

    public void GenerateEnemyJumpingSpider(float leftPositionX, float rightPositionX, float worldPositionY)
    {
        int howManyEnemies = 0;

        switch (transform.parent.tag)
        {
            case "Platform_1_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 1) + 1); if (enemiesPacksCountOnThisPlatform >= 1) return; break;
            case "Platform_1_5": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 1.2f)) + 1); if (enemiesPacksCountOnThisPlatform >= 2) return; break;
            case "Platform_2_0": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 1.5f)) + 1); break;
            case "Platform_2_5": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 2f)) + 1); break;
            case "Platform_3_0": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 2.5f)) + 1); break;
            case "Platform_4_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 3) + 1); break;
            case "Platform_5_0": howManyEnemies = Random.Range(1, ((int)(enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 3.5f)) + 1); break;
            case "Platform_6_0": howManyEnemies = Random.Range(1, (enemiesManager.enemyJumpingSpiderMaxQuanityParameter * 2) + 1); break;
        }

        for (int i = 1; i <= howManyEnemies; i++)
        {
            GameObject spawnedEnemy = null;
            Vector3 enemyPosition = new Vector3(0, 0, 0);

            spawnedEnemy = enemiesManager.enemyJumpingSpiderPrefab as GameObject;
            enemyPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY + 1.5f, Random.Range(-2.0f, 0.0f));

            spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
            spawnedEnemy.transform.SetParent(transform);
            spawnedEnemy.transform.position = enemyPosition;

            if (spawnedEnemy.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedEnemy.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
            {
                Destroy(spawnedEnemy);
                continue;
            }

            enemiesManager.enemiesJumpingSpiderObjectsList.Add(spawnedEnemy);
        }
        enemiesPacksCountOnThisPlatform++;
    }

    public void GenerateHangingEnemy(float leftPositionX, float rightPositionX, float worldPositionY)
    {

        GameObject spawnedWebString = null;
        Vector3 webStringPosition = new Vector3(0, 0, 0);

        spawnedWebString = towerSpawnManager.webStringPrefab;

        webStringPosition = new Vector3(Random.Range(leftPositionX + 1.5f, rightPositionX - 1.5f), worldPositionY - 1.5f, 0);

        spawnedWebString = Instantiate(spawnedWebString) as GameObject;
        spawnedWebString.transform.SetParent(transform);
        spawnedWebString.transform.position = webStringPosition;

        spawnedWebString.GetComponent<RopeBehaviourComponent>().webStringType = "Enemy";

        if (spawnedWebString.transform.position.x < towerSpawnManager.maxLeftSidePlatformX + 2f || spawnedWebString.transform.position.x > towerSpawnManager.maxRightSidePlatformX - 2f)
        {
            Destroy(spawnedWebString);
            return;
        }

        towerSpawnManager.webStringObjectsList.Add(spawnedWebString);
    }

    public void GenerateEnemyFirefly(float minXPosition, float maxXPosition, float minYPosition, float maxYPosition)
    {
        int howManyEnemies = Random.Range(3, enemiesManager.enemyFireflyMaxQuanityParameter);
        bool isSucceded = false;
        int itrationCounter = 0;

        while (!isSucceded)
        {
            if (itrationCounter >= 6)
            {
                Debug.Log("Zapetlilo spawn firefly");
                break;
            }

            GameObject spawnedEnemy = null;
            Vector3 enemyPosition = new Vector3(0, 0, 0);

            spawnedEnemy = enemiesManager.enemyFireflyHivePrefab as GameObject;

            enemyPosition = new Vector3(Random.Range(minXPosition, maxXPosition), Random.Range(minYPosition, maxYPosition), 0);
            spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
            spawnedEnemy.transform.SetParent(enemiesManager.enemiesParent);
            spawnedEnemy.transform.position = enemyPosition;

            spawnedEnemy.GetComponent<EnemyFireflyHive>().howManyHiveFirelfies = howManyEnemies;

            if (spawnedEnemy.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedEnemy.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
            {
                itrationCounter++;
                Destroy(spawnedEnemy);
                continue;
            }

            if (Physics2D.OverlapCircle(spawnedEnemy.transform.position, 3.0f, (1 << 8) | (1 << 18)))
            {
                itrationCounter++;
                Destroy(spawnedEnemy);
                continue;
            }

            enemiesManager.enemiesFireflyHiveObjectsList.Add(spawnedEnemy);
            isSucceded = true;
        }
    }

    /*public void GenerateEnemySkeletalDragon(float leftPositionX, float rightPositionX, float worldPositionY)
    {

        GameObject spawnedEnemy = null;
        Vector3 enemyPosition = new Vector3(0, 0, 0);

        spawnedEnemy = enemiesManager.enemySkeletalDragonPrefab as GameObject;

        enemyPosition = new Vector3(Random.Range(leftPositionX, rightPositionX), worldPositionY + 3f, 0);
        spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
        spawnedEnemy.transform.SetParent(enemiesManager.enemiesParent);
        spawnedEnemy.transform.position = enemyPosition;

        if (spawnedEnemy.transform.position.x < towerSpawnManager.maxLeftSidePlatformX || spawnedEnemy.transform.position.x > towerSpawnManager.maxRightSidePlatformX)
        {
            Destroy(spawnedEnemy);
            return;
        }

        enemiesManager.enemiesSkeletalDragonObjectsList.Add(spawnedEnemy);

    }*/

}


  
