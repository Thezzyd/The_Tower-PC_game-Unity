
using UnityEngine;
using UnityEngine.UI;

public class HeroesStatisticsCanvasManager : MonoBehaviour
{
    private HeroesManager heroesManager;
    public GameObject inventoryCanvas;
    private LevelManager levelManager;
    private FirebaseManager firebaseManager;

    public  GameObject DmgButton;
    public  GameObject WalkButton;
    public  GameObject JumpButton;
    public  GameObject PortalHealthButton;
    public  GameObject ItemButton;
    public  GameObject LifeButton;

    public GameObject[] HEROpanels;

    public ParticleSystem[] clickEffect;

    public Material heroInfoModelMaterial;

    [HideInInspector] public string zalamanieLini1 = "\n";
    [HideInInspector] public string zalamanieLini2 = "\n";
    [HideInInspector] public string zalamanieLini3 = "\n";
    [HideInInspector] public string zalamanieLini4 = "\n";
    [HideInInspector] public string zalamanieLini5 = "\n";
    [HideInInspector] public string zalamanieLini6 = "\n";

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        firebaseManager = FindObjectOfType<FirebaseManager>();
       // TextRefresh();
    }

 /*   public void RefreshValuesOfTextsInStatsPanel()
    {
        levelManager.RefreshLvlPointsInfo();
    }*/

    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }

    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }

    public void CloseinventoryCanvas()
    {
        if (inventoryCanvas.activeSelf)
            inventoryCanvas.SetActive(false);
    }

    public void AssignDamageMultiplierPoints()
    {
               
        if (firebaseManager.GetAvailablePoints() > 0)
        {
            clickEffect[0].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            firebaseManager.IncreaseStrengthPoints(1);
            heroesManager.AsignLvlPointsOnStart();
        //    levelManager.RefreshLvlPointsInfo();
         //   levelManager.CheckLvlPoints();

         //   levelManager.SaveLevel();
          //  TextRefresh();
        }
    }

    //SET MAX VALUES FOR ALL OF THEM

    public void AssigEssenceFromStarPoints()
    {

        if (firebaseManager.GetAvailablePoints() > 0)
        {
            clickEffect[2].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            firebaseManager.IncreaseStarEssencePoints(1);
            heroesManager.AsignLvlPointsOnStart();
          //  levelManager.RefreshLvlPointsInfo();
           // levelManager.CheckLvlPoints();

            //   levelManager.SaveLevel();
          //  TextRefresh();
        }
    }

    public void AssigStarTimerPoints()
    {

        if (firebaseManager.GetAvailablePoints() > 0)
        {
            clickEffect[3].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            firebaseManager.IncreaseStarTimerPoints(1);
            heroesManager.AsignLvlPointsOnStart();
           // levelManager.RefreshLvlPointsInfo();
         //  levelManager.CheckLvlPoints();

            //   levelManager.SaveLevel();
          //  TextRefresh();
        }
    }

    public void AssigTimerPenaltyPoints()
    {

        if (firebaseManager.GetAvailablePoints() > 0)
        {
            clickEffect[4].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            firebaseManager.IncreaseTimerPenaltyPoints(1);
            heroesManager.AsignLvlPointsOnStart();
        //    levelManager.RefreshLvlPointsInfo();
          //  levelManager.CheckLvlPoints();

            //   levelManager.SaveLevel();
          //  TextRefresh();
        }
    }


    public void ResetAbilityPoints()
    {
        clickEffect[5].Emit(50);
        FindObjectOfType<AudioManager>().Play("ButtonPress");
        firebaseManager.ResetUserAbilityPoints();
        heroesManager.AsignLvlPointsOnStart();
      //  levelManager.RefreshLvlPointsInfo();
     //   levelManager.CheckLvlPoints();

       // TextRefresh();
    }



    /*
    public void AssignPortalHealthPoints()
    {
      

        if (FirebaseManager.availablePoints > 0f)
        {
            clickEffect[4].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            FirebaseManager.basePortalHealthPoints += 1;
            levelManager.AsignLvlPointsOnStart();
            levelManager.RefreshLvlPointsInfo();
            levelManager.CheckLvlPoints();

            levelManager.SaveLevel();
            TextRefresh();

        }
    }*/

    public void AssignWalkingSpeedPoints()
    {
     

        if (firebaseManager.GetAvailablePoints() > 0f && (12f +(firebaseManager.GetWalkSpeedPoints() * 0.1f)) < 22f)
        {
            clickEffect[1].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            firebaseManager.IncreaseWalkSpeedPoints(1);
            heroesManager.AsignLvlPointsOnStart();
          //  levelManager.RefreshLvlPointsInfo();
          //  levelManager.CheckLvlPoints();

           // levelManager.SaveLevel();
         //   TextRefresh();
        }
    }

  /*  public void AssignJumpStrengthPoints()
    {
      

        if (firebaseManager.GetAvailablePoints() > 0f && (30f + (firebaseManager.GetJumpPowerPoints() * 0.1f)) < 38f)
        {
            clickEffect[2].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            firebaseManager.IncreaseJumpPowerPoints(1);
            heroesManager.AsignLvlPointsOnStart();
            levelManager.RefreshLvlPointsInfo();
            levelManager.CheckLvlPoints();
           // levelManager.SaveLevel();
            TextRefresh();

        }
    }*/
    /*
    public void AssignAdditionalItemChanceDropPoints()
    {
        if (FirebaseManager.availablePoints > 0f )
        {
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            FirebaseManager.additionalItemDropChancePoints += 1;
            levelManager.AsignLvlPointsOnStart();
            levelManager.RefreshLvlPointsInfo();
            levelManager.CheckLvlPoints();
            levelManager.SaveLevel();
            TextRefresh();

        }
    }*/
