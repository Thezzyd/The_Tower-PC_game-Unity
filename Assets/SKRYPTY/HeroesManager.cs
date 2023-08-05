using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroesManager : MonoBehaviour
{

    [Header("Find other instances ")]
    private FirebaseManager firebaseManager;
    private LevelManager levelManager;
    private SkillsUpgradeManager skillsUpgradeManager;
    private TowerSpawnManager towerSpawnManager;
    private EnemiesManager enemiesManager;

    [Header("General")]
    public GameObject HeroBasePrefab;

    [HideInInspector] public GameObject activeHero;
    public ParticleSystem[] shatredHeroEffect;
    public GameObject spazzShatredEffect;
    public float[] minHeroEnergySize;
    public float[] minHeroEnergyStartingSize;
    public float[] maxHeroEnergySize;
    public float[] maxHeroEnergyStartingSize;
    public int[] heroesEssenceCollected;
    public int[] heroesPowerLevel;
    public Material[] fontHeroMaterials;
    public TextMeshPro heroLootTextPositive;
    public TextMeshPro heroLootTextNegative;
    public ParticleSystem heroSmokeEffect;
    public ParticleSystem.MainModule heroSmokeEffectColorModule;
    public ParticleSystem.EmissionModule heroSmokeEffectEmissionModule;
    public ParticleSystem heroTrailEffect;
    public ParticleSystem.ColorOverLifetimeModule heroTrailEffectGradientModule;
    public GameObject HeroSpawnPoint;
    public GameObject HeroSpawnPortalPrefab;
    public TextMeshProUGUI[] heroCurrentPowerText;
    public TextMeshProUGUI[] heroEssenceToPowerUpText;
    public GameObject[] heroPowerBars;
    public Animator[] heroPowerBarsAnimators;
    public GameObject heroLevelUpEffect;
    [HideInInspector] public bool isHeroDefeated;
    public ParticleSystem jumpingEffect;
    public ParticleSystem runningEffect;
    public ParticleSystem emittingSteamEffect;
    public ParticleSystem.EmissionModule emittingSteamEffectEmissionModule;
    public ParticleSystem.MainModule emittingSteamEffectMainModule;
    public ParticleSystem emittingEssenceEffect;
    public ParticleSystem.EmissionModule emittingEssenceEffectEmissionModule;
    public ParticleSystem.MainModule emittingEssenceEffectMainModule;
    public float steamEmissionEffectParticlesCount;
    public float essenceEmissionEffectParticlesCount;
    public float groundCheckRadius;
    public float groundedSafeDelayTimer;

    public bool isHeroHeadingRight;
    public bool isHeroGrounded;
    public bool isDoubleJump;
    public bool isNormalJump;
  //  public float walkingSoundTimer;
    public float heroFallMultiplier;


    public Gradient heroTrialEffectGradient = new Gradient();
    public GradientColorKey[] heroTrialEffectGradientColorKey = new GradientColorKey[2];
    public GradientAlphaKey[] heroTrialEffectGradientAlphaKey = new GradientAlphaKey[4];
    public ParticleSystem.EmissionModule heroTrailEffectEmissionModule;
    public ParticleSystem.MainModule heroTrailEffectMainModule;
    public float[] heroTrailEffectEmission;
    public float[] heroTrailEffectStartingEmission;
    public float[] heroTrailEffectLifetimeMax;
    public float[] heroTrailEffectStartingLifetimeMax;
    public float[] heroSmokeEffectMaxSize;
    public float[] heroSmokeEffectMaxStartingSize;
    public GameObject HeroEssenceTextPrefab;

    [HideInInspector] public int HeroesHealth = 1;
    [HideInInspector] public float HeroesMaxWalkingSpeed;
//    [HideInInspector] public float HeroesJumpPowerMultiplier;
  //  [HideInInspector] public float HeroesWalkingSpeedMultiplier;
    [HideInInspector] public float HeroesDamageMultiplier;
    [HideInInspector] public float HeroesStarTimerValue;
    [HideInInspector] public float HeroesTimerPenaltyValue;
    [HideInInspector] public float HeroesStarEssenceValue;
    [HideInInspector] public float HeroesActiveWalkSpeedValue;
    [HideInInspector] public float HeroesActiveJumpPowerValue;
    [HideInInspector] public bool isHeroAlive;


    [Header("Hero PowerBar")]
    public GameObject heroPowerBar;
    public Animator heroPowerBarAnimator;
    public TextMeshProUGUI heroPowerTierInfoText;
    public TextMeshProUGUI heroPowerEssenceInfoText;
    public GameObject essenceParticleEffect;
    public ParticleSystem progressionEffectOnBar;
    public ParticleSystem.MainModule progressionEffectMainModuleOnBar;
    public Material[] heroPowerBarTextMaterials;
    private int heroPowerActive;
    private PowerBar heroPowerBarInstance;


    [Header("Hero 1")]
    public float Hero1LaserDamageValue;
    public float Hero1LaserDamageStartingValue;
    public float Hero1LaserKnockbackValue;
    public float Hero1LaserKnockbackStartingValue;
    public float Hero1maxChainsNumber;
    public float Hero1maxChainsStartingNumber;
    public List<ParticleSystem> Hero1chainHitEffect;
    public ParticleSystem Hero1chainHitEffectPrefab;
    public float Hero1chainEfficiency;
    public float Hero1chainStartingEfficiency;
    public float Hero1chainRadius;
    public float Hero1chainStartingRadius;


    [Header("Hero 2")]
    public GameObject Hero2SlashLeftPrefab;
    public GameObject Hero2SlashRightPrefab;
    public GameObject critTextPrefabHERO2;
    public float Hero2WeaponCriticalStrikeChanceValue;
    public float Hero2WeaponCriticalStrikeChanceStartingValue;
    public float Hero2WeaponCriticalStrikeMultiplierValue;
    public float Hero2WeaponCriticalStrikeMultiplierStartingValue;
    public float Hero2WeaponDamageValue;
    public float Hero2WeaponDamageStartingValue;
    public float Hero2AttackSpeedValue;
    public float Hero2AttackSpeedStartingValue;
    public float Hero2WeaponKnockbackValue;
    public float Hero2WeaponKnockbackStartingValue;


    [Header("Hero 3")]
    //public GameObject Hero3TurretProjectilePrefab;
    //public GameObject Hero3TurretPrefab;
    //public List<GameObject> Hero3ActiveTurrets;
    //public float Hero3TurretAttackSpeedValue;
    //public float Hero3TurretAttackSpeedStartingValue;
    //public float Hero3TurretLifeTimeValue;
    //public float Hero3TurretLifeTimeStartingValue;
    //public int Hero3TurretMaxQuantityValue;
    //public int Hero3TurretMaxQuantityStartingValue;
    //public float Hero3TurretDamageStartingValue;
    //public float Hero3TurretProjectileSizeValue;
    //public float Hero3TurretProjectileSizeStartingValue;
    //public float Hero3TurretProjectileSpeed;
    //public float Hero3TurretProjectileStartingSpeed;
    //public float Hero3TurretProjectileLifetime;
    //public float Hero3TurretProjectileStartingLifetime;
    public float Hero3AttackSpeedValue;
    public float Hero3StartingAttackSpeedValue;
    public GameObject Hero3ProjectilePrefab;
    public GameObject Hero3ProjectileCrashEffectPrefab;
    //public float Hero3ProjectileSizeValue;
    public float Hero3ProjectileDamageValue;
    public float Hero3ProjectileStartingDamageValue;
    public float Hero3ProjectileSpeedValue;
    public float Hero3ProjectileStartingSpeedValue;
    public int Hero3BarrageQuantity;
    public int Hero3StartingBarrageQuantity;
    

    [Header("Hero 4")]
    public ParticleSystem Hero4Particle;
    public float Hero4BeamDamageValue;
    public float Hero4BeamDamageStartingValue;
    public float Hero4BeamLengthValue;
    public float Hero4BeamLengthStartingValue;
    public float Hero4BeamSizeValue;
    public float Hero4BeamSizeStartingValue;
    public float Hero4BeamTrailSizeValue;
    public float Hero4BeamTrailSizeStartingValue;
    public float Hero4BeamQuantityValue;
    public float Hero4BeamQuantityStartingValue;
    public bool isHero4TendrilsClipReseted;


    [Header("Hero 5")]
    public GameObject Heero5ProjectilesRightPrefab;
    public GameObject Hero5ProjectilesLeftPrefab;
    public int Hero5ProjectileQuantityValue;
    public int Hero5ProjectileQuantityStartingValue;
    public float Hero5ProjectileLifeTimeMaxValue;
    public float Hero5ProjectileLifeTimeMaxStartingValue;
    public float Hero5ProjectileLifeTimeMinValue;
    public float Hero5ProjectileLifeTimeMinStartingValue;
    public float Hero5ProjectileSpeedMaxValue;
    public float Hero5ProjectileSpeedMaxStartingValue;
    public float Hero5ProjectileSpeedMinValue;
    public float Hero5ProjectileSpeedMinStartingValue;
    public float Hero5ProjectileDamageValue;
    public float Hero5ProjectileDamageStartingValue;
    public float Hero5AttackSpeedValue;
    public float Hero5AttackSpeedStartingValue;

    [Header("Hero 6")]
    public GameObject Hero6ProjectilePrefab;
    public float Hero6ProjectileLifetime;
    public float Hero6ProjectileStartingLifetime;
    public float Hero6ProjectileDamageValue;
    public float Hero6ProjectileDamageStartingValue;
    public float Hero6AttackSpeedValue;
    public float Hero6AttackSpeedStartingValue;
    public float Hero6ProjectileSpeedValue;
    public float Hero6ProjectileSpeedStartingValue;
    public float Hero6ProjectileSizeValue;
    public float Hero6ProjectileSizeStartingValue;


    void Start()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();
        levelManager = FindObjectOfType<LevelManager>();
        skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
        enemiesManager = FindObjectOfType<EnemiesManager>();
        heroPowerBarInstance = heroPowerBar.GetComponentInChildren<PowerBar>();

        steamEmissionEffectParticlesCount = 0;
        essenceEmissionEffectParticlesCount = 0;

        emittingSteamEffectEmissionModule = emittingSteamEffect.emission;
        emittingEssenceEffectEmissionModule = emittingEssenceEffect.emission;

        emittingSteamEffectMainModule = emittingSteamEffect.main;
        emittingEssenceEffectMainModule = emittingEssenceEffect.main;



        AsignLvlPointsOnStart();
        SetStartingValuesGeneral();
        SetStartingValuesHero1();
        SetStartingValuesHero2();
        SetStartingValuesHero3();
        SetStartingValuesHero4();
        SetStartingValuesHero5();
        SetStartingValuesHero6();
        HeroParticleSystemEffectsSetup();
        SpawnHero();
       
   }

    private void Update()
    {


        if (groundedSafeDelayTimer <= 0.0f && !isHeroGrounded)
        {
            isNormalJump = true;
            groundedSafeDelayTimer -= Time.deltaTime;
        }
        else if (!isHeroGrounded)
        {
            groundedSafeDelayTimer -= Time.deltaTime;

        }
        else if (groundedSafeDelayTimer <= 0.0f && isHeroGrounded)
        {
            isNormalJump = false;
            groundedSafeDelayTimer = 0.2f;
        }

         
      
    }


    private void SetStartingValuesGeneral()
    {
          
        progressionEffectMainModuleOnBar = progressionEffectOnBar.main;

     
     //   HeroesJumpPowerMultiplier = 1f;
       // HeroesWalkingSpeedMultiplier = 1f;
     //   HeroesDamageMultiplier = 1f;

      //  HeroesStarTimerValue = 20f;

        isHeroAlive = true;
        heroSmokeEffect.gameObject.SetActive(false);
        heroTrailEffect.gameObject.SetActive(false);

        HeroBasePrefab.SetActive(true);

        jumpingEffect = GameObject.FindGameObjectWithTag("JumpingEffect").GetComponent<ParticleSystem>();
        runningEffect = GameObject.FindGameObjectWithTag("RunningEffect").GetComponent<ParticleSystem>();

        isHeroHeadingRight = false;
        isDoubleJump = false;
    //    walkingSoundTimer = 0.2f;
        heroFallMultiplier = 9f;
        Hero6ProjectileLifetime = 20f;
        //Hero3TurretProjectileSpeed = 200f;

        groundCheckRadius = 0.3f;
        groundedSafeDelayTimer = 0.25f;

       // heroesPowerLevel = new int[minHeroEnergySize.Length];

        minHeroEnergyStartingSize = new float[minHeroEnergySize.Length];
        maxHeroEnergyStartingSize = new float[maxHeroEnergySize.Length];

        heroTrailEffectStartingEmission = new float[heroTrailEffectEmission.Length];
        heroTrailEffectStartingLifetimeMax = new float[heroTrailEffectLifetimeMax.Length];

        heroSmokeEffectMaxStartingSize = new float[heroSmokeEffectMaxSize.Length];

        for (int i = 0; i < minHeroEnergySize.Length; i++)
        {
          //  heroesPowerLevel[i] = 0;

            minHeroEnergyStartingSize[i] = minHeroEnergySize[i];
            maxHeroEnergyStartingSize[i] = maxHeroEnergySize[i];
            heroTrailEffectStartingEmission[i] = heroTrailEffectEmission[i];
            heroTrailEffectStartingLifetimeMax[i] = heroTrailEffectLifetimeMax[i];
            heroSmokeEffectMaxStartingSize[i] = heroSmokeEffectMaxSize[i];
        }

    }

    public void SetCameraAndEnemyTargetPoint(Transform heroPositionPoint)
    {
        levelManager.cameraAndEnemiesTargetPoint.position = heroPositionPoint.position + new Vector3(0, 2.2f, 0);
    }



    public void SetPlayerBehaviourAndEffects(Transform heroTransform, Rigidbody2D heroRigidbody, Animator heroAnimator)
    {
        jumpingEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0, -2.2f, 0);
        runningEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0, -2.2f, 0);
        heroSmokeEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0, -0.2f, 0);
        emittingEssenceEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0, -0.2f, 0);
        emittingSteamEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0, -0.2f, 0);

        if (heroRigidbody.velocity.x > 2.0f)
        {
            heroTransform.localScale = new Vector3(0.6f, 0.6f, 1);
            isHeroHeadingRight = true;
            heroTrailEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0.3f, -0.15f, 0);

            if (isHeroGrounded)
                runningEffect.Play();

        }
        else if (heroRigidbody.velocity.x < -2.00f)
        {
            heroTransform.localScale = new Vector3(-0.6f, 0.6f, 1);

            isHeroHeadingRight = false;
            heroTrailEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(-0.4f, -0.15f, 0);
           
            if (isHeroGrounded)
                runningEffect.Play();

        }
        else
        {
            runningEffect.Stop();

            if (isHeroHeadingRight)
            {
                heroTrailEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(-0.18f, -0.15f, 0);
            }
            else
            {
                heroTrailEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0.18f, -0.15f, 0);
            }
        }

        if (heroRigidbody.velocity.y < 0)
        {
            heroRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (heroFallMultiplier - 1) * Time.deltaTime;
            heroAnimator.SetBool("IsFalling", true);
            heroAnimator.SetBool("IsJumping", false);
        }
        else
        {
            heroAnimator.SetBool("IsFalling", false);
        }

        if (isHeroGrounded)
        {
            isDoubleJump = false;
      
        }
        else runningEffect.Stop();

        heroAnimator.SetBool("Grounded", isHeroGrounded);
        heroAnimator.SetFloat("Speed", Mathf.Abs(heroRigidbody.velocity.x));
        heroAnimator.SetFloat("RunningAnimSpeed", Mathf.Abs(heroRigidbody.velocity.x) / 12.0f);
    }

    public void SetHeroesGameObjectsInactive()
    {
        HeroBasePrefab.SetActive(false);
    }

    private void SetStartingValuesHero1()
    {

        Hero1chainHitEffect = new List<ParticleSystem>();

        Hero1LaserDamageValue = 1f;
        Hero1LaserKnockbackValue = 0f;
        Hero1maxChainsNumber = 0;
        Hero1chainEfficiency = 0.35f; // max = 1
        Hero1chainRadius = 2.5f;

        Hero1LaserDamageStartingValue = Hero1LaserDamageValue;
        Hero1LaserKnockbackStartingValue = Hero1LaserKnockbackValue;
        Hero1maxChainsStartingNumber = Hero1maxChainsNumber;
        Hero1chainStartingEfficiency = Hero1chainEfficiency; 
        Hero1chainStartingRadius = Hero1chainRadius;

    }

    private void SetStartingValuesHero2()
    {
        Hero2WeaponCriticalStrikeChanceValue = 0f;
        Hero2WeaponCriticalStrikeMultiplierValue = 0.05f;
        Hero2AttackSpeedValue = 0.75f;
        Hero2WeaponDamageValue = 10f;
        Hero2WeaponKnockbackValue = 0f;

        Hero2WeaponCriticalStrikeChanceStartingValue = Hero2WeaponCriticalStrikeChanceValue;
        Hero2WeaponCriticalStrikeMultiplierStartingValue = Hero2WeaponCriticalStrikeMultiplierValue;
        Hero2AttackSpeedStartingValue = Hero2AttackSpeedValue;
        Hero2WeaponDamageStartingValue = Hero2WeaponDamageValue;
        Hero2WeaponKnockbackStartingValue = Hero2WeaponKnockbackValue;
    }

    private void SetStartingValuesHero3()
    {
        /*Hero3TurretAttackSpeedValue = 0.5f;
        Hero3TurretLifeTimeValue = 10f;
        Hero3TurretMaxQuantityValue = 1;
        Hero3ActiveTurrets = new List<GameObject>();
        Hero3ProjectileDamageValue = 4f;
        Hero3TurretProjectilePrefab.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
        Hero3TurretProjectileSizeValue = 0.75f;
        Hero3TurretProjectileLifetime = 2.0f;

        Hero3TurretAttackSpeedStartingValue = Hero3TurretAttackSpeedValue;
        Hero3TurretLifeTimeStartingValue = Hero3TurretLifeTimeValue;
        Hero3TurretMaxQuantityStartingValue = Hero3TurretMaxQuantityValue;
        Hero3TurretDamageStartingValue = Hero3ProjectileDamageValue;
        Hero3TurretProjectileSizeStartingValue = Hero3TurretProjectileSizeValue;
        Hero3TurretProjectileStartingLifetime = Hero3TurretProjectileLifetime;*/
   /* public float Hero3AttackSpeedValue;
    public float Hero3StartingAttackSpeedValue;
    public GameObject Hero3ProjectilePrefab;
    public GameObject Hero3ProjectileCrashEffectPrefab;
    //public float Hero3ProjectileSizeValue;
    public float Hero3ProjectileDamageValue;
    public float Hero3ProjectileStartingDamageValue;
    public float Hero3ProjectileSpeedValue;
    public float Hero3ProjectileStartingSpeedValue;
    public int Hero3BarrageQuantity;*/

        Hero3ProjectileDamageValue = 6f;
        Hero3ProjectileSpeedValue = 20f;
        Hero3AttackSpeedValue = 1f;
        Hero3BarrageQuantity = 3;

        Hero3ProjectileStartingDamageValue = Hero3ProjectileDamageValue;
        Hero3ProjectileStartingSpeedValue = Hero3ProjectileSpeedValue;
        Hero3StartingAttackSpeedValue = Hero3AttackSpeedValue;
        Hero3StartingBarrageQuantity = Hero3BarrageQuantity;

    }

    private void SetStartingValuesHero4()
    {
        Hero4BeamDamageValue = 0.55f;
        Hero4BeamLengthValue = 0f;
        Hero4BeamSizeValue = 0.3f;
        Hero4BeamTrailSizeValue = 0.25f;
        Hero4BeamQuantityValue = 50f;
   //     Hero4BeamQuantityTransitionValue = 1;

        Hero4BeamDamageStartingValue = Hero4BeamDamageValue;
        Hero4BeamLengthStartingValue = Hero4BeamLengthValue;
        Hero4BeamSizeStartingValue = Hero4BeamSizeValue;
        Hero4BeamTrailSizeStartingValue = Hero4BeamTrailSizeValue;
        Hero4BeamQuantityStartingValue = Hero4BeamQuantityValue;
     //   Hero4BeamQuantityTransitionStartingValue = Hero4BeamQuantityTransitionValue;
    }

    private void SetStartingValuesHero5()
    {
        Hero5ProjectileQuantityValue = 10;
        Hero5ProjectileLifeTimeMaxValue = 0.1f;
        Hero5ProjectileLifeTimeMinValue = 0.3f;
        Hero5ProjectileSpeedMaxValue = 30.0f;
        Hero5ProjectileSpeedMinValue = 5.0f;
        Hero5ProjectileDamageValue = 6f;
        Hero5AttackSpeedValue = 1f;

        Hero5ProjectileQuantityStartingValue = Hero5ProjectileQuantityValue;
        Hero5ProjectileLifeTimeMaxStartingValue = Hero5ProjectileLifeTimeMaxValue;
        Hero5ProjectileLifeTimeMinStartingValue = Hero5ProjectileLifeTimeMinValue;
        Hero5ProjectileSpeedMaxStartingValue = Hero5ProjectileSpeedMaxValue;
        Hero5ProjectileSpeedMinStartingValue = Hero5ProjectileSpeedMinValue;
        Hero5ProjectileDamageStartingValue = Hero5ProjectileDamageValue;
        Hero5AttackSpeedStartingValue = Hero5AttackSpeedValue;
    }

    private void SetStartingValuesHero6()
    {
        Hero6ProjectileDamageValue = 7f;
        Hero6AttackSpeedValue = 0.8f;
        Hero6ProjectileSpeedValue = 30f;
        Hero6ProjectileSizeValue = 1f;

        Hero6ProjectileDamageStartingValue = Hero6ProjectileDamageValue;
        Hero6AttackSpeedStartingValue = Hero6AttackSpeedValue;
        Hero6ProjectileSpeedStartingValue = Hero6ProjectileSpeedValue;
        Hero6ProjectileSizeStartingValue = Hero6ProjectileSizeValue;
    }

    public void AsignLvlPointsOnStart()
    {
        HeroesMaxWalkingSpeed = 22f;

        HeroesDamageMultiplier = 1f + (firebaseManager.GetStrengthPoints() * 0.005f);
        HeroesActiveWalkSpeedValue = 12f * (1 + (firebaseManager.GetWalkSpeedPoints() * 0.005f));
        HeroesActiveJumpPowerValue = 30f;// + (firebaseManager.GetJumpPowerPoints() * 0.005f);
        HeroesStarTimerValue = 20f + (firebaseManager.GetStarTimerPoints() * 0.1f);
        HeroesTimerPenaltyValue = 1f + (firebaseManager.GetTimerPenaltyPoints() * 0.02f);
        HeroesStarEssenceValue = 8f * (1f + (firebaseManager.GetStarEssencePoints()) * 0.01f);

        levelManager.generalStatsValues[0].text = firebaseManager.GetStrengthPoints().ToString();
        levelManager.generalStatsValues[1].text = firebaseManager.GetWalkSpeedPoints().ToString();
        levelManager.generalStatsValues[2].text = firebaseManager.GetStarEssencePoints().ToString();
        levelManager.generalStatsValues[3].text = firebaseManager.GetStarTimerPoints().ToString();
        levelManager.generalStatsValues[4].text = firebaseManager.GetTimerPenaltyPoints().ToString();

        levelManager.upgradeRealValuesText[0].text = "+ "+ (firebaseManager.GetStrengthPoints() * 0.005f * 100) + "%";
        levelManager.upgradeRealValuesText[1].text = "+ "+ (firebaseManager.GetWalkSpeedPoints() * 0.005f * 100) + "%";
        levelManager.upgradeRealValuesText[2].text = "+ "+ (firebaseManager.GetStarEssencePoints() * 0.01f * 100) + "%";
        levelManager.upgradeRealValuesText[3].text = "+ "+ (firebaseManager.GetStarTimerPoints() * 0.1f) + "s";
        levelManager.upgradeRealValuesText[4].text = "+ "+ (firebaseManager.GetTimerPenaltyPoints() * 0.02f * 100) + "%";

        levelManager.availableLevelUpgradePoints.text = "" + firebaseManager.GetAvailablePoints();
        levelManager.availableLevelUpgradePointsOnGameViewIcon.text = "" + firebaseManager.GetAvailablePoints();
    }

    private IEnumerator ShowHero(float timeToPass)
    {
        yield return new WaitForSecondsRealtime(timeToPass);

        levelManager.heroIndexRolledByStar = 3;

        EssenceMaterialColorRefresh(levelManager.heroIndexRolledByStar);
        ChestMaterialsColorRefresh();
        FogMaterialColorRefresh();
        BackgroundSpriteColorRefresh();
        EnviromentMaterialsColorRefresh();
        EmitSteamAndEssenceColorRefresh();

        Instantiate(HeroBasePrefab, HeroSpawnPoint.transform.position + new Vector3(0, -1.5f, 0), gameObject.transform.rotation);
     
        heroSmokeEffect.gameObject.SetActive(true);
        heroTrailEffect.gameObject.SetActive(true);

        PlayerSmokeColorRefresh();
     
        HeroPowerSetRefresh(0, levelManager.heroIndexRolledByStar);

        levelManager.AllowPlay();
    }

    private void SpawnHero()
    {
        var HeroPortal = Instantiate(HeroSpawnPortalPrefab, HeroSpawnPoint.transform.position, HeroSpawnPoint.transform.rotation);
        StartCoroutine(ShowHero(0f));
        Destroy(HeroPortal, 4f);
    }


    private void HeroParticleSystemEffectsSetup()
    {
        heroTrailEffectEmissionModule = heroTrailEffect.emission;
        heroTrailEffectMainModule = heroTrailEffect.main;
        heroTrialEffectGradientColorKey[0].color = Color.white;
        heroTrialEffectGradientColorKey[0].time = 0.0F;

        heroTrialEffectGradientAlphaKey[0].time = 0.0F;
        heroTrialEffectGradientAlphaKey[0].alpha = 1.0F;

        heroTrialEffectGradientAlphaKey[1].time = 0.36F;
        heroTrialEffectGradientAlphaKey[1].alpha = 1.0F;

        heroTrialEffectGradientAlphaKey[2].time = 0.6F;
        heroTrialEffectGradientAlphaKey[2].alpha = 0.23F;

        heroTrialEffectGradientAlphaKey[3].time = 1.0F;
        heroTrialEffectGradientAlphaKey[3].alpha = 0.0F;
        heroSmokeEffectEmissionModule = heroSmokeEffect.emission;
    }

   

    public void PlayerSmokeColorRefresh()
    {
        heroSmokeEffectColorModule = heroSmokeEffect.main;
        heroTrailEffectGradientModule = heroTrailEffect.colorOverLifetime;
        //  powerBarSmokeEffectMainModule = powerBarSmokeEffect.main;

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1:
                heroSmokeEffectColorModule.startColor = new Color(1, 0, 0.08f, 0.08f);
                heroTrialEffectGradientColorKey[1].color = new Color(0.9f, 0, 0.2f);
                heroTrialEffectGradientColorKey[1].time = 0.53f;

                heroTrialEffectGradient.SetKeys(heroTrialEffectGradientColorKey, heroTrialEffectGradientAlphaKey);

                heroTrailEffectGradientModule.color = heroTrialEffectGradient;

                break;
            case 2:
                heroSmokeEffectColorModule.startColor = new Color(1, 0, 0.9f, 0.08f);
                heroTrialEffectGradientColorKey[1].color = new Color(0.68f, 0, 0.8f);
                heroTrialEffectGradientColorKey[1].time = 0.53f;

                heroTrialEffectGradient.SetKeys(heroTrialEffectGradientColorKey, heroTrialEffectGradientAlphaKey);

                heroTrailEffectGradientModule.color = heroTrialEffectGradient;

                break;
            case 3:
                heroSmokeEffectColorModule.startColor = new Color(0.13f, 0.71f, 1, 0.08f);
                heroTrialEffectGradientColorKey[1].color = new Color(0.0f, 0.35f, 1.0f);
                heroTrialEffectGradientColorKey[1].time = 0.53f;

                heroTrialEffectGradient.SetKeys(heroTrialEffectGradientColorKey, heroTrialEffectGradientAlphaKey);

                heroTrailEffectGradientModule.color = heroTrialEffectGradient;

                break;
            case 4:
                heroSmokeEffectColorModule.startColor = new Color(0, 0.1f, 1, 0.08f);
                heroTrialEffectGradientColorKey[1].color = new Color(0.22f, 0, 0.87f);
                heroTrialEffectGradientColorKey[1].time = 0.53f;

                heroTrialEffectGradient.SetKeys(heroTrialEffectGradientColorKey, heroTrialEffectGradientAlphaKey);

                heroTrailEffectGradientModule.color = heroTrialEffectGradient;

                break;
            case 5:
                heroSmokeEffectColorModule.startColor = new Color(0.01f, 0.72f, 0, 0.08f);
                heroTrialEffectGradientColorKey[1].color = new Color(0.0f, 0.87f, 0.22f);
                heroTrialEffectGradientColorKey[1].time = 0.53f;

                heroTrialEffectGradient.SetKeys(heroTrialEffectGradientColorKey, heroTrialEffectGradientAlphaKey);

                heroTrailEffectGradientModule.color = heroTrialEffectGradient;

                break;
            case 6:
                heroSmokeEffectColorModule.startColor = new Color(0.73f, 0.73f, 0, 0.08f);
                heroTrialEffectGradientColorKey[1].color = new Color(1.0f, 0.92f, 0.0f);
                heroTrialEffectGradientColorKey[1].time = 0.53f;

                heroTrialEffectGradient.SetKeys(heroTrialEffectGradientColorKey, heroTrialEffectGradientAlphaKey);

                heroTrailEffectGradientModule.color = heroTrialEffectGradient;

                break;
        }
    }

    /*
    public void PowerBarReload()
    {

        heroPowerBars[0].SetActive(false);
        heroPowerBars[1].SetActive(false);
        heroPowerBars[2].SetActive(false);
        heroPowerBars[3].SetActive(false);
        heroPowerBars[4].SetActive(false);
        heroPowerBars[5].SetActive(false);

    }*/
    /* public GameObject heroPowerBar;
    public Animator heroPowerBarAnimator;
    public TextMeshProUGUI heroPowerTierInfoText;
    public TextMeshProUGUI heroPowerEssenceInfoText;
    public GameObject essenceParticleEffect;
    public ParticleSystem progressionEffectOnBar;
    public Material[] heroPowerBarTextMaterials;*/

    public void HeroPowerBarRefresh(int whichHero)
    {

        if (heroPowerActive != levelManager.heroIndexRolledByStar || heroPowerActive == 0)
        {
            heroPowerBarAnimator.SetTrigger("barChange");
            heroPowerActive = levelManager.heroIndexRolledByStar;
        }

        heroPowerTierInfoText.fontMaterial = heroPowerBarTextMaterials[whichHero];
        heroPowerEssenceInfoText.fontMaterial = heroPowerBarTextMaterials[whichHero];
        levelManager.towerHightReachedText.fontMaterial = heroPowerBarTextMaterials[whichHero];
        levelManager.towerHightConstText.fontMaterial = heroPowerBarTextMaterials[whichHero];

        switch (whichHero)
        {
            case 0: progressionEffectMainModuleOnBar.startColor = new Color(1, 0.5411f, 0.5815f, 1); break;
            case 1: progressionEffectMainModuleOnBar.startColor = new Color(1, 0.5411f, 1, 1); break;
            case 2: progressionEffectMainModuleOnBar.startColor = new Color(0.5411f, 0.7185f, 1, 1); break;
            case 3: progressionEffectMainModuleOnBar.startColor = new Color(0.5111f, 0.4764f, 1, 1); break;
            case 4: progressionEffectMainModuleOnBar.startColor = new Color(0.4952f, 1, 0.5205f, 1); break;
            case 5: progressionEffectMainModuleOnBar.startColor = new Color(1, 1, 0.4941f, 1); break;
        }

        heroPowerEssenceInfoText.text = " " + heroesEssenceCollected[whichHero] + "  I  " + skillsUpgradeManager.powerUpProg[whichHero];
        heroPowerTierInfoText.text = "Power tier: " + heroesPowerLevel[whichHero];

    }


    public void HeroPowerSetRefresh(int ammountOfEssence, int whichHero)
    {
      
        whichHero -= 1;
        //  heroPowerBars[levelManager.heroIndexRolledByStar - 1].SetActive(true);

        if (ammountOfEssence >= 0)
        {
            heroPowerBarInstance.isIncreasing = true;
        }
        else
        {
            heroPowerBarInstance.isIncreasing = false;

        }

        levelManager.ColorOfMenuIconsAndTablets(levelManager.heroIndexRolledByStar - 1);

       // Debug.Log("whichHero: "+ whichHero);
        heroesEssenceCollected[whichHero] += ammountOfEssence;

        if (heroesPowerLevel[whichHero] <= 0 && heroesEssenceCollected[whichHero] < 0)
        {
            heroesEssenceCollected[whichHero] = 0;
            if (isHeroAlive)
            {
                FindObjectOfType<HeroKillTrigger>().PlayerDeath();
            }
        }


        if (heroesEssenceCollected[whichHero] >= skillsUpgradeManager.powerUpProg[whichHero])
        {
            heroPowerBar.GetComponentInChildren<PowerBar>().levelManager = FindObjectOfType<LevelManager>();
            //  if (heroPowerBars[levelManager.heroIndexRolledByStar - 1].activeSelf)
            // {
           // heroPowerBarInstance.ResetAfterPowerUp();

            heroPowerBarInstance.lvlUpDelay = true;
            //  GameObject.FindGameObjectWithTag("PowerBar").GetComponentInChildren<PowerBar>().lvlUpDelay = true;
            heroPowerBarInstance.targetScale = 1;

            //   var efect = Instantiate(skillsUpgradeManager.powerUpEffect[whichHero], skillsUpgradeManager.powerUpEffectPoint.position, skillsUpgradeManager.powerUpEffectPoint.rotation);
            //   efect.transform.SetParent(skillsUpgradeManager.powerUpEffectPoint.transform);

            //   Destroy(efect, 3f);

            //   efect.transform.localScale = new Vector3(1, 1, 1);
            // }

            heroesEssenceCollected[whichHero] = 0;
            heroesPowerLevel[whichHero] += 1;
            skillsUpgradeManager.powerUpProg[whichHero] += 500;

            var efect2 = Instantiate(skillsUpgradeManager.powerUpHeroEffect[whichHero], levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0, -2.2f, 0), skillsUpgradeManager.powerUpHeroEffect[whichHero].transform.rotation);
            efect2.transform.SetParent(levelManager.cameraAndEnemiesTargetPoint.transform);
            Destroy(efect2, 3f);

           // HeroEnergySizeRefresh(whichHero);
            PlayerSmokeColorRefresh();

            HeroTrialEffectEmissionRefresh(whichHero);
            SetPlayerTrailValues(whichHero);

            HeroSmokeEffectSizeRefresh(whichHero);

            skillsUpgradeManager.RefreshHeroPowerStatistics(whichHero);
        }
        else if (heroesEssenceCollected[whichHero] < 0 && heroesPowerLevel[whichHero] > 0)
        {
            heroesPowerLevel[whichHero] -= 1;
            skillsUpgradeManager.powerUpProg[whichHero] -= 500;
            heroesEssenceCollected[whichHero] = skillsUpgradeManager.powerUpProg[whichHero] + ammountOfEssence;
            //heroPowerBarInstance.lvlUpDelay = true;
            //HeroEnergySizeRefresh(whichHero);
            heroPowerBarInstance.ResetAfterPowerDown();
            PlayerSmokeColorRefresh();
           // heroPowerBarInstance.lvlUpDelay = true;
            HeroTrialEffectEmissionRefresh(whichHero);
            SetPlayerTrailValues(whichHero);

            HeroSmokeEffectSizeRefresh(whichHero);

            skillsUpgradeManager.RefreshHeroPowerStatistics(whichHero);
        }
      

        HeroPowerBarRefresh(whichHero);

    }

    public void HeroEnergySizeRefresh(int whichHero)
    {
        //minHeroEnergySize[whichHero] = minHeroEnergyStartingSize[whichHero] * (1f + (heroesPowerLevel[whichHero] * 0.02f));
        //maxHeroEnergySize[whichHero] = maxHeroEnergyStartingSize[whichHero] * (1f + (heroesPowerLevel[whichHero] * 0.02f));
    }

    public void HeroTrialEffectEmissionRefresh(int whichHero)
    {
        heroTrailEffectLifetimeMax[whichHero] = heroTrailEffectStartingLifetimeMax[whichHero] + (heroesPowerLevel[whichHero] * 0.03f);
        heroTrailEffectEmission[whichHero] = heroTrailEffectStartingEmission[whichHero] + (heroesPowerLevel[whichHero] * 0.1f);
    }

    public void HeroSmokeEffectSizeRefresh(int whichHero)
    {
        heroSmokeEffectMaxSize[whichHero] = heroSmokeEffectMaxStartingSize[whichHero] + (heroesPowerLevel[whichHero] * 0.1f);
    }

  /*  public void HeroInfoPanelPowerSetRefresh(int ammountOfEssence, int whichHero)
    {
       
       // heroPowerBars[whichHero].SetActive(true);
        levelManager.ColorOfMenuIconsAndTablets(whichHero);
        heroPowerBar.GetComponentInChildren<PowerBar>().levelManager = FindObjectOfType<LevelManager>();

        //   heroPowerBars[whichHero].GetComponentInChildren<PowerBar>().levelManager = FindObjectOfType<LevelManager>();
        heroesEssenceCollected[whichHero] += ammountOfEssence;

        heroEssenceToPowerUpText[whichHero].text = " " + heroesEssenceCollected[whichHero] + "  I  " + skillsUpgradeManager.powerUpProg[whichHero];
        heroCurrentPowerText[whichHero].text = "HERO POWER " + skillsUpgradeManager.heroesPowerLvl[whichHero];

    }*/

    public void SetPlayerTrailValues(int wchihHero)
    {
        heroTrailEffectEmissionModule.rateOverTime = heroTrailEffectEmission[wchihHero];
        heroTrailEffectMainModule.startLifetime = new ParticleSystem.MinMaxCurve(0.2f, heroTrailEffectLifetimeMax[wchihHero]);
        heroSmokeEffectColorModule.startSize = new ParticleSystem.MinMaxCurve(0.5f, heroSmokeEffectMaxSize[wchihHero]);
    }

    public void HeroJump(Rigidbody2D heroRigidbody, Animator heroAnimator)
    {
        isNormalJump = true;

        heroRigidbody.velocity = new Vector2(0, HeroesActiveJumpPowerValue);
        levelManager.audioManager.Play("Jumping");
        jumpingEffect.Play();
        heroAnimator.SetBool("IsJumping", true);
    }

    public void HeroDoubleJump(Rigidbody2D heroRigidbody, Animator heroAnimator)
    {
        isDoubleJump = true;
      //  isNormalJump = false;
        heroRigidbody.velocity = new Vector2(0, HeroesActiveJumpPowerValue);
        levelManager.audioManager.Play("Jumping");
        jumpingEffect.Play();
        heroAnimator.Play("BrandNewHeroJumping", 0, 0.3f);
        heroAnimator.SetBool("IsJumping", true);
    }

    public void HeroGoLeft(Rigidbody2D heroRigidbody)
    {
        heroRigidbody.velocity = new Vector2(-HeroesActiveWalkSpeedValue, heroRigidbody.velocity.y);
    }

    public void HeroGoRight(Rigidbody2D heroRigidbody)
    {
        heroRigidbody.velocity = new Vector2(HeroesActiveWalkSpeedValue, heroRigidbody.velocity.y);
    }

    public void StopMovingHero(Rigidbody2D heroRigidbody)
    {
        heroRigidbody.velocity = new Vector2(0, heroRigidbody.velocity.y);
    }

    public void DisableMicroVelocityOnPLayer(Rigidbody2D rb)
    {
        if (rb.velocity.x < 5f && rb.velocity.x > -5f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }



    public void EssenceMaterialColorRefresh(int whichHeroIndex)
    {
        float glownItensity = 6.3f;
        float dustItensity = 4.8f;
        float energyItensity = 5.7f;
        float circleItensity = 4.1f;

        float factorGlow = Mathf.Pow(2, glownItensity);
        float factorDust = Mathf.Pow(2, dustItensity);
        float factorEnergy = Mathf.Pow(2, energyItensity);
        float factorCircle = Mathf.Pow(2, circleItensity);

        switch (whichHeroIndex)
        {
           
            case 1:
                levelManager.essenceGlowMaterial.SetColor("_GlowColor", new Color(0.749f * factorGlow, 0.047f * factorGlow, 0.035f * factorGlow, 0));
                levelManager.essenceDustMaterial.SetColor("_GlowColor", new Color(0.749f * factorDust, 0.035f * factorDust, 0 * factorDust, 0));
                levelManager.essenceEnergyMaterial.SetColor("_GlowColor", new Color(0.749f * factorEnergy, 0.239f * factorEnergy, 0.203f * factorEnergy, 0));
                levelManager.essenceCircleMaterial.SetColor("_GlowColor", new Color(0.749f * factorCircle, 0.168f * factorCircle, 0.137f * factorCircle, 0));
                HeroEssenceTextPrefab.GetComponent<TextMeshPro>().color = new Color(1, 0.08086252f, 0.08086252f);
                enemiesManager.EnemyDamageTextPrefab.GetComponent<TextMeshPro>().color = new Color(1, 0.08086252f, 0.08086252f);
                break;

            case 2:
                levelManager.essenceGlowMaterial.SetColor("_GlowColor", new Color(0.749f * factorGlow, 0.035f * factorGlow, 0.509f * factorGlow, 0));
                levelManager.essenceDustMaterial.SetColor("_GlowColor", new Color(0.749f * factorDust, 0 * factorDust, 0.419f * factorDust, 0));
                levelManager.essenceEnergyMaterial.SetColor("_GlowColor", new Color(0.749f * factorEnergy, 0.207f * factorEnergy, 0.509f * factorEnergy, 0));
                levelManager.essenceCircleMaterial.SetColor("_GlowColor", new Color(0.749f * factorCircle, 0.137f * factorCircle, 0.537f * factorCircle, 0));
                HeroEssenceTextPrefab.GetComponent<TextMeshPro>().color = new Color(1, 0.08235294f, 0.9358831f);
                enemiesManager.EnemyDamageTextPrefab.GetComponent<TextMeshPro>().color = new Color(1, 0.08235294f, 0.9358831f);

                break;

            case 3:
                levelManager.essenceGlowMaterial.SetColor("_GlowColor", new Color(0.047f * factorGlow, 0.549f * factorGlow, 0.749f * factorGlow, 0));
                levelManager.essenceDustMaterial.SetColor("_GlowColor", new Color(0 * factorDust, 0.435f * factorDust, 0.749f * factorDust, 0));
                levelManager.essenceEnergyMaterial.SetColor("_GlowColor", new Color(0 * factorEnergy, 0.384f * factorEnergy, 0.749f * factorEnergy, 0));
                levelManager.essenceCircleMaterial.SetColor("_GlowColor", new Color(0.137f * factorCircle, 0.552f * factorCircle, 0.749f * factorCircle, 0));
                HeroEssenceTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.09423789f, 0.3889226f, 0.6415094f);
                enemiesManager.EnemyDamageTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.09423789f, 0.3889226f, 0.6415094f);
                break;

            case 4:
                levelManager.essenceGlowMaterial.SetColor("_GlowColor", new Color(0.062f * factorGlow * 1.30f, 0.035f * factorGlow * 1.30f, 0.749f * factorGlow * 1.30f, 0));
                levelManager.essenceDustMaterial.SetColor("_GlowColor", new Color(0 * factorDust * 1.30f, 0.004f * factorDust * 1.30f, 0.749f * factorDust * 1.30f, 0));
                levelManager.essenceEnergyMaterial.SetColor("_GlowColor", new Color(0.219f * factorEnergy, 0.207f * factorEnergy, 0.749f * factorEnergy, 0));
                levelManager.essenceCircleMaterial.SetColor("_GlowColor", new Color(0.188f * factorCircle, 0.137f * factorCircle, 0.749f * factorCircle, 0));
                HeroEssenceTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.09670561f, 0.08235294f, 1);
                enemiesManager.EnemyDamageTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.09670561f, 0.08235294f, 1);
                break;

            case 5:
                levelManager.essenceGlowMaterial.SetColor("_GlowColor", new Color(0.059f * factorGlow * 0.8f, 0.749f * factorGlow * 0.8f, 0.172f * factorGlow * 0.8f, 0));
                levelManager.essenceDustMaterial.SetColor("_GlowColor", new Color(0 * dustItensity * 0.8f, 0.749f * dustItensity * 0.8f, 0.105f * dustItensity * 0.8f, 0));
                levelManager.essenceEnergyMaterial.SetColor("_GlowColor", new Color(0.207f * factorEnergy, 0.749f * factorEnergy, 0.325f * factorEnergy, 0));
                levelManager.essenceCircleMaterial.SetColor("_GlowColor", new Color(0.137f * factorCircle, 0.749f * factorCircle, 0.309f * factorCircle, 0));
                HeroEssenceTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.06199461f, 1, 0.1686697f);
                enemiesManager.EnemyDamageTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.06199461f, 1, 0.1686697f);

                break;

            case 6:
                levelManager.essenceGlowMaterial.SetColor("_GlowColor", new Color(0.729f * factorGlow * 0.7f, 0.749f * factorGlow * 0.7f, 0.215f * factorGlow * 0.7f, 0));
                levelManager.essenceDustMaterial.SetColor("_GlowColor", new Color(0.749f * dustItensity * 0.7f, 0.607f * dustItensity * 0.7f, 0 * dustItensity * 0.7f, 0));
                levelManager.essenceEnergyMaterial.SetColor("_GlowColor", new Color(0.749f * energyItensity, 0.713f * energyItensity, 0.207f * energyItensity, 0));
                levelManager.essenceCircleMaterial.SetColor("_GlowColor", new Color(0.749f * circleItensity, 0.702f * circleItensity, 0.137f * circleItensity, 0));
                HeroEssenceTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.490566f, 0.4828727f, 0.0674363f);
                enemiesManager.EnemyDamageTextPrefab.GetComponent<TextMeshPro>().color = new Color(0.490566f, 0.4828727f, 0.0674363f);

                break;
        }
    }



    public void ChestMaterialsColorRefresh()
    {
        float chestSpriteMatIntensity = 4.9f;
        float chestParticleMatIntensity = 4.9f;

        float factorSpriteMat = Mathf.Pow(2, chestSpriteMatIntensity);
        float factorParticleMat = Mathf.Pow(2, chestParticleMatIntensity);

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1:
                towerSpawnManager.chestSpriteMaterial.SetColor("Color_71B81134", new Color(0.749f * factorSpriteMat, 0 * factorSpriteMat, 0.007f * factorSpriteMat, 0));
                towerSpawnManager.chestParticlesMaterial.SetColor("_GlowColor", new Color(0.749f * factorParticleMat, 0.047f * factorParticleMat, 0.043f * factorParticleMat, 0));
                break;

            case 2:
                towerSpawnManager.chestSpriteMaterial.SetColor("Color_71B81134", new Color(0.749f * factorSpriteMat, 0.035f * factorSpriteMat, 0.403f * factorSpriteMat, 0));
                towerSpawnManager.chestParticlesMaterial.SetColor("_GlowColor", new Color(0.749f * factorParticleMat, 0 * factorParticleMat, 0.337f * factorParticleMat, 0));
                break;

            case 3:
                towerSpawnManager.chestSpriteMaterial.SetColor("Color_71B81134", new Color(0 * factorSpriteMat, 0.372f * factorSpriteMat, 0.749f * factorSpriteMat, 0));
                towerSpawnManager.chestParticlesMaterial.SetColor("_GlowColor", new Color(0.009f * factorParticleMat, 0.423f * factorParticleMat, 0.749f * factorParticleMat, 0));
                break;

            case 4:
                towerSpawnManager.chestSpriteMaterial.SetColor("Color_71B81134", new Color(0.055f * factorSpriteMat, 0.066f * factorSpriteMat, 0.749f * factorSpriteMat, 0));
                towerSpawnManager.chestParticlesMaterial.SetColor("_GlowColor", new Color(0.090f * factorParticleMat, 0.129f * factorParticleMat, 0.749f * factorParticleMat, 0));
                break;

            case 5:
                towerSpawnManager.chestSpriteMaterial.SetColor("Color_71B81134", new Color(0.102f * factorSpriteMat, 0.749f * factorSpriteMat, 0.110f * factorSpriteMat, 0));
                towerSpawnManager.chestParticlesMaterial.SetColor("_GlowColor", new Color(0 * factorParticleMat, 0.749f * factorParticleMat, 0.110f * factorParticleMat, 0));
                break;

            case 6:
                towerSpawnManager.chestSpriteMaterial.SetColor("Color_71B81134", new Color(0.749f * factorSpriteMat * 0.5f, 0.705f * factorSpriteMat * 0.5f, 0.098f * factorSpriteMat * 0.5f, 0));
                towerSpawnManager.chestParticlesMaterial.SetColor("_GlowColor", new Color(0.729f * factorParticleMat, 0.749f * factorParticleMat, 0 * factorParticleMat, 0));
                break;
        }
    }

    public void FogMaterialColorRefresh()
    {

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1:
                levelManager.bgFogMaterial.SetColor("_TintColor", new Color(0.5098f, 0, 0.0148f, 0.2f));
                break;

            case 2:
                levelManager.bgFogMaterial.SetColor("_TintColor", new Color(0.5098f, 0, 0.4053f, 0.2f));
                break;

            case 3:
                levelManager.bgFogMaterial.SetColor("_TintColor", new Color(0, 0.3218f, 0.5098f, 0.2f));
                break;

            case 4:
                levelManager.bgFogMaterial.SetColor("_TintColor", new Color(0.0268f, 0, 0.5098f, 0.2f));
                break;

            case 5:
                levelManager.bgFogMaterial.SetColor("_TintColor", new Color(0, 0.5098f, 0.1323f, 0.2f));
                break;

            case 6:
                levelManager.bgFogMaterial.SetColor("_TintColor", new Color(0.5098f, 0.5069f, 0, 0.2f));
                break;
        }
    }

    public void EmitSteamAndEssenceColorRefresh()
    {

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1:
                emittingSteamEffectMainModule.startColor = new Color(1, 0.5068f, 0.4862f, 0.0705f);
                emittingEssenceEffectMainModule.startColor = new Color(1, 0.4f, 0.4410f, 1);
                break;

            case 2:
                emittingSteamEffectMainModule.startColor = new Color(1, 0.4862f, 0.9262f, 0.0705f);
                emittingEssenceEffectMainModule.startColor = new Color(1, 0.4f, 0.9825f, 1);
                break;

            case 3:
                emittingSteamEffectMainModule.startColor = new Color(0.4862f, 0.8889f, 1, 0.0705f);
                emittingEssenceEffectMainModule.startColor = new Color(0.4f, 0.8958f, 1, 1); break;

            case 4:
                emittingSteamEffectMainModule.startColor = new Color(0.5028f, 0.4862f, 1, 0.0705f);
                emittingEssenceEffectMainModule.startColor = new Color(0.4f, 0.4258f, 1, 1); break;

            case 5:
                emittingSteamEffectMainModule.startColor = new Color(0.5047f, 1, 0.4858f, 0.0705f);
                emittingEssenceEffectMainModule.startColor = new Color(0.4f, 1, 0.4844f, 1); break;

            case 6:
                emittingSteamEffectMainModule.startColor = new Color(0.9797f, 1, 0.4862f, 0.0705f);
                emittingEssenceEffectMainModule.startColor = new Color(0.9844f, 1, 0.4f, 1); break;
        }
    }

    public void EnviromentMaterialsColorRefresh()
    {
        float lanternMatIntensity = 3f;
        float factorLanternMat = Mathf.Pow(2, lanternMatIntensity);

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1:
                levelManager.platformMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.0901f, 0.0901f));
                levelManager.stoneMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.0901f, 0.0901f));
                levelManager.treeMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.0901f, 0.0901f));
                levelManager.grassMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.0901f, 0.0901f));
                levelManager.lanternMaterial.SetColor("_GlowColor", new Color(0.6039f * factorLanternMat, 0.0901f * factorLanternMat, 0.0901f * factorLanternMat));
                levelManager.lanternBulbMaterial.SetColor("_GlowColor", new Color(0.6039f * factorLanternMat, 0.0901f * factorLanternMat, 0.0901f * factorLanternMat));
                break;

            case 2:
                levelManager.platformMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.0901f, 0.4627f));
                levelManager.stoneMaterial.SetColor("_SpriteColor", new Color(0.2358491f, 0.08804608f, 0.1932618f));
                levelManager.treeMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.0901f, 0.4627f));
                levelManager.grassMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.0901f, 0.4627f));
                levelManager.lanternMaterial.SetColor("_GlowColor", new Color(0.6039f * factorLanternMat, 0.0901f * factorLanternMat, 0.4627f * factorLanternMat));
                levelManager.lanternBulbMaterial.SetColor("_GlowColor", new Color(0.6039f * factorLanternMat, 0.0901f * factorLanternMat, 0.4627f * factorLanternMat));

                break;

            case 3:
                levelManager.platformMaterial.SetColor("_SpriteColor", new Color(0.0901f, 0.5372f, 0.6039f));
                levelManager.stoneMaterial.SetColor("_SpriteColor", new Color(0.0901f, 0.5372f, 0.6039f));
                levelManager.treeMaterial.SetColor("_SpriteColor", new Color(0.0901f, 0.5372f, 0.6039f));
                levelManager.grassMaterial.SetColor("_SpriteColor", new Color(0.0901f, 0.5372f, 0.6039f));
                levelManager.lanternMaterial.SetColor("_GlowColor", new Color(0.0901f * factorLanternMat, 0.5372f * factorLanternMat, 0.6039f * factorLanternMat));
                levelManager.lanternBulbMaterial.SetColor("_GlowColor", new Color(0.0901f * factorLanternMat, 0.5372f * factorLanternMat, 0.6039f * factorLanternMat));
                break;

            case 4:
                levelManager.platformMaterial.SetColor("_SpriteColor", new Color(0.2313f, 0.1960f, 0.7490f));
                levelManager.stoneMaterial.SetColor("_SpriteColor", new Color(0.2313f, 0.1960f, 0.7490f));
                levelManager.treeMaterial.SetColor("_SpriteColor", new Color(0.2313f, 0.1960f, 0.7490f));
                levelManager.grassMaterial.SetColor("_SpriteColor", new Color(0.2313f, 0.1960f, 0.7490f));
                levelManager.lanternMaterial.SetColor("_GlowColor", new Color(0.2313f * factorLanternMat, 0.1960f * factorLanternMat, 0.7490f * factorLanternMat));
                levelManager.lanternBulbMaterial.SetColor("_GlowColor", new Color(0.2313f * factorLanternMat, 0.1960f * factorLanternMat, 0.7490f * factorLanternMat));
                break;

            case 5:
                levelManager.platformMaterial.SetColor("_SpriteColor", new Color(0.0882f, 0.6037f, 0.2345f));
                levelManager.stoneMaterial.SetColor("_SpriteColor", new Color(0.06997915f, 0.3018868f, 0.1362385f));
                levelManager.treeMaterial.SetColor("_SpriteColor", new Color(0.0882f, 0.6037f, 0.2345f));
                levelManager.grassMaterial.SetColor("_SpriteColor", new Color(0.0882f, 0.6037f, 0.2345f));
                levelManager.lanternMaterial.SetColor("_GlowColor", new Color(0.0882f * factorLanternMat, 0.6037f * factorLanternMat, 0.2345f * factorLanternMat));
                levelManager.lanternBulbMaterial.SetColor("_GlowColor", new Color(0.0882f * factorLanternMat, 0.6037f * factorLanternMat, 0.2345f * factorLanternMat));
                break;

            case 6:
                levelManager.platformMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.5906f, 0.0901f));
                levelManager.stoneMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.5906f, 0.0901f));
                levelManager.treeMaterial.SetColor("_SpriteColor", new Color(0.3396226f, 0.3300624f, 0.001830845f));
                levelManager.grassMaterial.SetColor("_SpriteColor", new Color(0.6039f, 0.5906f, 0.0901f));
                levelManager.lanternMaterial.SetColor("_GlowColor", new Color(0.6039f * factorLanternMat, 0.5906f * factorLanternMat, 0.0901f * factorLanternMat));
                levelManager.lanternBulbMaterial.SetColor("_GlowColor", new Color(0.6039f * factorLanternMat * 0.75f, 0.5906f * factorLanternMat * 0.75f, 0.0901f * factorLanternMat * 0.75f));

                break;
        }
    }
    public void BackgroundSpriteColorRefresh()
    {

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1:
                levelManager.backgroundSprite.color = new Color(0.0849f, 0.0204f, 0.0311f, 1);
                break;

            case 2:
                levelManager.backgroundSprite.color = new Color(0.1215f, 0.0313f, 0.1012f, 1);
                break;

            case 3:
                levelManager.backgroundSprite.color = new Color(0.0313f, 0.0866f, 0.1215f, 1);
                break;

            case 4:
                levelManager.backgroundSprite.color = new Color(0.0313f, 0.0313f, 0.1215f, 1);
                break;

            case 5:
                levelManager.backgroundSprite.color = new Color(0.0210f, 0.0754f, 0.0328f, 1);
                break;

            case 6:
                levelManager.backgroundSprite.color = new Color(0.0943f, 0.0911f, 0.0209f, 1);
                break;
        }
    }

}
