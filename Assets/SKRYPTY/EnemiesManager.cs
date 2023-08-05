using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;

public class EnemiesManager : MonoBehaviour
{
    private LevelManager levelManager;
    private HeroesManager heroesManager;
    private SkillsUpgradeManager skillsUpgradeManager;

    [Header("General")]

    public bool isAllowedBigSpider;
    public bool isAllowedJumpingSpider;
    public bool isAllowedBat;
    public bool isAllowedShootingSpider;
    public float bloodHitEffectReload;

    public GameObject portalDestructionEffect;
    public GameObject PortalExplozion;
    public Transform enemiesParent;
    public int enemiesLevel;
    public int enemiesLevelConst;
    public int enemiesHightBoundToLevelUp;
    public GameObject EnemyDamageTextPrefab;
   

    [Header("Enemy Bat")]
    public GameObject enemyBatPrefab;
    public GameObject enemyBatDeathBloodEffect;
    public GameObject enemyBatHitBloodEffect;

    public float enemyBatLifePool;
    public int enemyBatEssenceDropMaxQuantity;
    public int enemyBatEssenceDropMinQuantity;
    public float enemyBatScaleMultiplierMin;
    public float enemyBatScaleMultiplierMax;
    public float enemyBatMoveSpeedMin;
    public float enemyBatMoveSpeedMax;
    public float enemyBatDetectionRangeMin;
    public float enemyBatDetectionRangeMax;
    public int enemyBatMaxQuanityParameter;
    public int enemyBatMaxQuantityPerPlatform;
    public List<GameObject> enemiesBatObjectsList;
   

    [Header("Enemy Baby Spider")]
    public GameObject enemyBabySpiderPrefab;
    public GameObject enemyBabySpiderHitBloodEffect;
    public GameObject enemyBabySpiderDeathBloodEffect;
    public float enemyBabySpiderLifePool;
    public int enemyBabySpiderEssenceDropMaxQuantity;
    public int enemyBabySpiderEssenceDropMinQuantity;
    public float enemyBabySpiderScaleMultiplierMin;
    public float enemyBabySpiderScaleMultiplierMax;
    public float enemyBabySpiderMoveSpeedMin;
    public float enemyBabySpiderMoveSpeedMax;
    public List<GameObject> enemiesBabySpiderObjectsList;

    [Header("Enemy Normal Spider")]
    public GameObject enemyNormalSpiderPrefab;
    public GameObject enemyNormalSpiderHitBloodEffect;
    public GameObject enemyNormalSpiderDeathBloodEffect;
    public float enemyNormalSpiderMoveSpeedMin;
    public float enemyNormalSpiderMoveSpeedMax;
    public float enemyNormalSpiderLifePool;
    public float enemyNormalSpiderScaleMultiplierMin;
    public float enemyNormalSpiderScaleMultiplierMax;
    public int enemyNormalSpiderEssenceDropMaxQuantity;
    public int enemyNormalSpiderEssenceDropMinQuantity;
    public int enemyNormalSpiderMaxQuanityParameter;
    public int enemyNormalSpiderMaxQuantityPerPlatform;
    public List<GameObject> enemiesNormalSpiderObjectsList;


    [Header("Enemy Big Spider")]
    public GameObject enemyBigSpiderProjectilePrefeb;
    public GameObject enemyBigSpiderPrefab;
    public ParticleSystem bigSpiderProjectileDestryedEffect;
    public GameObject enemyBigSpiderHitBloodEffect;
    public GameObject enemyBigSpiderDeathBloodEffect;
    public float enemyBigSpiderMoveSpeedMin;
    public float enemyBigSpiderMoveSpeedMax;
    public float enemyBigSpiderFireRate;
    public float enemyBigSpiderProjectileLifetime;
    public float enemyBigSpiderProjectileMoveSpeed;
    public float enemyBigSpiderLifePool;
    public float enemyBigSpiderDetectionRangeMin;
    public float enemyBigSpiderDetectionRangeMax;
    public float enemyBigSpidderAttackSpeedAnimator;
    public float enemyBigSpiderScaleMultiplierMin;
    public float enemyBigSpiderScaleMultiplierMax;
    public int enemyBigSpiderEssenceDropMaxQuantity;
    public int enemyBigSpiderEssenceDropMinQuantity;
    public int enemyBigSpiderMaxQuanityParameter;
    public List<GameObject> enemiesBigSpiderObjectsList;