/*
    public void AssignMaxLifesPoints()
    {
       

        if (FirebaseManager.availablePoints >= 10f && FirebaseManager.maxLivesPoints < 70f)
        {
            clickEffect[3].Emit(150);
            FindObjectOfType<AudioManager>().Play("ButtonPress");

            FirebaseManager.maxLivesPoints+= 10;
            levelManager.AsignLvlPointsOnStart();
            levelManager.RefreshLvlPointsInfo();
            levelManager.CheckLvlPoints();
            levelManager.SaveLevel();
            TextRefresh();

        }
    }*/

    public void SaveLevel()
    {
      //  SaveSystem.SaveLevel(levelManager);
    }
  //  public void TextRefresh()
   // {
        //DONT WORK TO REPAIR!!!!!!!!!!!!!!!!!!


     //   if ((12f + (firebaseManager.GetWalkSpeedPoints() * 0.1f)) > 22f) { zalamanieLini3 = " (max) \n"; WalkButton.GetComponent<Button>().interactable = false; }
     //   if ((30f + (firebaseManager.GetJumpPowerPoints() * 0.1f)) > 38f) { zalamanieLini4 = " (max) \n"; JumpButton.GetComponent<Button>().interactable = false; }
      // if (FirebaseManager.maxLivesPoints >= 70f) { zalamanieLini6 = " (max) \n"; LifeButton.GetComponent<Button>().interactable = false; }

       // levelManager.generalStatsValues.text = MainMenu.damageMultipierPoints + zalamanieLini1 + MainMenu.basePortalHealthPoints + zalamanieLini2 + MainMenu.walkSpeedPoints + zalamanieLini3 + MainMenu.jumpStrengthPoints + zalamanieLini4 + MainMenu.additionalItemDropChancePoints + zalamanieLini5 + (LevelManager.maxHealth - 3) + zalamanieLini6 ;
      //  levelManager.generalStatsValues[0].text = firebaseManager.GetStrengthPoints().ToString();
      //  levelManager.generalStatsValues[1].text = firebaseManager.GetWalkSpeedPoints().ToString();
     //   levelManager.generalStatsValues[2].text = firebaseManager.GetJumpPowerPoints().ToString();
      //  levelManager.generalStatsValues[3].text = (LevelManager.maxHealth - 3).ToString();
       // levelManager.generalStatsValues[4].text = FirebaseManager.basePortalHealthPoints.ToString();

        /*   if ((12f + (MainMenu.walkSpeedPoints * 0.1f)) > 22f)
       {
           WalkButton.GetComponent<Button>().interactable = false;
           levelManager.generalStatsValues.text = levelManager.damagePlayerMultiplier + "\n" + levelManager.startingCash + "\n" + levelManager.walkSpeed + " (max) \n" + levelManager.jumpHigh + "\n" + levelManager.itemDropChanceMultiplier + "\n" + LevelManager.maxHealth;

       }
       if (MainMenu.maxLivesPoints >= 70f)
       {
           LifeButton.GetComponent<Button>().interactable = false;
           levelManager.generalStatsValues.text = levelManager.damagePlayerMultiplier + "\n" + levelManager.startingCash + "\n" + levelManager.walkSpeed + " \n" + levelManager.jumpHigh+ "\n"+ levelManager.itemDropChanceMultiplier+"\n"+LevelManager.maxHealth+" (max)";

       }

       if (( 30f + (MainMenu.jumpStrengthPoints * 0.1f)) > 38f)
       {
           JumpButton.GetComponent<Button>().interactable = false;
           levelManager.generalStatsValues.text = levelManager.damagePlayerMultiplier + "\n" + levelManager.startingCash + "\n" + levelManager.walkSpeed + "\n " + levelManager.jumpHigh + " (max)" + "\n" + levelManager.itemDropChanceMultiplier + "\n" + LevelManager.maxHealth;

       }
       if( (12f + (MainMenu.walkSpeedPoints * 0.1f)) >22f && (30f + (MainMenu.jumpStrengthPoints * 0.1f) >38f))
           levelManager.generalStatsValues.text = levelManager.damagePlayerMultiplier + "\n" + levelManager.startingCash + "\n"  + levelManager.walkSpeed + " (max) \n" + levelManager.jumpHigh + " (max)" + "\n" + levelManager.itemDropChanceMultiplier + "\n" + LevelManager.maxHealth;
       */


    //}

    public void DeactivateHeroCanvas()
    {

        HEROpanels[0].SetActive(false);
        HEROpanels[1].SetActive(false);
        HEROpanels[2].SetActive(false);
        HEROpanels[3].SetActive(false);
        HEROpanels[4].SetActive(false);
        HEROpanels[5].SetActive(false);

    }
    /* 
     * 
     * float intensity = 4.2f;
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
            */

    public void SetHeroInfoModelMaterialColor()
    {
        float intensity = 4.2f;
        float factor = Mathf.Pow(2, intensity);

        if (HEROpanels[0].activeSelf)
        {
            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.415f * factor, 0.074f * factor, 0.027f * factor, 0));
        }
        else if (HEROpanels[1].activeSelf)
        {
            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.360f * factor, 0.047f * factor, 0.357f * factor, 0));
        }
        else if (HEROpanels[2].activeSelf)
        {
            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.090f * factor, 0.215f * factor, 0.407f * factor, 0));
        }
        else if (HEROpanels[3].activeSelf)
        {
            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.070f * factor, 0.090f * factor, 0.474f * factor, 0));
        }
        else if (HEROpanels[4].activeSelf)
        {
            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.027f * factor, 0.247f * factor, 0.047f * factor, 0));
        }
        else if (HEROpanels[5].activeSelf)
        {
            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.255f * factor, 0.227f * factor, 0.027f * factor, 0));
        }
    }

    public void EnableActiveHeroPanel()
    {
        DeactivateHeroCanvas();
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();

        float intensity = 4.2f;
        float factor = Mathf.Pow(2, intensity);

        heroesManager.HeroPowerSetRefresh(0, levelManager.heroIndexRolledByStar);

        if (levelManager.heroIndexRolledByStar == 1)
        {
            HEROpanels[0].SetActive(true);
            FindObjectOfType<SkillsUpgradeManager>().HERO1StatsValuesRefresh();

            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.415f * factor, 0.074f * factor, 0.027f * factor, 0));
        }
        else if (levelManager.heroIndexRolledByStar == 2)
        {
            HEROpanels[1].SetActive(true);

            FindObjectOfType<SkillsUpgradeManager>().HERO2StatsValuesRefresh();

            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.360f * factor, 0.047f * factor, 0.357f * factor, 0));
        }
        else if (levelManager.heroIndexRolledByStar == 3)
        {
            HEROpanels[2].SetActive(true);

            FindObjectOfType<SkillsUpgradeManager>().HERO3StatsValuesRefresh();

            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.090f * factor, 0.215f * factor, 0.407f * factor, 0));
        }
        else if (levelManager.heroIndexRolledByStar == 4)
        {
            HEROpanels[3].SetActive(true);

            FindObjectOfType<SkillsUpgradeManager>().HERO4StatsValuesRefresh();

            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.070f * factor, 0.090f * factor, 0.474f * factor, 0));
        }
        else if (levelManager.heroIndexRolledByStar == 5)
        {
            HEROpanels[4].SetActive(true);

            FindObjectOfType<SkillsUpgradeManager>().HERO5StatsValuesRefresh();

            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.027f * factor, 0.247f * factor, 0.047f * factor, 0));
        }
        else if (levelManager.heroIndexRolledByStar == 6)
        {
            HEROpanels[5].SetActive(true);

            FindObjectOfType<SkillsUpgradeManager>().HERO6StatsValuesRefresh();

            heroInfoModelMaterial.SetColor("_GlowColor", new Color(0.255f * factor, 0.227f * factor, 0.027f * factor, 0));
        }

    }

    public void SoundOnMouseOver()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    public void SoundOnMouseClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress_2");
    }


}
