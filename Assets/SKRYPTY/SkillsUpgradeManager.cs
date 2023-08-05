
using UnityEngine;

using TMPro;
using System;

public class SkillsUpgradeManager : MonoBehaviour
{

    [Header("General")]
    private LevelManager levelManager;
    private HeroesManager heroesManager;
    public ParticleSystem[] powerUpEffect;
    public Transform powerUpEffectPoint;
    public int[] heroesPowerLvl;
    public int[] powerUpProg;
    public GameObject[] powerUpHeroEffect;

    [Header("Hero 1")]

    [HideInInspector] public float Hero1LaserDamageLevel;
    [HideInInspector] public float Hero1LaserKnockbacklevel;
    [HideInInspector] public float Hero1LaserChainsNumberLevel;
    [HideInInspector] public float Hero1LaserChainsEfficiencyLevel;
    [HideInInspector] public float Hero1LaserChainsRangeLevel;

    private bool isHero1ProjectileKnockbackMaxLevel = false;
    private bool isHero1LaserChainsNumberMaxLevel = false;
    private bool isHero1LaserChainsEfficiencyMaxLevel = false;
    private bool isHero1LaserChainRangeMaxLevel = false;


    [Header("Hero 2")]
    [HideInInspector] public float Hero2WeaponDamageLevel;
    [HideInInspector] public float Hero2WeaponKnockbackLevel;
    [HideInInspector] public float Hero2AttackSpeedLevel;
    [HideInInspector] public float Hero2WeaponCriticalStrikeChanceLevel;
    [HideInInspector] public float Hero2WeaponCriticalStrikeMultiplierLevel;

    private bool isHero2WeaponKnockbackMaxLevel = false;
    private bool isHero2AttackSpeedMaxLevel = false;
    private bool isHero2WeaponCriticalStrikeChanceMaxLevel = false;
    private bool isHero2WeaponCriticalStrikeMultiplierLevel = false;

    [Header("Hero 3")]
    [HideInInspector] public float Hero3ProjectileDamageLevel;
    [HideInInspector] public float Hero3AttackSpeedLevel;
    [HideInInspector] public float Hero3BarrageQuantityLevel;
    [HideInInspector] public float Hero3ProjectileSpeedLevel;
    //[HideInInspector] public float Hero3TurretProjectileSizeLevel;

    //Vector3 Hero3StartingTurretProjectileSizeLocalScale;

    //private bool isHero3TurretProjectileSizeMaxLevel = false;
    private bool isHero3AttackSpeedMaxLevel = false;
    private bool isHero3BarrageQuantityMaxLevel = false;
    private bool isHero3ProjectileSpeedMaxLevel = false;
   // private bool isHero3TurretMaxQuantityMaxLevel = false;
    //private bool isHero3TurretLifeTimeMaxLevel = false;

    [Header("Hero 4")]
    [HideInInspector] public float Hero4BeamDamageLevel;
    [HideInInspector] public float Hero4BeamQuantityLevel;
    [HideInInspector] public float Hero4BeamSizeLevel;
    [HideInInspector] public float Hero4BeamLengthLevel;

    private bool isHero4BeamLengthMaxLevel = false;
    private bool isHero4BeamSizeMaxLevel = false;
    private bool isHero4BeamQuantityMaxLevel = false;

    [Header("Hero 5")]
    [HideInInspector] public float Hero5ProjectileDamageLevel;
    [HideInInspector] public float Hero5AttackSpeedLevel;
    [HideInInspector] public float Hero5ProjectileSpeedLevel;
    [HideInInspector] public float Hero5ProjectileLifeTimeLevel;
    [HideInInspector] public float Hero5ProjectileQuantityLevel;

    private bool isHero5AttackSpeedMaxLevel = false;
    private bool isHero5ProjectileSpeedMaxLevel = false;
    private bool isHero5ProjectileLifeTimeMaxLevel = false;
    private bool isHero5ProjectileQuantityMaxLevel = false;

    [Header("Hero 6")]
    [HideInInspector] public float Hero6ProjectileDamageLevel;
    [HideInInspector] public float Hero6AttackSpeedLevel;
    [HideInInspector] public float Hero6ProjectileSpeedLevel;
    [HideInInspector] public float Hero6ProjectileSizeLevel;