    [Header("Enemy Jumping Spider")]
    public GameObject enemyJumpingSpiderPrefab;
    public GameObject enemyJumpingSpiderHitBloodEffect;
    public GameObject enemyJumpingSpiderDeathBloodEffect;
    public float enemyJumpingSpiderJumpPauseTime;
    public float enemyJumpingSpiderJumpPower;
    public float enemyJumpingSpiderSerachRadius;
    public float enemyJumpingSpiderLifePool;
    public float enemyJumpingSpiderScaleMultiplierMin;
    public float enemyJumpingSpiderScaleMultiplierMax;
    public int enemyJumpingSpiderEssenceDropMaxQuantity;
    public int enemyJumpingSpiderEssenceDropMinQuantity;
    public int enemyJumpingSpiderMaxQuanityParameter;
    public int enemyJumpingSpiderMaxQuantityPerPlatform;
    public List<GameObject> enemiesJumpingSpiderObjectsList;

    [Header("Enemy Shooting Spider")]
    public GameObject shootingSpiderPrefab;

    public float moveSpeedShootingSpider;
    public float shootingSpiderAttackTimer;
    public float lifePoolShootingSpider;

    [Header("Enemy Egg Spider")]
    public GameObject enemyEggSpiderPrefab;
    public Material[] enemyEggSpiderHatchingMaterials;
    public GameObject enemyEggSpiderExplozionEffect;
    public GameObject enemyEggSpiderDeathBloodEffect;
    public GameObject enemyEggSpiderHitBloodEffect;
    public float enemyEggSpiderGrowTime;
    public int enemyEggSpiderEssenceDropMaxQuantity;
    public int enemyEggSpiderEssenceDropMinQuantity;
    public float enemyEggSpiderLifePool;
    public List<GameObject> enemiesEggSpiderObjectsList;

    [Header("Enemy Egg Empty")]
    public GameObject enemyEggEmptyExplozionEffect;
    public GameObject[] enemyEggEmptyPrefab;
    public int enemyEgEmptyEssenceDropMaxQuantity;
    public int enemyEgEmptyEssenceDropMinQuantity;
    public float enemyEggEmptyScaleMultiplierMin;
    public float enemyEggEmptyScaleMultiplierMax;

    [Header("Enemy Boss Spider")]
    public float lifePoolSpiderBoss;
    public GameObject spiderBossBulletPrefab;

    [Header("Enemy Skeletal Dragon")]
    public GameObject enemySkeletalDragonPrefab;
    public GameObject enemySkeletalDragonStage1Projectile;
    public GameObject enemySkeletalDragonStage2Projectile;
    public float enemySkeletalDragonProjectileSpeed;
    public float enemySkeletalDragonFireRate;
    public float enemySkeletalDragonMoveSpeedMin;
    public float enemySkeletalDragonMoveSpeedMax;

    public float enemySkeletalDragonLifePool;
    public GameObject enemySkeletalDragonDeathBloodEffect;
    public GameObject enemySkeletalDragonHitBloodEffect;
    public int enemySkeletalDragonEssenceDropMinQuantity;
    public int enemySkeletalDragonEssenceDropMaxQuantity;
    public float enemySkeletalDragonDetectionRange; // 50
    public float enemySkeletalDragonScaleMultiplierMin; 
    public float enemySkeletalDragonScaleMultiplierMax;
    public List<GameObject> enemiesSkeletalDragonObjectsList;


    [Header("EnemyPortal")]
    public GameObject enemyPortalPrefab;
    public List<GameObject> enemiesPortalObjectsList;
    public int enemyPortalMonstersQuantityMin;
    public int enemyPortalMonstersQuantityMax;
    public float enemyPortaTimeOfSpawningMin;
    public float enemyPortaTimeOfSpawningMax;

    [Header("Enemy Hanging Spider")]
    public GameObject enemyHangingSpiderPrefab;
    public GameObject enemyHangingSiderProjectilePrefab;
    public GameObject enemyHangingSiderProjectileCrashPrefab;
    public GameObject enemyHangingSpiderHitBloodEffect;
    public GameObject enemyHangingSpiderDeathBloodEffect;
    public List<GameObject> enemiesHangingSpiderObjectsList;
    public float enemyHangingSpiderLifePool;
    public float enemyHangingSpiderAnimationSpeed;
    public float enemyHangingSpiderProjectileSpeed;
    public float enemyHangingSpiderMoveSpeedMin;
    public float enemyHangingSpiderMoveSpeedMax;
    public float enemyHangingSpiderScaleMultiplierMin;
    public float enemyHangingSpiderScaleMultiplierMax;
    public int enemyHangingSpiderEssenceDropMinQuantity;
    public int enemyHangingSpiderEssenceDropMaxQuantity;

