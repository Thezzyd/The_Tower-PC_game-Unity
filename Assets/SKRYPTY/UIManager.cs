using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private FirebaseManager firebaseManager;

    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField birthYearRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    [Header("Reset Password")]
    public TMP_InputField emailResetPasswordField;
    public TMP_Text warningResetPasswordTextn;

    [Header("No connection")]
    public TMP_Text warningNoConnectionText;

    [Header("All panels")]
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject resetPasswordUI;
    public GameObject noConnectionUI;
    public GameObject userDataUI;
 //   public GameObject scoreboardUI;
    public GameObject statisticsUI;
    public GameObject mainMenuUI;
    public GameObject optionsUI;
    public GameObject mapsUI;

    [Header("Main menu panel - objects")]
    public GameObject playTextShatredEffect;
    public GameObject optionsTextShatredEffect;
    public GameObject quitTextShatredEffect;
    public GameObject statisticsTextShatredEffect;
    public GameObject spiderBigBloodEffect;

    public Transform playTextPosition;
    public Transform optionsTextPosition;
    public Transform quitTextPosition;
    public Transform statisticsTextPosition;
    public Transform spiderPosition;

    public GameObject playTextGameObject;
    public GameObject optionsTextGameObject;
    public GameObject quitTextGameObject;
    public GameObject statisticsTextGameObject;
    public GameObject spiderGameObject;

    [Header("Map panel - objects")]
    public ParticleSystem mapPlayButtonHoverEffect;

    public bool isFirstScreenLoaded;
    public float firstScreenTimeout;

    private void Awake()
    {
        isFirstScreenLoaded = false;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        Time.timeScale = 1.0f;
    }

    void Start()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();
       // firebaseManager.RunCheckIfLoginDataRemembered();
    }

    private void FixedUpdate()
    {
        if (!isFirstScreenLoaded && firebaseManager.isFirebaseInitialized)
        {
            firebaseManager.RunCheckIfLoginDataRemembered();
            isFirstScreenLoaded = true;
        }

        firstScreenTimeout += Time.fixedDeltaTime;


        if (!isFirstScreenLoaded && firstScreenTimeout >= 10.0f)
        {
            Debug.Log("FIRST SCREEN TIMEOUT");
            ClearScreen();
            LoginScreen();
        }

    }

    public void LoginButton()
    {
        firebaseManager.LoginButton();
    }

    public void RegisterButton()
    {
        firebaseManager.RegisterButton();
    }

    public void RetryConnectionButton()
    {
        firebaseManager.RetryConnectionButton();
    }

    public void ResetPasswordButton()
    {
        firebaseManager.ResetPasswordButton();
    }

    public void LogoutButton()
    {
        firebaseManager.SignOutButton();
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


    public void OnButtonMouseOver()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    public void OnButtonMousePress()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress");
    }


    public void PlayTextShatred()
    {
        var playEffect = Instantiate(playTextShatredEffect, playTextPosition.position, playTextShatredEffect.transform.rotation);
        Destroy(playEffect, 5f);
        var optionsEffect = Instantiate(optionsTextShatredEffect, optionsTextPosition.position, optionsTextShatredEffect.transform.rotation);
        Destroy(optionsEffect, 5f);
        var quitEffect = Instantiate(quitTextShatredEffect, quitTextPosition.position, quitTextShatredEffect.transform.rotation);
        Destroy(quitEffect, 5f);
        var statisticsEffect = Instantiate(statisticsTextShatredEffect, statisticsTextPosition.position, statisticsTextShatredEffect.transform.rotation);
        Destroy(statisticsEffect, 5f);
        var spiderEffect = Instantiate(spiderBigBloodEffect, spiderPosition.position, spiderBigBloodEffect.transform.rotation);
        Destroy(spiderEffect, 5f);

        playTextGameObject.SetActive(false);
        optionsTextGameObject.SetActive(false);
        quitTextGameObject.SetActive(false);
        spiderGameObject.SetActive(false);
        statisticsTextGameObject.SetActive(false);

    }

    public void ActivateMainMenuGameObjects()
    {
        playTextGameObject.SetActive(true);
        optionsTextGameObject.SetActive(true);
        quitTextGameObject.SetActive(true);
        spiderGameObject.SetActive(true);
        statisticsTextGameObject.SetActive(true);
    }

    public void PointerEnterMapPlayButton() {
        mapPlayButtonHoverEffect.Play();
    }

    public void PointerExitMapPlayButton(){
        mapPlayButtonHoverEffect.Stop();
    }


    //Functions to change the login screen UI

    public void ClearScreen() //Turn off all screens
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        resetPasswordUI.SetActive(false);
        userDataUI.SetActive(false);
        noConnectionUI.SetActive(false);
  //      scoreboardUI.SetActive(false);
        statisticsUI.SetActive(false);
        mainMenuUI.SetActive(false);
        optionsUI.SetActive(false);
    }

    public IEnumerator LoginScreen(float timeInSeconds) //Back button
    {
        ClearScreen();
        loginUI.SetActive(true);

        yield return new WaitForSeconds(timeInSeconds);
        print("Transition in " + timeInSeconds + " secnds");
    }

    public void LoginScreen() //Back button
    {
        ClearScreen();
        loginUI.SetActive(true);
    }

    public void NoConnectionScreen() //Back button
    {
        ClearScreen();
        noConnectionUI.SetActive(true);
    }

    public void ResetPasswordScreen() //Back button
    {
        ClearScreen();
        resetPasswordUI.SetActive(true);
    }

    public void RegisterScreen() // Regester button
    {
        ClearScreen();
        registerUI.SetActive(true);
    }

    public void UserDataScreen() //Logged in
    {
        ClearScreen();
        userDataUI.SetActive(true);
    }

    public void StatisticsScreen() 
    {
        ClearScreen();
        statisticsUI.SetActive(true);
    }

 //   public void ScoreboardScreen() //Scoreboard button
   // {
  //      ClearScreen();
   //     scoreboardUI.SetActive(true);
   // }

    public void MainMenuScreen() //Scoreboard button
    {
        ClearScreen();
        mainMenuUI.SetActive(true);
    }

    public void OptionsScreen() //Scoreboard button
    {
        ClearScreen();
        optionsUI.SetActive(true);
    }

    public void NoConnectionScreenDelayed()
    {
        Invoke("NoConnectionScreen", 2.0f);
    }

    public void LoginScreenDelayed() //Back button
    {
        Invoke("LoginScreen", 2.0f);
    }

    public void ResetPasswordScreenDelayed()
    {
        Invoke("ResetPasswordScreen", 2.0f);
    }

    public void RegisterScreenDelayed() // Regester button
    {
        Invoke("RegisterScreen", 2.0f);
    }

    public void StatisticsScreenDelayed() 
    {
        Invoke("StatisticsScreen", 2.0f);
    }

    public void UserDataScreenDelayed() //Logged in
    {
        Invoke("UserDataScreen", 2.0f);
    }

    public void ScoreboardScreenDelayed() //Scoreboard button
    {
        Invoke("ScoreboardScreen", 2.0f);
    }

    public void OptionsScreenDelayed() //Options button
    {
        Invoke("OptionsScreen", 2.0f);
    }

    public void MainMenuScreenDelayed() //MainMenu button
    {
        Invoke("MainMenuScreen", 2.0f);
    }

    public void SaveData()
    {
        SaveSystem.SaveLevel();
    }
}
