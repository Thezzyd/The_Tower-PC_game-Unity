
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverFunctions : MonoBehaviour
{

    public GameObject[] HEROpanels;
    private HeroesManager heroesManager;
    private LevelManager levelManager;

    private void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
    }

    public void PlayGame()
    {
       // Debug.Log("Przejscie sceny");
        SceneManager.LoadScene("Wieza_01");
       
    }

    public void PlayAgain()
    {
       // Debug.Log("Restart poziomu");
        SceneManager.LoadScene("Wieza_01");
       

    }

    public void QuitGame()
    {
      //  Debug.Log("Quit");
        Application.Quit();
    }

    public void BackToMenu()
    {

       // Debug.Log("Powrot do glownego MENU");
        SceneManager.LoadScene("Wieza_Menu");
        // ExpBarRefresh();
    }
    public void BackToGame()
    {
            Time.timeScale = 1.0f; 
    }

    public void Pause()
    {
        //  Debug.Log("Pauza");
    
        Time.timeScale = 0.0f;
        FindObjectOfType<AudioManager>().StopEveryActiveSoundEffect();
    }

    public void PowerBarChangeHERO1()
    {
        heroesManager.HeroPowerSetRefresh(0, 1);
        heroesManager.EssenceMaterialColorRefresh(1);

        FindObjectOfType<SkillsUpgradeManager>().HERO1StatsValuesRefresh();
    }
    public void PowerBarChangeHERO2()
    {
        heroesManager.HeroPowerSetRefresh(0, 2);
        FindObjectOfType<SkillsUpgradeManager>().HERO2StatsValuesRefresh();
        heroesManager.EssenceMaterialColorRefresh(2);
    }
    public void PowerBarChangeHERO3()
    {
        heroesManager.HeroPowerSetRefresh(0, 3);
        FindObjectOfType<SkillsUpgradeManager>().HERO3StatsValuesRefresh();
        heroesManager.EssenceMaterialColorRefresh(3);
    }
    public void PowerBarChangeHERO4()
    {
        heroesManager.HeroPowerSetRefresh(0, 4);
        FindObjectOfType<SkillsUpgradeManager>().HERO4StatsValuesRefresh();
        heroesManager.EssenceMaterialColorRefresh(4);
    }
    public void PowerBarChangeHERO5()
    {
        heroesManager.HeroPowerSetRefresh(0, 5);
        FindObjectOfType<SkillsUpgradeManager>().HERO5StatsValuesRefresh();
        heroesManager.EssenceMaterialColorRefresh(5);
    }
    public void PowerBarChangeHERO6()
    {
        heroesManager.HeroPowerSetRefresh(0, 6);
        FindObjectOfType<SkillsUpgradeManager>().HERO6StatsValuesRefresh();
        heroesManager.EssenceMaterialColorRefresh(6);
    }

    public void EnableActivePowerBar()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();

        heroesManager.HeroPowerSetRefresh(0, levelManager.heroIndexRolledByStar);
        heroesManager.EssenceMaterialColorRefresh(levelManager.heroIndexRolledByStar);

        /*  if (levelManager.heroIndexRolledByStar == 1)
          {
              heroesManager.HeroPowerSetRefresh(0, levelManager.heroIndexRolledByStar);
          }
          else if (levelManager.heroIndexRolledByStar == 2)
          {
              heroesManager.HeroPowerSetRefresh(0, 5);

          }
          else if (levelManager.heroIndexRolledByStar == 3)
          {
              heroesManager.HeroPowerSetRefresh(0, 5);

          }
          else if (levelManager.heroIndexRolledByStar == 4)
          {
              heroesManager.HeroPowerSetRefresh(0, 5);

          }
          else if (levelManager.heroIndexRolledByStar == 5)
          {
              heroesManager.HeroPowerSetRefresh(0, 5);

          }
          else if (levelManager.heroIndexRolledByStar == 6)
          {
              heroesManager.HeroPowerSetRefresh(0, 6);

          }*/
    }

}