    [Header("Enemy Firefly")]
    public GameObject enemyFireflyPrefab;
    public GameObject enemyFireflyHivePrefab;
    public GameObject enemyFireflyExplozionEffectPrefab;
    public GameObject enemyFireflyBigExplozionEffectPrefab;
    public GameObject enemyFireflyHitBloodEffectPrefab;
    public float enemyFireflyDetectionRadius = 12f;
    public float enemyFireflyLifePool;
    public int enemyFireflyEssenceDropMaxQuantity;
    public int enemyFireflyEssenceDropMinQuantity;
    public float enemyFireflyScaleMultiplierMin;
    public float enemyFireflyScaleMultiplierMax;
    public float enemyFireflyMoveSpeedMin;
    public float enemyFireflyMoveSpeedMax;
    public int enemyFireflyMaxQuanityParameter;
    public List<GameObject> enemiesFireflyHiveObjectsList;

    [Header("Enemy Tentacle Worm")]
    public GameObject enemyTentacleWormProjectilePrefeb;
    public GameObject enemyTentacleWormPrefab;
    public ParticleSystem enemyTentacleWormProjectileDestryedEffect;
    public GameObject enemyTentacleWormProjectileShatredEffect;
    public GameObject enemyTentacleWormHitBloodEffect;
    public GameObject enemyTentacleWormDeathBloodEffect;
    public float enemyTentacleWormFireRate;
    public float enemyTentacleWormProjectileLifetime;
    public float enemyTentacleWormProjectileMoveSpeed;
    public float enemyTentacleWormLifePool;
    public float enemyTentacleWormDetectionRangeMin;
    public float enemyTentacleWormDetectionRangeMax;
    //public float enemyTentacleWormAttackSpeedAnimator;
    public float enemyTentacleWormScaleMultiplierMin;
    public float enemyTentacleWormScaleMultiplierMax;
    public int enemyTentacleWormEssenceDropMaxQuantity;
    public int enemyTentacleWormEssenceDropMinQuantity;
    public int enemyTentacleWormMaxQuanityParameter;
    public List<GameObject> enemiesTentacleWormObjectsList;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();

        SetStartingValuesGeneral();

        SetStartingValuesEnemyBat();
        SetStartingValuesEnemyEggSpider();
        SetStartingValuesEnemyBabySpider();
        SetStartingValuesEnemyEggEmpty();
        SetStartingValuesEnemyNormalSpider();
        SetStartingValuesEnemyBigSpider();
        SetStartingValuesEnemySkeletalDragon();
        SetStartingValuesEnemyHangingSpider();
        SetStartingValuesEnemyJumpingSpider();
        SetStartingValuesEnemyFirefly();
        SetStartingValuesEnemyTentacleWorm();

