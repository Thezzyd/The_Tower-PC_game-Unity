using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour
{

    [Header("Find other instances ")]
    private HeroesManager heroesManager;
    private FirebaseManager firebaseManager;
    private HeroesStatisticsCanvasManager heroesStatisticsCanvasManager;
    private SkillsUpgradeManager skillsUpgradeManager;
    private EnemiesManager enemiesManager;
    public AudioManager audioManager;
    private HeroBaseComponent heroBaseComponent;
    private PowerBar powerBar;


    [Header("Counters")]
    public Transform highestTowerPointReached;
    public int towerHightReachedFinalCounter;
    [HideInInspector] public float highestTowerYaxisValueReached;
    private int endGameExperiencePoints;
    [HideInInspector] public int scoreFinalCounter;
    [HideInInspector] public int killFinalCounter;
    [HideInInspector] public float timeFinalCounter;
    [HideInInspector] public float bossesCounter;
    public Transform gridGraphFollowPoint;


    [Header("UI Texts")]
    public TextMeshProUGUI towerHightReachedText;
    public TextMeshProUGUI towerHightConstText;
    public TextMeshProUGUI starsText;
    public TextMeshProUGUI[] statsValuesHERO1;
    public TextMeshProUGUI[] statsValuesHERO2;
    public TextMeshProUGUI[] statsValuesHERO3;
    public TextMeshProUGUI[] statsValuesHERO4;
    public TextMeshProUGUI[] statsValuesHERO5;
    public TextMeshProUGUI[] statsValuesHERO6;
    public TextMeshProUGUI currentLevelTextGameViewCanvas;
    public TextMeshProUGUI currentLevelTextGameOverCanvas;
    public TextMeshProUGUI currentExperiencePointsInfoTextGameViewCanvas;
    public TextMeshProUGUI currentExperiencePointsInfoTextGameOverCanvas;
    public TextMeshProUGUI availableLevelUpgradePoints;
    public TextMeshProUGUI availableLevelUpgradePointsOnGameViewIcon;
    public TextMeshProUGUI[] generalStatsValues;
    public TextMeshProUGUI[] gameOverTextsBest;
    public TextMeshProUGUI[] upgradeRealValuesText;


    [Header("UI Canvases")]
    public Animator canvasGameViewAnimator;
    public Material canvasScreenTabletMaterial;
    public Image[] canvasGameViewIcons;
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;


    [Header("Star")]
    public GameObject[] StarEffect;
    [HideInInspector] public int heroIndexRolledByStar;
    [HideInInspector] public int starRampageCounter;
    [HideInInspector] public float starTimer;
    [HideInInspector] public float starPenaltyTimer;
    [HideInInspector] public float starPenaltyWeight;
    // [HideInInspector] public float startingStarTimerValue;
    public Animator starTimerTextAnimator;
    public TextMeshProUGUI starTimerText;
    public GameObject starHomingParticlesEffect;
    public GameObject starDirectionPointingArrowPrefab;
    [HideInInspector] public int starFinalCounter;
    public GameObject starPrefab;
    public Animator starRampageAnimator;
    public int rampageEssenceDropBonusX1Value;
    public int rampageEssenceDropBonusX2Value;
    public bool isStarCollectedSequenceOver;
    public Vector3 starEssenceSpawnPosition;


    [Header("Map settings")]
    public Transform cameraAndEnemiesTargetPoint;
    [HideInInspector] public bool isEnemyAllowed;
    [HideInInspector] public bool isPlayerAllowed;


    [Header("Level upgrades")]
    public ExpBarFillComponent gameOverExpBarScript;
    [HideInInspector] public int spentPoints;


    [Header("Essence")]
    public GameObject[] essenceOrbX1Prefab;
    public GameObject[] essenceOrbX2Prefab;
    public GameObject[] essenceOrbX3Prefab;

    public Material essenceGlowMaterial;
    public Material essenceCircleMaterial;
    public Material essenceEnergyMaterial;
    public Material essenceDustMaterial;

    public GameObject essence_x1_prefab;
    public GameObject essence_x2_prefab;
    public GameObject essence_x3_prefab;

    [HideInInspector] public int essenceCounter;
    [HideInInspector] public float essenceLifeTime;


    [Header("Chest")]
    [HideInInspector] public float chestSmallEssenceQuantity;
    [HideInInspector] public float chestNormalEssenceQuantity;
    [HideInInspector] public float chestBigEssenceQuantity;

    [Header("Other")]
    public Texture2D[] auroraEffectMask;
    public Shader auroraShader;
    private int currentExperienceToLevelUp;
    public SpriteRenderer backgroundSprite;
    public Material bgFogMaterial;
    public Material platformMaterial;
    public Material stoneMaterial;
    public Material treeMaterial;
    public Material grassMaterial;
    public Material lanternMaterial;
    public Material lanternBulbMaterial;

   

    public void Awake()
    {
        Application.targetFrameRate = 60;
      
        firebaseManager = FindObjectOfType<FirebaseManager>();
        firebaseManager.PullFreshDataFromDatabase();
        StartCoroutine(firebaseManager.GetBestPlayerGame());
    }

    private void ResetMapBaseValues()
    {
        starRampageCounter = 0;
        starTimer = heroesManager.HeroesStarTimerValue;
        starFinalCounter = 0;
        killFinalCounter = 0;
        scoreFinalCounter = 0;
        timeFinalCounter = 0f;
        towerHightReachedFinalCounter = 0;

        chestSmallEssenceQuantity = 0.3f;
        chestNormalEssenceQuantity = 0.8f;
        chestBigEssenceQuantity = 1.2f;
        essenceLifeTime = 10f;

     //   startingStarTimerValue = 15f;

        currentLevelTextGameViewCanvas.text = "Lvl: " + firebaseManager.GetLevelInt();
        currentExperiencePointsInfoTextGameViewCanvas.text = firebaseManager.GetExperience() + " / " + firebaseManager.GetExperienceToLevelUp() + " EXP";
       
        currentExperiencePointsInfoTextGameOverCanvas.text = firebaseManager.GetExperience() + " / " + firebaseManager.GetExperienceToLevelUp() + " EXP";
        currentLevelTextGameOverCanvas.text = "Lvl: " + firebaseManager.GetLevelInt();

        cameraAndEnemiesTargetPoint.position = heroesManager.HeroSpawnPoint.transform.position;
        heroesManager.heroSmokeEffect.transform.position = cameraAndEnemiesTargetPoint.position;

        highestTowerPointReached.position = cameraAndEnemiesTargetPoint.position;
        highestTowerYaxisValueReached = highestTowerPointReached.position.y;

        rampageEssenceDropBonusX1Value = 70;
        rampageEssenceDropBonusX2Value = 90;
    }

    private void FindInMapObjects()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();
        audioManager = FindObjectOfType<AudioManager>();
        heroesStatisticsCanvasManager = FindObjectOfType<HeroesStatisticsCanvasManager>();
        skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        enemiesManager = FindObjectOfType<EnemiesManager>();
        powerBar = FindObjectOfType<PowerBar>();
        FindObjectOfType<ExpBarFillComponent>().ExpBarRefresh();
    }

    private void SetNormalTimescale() 
    {
        Time.timeScale = 1.0f;
    }

    private void SetPauseTimescale()
    {

        Time.timeScale = 0.0f;
        FindObjectOfType<AudioManager>().StopEveryActiveSoundEffect();
    }


    private void PlayBackgroundAudio()
    {
        audioManager.Play("WindSound");
    }

    private void PlayMusic()
    {
        audioManager.Play("MUSIC");
    }


    public void AllowPlay()
    {
        isEnemyAllowed = true;
        isPlayerAllowed = true;
    }


    void Start()
    {

        FindInMapObjects();
      //  RefreshLvlPointsInfo();
        CheckLvlPoints();
        ExpGain("NaN");
        ResetMapBaseValues();
        PlayBackgroundAudio();
        PlayMusic();
        SetNormalTimescale();
        RefreshStarCounterText();

        currentExperienceToLevelUp = firebaseManager.GetExperienceToLevelUp();
        isStarCollectedSequenceOver = true;

        audioManager.SetSfxVolume(OptionsManager.sfxVolume);
        audioManager.SetMusicVolume(OptionsManager.musicVolume);
    }
  
    private void CalculateCameraMoveBounds()
    {
        if (highestTowerYaxisValueReached < highestTowerPointReached.position.y)
        {
            highestTowerYaxisValueReached = highestTowerPointReached.position.y;
        }

        if (highestTowerPointReached.position.y + 5 < cameraAndEnemiesTargetPoint.position.y)
        {
            highestTowerPointReached.position = new Vector3(cameraAndEnemiesTargetPoint.position.x, cameraAndEnemiesTargetPoint.position.y - 5, highestTowerPointReached.position.z);
        }

      /*  else if (highestTowerYaxisValueReached - 45 < cameraAndEnemiesTargetPoint.position.y)
        {
            highestTowerPointReached.position = new Vector3(cameraAndEnemiesTargetPoint.position.x, cameraAndEnemiesTargetPoint.position.y, highestTowerPointReached.position.z);
        }*/
        else
        {
            highestTowerPointReached.position = new Vector3(cameraAndEnemiesTargetPoint.position.x, cameraAndEnemiesTargetPoint.position.y, cameraAndEnemiesTargetPoint.position.z);
        }
        


    }

    private void CalculateTowerHightReached() 
    {
        if (towerHightReachedFinalCounter < (int)highestTowerPointReached.position.y / 2 - 6)
        {
            towerHightReachedFinalCounter = (int)highestTowerPointReached.position.y / 2 - 6;
            towerHightReachedText.text = towerHightReachedFinalCounter + " m";
            towerHightReachedText.gameObject.GetComponent<Animator>().SetTrigger("textAnimTrigger");
        }
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1.0f)
                  Time.timeScale = 0.0f;
            else
                Time.timeScale = 1.0f;
        }
    }

    public IEnumerator OnStarCollectedSequence1(){

        if(!heroBaseComponent){
              heroBaseComponent = FindObjectOfType<HeroBaseComponent>();
        }

        //Debug.Log("1kurła ileee?");

        starTimer = heroesManager.HeroesStarTimerValue;
        starFinalCounter++;
        starRampageCounter++;
        RefreshStarCounterText();
        starRampageAnimator.SetTrigger("isStarCollected");
        audioManager.Play("StarCollected");
        CollectedStarEffect();
        ExpGain("Star");

        yield return null;
        StartCoroutine(OnStarCollectedSequence2());
       
        //Spawn Of Essence
     /*  
        }*/
        
    } 

    public IEnumerator OnStarCollectedSequence2(){
        heroBaseComponent.PlayerSpriteMaterialColorRefresh();
        heroBaseComponent.PlayerEnergyColorRefresh();
        heroBaseComponent.PlayerOrbCollectedEffectColorRefresh(heroIndexRolledByStar);
        heroBaseComponent.PlayerEnergySizeRefresh(heroIndexRolledByStar - 1);
        heroBaseComponent.EnableCorrectSkillObject();
        heroesManager.EssenceMaterialColorRefresh(heroIndexRolledByStar);
        heroesManager.ChestMaterialsColorRefresh();
      //  yield return null;
        heroesManager.FogMaterialColorRefresh();
        heroesManager.BackgroundSpriteColorRefresh();
       // yield return null;
        heroesManager.EnviromentMaterialsColorRefresh();
        heroesManager.EmitSteamAndEssenceColorRefresh();
       // yield return null;
        heroesManager.PlayerSmokeColorRefresh();
        heroesManager.HeroPowerSetRefresh(0, heroIndexRolledByStar);
        heroesManager.SetPlayerTrailValues(heroIndexRolledByStar - 1);
        
        powerBar.ResetAfterPowerChange();
       // Debug.Log("2kurła ileee?");

        yield return null;
        StartCoroutine(OnStarCollectedSequence3());
    }

    public IEnumerator OnStarCollectedSequence3()
    {
        bool lastEssenceChance = false;
        List<GameObject> essencesToSpawn = new List<GameObject>();
        float starEssenceValue = heroesManager.HeroesStarEssenceValue;


        while (starEssenceValue > 0)
        {
            int whichEssence;

            if (starEssenceValue >= 3)
                whichEssence = UnityEngine.Random.Range(0, 3);
            else if (starEssenceValue >= 2)
                whichEssence = UnityEngine.Random.Range(0, 2);
            else
                whichEssence = 0;

            switch (whichEssence)
            {
                case 0: essencesToSpawn.Add(essence_x1_prefab); starEssenceValue -= 1; break;
                case 1: essencesToSpawn.Add(essence_x2_prefab); starEssenceValue -= 2; break;
                case 2: essencesToSpawn.Add(essence_x3_prefab); starEssenceValue -= 3; break;
            }

          

            if (!lastEssenceChance && starEssenceValue > 0.0f && starEssenceValue < 1.0f)
            {
                float lastEssenceChanceValue = UnityEngine.Random.Range(0.0f, 1.0f);
                if (lastEssenceChanceValue < starEssenceValue)
                {
                    starEssenceValue = 1;
                }
                else
                {
                    starEssenceValue = 0;
                }
                lastEssenceChance = true;
            }
        }        

        yield return null;
        
        foreach(GameObject essence in essencesToSpawn){
            GameObject essenceInstance = Instantiate(essence, starEssenceSpawnPosition, essence.transform.rotation);
           // essence.transform.position = starEssenceSpawnPosition;
            essenceInstance.layer = 0;
            essenceInstance.AddComponent<EssenceOrbDelayed>();
            essenceInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-2.5f, 2.5f), UnityEngine.Random.Range(7f, 12f)), ForceMode2D.Impulse);
            Destroy(essenceInstance, essenceLifeTime);
        }
      //  Debug.Log("4kurła ileee?");
    }   
    

    void FixedUpdate()
    {
        if(!isStarCollectedSequenceOver){
            StartCoroutine(OnStarCollectedSequence1());
            isStarCollectedSequenceOver = true;
        }

        timeFinalCounter += Time.fixedDeltaTime;

        CalculateCameraMoveBounds();
        CalculateTowerHightReached();
        //starTimerTextAnimator

        starTimer -= Time.fixedDeltaTime;

        if (starTimer > 5f)
        {
            starTimerTextAnimator.SetBool("isAlert", false);
            starTimerTextAnimator.SetBool("isNegative", false);
            starTimerText.text = StarRampageTextRefresh(starTimer);
            starPenaltyTimer = 1 + (firebaseManager.GetTimerPenaltyPoints() * 0.02f);
            starPenaltyWeight = 1.0f;

            heroesManager.emittingSteamEffectEmissionModule.rateOverTime = 0;
            heroesManager.emittingEssenceEffectEmissionModule.rateOverTime = 0;

            heroesManager.steamEmissionEffectParticlesCount = 0;
            heroesManager.essenceEmissionEffectParticlesCount = 0;
        }
        else if (starTimer > 0f)
        {
            starTimerTextAnimator.SetBool("isAlert", true);
            starTimerTextAnimator.SetBool("isNegative", false);
            starTimerText.text = StarRampageTextRefresh(starTimer);
            starPenaltyTimer = 1.0f;
            starPenaltyWeight = 1.0f;

            heroesManager.emittingSteamEffectEmissionModule.rateOverTime = 0;
            heroesManager.emittingEssenceEffectEmissionModule.rateOverTime = 0;

            heroesManager.steamEmissionEffectParticlesCount = 0;
            heroesManager.essenceEmissionEffectParticlesCount = 0;
        }
        else // if (starTimer <= 0f)
        {
            starTimerTextAnimator.SetBool("isAlert", false);
            starTimerTextAnimator.SetBool("isNegative", true);
            starTimerText.text = StarRampageTextRefresh(0.0f);

            heroesManager.emittingSteamEffectEmissionModule.rateOverTime = heroesManager.steamEmissionEffectParticlesCount;
            heroesManager.emittingEssenceEffectEmissionModule.rateOverTime = heroesManager.essenceEmissionEffectParticlesCount;

            StarTimerPenalty();

        }
    }

    private void StarTimerPenalty()
    {

        starPenaltyTimer -= Time.fixedDeltaTime;

        if (starPenaltyTimer <= 0.0f)
        {
            heroesManager.HeroPowerSetRefresh(-100, heroIndexRolledByStar);
        //    LootText("-1 Essence", false);
            starPenaltyWeight *= 0.94f;

            starPenaltyTimer = starPenaltyWeight * heroesManager.HeroesTimerPenaltyValue;

            if (starPenaltyTimer <= 0.2f)
                starPenaltyTimer = 0.2f;

            heroesManager.steamEmissionEffectParticlesCount = 100 / starPenaltyTimer;
            heroesManager.essenceEmissionEffectParticlesCount = 30 / starPenaltyTimer;
        }

    }

    public string StarRampageTextRefresh(float time)
    {
       // starRampageText.text = "Rampage: " + starRampageCounter;

        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 100;
        fraction = (fraction % 100);
        string timeText = String.Format("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
        return timeText;
    }


    public void GameOverSecwenction()
    {
       
        canvasGameViewAnimator.SetTrigger("GameOver");
      
        Invoke("GameOver", 2f);
    }

    public void GameOverTextSet()
    {
        string mb;
        string sb;
        
        int minutesBest = (firebaseManager.GetBestPlayTime() / 60);
        if (minutesBest < 10)
            mb = "0" + minutesBest.ToString();
        else
            mb = minutesBest.ToString();

        float secondsBest = ((float)firebaseManager.GetBestPlayTime() % 60);
        if (secondsBest < 10)
            sb = "0" + secondsBest.ToString();
        else sb = secondsBest.ToString();

       StartCoroutine(firebaseManager.GetBestPlayerGame());

        gameOverTextsBest[0].text = firebaseManager.GetBestScore().ToString("0");
        gameOverTextsBest[1].text = firebaseManager.GetBestStars().ToString("0");
        gameOverTextsBest[2].text = firebaseManager.GetBestKills().ToString("0");
        gameOverTextsBest[3].text = mb + " : " + sb;
        gameOverTextsBest[4].text = firebaseManager.GetBestTowerHight().ToString("0");
    }

  
    public virtual void HealthCheck()
    {
         if(heroesManager.HeroesHealth <= 0)  GameOverSecwenction();
              
    }

    private void CalculateEndGAmeScore()
    {
        scoreFinalCounter = scoreFinalCounter + (killFinalCounter * 3) + (towerHightReachedFinalCounter * 2) + (starFinalCounter * 5);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        audioManager.StopEveryActiveSoundEffect();
        CalculateEndGAmeScore();
        firebaseManager.WriteGameData(scoreFinalCounter, starFinalCounter, towerHightReachedFinalCounter, killFinalCounter, (int)Mathf.Round(timeFinalCounter), "Spazz Dream");

        // Time.timeScale = 0;
        heroesManager.SetHeroesGameObjectsInactive();

        GameOverTextSet();
        gameOverCanvas.SetActive(true);
        StartCoroutine(EndGameExp(1f));
    }

    public void AllowPlayerAfterDeath()
    {
        isPlayerAllowed = true;
      //  actualPlayer.GetComponent<Animator>().SetBool("isResurecting", false);

    }

  

    public void SmokeEmissionOnDelay()
    {
        heroesManager.heroSmokeEffectEmissionModule.rateOverDistance = 10f;
    }

     public void PlayerSmokeOnDelay()
     {
        heroesManager.heroSmokeEffect.gameObject.SetActive(true);
     }


  

    /// FUNKCJE SPAWNU NIETOPERZY - WROGOW

    /*
    public void EnemyPortalDeactivation()
    {
        animEnemyPortal1.SetBool("grow", false);
        animEnemyPortal2.SetBool("grow", false);
    }*/


    public void RefreshStarCounterText()
    {
        starsText.text = "x  " + starFinalCounter;
    }


   
    //!!!!!!!!!!!!!!!!!!!!! zmienione stringi w metodzie ze starych na nowe
    public void ExpGain(string who)
    {
        if (who == "EnemyBat")
        {
            firebaseManager.IncreaseExperience(2);
            scoreFinalCounter += 2;
            LootText("+2 Exp", true);
            killFinalCounter += 1;
        }
        else if (who == "EnemyFirefly")
        {
            firebaseManager.IncreaseExperience(1);
            scoreFinalCounter += 1;
            LootText("+1 Exp", true);
            killFinalCounter += 1;

        }
        else if (who == "EnemyJumpingSpider")
        {
            firebaseManager.IncreaseExperience(3);
            scoreFinalCounter += 4;
            LootText("+3 Exp", true);
            killFinalCounter += 1;

        }
        else if (who == "EnemyBigSpider")
        {
            firebaseManager.IncreaseExperience(3);
            scoreFinalCounter += 3;
            LootText("+3 Exp", true);
            killFinalCounter += 1;

        }
        else if (who == "EnemySkeletalDragon")
        {
            firebaseManager.IncreaseExperience(50);
            scoreFinalCounter += 100;
            bossesCounter += 1;
            LootText("+50 Exp", true);
            killFinalCounter += 1;

        }
        else if (who == "Star")
        {
            firebaseManager.IncreaseExperience(5);
            scoreFinalCounter += (starRampageCounter + 5)*5;
            LootText("+5 Exp", true);
          
        }
        else if (who == "EnemyNormalSpider")
        {
            firebaseManager.IncreaseExperience(1);
            LootText("+1 Exp", true);
            scoreFinalCounter += 2;
            killFinalCounter += 1;

        }
     /*   else if (who == "EnemyEgg")
        {
            firebaseManager.IncreaseExperience(1);
            LootText("+1 Exp");
            scoreFinalCounter += 1;
            killFinalCounter += 1;

        }*/
        else if (who == "EnemyBabySpider")
        {
            firebaseManager.IncreaseExperience(1);
            LootText("+1 Exp", true);
            scoreFinalCounter += 1;
            killFinalCounter += 1;

        }
        else if (who == "EnemyShootingSpider")
        {
            firebaseManager.IncreaseExperience(4);
            LootText("+5 Exp", true);
            scoreFinalCounter += 6;
            killFinalCounter += 1;

        }
        else if (who == "EnemyHangingSpider")
        {
            firebaseManager.IncreaseExperience(4);
            LootText("+4 Exp", true);
            scoreFinalCounter += 5;
            killFinalCounter += 1;

        }
        else if (who == "EnemyTentacleWorm")
        {
            firebaseManager.IncreaseExperience(9);
            LootText("+9 Exp", true);
            scoreFinalCounter += 11;
            killFinalCounter += 1;

        }
        //  Debug.Log("currentExperienceToLevelUp: " + currentExperienceToLevelUp);


        FindObjectOfType<ExpBarFillComponent>().ExpBarRefresh();

        if (firebaseManager.GetExperience() >= currentExperienceToLevelUp && currentExperienceToLevelUp > 0)
        {
            ExpBarFillComponent[] NewExpBars = FindObjectsOfType<ExpBarFillComponent>();

            foreach (ExpBarFillComponent bars in NewExpBars)
            {
                bars.isLevelUpAfterEndGameExperienceAdded = true;
            }

        //    Debug.Log("lVL UP FUNCTION...");

            currentLevelTextGameViewCanvas.text = "Lvl: " + firebaseManager.GetLevelInt();
            currentLevelTextGameOverCanvas.text = "Lvl: " + firebaseManager.GetLevelInt();
            currentExperiencePointsInfoTextGameViewCanvas.text = firebaseManager.GetExperience() + " / " + firebaseManager.GetExperienceToLevelUp() + " EXP";
            currentExperiencePointsInfoTextGameOverCanvas.text = firebaseManager.GetExperience() + " / " + firebaseManager.GetExperienceToLevelUp() + " EXP";

            var lvlUp = Instantiate(heroesManager.heroLevelUpEffect, cameraAndEnemiesTargetPoint.position + new Vector3(0, -2, 0), heroesManager.heroLevelUpEffect.transform.rotation);
            lvlUp.transform.parent = cameraAndEnemiesTargetPoint.transform;
            Destroy(lvlUp, 10f);
            audioManager.Play("LevelUp", cameraAndEnemiesTargetPoint.position);

            CheckLvlPoints();

            currentExperienceToLevelUp = firebaseManager.GetExperienceToLevelUp();
        }

    }


    public void CheckLvlPoints()
    {
        availableLevelUpgradePoints.text = "" + firebaseManager.GetAvailablePoints();
        availableLevelUpgradePointsOnGameViewIcon.text = "" + firebaseManager.GetAvailablePoints();
    }


  /*  public void RefreshLvlPointsInfo()
    {
        CheckLvlPoints();

        generalStatsValues[0].text = firebaseManager.GetStrengthPoints().ToString();
        generalStatsValues[1].text = firebaseManager.GetWalkSpeedPoints().ToString();
        generalStatsValues[2].text = firebaseManager.GetJumpPowerPoints().ToString();
     
    }*/

    public void LootText(string zawartosc, bool isPositive)
    {
       

        TextMeshPro instanceOfLootText = null;
        if (isPositive)
        {
            heroesManager.heroLootTextPositive.text = zawartosc;
            instanceOfLootText = Instantiate(heroesManager.heroLootTextPositive, cameraAndEnemiesTargetPoint.position + new Vector3(0, 1.8f, 0f), gameObject.transform.rotation);
        }
        else
        {
            heroesManager.heroLootTextNegative.text = zawartosc;
            instanceOfLootText = Instantiate(heroesManager.heroLootTextNegative, cameraAndEnemiesTargetPoint.position + new Vector3(0, 1.8f, 0f), gameObject.transform.rotation);

        }

        instanceOfLootText.GetComponent<TextMovingUp>().isPositive = isPositive;
        instanceOfLootText.transform.SetParent(cameraAndEnemiesTargetPoint.transform);
    }

    public void CollectedStarEffect()
    {
        var starEffect = Instantiate(StarEffect[heroIndexRolledByStar - 1], cameraAndEnemiesTargetPoint.position + new Vector3(0f, -2.2f, 0f), gameObject.transform.rotation);
        starEffect.transform.parent = cameraAndEnemiesTargetPoint.transform;
        Destroy(starEffect, 2f);

    }


    public void destroyEffectHeroPortal()
    {
        var effect = Instantiate(enemiesManager.PortalExplozion, heroesManager.HeroSpawnPoint.transform.position, enemiesManager.PortalExplozion.transform.rotation);
        Destroy(effect, 15f);
    }

    public void ColorOfMenuIconsAndTablets(int kto)
    {
        //  Debug.Log("Odpalilo przecie...");
        float factor = Mathf.Pow(2, 4f);
        switch (kto)
        {
            case 0:
                for (int i = 0; i < canvasGameViewIcons.Length; i++)
                {
                    canvasGameViewIcons[i].color = new Color(1, 0.24f, 0.24f, 1);
                }


                availableLevelUpgradePointsOnGameViewIcon.fontMaterial = heroesManager.fontHeroMaterials[0];

                canvasScreenTabletMaterial.SetColor("_GlowColor", new Color(1f * factor, 0.356f * factor, 0.388f * factor, 1));

                break;

            case 1:
                for (int i = 0; i < canvasGameViewIcons.Length; i++)
                {
                    canvasGameViewIcons[i].color = new Color(0.93f, 0.26f, 1, 1);
                }
                availableLevelUpgradePointsOnGameViewIcon.fontMaterial = heroesManager.fontHeroMaterials[1];

                canvasScreenTabletMaterial.SetColor("_GlowColor", new Color(0.98f * factor, 0.345f * factor, 1 * factor, 1));

                break;

            case 2:
                for (int i = 0; i < canvasGameViewIcons.Length; i++)
                {
                    canvasGameViewIcons[i].color = new Color(0.285f, 0.79f, 1, 1);
                }
                availableLevelUpgradePointsOnGameViewIcon.fontMaterial = heroesManager.fontHeroMaterials[2];

                canvasScreenTabletMaterial.SetColor("_GlowColor", new Color(0.286f * factor, 0.556f * factor, 0.870f * factor, 1));

                break;

            case 3:
                for (int i = 0; i < canvasGameViewIcons.Length; i++)
                {
                    canvasGameViewIcons[i].color = new Color(0.37f, 0.316f, 0.94f, 1);
                }
                availableLevelUpgradePointsOnGameViewIcon.fontMaterial = heroesManager.fontHeroMaterials[3];

                canvasScreenTabletMaterial.SetColor("_GlowColor", new Color(0.353f * factor, 0.345f * factor, 1 * factor, 1));

                break;

            case 4:
                for (int i = 0; i < canvasGameViewIcons.Length; i++)
                {
                    canvasGameViewIcons[i].color = new Color(0.297f, 1, 0.465f, 1);
                }
                availableLevelUpgradePointsOnGameViewIcon.fontMaterial = heroesManager.fontHeroMaterials[4];

                canvasScreenTabletMaterial.SetColor("_GlowColor", new Color(0.298f * factor, 1f * factor, 0.407f * factor, 1));

                break;


            case 5:
                for (int i = 0; i < canvasGameViewIcons.Length; i++)
                {
                    canvasGameViewIcons[i].color = new Color(0.896f, 0.858f, 0.257f, 1);
                }

                availableLevelUpgradePointsOnGameViewIcon.fontMaterial = heroesManager.fontHeroMaterials[5];

                canvasScreenTabletMaterial.SetColor("_GlowColor", new Color(1f * factor, 0.965f * factor, 0.412f * factor, 1));
                
                break;

        }
    }



   public IEnumerator EndGameExp(float delay)
    {
    
        yield return StartCoroutine(WaitForRealSeconds(delay));

        int tak = firebaseManager.GetExperienceToLevelUp();
        endGameExperiencePoints = 0;
        endGameExperiencePoints = starFinalCounter * 10 + killFinalCounter * 1;
      
        firebaseManager.IncreaseExperience(endGameExperiencePoints);
        endGameExperiencePoints = 0;
        if (tak < firebaseManager.GetExperienceToLevelUp())
        {
            gameOverExpBarScript.isLevelUpAfterEndGameExperienceAdded = true;
        }
        gameOverExpBarScript.timeElapsedForExpTextUpdation = 0.0f;

        gameOverExpBarScript.isEndGameExpCalculated = true;

        ExpGain("NaN");
        
    }

    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    public void SaveData()
    {
        SaveSystem.SaveLevel();
    }

}
