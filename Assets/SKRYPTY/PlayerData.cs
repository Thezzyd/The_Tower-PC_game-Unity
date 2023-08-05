using UnityEngine;
using UnityEngine.SceneManagement;
/*
public class PlayerData : MonoBehaviour
{
    [HideInInspector] public static float accountLevel = 1f;
    [HideInInspector] public static float accountExperiencePoints = 0f;
    [HideInInspector] public static string accountBirthDate = "";
  //  [HideInInspector] public static float expToLvlUp = 100f;

    [HideInInspector] public static float strengthPoints = 0f;
  //  [HideInInspector] public static float basePortalHealthPoints = 0f;
    [HideInInspector] public static float walkSpeedPoints = 0f;
    [HideInInspector] public static float jumpPowerPoints = 0f;

  //  [HideInInspector] public static float maxLivesPoints = 0f;
 //   [HideInInspector] public static float additionalItemDropChancePoints = 0f;

   

    [HideInInspector] public static float bestScore = 0f;
    [HideInInspector] public static float bestStars = 0f;
    [HideInInspector] public static float bestKills = 0f;
    [HideInInspector] public static float bestPlayTime = 0f;
    [HideInInspector] public static float bestTowerHight = 0f;

    public void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Wieza_01");
    }

    public void PlayAgain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Wieza_01");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Wieza_Menu");
    }

    public void BackToGame()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    /*
    public void LoadLevel()
    {
        LevelData data = SaveSystem.LoadLevel();
        if (data != null)
        {
            accountLevel = data.accountLevel;
            accountExperiencePoints = data.accountExp;
            expToLvlUp = data.expToLvlUp;

            PlayerData.strengthPoints = data.damageMultipierPoints;
            PlayerData.basePortalHealthPoints = data.basePortalHealthPoints;
            PlayerData.walkSpeedPoints = data.walkSpeedPoints;
            PlayerData.jumpPowerPoints = data.jumpStrengthPoints;
            PlayerData.maxLivesPoints = data.maxLivesPoints;
            PlayerData.additionalItemDropChancePoints = data.additionalItemDropChancePoints;

            PlayerData.bestScore = data.highScore;
            PlayerData.bestStars = data.highStars;
            PlayerData.bestKills = data.highKills;
            PlayerData.bestPlayTime = data.highTime;
            PlayerData.bestTowerHight = data.highBosses;
        }
    }*/
    /*
    public void OnMouseOvder()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    public void OnMousePress()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress_2");
    }

  

}
*/