        SetStartingValuesEnemyPortal();
    }


    private void CalculateEnemiesLevel()
    {

        enemiesLevel = (levelManager.towerHightReachedFinalCounter / enemiesHightBoundToLevelUp) + 1;

        if (enemiesLevelConst < enemiesLevel)
        {
            RefreshEnemyBatStatistics();
            RefreshEnemyBabySpiderStatistics();
            RefreshEnemyNormalSpiderStatistics();
            RefreshEnemyBigSpiderStatistics();
            RefreshEnemySkeletalDragonStatistics();
            RefreshEnemyHangingSpiderStatistics();
            RefreshEnemyJumpingSpiderStatistics();
            RefreshEnemyFireflyStatistics();

            RefreshEnemyPortalStatistics();

            enemiesLevelConst = enemiesLevel;
        }

    }

    private void FixedUpdate()
    {
        CalculateEnemiesLevel();
    }

    private void RefreshEnemyPortalStatistics()
    {
        enemyPortalMonstersQuantityMin = 3 + (enemiesLevel / 2);
        enemyPortalMonstersQuantityMax = 8 + enemiesLevel;

        enemyPortaTimeOfSpawningMin = 5.0f - (enemiesLevel * 0.2f);
        enemyPortaTimeOfSpawningMax = 9.0f - (enemiesLevel * 0.2f);

        if (enemyPortaTimeOfSpawningMin <= 2.0f)
            enemyPortaTimeOfSpawningMin = 2.0f;

        if (enemyPortaTimeOfSpawningMax <= 3.0f)
            enemyPortaTimeOfSpawningMax = 3.0f;
    }

    private void RefreshEnemyBatStatistics()
    {
        enemyBatLifePool = 18f + (enemiesLevel * 4f);
        enemyBatMaxQuanityParameter = 1 + Mathf.FloorToInt((float)enemiesLevel * 0.25f);
        enemyBatMoveSpeedMin = 1.3f + (enemiesLevel * 0.025f);
        enemyBatMoveSpeedMax = 2.0f + (enemiesLevel * 0.040f);
    }

    private void RefreshEnemyFireflyStatistics()
    {
        enemyFireflyLifePool = 14f + (enemiesLevel * 3f);
        if(enemyFireflyMaxQuanityParameter < 16)
          enemyFireflyMaxQuanityParameter = 3 + Mathf.FloorToInt((float)enemiesLevel * 0.75f);
     
        enemyFireflyMoveSpeedMin = 2.6f + (enemiesLevel * 0.035f);
        enemyFireflyMoveSpeedMax = 3.4f + (enemiesLevel * 0.050f);

        if(enemyFireflyDetectionRadius < 30)
            enemyFireflyDetectionRadius = 10f + (enemiesLevel * 1.2f);
       
    }

    private void RefreshEnemySkeletalDragonStatistics()
    {
        enemySkeletalDragonLifePool = 500f + (enemiesLevel * 50f);
        enemySkeletalDragonMoveSpeedMin = 2.8f + (enemiesLevel * 0.03f);
        enemySkeletalDragonMoveSpeedMax = 3.2f + (enemiesLevel * 0.035f);

        enemySkeletalDragonFireRate = 5.0f - (enemiesLevel * 0.1f);

        if (enemySkeletalDragonFireRate < 2.0f)
            enemySkeletalDragonFireRate = 2.0f;

        enemySkeletalDragonProjectileSpeed = 12.0f + (enemiesLevel * 0.25f);

        if (enemySkeletalDragonProjectileSpeed > 30.0f)
            enemySkeletalDragonProjectileSpeed = 30.0f;

    }

    private void RefreshEnemyBabySpiderStatistics()
    {
        enemyBabySpiderLifePool = 8f + (enemiesLevel * 2f);
        enemyBabySpiderMoveSpeedMin = 1.3f + (enemiesLevel * 0.025f);
        enemyBabySpiderMoveSpeedMax = 2.0f + (enemiesLevel * 0.040f);
    }

    private void RefreshEnemyNormalSpiderStatistics()
    {
        enemyNormalSpiderLifePool = 18 + (enemiesLevel * 5f);
        enemyNormalSpiderMaxQuanityParameter = 1 + Mathf.FloorToInt((float)enemiesLevel * 0.25f);

        if (enemyNormalSpiderMaxQuanityParameter > 3)
            enemyNormalSpiderMaxQuanityParameter = 3;

        enemyNormalSpiderMoveSpeedMin = 1.2f + (enemiesLevel * 0.020f);
        enemyNormalSpiderMoveSpeedMax = 1.9f + (enemiesLevel * 0.030f);
    }

    private void RefreshEnemyJumpingSpiderStatistics()
    {
        if(enemyJumpingSpiderMaxQuanityParameter < 3)
           enemyJumpingSpiderMaxQuanityParameter = 1 + Mathf.FloorToInt((float)enemiesLevel * 0.25f);

        enemyJumpingSpiderLifePool = 22 + (enemiesLevel * 6f);

        if(enemyJumpingSpiderJumpPower < 20)
            enemyJumpingSpiderJumpPower = 10 + (enemiesLevel * 0.35f);

        if(enemyJumpingSpiderJumpPauseTime > 0.2f)
            enemyJumpingSpiderJumpPauseTime = 2.0f - (enemiesLevel * 0.1f);

        enemyJumpingSpiderSerachRadius = 12.0f + (enemiesLevel * 0.75f);

    }

    private void RefreshEnemyBigSpiderStatistics()
    {
        enemyBigSpiderLifePool = 35 + (enemiesLevel * 10f);

        enemyBigSpiderMaxQuanityParameter = 1 + Mathf.FloorToInt((float)enemiesLevel * 0.25f);
        if (enemyBigSpiderMaxQuanityParameter > 2)
            enemyBigSpiderMaxQuanityParameter = 2;

        enemyBigSpiderMoveSpeedMin = 0.9f + (enemiesLevel * 0.012f);
        enemyBigSpiderMoveSpeedMax = 1.4f + (enemiesLevel * 0.020f);

        if (enemyBigSpiderFireRate <= 2f)  // tu coś rozdupcone, if nie ma sensu  if (enemyBigSpiderFireRate >= 0.2f)
        {
            enemyBigSpiderFireRate = 1.0f + (0.02f * enemiesLevel);
        }

        enemyBigSpiderProjectileMoveSpeed = 3f + Mathf.FloorToInt((float)enemiesLevel * 0.2f);
    }

    private void RefreshEnemyTentacleWormStatistics() 
    {
        enemyTentacleWormLifePool = 72f + (enemiesLevel * 14f);  ;
        //enemyBigSpiderMaxQuanityParameter zawsze równe 1, nie ma potrzeby więcej
        if (enemyTentacleWormFireRate <= 3.5f) 
        {
            enemyTentacleWormFireRate = 0.75f + (0.05f * enemiesLevel);
        }
        if (enemyTentacleWormProjectileMoveSpeed <= 6f)
        {
            enemyTentacleWormProjectileMoveSpeed = 3.5f + (0.05f * enemiesLevel);
        }
        if (enemyTentacleWormDetectionRangeMax <= 32f)
        {
            enemyTentacleWormDetectionRangeMin = 20f + (0.1f * enemiesLevel);
            enemyTentacleWormDetectionRangeMax = 25f + (0.2f * enemiesLevel);
        }
    }

    private void RefreshEnemyHangingSpiderStatistics()
    {

        enemyHangingSpiderLifePool = 32 + (enemiesLevel * 9f);
        enemyHangingSpiderAnimationSpeed =  (1.02f * enemiesLevel);

        if (enemyHangingSpiderAnimationSpeed <= 2.5f)
        {
            enemyHangingSpiderAnimationSpeed = 2.5f;
        }

        enemyHangingSpiderProjectileSpeed = 17.0f + (0.15f * enemiesLevel);
      
        if (enemyHangingSpiderProjectileSpeed >= 26f)
        {
            enemyHangingSpiderProjectileSpeed = 26f;
        }

        enemyHangingSpiderMoveSpeedMin = 3.8f + (enemiesLevel * 0.02f);
        enemyHangingSpiderMoveSpeedMax = 4.4f + (enemiesLevel * 0.025f);

    }

    private void SetStartingValuesGeneral()
    {
        bloodHitEffectReload = 0.14f;

          enemiesHightBoundToLevelUp = 100;
    
    }

    private void SetStartingValuesEnemyBat()
    {
        enemyBatMaxQuanityParameter = 1;
        enemyBatMaxQuantityPerPlatform = 1;
        enemiesBatObjectsList = new List<GameObject>();
        enemyBatLifePool = 18f;
        enemyBatEssenceDropMinQuantity = 2;
        enemyBatEssenceDropMaxQuantity = 5;
        enemyBatScaleMultiplierMin = 0.8f;
        enemyBatScaleMultiplierMax = 1.1f;
        enemyBatMoveSpeedMin = 1.3f;
        enemyBatMoveSpeedMax = 2.0f;
        enemyBatDetectionRangeMin = 15.0f;
        enemyBatDetectionRangeMax = 18.0f;
    }

    private void SetStartingValuesEnemyFirefly()
    {
        enemiesFireflyHiveObjectsList = new List<GameObject>();
        enemyFireflyMaxQuanityParameter = 3;
        enemyFireflyMoveSpeedMin = 2.6f;
        enemyFireflyMoveSpeedMax = 3.4f;
        enemyFireflyScaleMultiplierMin = 0.9f;
        enemyFireflyScaleMultiplierMax = 1.1f;
        enemyFireflyEssenceDropMinQuantity = 2;
        enemyFireflyEssenceDropMaxQuantity = 3;
        enemyFireflyLifePool = 14f;
        enemyFireflyDetectionRadius = 10f;

}

    private void SetStartingValuesEnemyEggSpider()
    {
        enemyEggSpiderGrowTime = 12f;
        enemyEggSpiderEssenceDropMaxQuantity = 2;
        enemyEggSpiderEssenceDropMinQuantity = 1;
        enemyEggSpiderLifePool = 100f;
        enemiesEggSpiderObjectsList = new List<GameObject>();

    }

    private void SetStartingValuesEnemyEggEmpty()
    {
        enemyEgEmptyEssenceDropMaxQuantity = 1;
        enemyEgEmptyEssenceDropMinQuantity = 1;
        enemyEggEmptyScaleMultiplierMin = 0.8f;
        enemyEggEmptyScaleMultiplierMax = 1.2f;
    }

    private void SetStartingValuesEnemyBabySpider()
    {
        enemyBabySpiderLifePool = 8f;
        enemyBabySpiderEssenceDropMaxQuantity = 3;
        enemyBabySpiderEssenceDropMinQuantity = 1;
        enemyBabySpiderScaleMultiplierMin = 0.8f;
        enemyBabySpiderScaleMultiplierMax = 1.1f;
        enemyBabySpiderMoveSpeedMin = 2f;
        enemyBabySpiderMoveSpeedMax = 2.5f;
        enemiesBabySpiderObjectsList = new List<GameObject>();

    }

    private void SetStartingValuesEnemyNormalSpider()
    {
        enemyNormalSpiderMaxQuanityParameter = 1;
        enemyNormalSpiderMaxQuantityPerPlatform = 1;
        enemyNormalSpiderLifePool = 18f;
        enemyNormalSpiderEssenceDropMinQuantity = 2;
        enemyNormalSpiderEssenceDropMaxQuantity = 4;
        enemyNormalSpiderScaleMultiplierMin = 0.8f;
        enemyNormalSpiderScaleMultiplierMax = 1.1f;
        enemyNormalSpiderMoveSpeedMin = 1.2f;
        enemyNormalSpiderMoveSpeedMax = 1.9f;
        enemiesNormalSpiderObjectsList = new List<GameObject>();

    }

    private void SetStartingValuesEnemyJumpingSpider()
    {
        enemyJumpingSpiderMaxQuanityParameter = 1;
        enemyJumpingSpiderMaxQuantityPerPlatform = 1;
        enemyJumpingSpiderJumpPauseTime = 2.0f;
        enemyJumpingSpiderJumpPower = 10.0f;
        enemyJumpingSpiderSerachRadius = 12.0f;
        enemyJumpingSpiderLifePool = 22f;
        enemyJumpingSpiderEssenceDropMinQuantity = 3;
        enemyJumpingSpiderEssenceDropMaxQuantity = 5;
        enemyJumpingSpiderScaleMultiplierMin = 0.8f;
        enemyJumpingSpiderScaleMultiplierMax = 1.1f;
        enemiesJumpingSpiderObjectsList = new List<GameObject>();
    }

    private void SetStartingValuesEnemyBigSpider()
    {
        enemyBigSpiderMaxQuanityParameter = 1;
        enemyBigSpiderLifePool = 35f;
        enemyBigSpiderMoveSpeedMin = 0.9f;
        enemyBigSpiderMoveSpeedMax = 1.4f;
        enemyBigSpiderFireRate = 2f;
        enemyBigSpiderProjectileMoveSpeed = 3f;
        enemyBigSpiderProjectileLifetime = 10f;
        enemyBigSpiderDetectionRangeMin = 9f;
        enemyBigSpiderDetectionRangeMax = 11f;
        enemyBigSpidderAttackSpeedAnimator = 1f;
        enemyBigSpiderScaleMultiplierMin = 0.55f;
        enemyBigSpiderScaleMultiplierMax = 0.70f;
        enemyBigSpiderEssenceDropMaxQuantity = 9;
        enemyBigSpiderEssenceDropMinQuantity = 5;
        enemiesBigSpiderObjectsList = new List<GameObject>();
    }

    private void SetStartingValuesEnemyTentacleWorm() {

        enemyTentacleWormMaxQuanityParameter = 1;
        enemyTentacleWormLifePool = 72f;
        enemyTentacleWormFireRate = 0.75f;
        enemyTentacleWormProjectileMoveSpeed = 3.5f;
        enemyTentacleWormProjectileLifetime = 100f;
        enemyTentacleWormDetectionRangeMin = 20f;
        enemyTentacleWormDetectionRangeMax = 25f;
       //enemyTentacleWormAttackSpeedAnimator = 1f;
        enemyTentacleWormScaleMultiplierMin = 0.9f;
        enemyTentacleWormScaleMultiplierMax = 1f;
        enemyTentacleWormEssenceDropMinQuantity = 10;
        enemyTentacleWormEssenceDropMaxQuantity = 16;
        enemiesTentacleWormObjectsList = new List<GameObject>();
     
        }

        private void SetStartingValuesEnemySkeletalDragon()
        {
            enemySkeletalDragonLifePool = 500f;
            enemySkeletalDragonEssenceDropMinQuantity = 60;
            enemySkeletalDragonEssenceDropMaxQuantity = 100;
            enemySkeletalDragonDetectionRange = 40f;
            enemySkeletalDragonScaleMultiplierMin = 0.9f;
            enemySkeletalDragonScaleMultiplierMax = 1.1f;
            enemySkeletalDragonMoveSpeedMin = 2.8f;
            enemySkeletalDragonMoveSpeedMax = 3.2f;
            enemySkeletalDragonFireRate = 5f;
            enemySkeletalDragonProjectileSpeed = 12f;
            enemiesSkeletalDragonObjectsList = new List<GameObject>();

        }

        private void SetStartingValuesEnemyHangingSpider()
        {
            enemyHangingSpiderLifePool = 32.0f;
            enemyHangingSpiderAnimationSpeed = 1.0f;
            enemyHangingSpiderProjectileSpeed = 17.0f;
            enemyHangingSpiderMoveSpeedMin = 3.8f;
            enemyHangingSpiderMoveSpeedMax = 4.4f;
            enemyHangingSpiderScaleMultiplierMin = 0.9f;
            enemyHangingSpiderScaleMultiplierMax = 1.1f;
            enemyHangingSpiderEssenceDropMinQuantity = 8;
            enemyHangingSpiderEssenceDropMaxQuantity = 13;

            enemiesHangingSpiderObjectsList = new List<GameObject>();
        }

        private void SetStartingValuesEnemyPortal()
        {
            enemiesPortalObjectsList = new List<GameObject>();
            enemyPortalMonstersQuantityMin = 3;
            enemyPortalMonstersQuantityMax = 8;
            enemyPortaTimeOfSpawningMin = 5.0f;
            enemyPortaTimeOfSpawningMax = 9.0f;
        }

                private void ResetEnemiesBaseValues()
                {
                        moveSpeedShootingSpider = 1.1f;



                        shootingSpiderAttackTimer = 10f;

                        enemyNormalSpiderLifePool = 2f;
                        enemySkeletalDragonLifePool = 250f;
                        lifePoolShootingSpider = 75f;
                        enemyJumpingSpiderLifePool = 40f;

                        lifePoolSpiderBoss = 5000f;
                }
            /*
                    public void SpawnSkeletalDragon()
                    {
                        var skeletalDragon = Instantiate(skeletalDragonPrefab);
                        skeletalDragon.tag = "DRAGONBOSS";
                        var skeletalDragonTarget = skeletalDragon.GetComponent<AIDestinationSetter>();
                        //  var dragonBossRB = DragonBoss.GetComponent<CapsuleCollider2D>();
                        skeletalDragonTarget.target = levelManager.cameraAndEnemiesTargetPoint;
                        Invoke("EnemyUpgradDragonBoss", 5f);

                    }*/

    /*
    public void EnemyUpgradeBat()
    {
        enemyBatLifePool += 1.5f;

    }

    public void EnemyUpgradeJumpingSpider()
    {
        lifePoolJumpingSpider += 2f;

    }


    public void EnemyUpgradeNormalSpider()
    {
        lifePoolNormalSpider += 1f;
        moveSpeedNormalSpider += 0.05f;

    }

    public void EnemyUpgradeShootingSpider()
    {
        lifePoolShootingSpider += 5f;
        moveSpeedShootingSpider += 0.02f;
        if (shootingSpiderAttackTimer > 4.5f)
            shootingSpiderAttackTimer *= 0.985f;

    }

    public void EnemyUpgradeSpiderEgg()
    {
        lifePoolSpiderEgg += 3f;
    }

    public void EnemyUpgradeBabySpider()
    {
        lifePoolBabySpider += 0.4f;
    }

    public void EnemyUpgradeBigSpider()
    {

        lifePoolBigSpider += 3f;

        if (fireRateBigSpider >= 0.2f)
        {
            fireRateBigSpider *= 0.98f;
            attackSpeedBigSpiderAnim *= 1.02f;
        }

        moveSpeedBigSpider += 0.05f;


    }

    */

    public void EnemyUpgradSkeletalDragon()
    {

        enemySkeletalDragonLifePool += 250f;
        if (enemySkeletalDragonFireRate >= 0.2f)
            enemySkeletalDragonFireRate *= 0.95f;
        enemySkeletalDragonProjectileSpeed += 0.5f;
        enemySkeletalDragonMoveSpeedMin += 0.4f;
    }

    public float TakeDamageAfterHeroAttack(string attackTag, GameObject enemyObject, GameObject attackerObject, float damageMultiplier)
    {
     
        float damageAfterMultiplier = 0.0f;

        switch (attackTag)
        {
            case "AttackHero1": 
                CinemachineCameraShake.Instance.ShakeCamera(1.5f, 0.1f);

                if (levelManager.cameraAndEnemiesTargetPoint.position.x >= enemyObject.transform.position.x)
                    enemyObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-heroesManager.Hero1LaserKnockbackValue, 0.0f), ForceMode2D.Impulse);
                else
                    enemyObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(heroesManager.Hero1LaserKnockbackValue, 0.0f), ForceMode2D.Impulse);

                damageAfterMultiplier = (heroesManager.Hero1LaserDamageValue) * heroesManager.HeroesDamageMultiplier * damageMultiplier;
                break;


            case "AttackHero2":
                CinemachineCameraShake.Instance.ShakeCamera(1.5f, 0.1f);
                levelManager.audioManager.Play("Hero2SlashHit", enemyObject.transform.position);

                if (levelManager.cameraAndEnemiesTargetPoint.position.x >= enemyObject.transform.position.x)     
                    enemyObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-heroesManager.Hero2WeaponKnockbackValue, 0.0f), ForceMode2D.Impulse);
                else 
                    enemyObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(heroesManager.Hero2WeaponKnockbackValue, 0.0f), ForceMode2D.Impulse);

                if (UnityEngine.Random.Range(0.0f, 1.0f) <= heroesManager.Hero2WeaponCriticalStrikeChanceValue)
                {
                    damageAfterMultiplier = heroesManager.Hero2WeaponDamageValue * heroesManager.HeroesDamageMultiplier * (1f + heroesManager.Hero2WeaponCriticalStrikeMultiplierValue) * damageMultiplier;
                    Vector2 pozycja = enemyObject.transform.position;
                    pozycja.y += 2f;
                    var crit = Instantiate(heroesManager.critTextPrefabHERO2, pozycja, enemyObject.transform.rotation);
                    Destroy(crit, 0.2f);
                }
                else
                {
                    damageAfterMultiplier = heroesManager.Hero2WeaponDamageValue * heroesManager.HeroesDamageMultiplier * damageMultiplier;
                }
                
                break;


            case "AttackHero3":
                CinemachineCameraShake.Instance.ShakeCamera(1.1f, 0.1f);
                damageAfterMultiplier = heroesManager.Hero3ProjectileDamageValue * heroesManager.HeroesDamageMultiplier * damageMultiplier;
                Destroy(attackerObject);
                break;


            case "AttackHero4":
                CinemachineCameraShake.Instance.ShakeCamera(1.3f, 0.1f);
                //  levelManager.audioManager.Play("Hero4TendrilsHit");
                if (heroesManager.isHero4TendrilsClipReseted)
                    levelManager.audioManager.sounds[15].source.PlayOneShot(levelManager.audioManager.sounds[15].source.clip);
                else
                {
                    levelManager.audioManager.Play("Hero4TendrilsHit", enemyObject.transform.position);
                    heroesManager.isHero4TendrilsClipReseted = true;
                }
                damageAfterMultiplier = heroesManager.Hero4BeamDamageValue * heroesManager.HeroesDamageMultiplier * damageMultiplier;
                break;


            case "AttackHero5":
                CinemachineCameraShake.Instance.ShakeCamera(2.0f, 0.1f);
                levelManager.audioManager.Play("Hero5ProjectileHit", enemyObject.transform.position);
                damageAfterMultiplier = heroesManager.Hero5ProjectileDamageValue * heroesManager.HeroesDamageMultiplier * damageMultiplier;
                break;


            case "AttackHero6":
                CinemachineCameraShake.Instance.ShakeCamera(1.1f, 0.1f);
                levelManager.audioManager.Play("Hero6ProjectileHit", enemyObject.transform.position);
                damageAfterMultiplier = heroesManager.Hero6ProjectileDamageValue * heroesManager.HeroesDamageMultiplier * damageMultiplier;
                break;
        }

        damageAfterMultiplier = (float)Math.Round(damageAfterMultiplier, 2);
        var damageText = Instantiate(EnemyDamageTextPrefab, enemyObject.transform.position +
         new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(2.5f,3.5f), 0), EnemyDamageTextPrefab.transform.rotation);
        damageText.GetComponent<TextMeshPro>().SetText(damageAfterMultiplier.ToString());
        Destroy(damageText, 0.5f);
        return damageAfterMultiplier;

    }


    public void DropOfEssence(int minQuantity, int maxQuantity, Vector3 dropPosition)
    {
        var essenceQuantity = UnityEngine.Random.Range(minQuantity, maxQuantity + 1);

        for (int i = essenceQuantity; i > 0; i--)
        {
            Vector3 temporaryDropPosition = dropPosition;
         //   temporaryDropPosition.x += (float)i / 2;
         //   temporaryDropPosition.y += (float)i / 2;

            GameObject essence = Instantiate(levelManager.essence_x1_prefab, temporaryDropPosition, gameObject.transform.rotation);
          //  essence.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essence.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1.2f, 1.2f), UnityEngine.Random.Range(6.5f, 8f)), ForceMode2D.Impulse);
            
            Destroy(essence, levelManager.essenceLifeTime);
        }

    }

    public void DropOfEssenceMiniBoss(int minQuantity, int maxQuantity, Vector3 dropPosition)
    {
        var essenceQuantity = UnityEngine.Random.Range(minQuantity, maxQuantity + 1);

        for (int i = essenceQuantity; i > 0; i--)
        {
            Vector3 temporaryDropPosition = dropPosition;

            GameObject essence = Instantiate(levelManager.essence_x1_prefab, temporaryDropPosition, gameObject.transform.rotation);
          //  essence.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essence.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-8.5f, 8.5f), UnityEngine.Random.Range(2f, 15f)), ForceMode2D.Impulse);

            Destroy(essence, levelManager.essenceLifeTime);
        }

    }

    public void HitBloodEffect(GameObject bloodEffect, Vector3 spawnPosition)
    {
        GameObject blood = Instantiate(bloodEffect, spawnPosition, bloodEffect.transform.rotation);
        Destroy(blood, 6f);
    }

    public void DeathBloodEffect(GameObject bloodEffect, Vector3 spawnPosition)
    {
        GameObject blood = Instantiate(bloodEffect, spawnPosition, bloodEffect.transform.rotation);
        Destroy(blood, 6f);
    }

}