    private bool isHero6AttackSpeedMaxLevel = false;
    private bool isHero6ProjectileSpeedMaxLevel = false;
    private bool isHero6ProjectileSizeMaxLevel = false;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }



    public void RefreshHeroPowerStatistics(int kto)
    {
        switch (kto)
        {
            case 0:
           
                Hero1LaserDamageUpgrade();
                Hero1LaserKnockbackUpgrade();
                Hero1LaserChainRangeUpgrade();
                Hero1LaserChainsEfficiencyUpgrade();
                Hero1LaserChainsNumberUpgrade();
         
                if (FindObjectOfType<HeroBaseComponent>())
                    FindObjectOfType<HeroBaseComponent>().PlayerEnergySizeRefresh(0);

                HERO1StatsValuesRefresh();
                break;

            case 1:
                SwordDamageUpgrade();
                SwordCritChanceUpgrade();
                SwordCritMultiplierUpgrade();
                SwordKnockbackUpgrade();
                AttackSpeedHERO2();
                if (FindObjectOfType<HeroBaseComponent>())
                    FindObjectOfType<HeroBaseComponent>().PlayerEnergySizeRefresh(1);

                HERO2StatsValuesRefresh();
                break;

            case 2:
                Hero3ProjectileDamageUpgrade();
                Hero3AttackSpeedUpgrade();
                Hero3BarrageQuantityUpgrade();
                Hero3ProjectileSpeedUpgrade();
                //CanoonLifeTimeUpgrade();
                //CanoonMaxNumberUpgrade();
                if (FindObjectOfType<HeroBaseComponent>())
                    FindObjectOfType<HeroBaseComponent>().PlayerEnergySizeRefresh(2);

                HERO3StatsValuesRefresh();
                break;

            case 3:
                BeamDamageUpgrade();
                BeamQuantityUpgrade();
                BeamSizeUpgrade();
                BeamStrengthUpgrade();
                if (FindObjectOfType<HeroBaseComponent>())
                    FindObjectOfType<HeroBaseComponent>().PlayerEnergySizeRefresh(3);

                HERO4StatsValuesRefresh();
                break;

            case 4:
                DamageUpgradeHERO5();
                AttackSpeedUpgradeHERO5();
                ParticleBurstdUpgradeHERO5();
                ParticleLifedUpgradeHERO5();
                ParticleSpeedUpgradeHERO5();
                if (FindObjectOfType<HeroBaseComponent>())
                    FindObjectOfType<HeroBaseComponent>().PlayerEnergySizeRefresh(4);

                HERO5StatsValuesRefresh();
                break;

            case 5:
                DamageUpgradeHERO6();
                AttackSpeedUpgradeHERO6();
                ProjectileSpeedUpgradeHERO6();
                SizeMultiplierUpgradeHERO6();
                if (FindObjectOfType<HeroBaseComponent>())
                    FindObjectOfType<HeroBaseComponent>().PlayerEnergySizeRefresh(5);

                HERO6StatsValuesRefresh();
                break;

        }

    }

    public void Hero1LaserDamageUpgrade()
    {
        heroesManager.Hero1LaserDamageValue = heroesManager.Hero1LaserDamageStartingValue + (0.2f * heroesManager.heroesPowerLevel[0]);
        //HERO1StatsValuesRefresh();
    }

    public void Hero1LaserChainsNumberUpgrade()
    {
        heroesManager.Hero1maxChainsNumber = heroesManager.Hero1maxChainsStartingNumber + (heroesManager.heroesPowerLevel[0] / 4);

        if (heroesManager.Hero1maxChainsNumber >= 5)
        {
            heroesManager.Hero1maxChainsNumber = 5;
            isHero1LaserChainsNumberMaxLevel = true;
        }
        else
        {
            isHero1LaserChainsNumberMaxLevel = false;
        }
    }

    public void Hero1LaserChainsEfficiencyUpgrade()
    {
        heroesManager.Hero1chainEfficiency = heroesManager.Hero1chainStartingEfficiency + (heroesManager.heroesPowerLevel[0] * 0.025f);

        if (heroesManager.Hero1chainEfficiency >= 0.9f)
        {
            heroesManager.Hero1chainEfficiency = 0.9f;
            isHero1LaserChainsEfficiencyMaxLevel = true;
        }
        else
        {
            isHero1LaserChainsEfficiencyMaxLevel = false;
        }
    }

    public void Hero1LaserChainRangeUpgrade()
    {

        heroesManager.Hero1chainRadius = heroesManager.Hero1chainStartingRadius + (heroesManager.heroesPowerLevel[0] * 0.3f);

        if (heroesManager.Hero1chainRadius >= 15f)
        {
            heroesManager.Hero1chainRadius = 15f;
            isHero1LaserChainRangeMaxLevel = true;
        }
        else
        {
            isHero1LaserChainRangeMaxLevel = false;
        }
    }

    public void Hero1LaserKnockbackUpgrade()
    {
        heroesManager.Hero1LaserKnockbackValue = heroesManager.Hero1LaserKnockbackStartingValue + (heroesManager.heroesPowerLevel[0] * 0.05f);

        if (heroesManager.Hero1LaserKnockbackValue >= 1.5f)
        {
            heroesManager.Hero1LaserKnockbackValue = 1.5f;
            isHero1ProjectileKnockbackMaxLevel = true;
        }
        else
        {
            isHero1ProjectileKnockbackMaxLevel = false;
        }
    }

    public void SwordDamageUpgrade()
    {
         
        heroesManager.Hero2WeaponDamageValue = heroesManager.Hero2WeaponDamageStartingValue + (heroesManager.heroesPowerLevel[1] * 2.5f);
        
    }

    public void SwordCritChanceUpgrade()
    {
        heroesManager.Hero2WeaponCriticalStrikeChanceValue = heroesManager.Hero2WeaponCriticalStrikeChanceStartingValue + (heroesManager.heroesPowerLevel[1] * 0.01f);

        if (heroesManager.Hero2WeaponCriticalStrikeChanceValue >= 1f)
        {
            heroesManager.Hero2WeaponCriticalStrikeChanceValue = 1f;
            isHero2WeaponCriticalStrikeChanceMaxLevel = true;
        }
        else
        {
            isHero2WeaponCriticalStrikeChanceMaxLevel = false;
        }
    }

    public void SwordCritMultiplierUpgrade()
    {
        heroesManager.Hero2WeaponCriticalStrikeMultiplierValue = heroesManager.Hero2WeaponCriticalStrikeMultiplierStartingValue + (heroesManager.heroesPowerLevel[1] * 0.04f);

        if (heroesManager.Hero2WeaponCriticalStrikeMultiplierValue >= 2f)
        {
            heroesManager.Hero2WeaponCriticalStrikeMultiplierValue = 2f;
            isHero2WeaponCriticalStrikeMultiplierLevel = true;
        }
        else
        {
            isHero2WeaponCriticalStrikeMultiplierLevel = false;
        }
    }

    public void SwordKnockbackUpgrade()
    {
        heroesManager.Hero2WeaponKnockbackValue = heroesManager.Hero2WeaponKnockbackStartingValue + (heroesManager.heroesPowerLevel[1] * 0.25f);

        if (heroesManager.Hero2WeaponKnockbackValue >= 7f)
        {
            heroesManager.Hero2WeaponKnockbackValue = 7f;
            isHero2WeaponKnockbackMaxLevel = true;
        }
        else
        {
            isHero2WeaponKnockbackMaxLevel = false;
        }
    }

    public void AttackSpeedHERO2()
    {
        heroesManager.Hero2AttackSpeedValue = heroesManager.Hero2AttackSpeedStartingValue * ( 1f - (heroesManager.heroesPowerLevel[1] * 0.03f));

        if (heroesManager.Hero2AttackSpeedValue <= 0.18f)
        {
            heroesManager.Hero2AttackSpeedValue = 0.18f;
            isHero2AttackSpeedMaxLevel = true;
        }
        else
        {
            isHero2AttackSpeedMaxLevel = false;
        }

    }


    public void Hero3ProjectileDamageUpgrade()
    {
         
        heroesManager.Hero3ProjectileDamageValue = heroesManager.Hero3ProjectileStartingDamageValue + (heroesManager.heroesPowerLevel[2] * 1.8f);
          
    }

    public void Hero3AttackSpeedUpgrade()
    {
        heroesManager.Hero3AttackSpeedValue = heroesManager.Hero3StartingAttackSpeedValue * (1f - (heroesManager.heroesPowerLevel[2] * 0.01f));

        if (heroesManager.Hero3AttackSpeedValue <= 0.5f)
        {
            heroesManager.Hero3AttackSpeedValue = 0.5f;
            isHero3AttackSpeedMaxLevel = true;
        }
        else
        {
            isHero3AttackSpeedMaxLevel = false;
        }
           
    }

    public void Hero3BarrageQuantityUpgrade()
    {
        heroesManager.Hero3BarrageQuantity = heroesManager.Hero3StartingBarrageQuantity + (int)(heroesManager.heroesPowerLevel[2] * 0.4f);

        if (heroesManager.Hero3BarrageQuantity >= 12)
        {
            heroesManager.Hero3BarrageQuantity = 12;
            isHero3BarrageQuantityMaxLevel = true;
        }
        else
        {
            isHero3BarrageQuantityMaxLevel = false;
        }
           
    }

    public void Hero3ProjectileSpeedUpgrade()
    {
        heroesManager.Hero3ProjectileSpeedValue = heroesManager.Hero3ProjectileStartingSpeedValue * (1f + (heroesManager.heroesPowerLevel[2] * 0.02f));

        if (heroesManager.Hero3ProjectileSpeedValue >= 40)
        {
            heroesManager.Hero3ProjectileSpeedValue = 40;
            isHero3ProjectileSpeedMaxLevel = true;
        }
        else
        {
            isHero3ProjectileSpeedMaxLevel = false;
        }
           
    }

    /*public void CanoonLifeTimeUpgrade()
    {
        heroesManager.Hero3TurretLifeTimeValue = heroesManager.Hero3TurretLifeTimeStartingValue + (heroesManager.heroesPowerLevel[2] * 0.2f);

        if (heroesManager.Hero3TurretLifeTimeValue >= 15)
        {
            heroesManager.Hero3TurretLifeTimeValue = 15;
            isHero3TurretLifeTimeMaxLevel = true;
        }
        else 
        {
            isHero3TurretLifeTimeMaxLevel = false;
        }
    }
*/
 /*   public void CanoonMaxNumberUpgrade()
    {
        heroesManager.Hero3TurretMaxQuantityValue = heroesManager.Hero3TurretMaxQuantityStartingValue + (heroesManager.heroesPowerLevel[2] / 4);

        if (heroesManager.Hero3TurretMaxQuantityValue >= 5)
        {
            heroesManager.Hero3TurretMaxQuantityValue = 5;
            isHero3TurretMaxQuantityMaxLevel = true;
        }
        else
        {
            isHero3TurretMaxQuantityMaxLevel = false;
        }

      //  heroesManager.Hero3ActiveTurrets.

    }*/

    public void BeamDamageUpgrade()
    {
        heroesManager.Hero4BeamDamageValue = heroesManager.Hero4BeamDamageStartingValue + (heroesManager.heroesPowerLevel[3] * 0.16f);
    }

    public void BeamQuantityUpgrade()
    {

        heroesManager.Hero4BeamQuantityValue = heroesManager.Hero4BeamQuantityStartingValue + (heroesManager.heroesPowerLevel[3] * 1f);

        if (heroesManager.Hero4BeamQuantityValue >= 80)
        {
            heroesManager.Hero4BeamQuantityValue = 80;
            isHero4BeamQuantityMaxLevel = true;
        }
        else
        {
            isHero4BeamQuantityMaxLevel = false;
        }
    }

    public void BeamStrengthUpgrade()
    {
        heroesManager.Hero4BeamLengthValue = heroesManager.Hero4BeamLengthStartingValue + (heroesManager.heroesPowerLevel[3] * 1.6f);

        if (heroesManager.Hero4BeamLengthValue >= 60f)
        {
            heroesManager.Hero4BeamLengthValue = 60f;
            isHero4BeamLengthMaxLevel = true;
        }
        else
        {
            isHero4BeamLengthMaxLevel = false;
        }

    }

    public void BeamSizeUpgrade()
    {

        heroesManager.Hero4BeamSizeValue = heroesManager.Hero4BeamSizeStartingValue + (heroesManager.heroesPowerLevel[3] * 0.025f);
        heroesManager.Hero4BeamTrailSizeValue = heroesManager.Hero4BeamTrailSizeStartingValue + (heroesManager.heroesPowerLevel[3] * 0.025f);

        if (heroesManager.Hero4BeamSizeValue >= 0.8f)
        {
            heroesManager.Hero4BeamSizeValue = 0.8f;
            heroesManager.Hero4BeamTrailSizeValue = 0.8f;
            isHero4BeamSizeMaxLevel = true;
        }
        else
        {
            isHero4BeamSizeMaxLevel = false;
        }
    }

    public void DamageUpgradeHERO5()
    {
        heroesManager.Hero5ProjectileDamageValue = heroesManager.Hero4BeamDamageStartingValue + (heroesManager.heroesPowerLevel[4] * 3.4f);
    }

    public void AttackSpeedUpgradeHERO5()
    {
        heroesManager.Hero5AttackSpeedValue = heroesManager.Hero5AttackSpeedStartingValue * (1f - (heroesManager.heroesPowerLevel[4] * 0.015f));


        if (heroesManager.Hero5AttackSpeedValue <= 0.35f)
        {
            heroesManager.Hero5AttackSpeedValue = 0.35f;
            isHero5AttackSpeedMaxLevel = true;
        }
        else
        {
            isHero5AttackSpeedMaxLevel = false;
        }
    }

    public void ParticleSpeedUpgradeHERO5()
    {

        heroesManager.Hero5ProjectileSpeedMaxValue = heroesManager.Hero5ProjectileSpeedMaxStartingValue + (heroesManager.heroesPowerLevel[4] * 0.5f);

        if (heroesManager.Hero5ProjectileSpeedMaxValue >= 42)
        {
            heroesManager.Hero5ProjectileSpeedMaxValue = 42;
            isHero5ProjectileSpeedMaxLevel = true;
        }
        else
        {
            isHero5ProjectileSpeedMaxLevel = false;
        }

    }

    public void ParticleLifedUpgradeHERO5()
    {
        heroesManager.Hero5ProjectileLifeTimeMaxValue = heroesManager.Hero5ProjectileLifeTimeMaxStartingValue + (heroesManager.heroesPowerLevel[4] * 0.01f);

        if (heroesManager.Hero5ProjectileLifeTimeMaxValue >= 0.55f)
        {
            heroesManager.Hero5ProjectileLifeTimeMaxValue = 0.55f;
            isHero5ProjectileLifeTimeMaxLevel = true;
        }
        else
        {
            isHero5ProjectileLifeTimeMaxLevel = false;
        }

    }
    public void ParticleBurstdUpgradeHERO5()
    {
        heroesManager.Hero5ProjectileQuantityValue = heroesManager.Hero5ProjectileQuantityStartingValue + (heroesManager.heroesPowerLevel[4] / 3);


        if (Hero5ProjectileQuantityLevel >= 22)
        {
            heroesManager.Hero5ProjectileQuantityValue = 22;
            isHero5ProjectileQuantityMaxLevel = true;
        }
        else
        {
            isHero5ProjectileQuantityMaxLevel = false;
        }

    }

    /*  private bool isHero6AttackSpeedMaxLevel = false;
    private bool isHero6ProjectileSpeedMaxLevel = false;
    private bool isHero6ProjectileSizeMaxLevel = false;*/

    public void DamageUpgradeHERO6()
    {
        heroesManager.Hero6ProjectileDamageValue = heroesManager.Hero6ProjectileDamageStartingValue + (heroesManager.heroesPowerLevel[5] * 1.5f);
    }

    public void AttackSpeedUpgradeHERO6()
    {
        heroesManager.Hero6AttackSpeedValue = heroesManager.Hero6AttackSpeedStartingValue * (1f - (heroesManager.heroesPowerLevel[5] * 0.05f));

        if (heroesManager.Hero6AttackSpeedValue < 0.15f)
        {
            heroesManager.Hero6AttackSpeedValue = 0.15f;
            isHero6AttackSpeedMaxLevel = true;
        }
        else
        {
            isHero6AttackSpeedMaxLevel = false;

        }
    }

    public void ProjectileSpeedUpgradeHERO6()
    {
        heroesManager.Hero6ProjectileSpeedValue = heroesManager.Hero6ProjectileSpeedStartingValue + (heroesManager.heroesPowerLevel[5] * 1.8f);

        if (heroesManager.Hero6ProjectileSpeedValue > 100)
        {
            heroesManager.Hero6ProjectileSpeedValue = 100;
            isHero6ProjectileSpeedMaxLevel = true;
        }
        else
        {
            isHero6ProjectileSpeedMaxLevel = false;
        }
    }

    public void SizeMultiplierUpgradeHERO6()
    {

        heroesManager.Hero6ProjectileSizeValue = heroesManager.Hero6ProjectileSizeStartingValue + (heroesManager.heroesPowerLevel[5] * 0.04f);

        if (heroesManager.Hero6ProjectileSizeValue > 2.2f)
        {
            heroesManager.Hero6ProjectileSizeValue = 2.2f;
            isHero6ProjectileSizeMaxLevel = true;
        }
        else
        {
            isHero6ProjectileSizeMaxLevel = false;
        }
    }


    public void TimeScale1()
    {
        Time.timeScale = 1.0f;
        heroesManager.HeroPowerSetRefresh(0, levelManager.heroIndexRolledByStar);
    }


    public void HERO1StatsValuesRefresh()
    {
        levelManager.statsValuesHERO1[0].text = "Damage: \n\n" + heroesManager.Hero1LaserDamageValue;
        levelManager.statsValuesHERO1[5].text = "Chains number: \n\n" + heroesManager.Hero1maxChainsNumber;
        levelManager.statsValuesHERO1[3].text = "Chain range radius: \n\n" + (heroesManager.Hero1chainRadius);
        levelManager.statsValuesHERO1[4].text = "Chain damage efficiency: \n\n" + heroesManager.Hero1chainEfficiency * 100 +"%";
        levelManager.statsValuesHERO1[2].text = "Knockback: \n\n" + heroesManager.Hero1LaserKnockbackValue;
      //  levelManager.statsValuesHERO1[5].text = "Attack speed: \n\n" + (float)Math.Round(heroesManager.Hero1AttackSpeedValue, 3) + " sec";
    }

    public void HERO2StatsValuesRefresh()
    {
        // if(ItemsManager.knockbackWeaknessHERO2 == false)
        //   levelManager.statsValuesHERO2.text = levelManager.damageMiecza + " (+" + itemManager.itemDamageSwordHERO2 + ")\n" + levelManager.knockbackStrengthHERO2 + " (+" + itemManager.itemSwordKnockbackHERO2 + ") \n" + levelManager.critChanceHERO2 + "% (+" + itemManager.itemCritChanceSwordHERO2 + "%)\n" + (levelManager.critMultiplierHERO2 + 1f) + " (+" + itemManager.itemCritMultiplierSwordHERO2 + ") \n" + (float)Math.Round(levelManager.timeToAttack, 3) + " (+" + itemManager.itemAttackSpeedSwordHERO2 * 100 + "%)\n" + levelManager.walkSpeed + "\n" + levelManager.jumpHigh+"\n" + levelManager.damagePlayerMultiplier;
        // else levelManager.statsValuesHERO2.text = levelManager.damageMiecza + " (+"+itemManager.itemDamageSwordHERO2+")\n" + levelManager.knockbackStrengthHERO2 + " (+"+itemManager.itemSwordKnockbackHERO2+") (/ 2) \n" + levelManager.critChanceHERO2 + "% (+"+itemManager.itemCritChanceSwordHERO2+"%)\n" + (levelManager.critMultiplierHERO2 + 1f) + " (+" + itemManager.itemCritMultiplierSwordHERO2 + ") \n" + (float)Math.Round(levelManager.timeToAttack, 3)+" (+"+itemManager.itemAttackSpeedSwordHERO2*100+"%)\n" + levelManager.walkSpeed + "\n" + levelManager.jumpHigh+"\n" + levelManager.damagePlayerMultiplier;

        levelManager.statsValuesHERO2[0].text = "Damage: \n\n" + heroesManager.Hero2WeaponDamageValue;
        levelManager.statsValuesHERO2[1].text = "Knockback: \n\n" + heroesManager.Hero2WeaponKnockbackValue;
        levelManager.statsValuesHERO2[2].text = "Critical strike chance: \n\n" + heroesManager.Hero2WeaponCriticalStrikeChanceValue + " %";
        levelManager.statsValuesHERO2[3].text = "Critical strike multiplier: \n\n" + (heroesManager.Hero2WeaponCriticalStrikeMultiplierValue + 1f);
        levelManager.statsValuesHERO2[4].text = "Attack speed:: \n\n" + (float)Math.Round(heroesManager.Hero2AttackSpeedValue, 3) + " sec";
    }

    public void HERO3StatsValuesRefresh()
    {
        //levelManager.statsValuesHERO3.text = levelManager.canonDamage+" (+"+itemManager.itemCanoonDamageHERO3+")\n"+(float)Math.Round(levelManager.bulletSizeMultiplierHERO3,3)+" (+"+itemManager.itemCanoonBulletSizeHERO3+")\n"+levelManager.maxCanoons+" (+"+itemManager.itemCanoonMaxNumberHERO3+")\n"+levelManager.canoonLifeTime+"s (+"+itemManager.itemCanoonLifeTimeHERO3+"s) \n"+(float)Math.Round(levelManager.canoonAttackSpeed,3)+" (+"+itemManager.itemCanoonAttackSpeedHERO3*100+"%)\n" + levelManager.walkSpeed + "\n" + levelManager.jumpHigh+"\n" + levelManager.damagePlayerMultiplier;

        levelManager.statsValuesHERO3[0].text = "Damage: \n\n" + heroesManager.Hero3ProjectileDamageValue;
        //   levelManager.statsValuesHERO3[1].text = "Projectile size: \n\n" + (float)Math.Round(levelManager.bulletSizeMultiplierHERO3, 3) + " (+" + itemManager.itemCanoonBulletSizeHERO3+ ")";
        levelManager.statsValuesHERO3[2].text = "Projectile quantity: \n\n" + heroesManager.Hero3BarrageQuantity;
        levelManager.statsValuesHERO3[3].text = "ProjectileSpeed: \n\n" + heroesManager.Hero3ProjectileSpeedValue + " s";
        levelManager.statsValuesHERO3[4].text = "Attack speed: \n\n" + (float)Math.Round(heroesManager.Hero3AttackSpeedValue, 3) + " sec";
    }

    public void HERO4StatsValuesRefresh()
    {
        // levelManager.statsValuesHERO4.text = (float)Math.Round(levelManager.damageHERO4, 1) +" (+"+itemManager.itemBeamStrenghtHERO4+ ")\n" + (float)Math.Round(levelManager.beamStartSizeHERO4,1) +" (+"+ itemManager.itemBeamSizeHERO4*100+"%)\n" + levelManager.beamVelocityHERO4 +" ("+ itemManager.itemBeamStrenghtHERO4*100 +"%)\n"+ (levelManager.przeskokQuantityOfBeam * 60 + ( 10 - levelManager.quantityOfBeam) * 6)+" ("+itemManager.itemQuatityHERO4*100+ "%) / s \n" + levelManager.walkSpeed + "\n" + levelManager.jumpHigh + "\n" + levelManager.damagePlayerMultiplier; 

        levelManager.statsValuesHERO4[0].text = "Damage: \n\n" + heroesManager.Hero4BeamDamageValue;
        levelManager.statsValuesHERO4[1].text = "Beams thickness: \n\n" + (float)Math.Round(heroesManager.Hero4BeamSizeValue, 1);
        levelManager.statsValuesHERO4[2].text = "Beams range: \n\n" + heroesManager.Hero4BeamLengthValue;
        levelManager.statsValuesHERO4[3].text = "Beams quantity: \n\n" + heroesManager.Hero4BeamQuantityValue + " /sec";
    }

    public void HERO5StatsValuesRefresh()
    {
        //  levelManager.statsValuesHERO5.text = (float)Math.Round(levelManager.damageHERO5, 1) +" (+" + itemManager.itemDamageHERO5 + ")\n" + (float)Math.Round(levelManager.attackTimerHERO5, 1) + " (+" + itemManager.itemAttackSpeedHERO5 * 100 + "%)\n" + levelManager.startLifeMinHERO5+"-"+levelManager.startLifeMaxHERO5 + " (" + itemManager.itemParticlekLifeHERO5 * 100 + "%)\n" + levelManager.startSpeedMinHERO5+"-"+ levelManager.startSpeedMaxHERO5 + " (" + itemManager.itemParticlekSpeedHERO5 * 100 + "%)\n" + levelManager.burstValueHERO5 + " (+" + itemManager.itemParticlekBurstHERO5 * 100 + "%)\n" + levelManager.walkSpeed + "\n" + levelManager.jumpHigh + "\n" + levelManager.damagePlayerMultiplier;

        levelManager.statsValuesHERO5[0].text = "Damage: \n\n" + (float)Math.Round(heroesManager.Hero5ProjectileDamageValue, 1);
        levelManager.statsValuesHERO5[1].text = "Attack speed: \n\n" + (float)Math.Round(heroesManager.Hero5AttackSpeedValue, 3) + " sec";
        levelManager.statsValuesHERO5[2].text = "Peojectile range: \n\n ~" + ((heroesManager.Hero5ProjectileLifeTimeMinValue + heroesManager.Hero5ProjectileLifeTimeMaxValue) / 2f) * 100;
        levelManager.statsValuesHERO5[3].text = "Projectile quantity: \n\n" + heroesManager.Hero5ProjectileQuantityValue;
        levelManager.statsValuesHERO5[4].text = "Projectile speed: \n\n ~" + ((heroesManager.Hero5ProjectileSpeedMinValue + heroesManager.Hero5ProjectileSpeedMaxValue) / 2f) * 80;
    }
    public void HERO6StatsValuesRefresh()
    {
        //  levelManager.statsValuesHERO6.text = (float)Math.Round(levelManager.damageHERO6, 1) + " (+" + itemManager.itemDamageHERO6 + ")\n" + (float)Math.Round(levelManager.attackSpeedHERO6, 2) + " (+" + itemManager.itemAttackSpeedHERO6 * 100 + "%)\n" + levelManager.projectileSpeedHERO6 + " (" + itemManager.itemProjectileSpeedHERO6 * 100 + "%)\n" + levelManager.sizeMultiplierHERO6 +" (" + itemManager.itemSizeMultiplierHERO6 * 100 + "%)\n" + levelManager.walkSpeed + "\n" + levelManager.jumpHigh + "\n" + levelManager.damagePlayerMultiplier;

        levelManager.statsValuesHERO6[0].text = "Damage: \n\n" + (float)Math.Round(heroesManager.Hero6ProjectileDamageValue, 1);
        levelManager.statsValuesHERO6[1].text = "Attack speed: \n\n" + (float)Math.Round(heroesManager.Hero6AttackSpeedValue, 3) + " sec";
        levelManager.statsValuesHERO6[2].text = "Projectile speed: \n\n" + heroesManager.Hero6ProjectileSpeedValue;
        levelManager.statsValuesHERO6[3].text = "Projectile size: \n\n" + heroesManager.Hero6ProjectileSizeValue;
    }


    /* public void GeneralStatsValuesRefresh()
     {
         levelManager.statsValuesGeneral.text = (2f + levelManager.LvlKosztLife) +"\n"+ (float)Math.Round(levelManager.itemDropChanceMultiplier, 2);

     }*/

    public void OnMouseOvder()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    public void OnMousePress()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress_2");
    }

}
