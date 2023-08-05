using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HeroBaseComponent : MonoBehaviour
{
    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private TowerSpawnManager towerSpawnManager;

    private Rigidbody2D rb;
    private Animator anim;

    public bool isAndroidAttacking;
    public Transform spawnPocisku;
    public GameObject heroComponentHolder;
    public Transform spriteHolderTransform;

    public Material heroSpriteMaterial;

    public Transform groundCheck;
    public LayerMask WhatIsGround;

    public ParticleSystem energy;
    private ParticleSystem.MainModule energySize;
    private ParticleSystem.MainModule energyColor;
    private ParticleSystem.MainModule orbCollectedColor;

    public ParticleSystem orbCollectedEffect;

    public UnityEngine.Rendering.Universal.Light2D heroPointLight;

    bool isPressedLeft;
    bool isPressedRight;
    bool isPressedJump;
    bool isPressedDoubleJump;

    public int whichHero;

    [Header("Hero Components")]
    public Hero1Component hero1Component;
    public Hero2Component hero2Component;
    public Hero3Component hero3Component;
    public Hero4Component hero4Component;
    public Hero5Component hero5Component;
    public Hero6Component hero6Component;

    [Header("Hero Skills objects")]
    public GameObject hero1SkillObject;
    public GameObject hero2SkillObject;
    public GameObject hero3SkillObject;
    public GameObject hero4SkillObject;
    public GameObject hero5SkillObject;
    public GameObject hero6SkillObject;


    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
     
        energySize = energy.main;

        DisableAllHeroComponents();

        whichHero = levelManager.heroIndexRolledByStar;
        orbCollectedColor = orbCollectedEffect.main;
        energyColor = energy.main;

        switch (whichHero)
        {
            case 1: hero1Component.enabled = true; name = "Hero1Prefab"; break;
            case 2: hero2Component.enabled = true; ; name = "Hero2Prefab"; break;
            case 3: hero3Component.enabled = true; ; name = "Hero3Prefab"; break;
            case 4: hero4Component.enabled = true; ; name = "Hero4Prefab"; break;
            case 5: hero5Component.enabled = true; ; name = "Hero5Prefab"; break;
            case 6: hero6Component.enabled = true; ; name = "Hero6Prefab"; break;
        }

        PlayerSpriteMaterialColorRefresh();
        PlayerEnergyColorRefresh();
        PlayerOrbCollectedEffectColorRefresh(levelManager.heroIndexRolledByStar);
        EnableCorrectSkillObject();
        heroesManager.SetCameraAndEnemyTargetPoint(transform);
        heroesManager.heroTrailEffect.transform.position = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(-0.18f, -0.15f, 0);
      /*  heroesManager.EssenceMaterialColorRefresh();
        heroesManager.ChestMaterialsColorRefresh();
        heroesManager.FogMaterialColorRefresh();
        heroesManager.BackgroundSpriteColorRefresh();
        heroesManager.EnviromentMaterialsColorRefresh();*/
    }

    public void DisableAllHeroComponents()
    {
        hero1Component.enabled = false;
        hero2Component.enabled = false;
        hero3Component.enabled = false;
        hero4Component.enabled = false;
        hero5Component.enabled = false;
        hero6Component.enabled = false;

    }

    void FixedUpdate()
    {

        heroesManager.SetPlayerBehaviourAndEffects(spriteHolderTransform, rb, anim);
        heroesManager.isHeroGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.55f, 0.15f), 0.0f, WhatIsGround);

      //  Physics2D.OverlapBox
        if (isPressedJump) { heroesManager.HeroJump(rb, anim); isPressedJump = false; }
        if (isPressedDoubleJump) { heroesManager.HeroDoubleJump(rb, anim); isPressedDoubleJump = false; }
        if (isPressedLeft) { heroesManager.HeroGoLeft(rb); }
        if (isPressedRight) { heroesManager.HeroGoRight(rb); }

        heroesManager.DisableMicroVelocityOnPLayer(rb);

        heroComponentHolder.transform.position = spawnPocisku.position;

    }

    void Update()
    {

        heroesManager.SetCameraAndEnemyTargetPoint(transform);
        if (levelManager.isPlayerAllowed)
        {

            if (Input.GetKeyDown(KeyCode.Space) && heroesManager.groundedSafeDelayTimer > 0.0f)
                 isPressedJump = true;


            if (Input.GetKeyDown(KeyCode.Space) && heroesManager.isNormalJump && !heroesManager.isDoubleJump)
                isPressedDoubleJump = true;


            if (Input.GetMouseButton(0))
                isAndroidAttacking = true;
            else isAndroidAttacking = false;



            if (Input.GetKeyDown(KeyCode.A) && heroesManager.isHeroAlive)
                isPressedLeft = true;

            if (Input.GetKeyUp(KeyCode.A) && heroesManager.isHeroAlive)
                isPressedLeft = false;

            if (Input.GetKeyDown(KeyCode.D) && heroesManager.isHeroAlive)
                isPressedRight = true;

            if (Input.GetKeyUp(KeyCode.D) && heroesManager.isHeroAlive)
                isPressedRight = false;

            if (Input.GetKeyUp(KeyCode.A))
                heroesManager.StopMovingHero(rb);

            if (Input.GetKeyUp(KeyCode.D))
                heroesManager.StopMovingHero(rb);

        }
        else
        {
            rb.velocity = new Vector2(0f, 1f);
        }
    }


    public void PlayerEnergySizeRefresh(int whichHero)
    {
       // energySize.startSize = new ParticleSystem.MinMaxCurve(heroesManager.minHeroEnergySize[whichHero], heroesManager.maxHeroEnergySize[whichHero]);
    }

    public void PlayOneShotWalkingSfx()
    {
        levelManager.audioManager.PlayOneShoot("Walking", transform.position);
    }

    /*   public void PlayerLightColorRefresh()
       {
           switch (levelManager.heroIndexRolledByStar)
           {
               case 1: heroPointLight.color = new Color(0.556f, 0, 0.043f); break;
               case 2: heroPointLight.color = new Color(0.431f, 0, 0.443f); break;
               case 3: heroPointLight.color = new Color(0, 0.188f, 0.43f); break;
               case 4: heroPointLight.color = new Color(0.147f, 0, 0.519f); break;
               case 5: heroPointLight.color = new Color(0, 0.247f, 0.106f); break;
               case 6: heroPointLight.color = new Color(0.51f, 0.378f, 0); break;
           }
       }*/


    public void PlayerEnergyColorRefresh()
    {


        switch (levelManager.heroIndexRolledByStar)
        {
            case 1: energyColor.startColor = new Color(1, 0.203f, 0.244f); break;
            case 2: energyColor.startColor = new Color(0.954f, 0.174f, 1); break;
            case 3: energyColor.startColor = new Color(0.2946778f, 0.388329f, 0.4622642f, 0.6f); break;
            case 4: energyColor.startColor = new Color(0.2323125f, 0.2128363f, 0.8490566f); break;
            case 5: energyColor.startColor = new Color(0.264f, 1, 0.212f); break;
            case 6: energyColor.startColor = new Color(0.3396226f, 0.3255232f, 0.1876621f); break;
        }
    }

    public void PlayerSpriteMaterialColorRefresh()
    {
        float intensity = 4.2f;
        float factor = Mathf.Pow(2, intensity);

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1: heroSpriteMaterial.SetColor("_GlowColor", new Color(0.415f * factor, 0.074f * factor, 0.027f * factor, 0)); break;
            case 2: heroSpriteMaterial.SetColor("_GlowColor", new Color(0.360f * factor, 0.047f * factor, 0.357f * factor, 0)); break;
            case 3: heroSpriteMaterial.SetColor("_GlowColor", new Color(0.090f * factor, 0.215f * factor, 0.407f * factor, 0)); break;
            case 4: heroSpriteMaterial.SetColor("_GlowColor", new Color(0.070f * factor, 0.090f * factor, 0.474f * factor, 0)); break;
            case 5: heroSpriteMaterial.SetColor("_GlowColor", new Color(0.027f * factor, 0.247f * factor, 0.047f * factor, 0)); break;
            case 6: heroSpriteMaterial.SetColor("_GlowColor", new Color(0.255f * factor, 0.227f * factor, 0.027f * factor, 0)); break;
        }
    }

    public void PlayerOrbCollectedEffectColorRefresh(int whichHero)
    {
        switch (whichHero)
        {
            case 1: orbCollectedColor.startColor = new Color(1, 0.390f, 0.344f, 0.6f); break;
            case 2: orbCollectedColor.startColor = new Color(0.924f, 0.382f, 1, 0.6f); break;
            case 3: orbCollectedColor.startColor = new Color(0.372f, 0.628f, 1, 0.6f); break;
            case 4: orbCollectedColor.startColor = new Color(0.505f, 0.372f, 1, 0.6f); break;
            case 5: orbCollectedColor.startColor = new Color(0.391f, 1, 0.446f, 0.6f); break;
            case 6: orbCollectedColor.startColor = new Color(0.99f, 1, 0.419f, 0.6f); break;
        }
    }

    public void EnableCorrectSkillObject()
    {
        hero1SkillObject.SetActive(false);
        hero2SkillObject.SetActive(false);
        hero3SkillObject.SetActive(false);
        hero4SkillObject.SetActive(false);
        hero5SkillObject.SetActive(false);
        hero6SkillObject.SetActive(false);

        switch (levelManager.heroIndexRolledByStar)
        {
            case 1: hero1SkillObject.SetActive(true); break;
            case 2: hero2SkillObject.SetActive(true); break;
            case 3: hero3SkillObject.SetActive(true); break;
            case 4: hero4SkillObject.SetActive(true); break;
            case 5: hero5SkillObject.SetActive(true); break;
            case 6: hero6SkillObject.SetActive(true); break;
        }
    }
}
