using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables


    //User Data variables
    [Header("CollectUserData")]
    public TMP_InputField towerHightField;
    public TMP_InputField scoreField;
    public TMP_InputField starsField;
    public TMP_InputField playTimeField;
    public TMP_InputField killsField;
    public TMP_InputField rowsNumberField;
    public TMP_InputField timestampField;
    public GameObject scoreElement;
    public Transform scoreboardContent;
    private Animator registerTextAnim;
    private Animator loginTextAnim;
    private Animator resetPasswordTextAnim;
    private Animator noConnectionTextAnim;
    public bool isUsernameValid;

    //Remembered auntenthication data
    [HideInInspector] public static string rememberedEmail = "";
    [HideInInspector] public static string rememberedPassword = "";

    //User Data from database
    [HideInInspector] private int accountLevel = 1;                                  //do obliczenia
    [HideInInspector] private int accountExperiencePoints = 0;
    [HideInInspector] private int accountExperirncePointsToLevelUp = 0;              // do obliczenia
    [HideInInspector] private string accountBirthDate = "";

    [HideInInspector] private int strengthPoints = 0;
    [HideInInspector] private int walkSpeedPoints = 0;
    [HideInInspector] private int jumpPowerPoints = 0;
    [HideInInspector] private int starTimerPoints = 0;
    [HideInInspector] private int starEssencePoints = 0;
    [HideInInspector] private int timerPenaltyPoints = 0;

    [HideInInspector] private int availablePoints = 0;                               // do obliczenia

    [HideInInspector] private int bestScore = 0;
    [HideInInspector] private int bestStars = 0;
    [HideInInspector] private int bestKills = 0;
    [HideInInspector] private int bestPlayTime = 0;
    [HideInInspector] private float bestTowerHight = 0f;

    private int allGamesCount = 0;
    private int allGamesPlayTime = 0;
    private int allGamesTowerHightReached = 0;
    private int allGamesScore = 0;


    private static FirebaseManager instanceOfThisClass = null;
    private Scene currentlyActiveSceneInGame;

    public List<GameData> userGamesDataList;
    public List<string> testSprDaty;
    public bool isUserDataListLoaded;

    public List<string> achivementsOfUserList;
    public List<float> achivementsOfUserCalculationsList;
    public List<float> userAchievementsTimeOfPlay;
    public List<float> userAchievementsPlayedGames;

    public List<UserAchievementData>[] userAchievementDataList;
    public AchievementManager achievementManager;
    public List<AgeCorelationData> ageCorelationDataList;
    //public bool isArchivementCalculations
    //  public 


    public bool isFirebaseInitialized;

    void Awake()
    {
        // string tokenFCM = SystemInfo.deviceType != DeviceType.Handheld ? "" : await Firebase.Messaging.FirebaseMessaging.GetTokenAsync();


        isFirebaseInitialized = false;

        if (instanceOfThisClass == null)
        {
            instanceOfThisClass = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceOfThisClass != this)
        {
            Destroy(this.gameObject);
            return;
        }

        InitializeFirebase();

        userGamesDataList = new List<GameData>();

        //Check that all of the necessary dependencies for Firebase are present on the system
     

        currentlyActiveSceneInGame = SceneManager.GetActiveScene();

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;



    }

    private void InitializeFirebase()
    {
        //   yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesCount.IsCompleted);

       
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                // InitializeFirebase();
                Debug.Log("Setting up Firebase Auth");
                //Set the authentication instance object
                auth = FirebaseAuth.DefaultInstance;
                DBreference = FirebaseDatabase.DefaultInstance.RootReference;
                isFirebaseInitialized = true;
                // RunCheckIfLoginDataRemembered();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

       
    }

    /*
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }*/


    /*  void Start()
      {
          StartCoroutine(CheckIfLoginDataRemembered(2.0f));
      }
      */

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // return if not the start calling scene
        if (string.Equals(scene.path, this.currentlyActiveSceneInGame.path))
        {

            return;
        }

      
        Debug.Log("On scene reload data refresh");
        PullFreshDataFromDatabase();

    }

    public void PullFreshDataFromDatabase()
    {
        StartCoroutine(LoadUserData());
    }

    public void IncreaseExperience(int expPointsToAdd)
    {
        //  StartCoroutine(LoadUserData());

        if (accountExperiencePoints < accountExperiencePoints + expPointsToAdd)
        {
            accountExperiencePoints += expPointsToAdd;
            GetExperienceToLevelUp();

            StartCoroutine(UpdateUserDataExperiencePoints(User.UserId, accountExperiencePoints));
        }
        else
        {
            Debug.Log("PRoba odjecia expa!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    public void ResetUserAbilityPoints()
    {

        StartCoroutine(UpdateUserDataResetAbilityPoints());
        strengthPoints = 0;
        jumpPowerPoints = 0;
        walkSpeedPoints = 0;
        starTimerPoints = 0;
        starEssencePoints = 0;
        timerPenaltyPoints = 0;


    }

    public void IncreaseStrengthPoints(int strengtPointsToAdd)
    {
        strengthPoints += strengtPointsToAdd;
        StartCoroutine(UpdateUserDataStrengthPoints(User.UserId, strengthPoints));
    }
    public void IncreaseStarEssencePoints(int starEssencePointsToAdd)
    {
        starEssencePoints += starEssencePointsToAdd;
        StartCoroutine(UpdateUserDataStarEssencePoints(User.UserId, starEssencePoints));
    }

    public void IncreaseTimerPenaltyPoints(int timerPenaltyPointsToAdd)
    {
        timerPenaltyPoints += timerPenaltyPointsToAdd;
        StartCoroutine(UpdateUserDataTimerPenaltyPoints(User.UserId, timerPenaltyPoints));
    }

    public void IncreaseStarTimerPoints(int starTimerPointsToAdd)
    {
        starTimerPoints += starTimerPointsToAdd;
        StartCoroutine(UpdateUserDataStarTimerPoints(User.UserId, starTimerPoints));
    }

    public void IncreaseWalkSpeedPoints(int walkSpeedPointsToAdd)
    {
        walkSpeedPoints += walkSpeedPointsToAdd;
        StartCoroutine(UpdateUserDataWalkSpeedPoints(User.UserId, walkSpeedPoints));
    }

    public void IncreaseJumpPowerPoints(int jumpPowerPointsToAdd)
    {
        jumpPowerPoints += jumpPowerPointsToAdd;
        StartCoroutine(UpdateUserDataJumpPowerPoints(User.UserId, jumpPowerPoints));
    }

    public void WriteGameData(int score, int stars, int towerHight, int kills, int playTime, string mapName)
    {
        StartCoroutine(UpdateGameData(ServerValue.Timestamp, score, towerHight, kills, playTime, stars, mapName));
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public int GetBestStars()
    {
        return bestStars;
    }

    public int GetBestKills()
    {
        return bestKills;
    }

    public int GetBestPlayTime()
    {
        return bestPlayTime;
    }

    public float GetBestTowerHight()
    {
        return bestTowerHight;
    }


    public int GetAvailablePoints()
    {
        availablePoints = (GetLevelInt() - 1) * 3 - (GetStrengthPoints() + GetWalkSpeedPoints() + GetJumpPowerPoints() + GetStarTimerPoints() + GetTimerPenaltyPoints() + GetStarEssencePoints());

        return availablePoints;
    }

    public int GetExperience()
    {
        return accountExperiencePoints;
    }

    public int GetStrengthPoints()
    {
        return strengthPoints;
    }

    public int GetTimerPenaltyPoints()
    {
        return timerPenaltyPoints;
    }

    public int GetStarEssencePoints()
    {
        return starEssencePoints;
    }



    public int GetStarTimerPoints()
    {
        return starTimerPoints;
    }

    public int GetWalkSpeedPoints()
    {
        return walkSpeedPoints;
    }

    public int GetJumpPowerPoints()
    {
        return jumpPowerPoints;
    }

    public float GetLevelFloat()
    {
        float y = (float)accountExperiencePoints;
        float a = 75f;
        float b = 175f;
        float c = -250f - 2f * y;

        float result = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        accountLevel = (int)Mathf.Floor(result);

        return result;
    }

    public int GetLevelInt()
    {
        GetLevelFloat();
        return accountLevel;
    }

    public int GetExperienceToLevelUp()
    {

        float currentLevelInt = (float)GetLevelInt();
        float expToLvlUp = (75f / 2f) * (currentLevelInt + 1f) * (currentLevelInt + 1f) + (175f / 2f) * (currentLevelInt + 1f) - 125f;

        accountExperirncePointsToLevelUp = Mathf.RoundToInt(expToLvlUp);

        return accountExperirncePointsToLevelUp;

    }

    public int GetExperienceToLevelUp(int level)
    {

        float expToLvlUp = (75f / 2f) * (float)(level + 1) * (float)(level + 1) + (175f / 2f) * (float)(level + 1) - 125f;

        return Mathf.RoundToInt(expToLvlUp);

    }



    public void SaveData()
    {
        SaveSystem.SaveLevel();
    }


    public void ClearLoginFeilds()
    {
        UIManager.instance.emailLoginField.text = "";
        UIManager.instance.passwordLoginField.text = "";
        UIManager.instance.warningLoginText.text = "";
    }
    public void ClearRegisterFeilds()
    {
        UIManager.instance.usernameRegisterField.text = "";
        UIManager.instance.emailRegisterField.text = "";
        UIManager.instance.birthYearRegisterField.text = "";
        UIManager.instance.passwordRegisterField.text = "";
        UIManager.instance.passwordRegisterVerifyField.text = "";
        UIManager.instance.warningRegisterText.text = "";
    }

    public void WriteUserData() {
        Debug.Log("Account Level: " + accountLevel);
        Debug.Log("Account Experience: " + accountExperiencePoints);
        Debug.Log("User Birth Year: " + accountBirthDate);

        Debug.Log("Account Strength Points: " + strengthPoints);
        Debug.Log("Account Walk Speed Points: " + walkSpeedPoints);
        Debug.Log("Account Jump Power Points: " + jumpPowerPoints);
        Debug.Log("Account Star Timer Points: " + starTimerPoints);
        Debug.Log("Account Star Timer Points: " + starEssencePoints);
        Debug.Log("Account Star Timer Points: " + timerPenaltyPoints);
        Debug.Log("Account Aailable Points: " + availablePoints);       // do obliczenia

    }

    public void LoginButton()
    {
        StartCoroutine(Login(UIManager.instance.emailLoginField.text, UIManager.instance.passwordLoginField.text, true));
    }

    public void RunCheckIfLoginDataRemembered()
    {
        Debug.Log("weszloo dobrze 1");
        CheckIfLoginDataRemembered();
        Debug.Log("weszloo dobrze 3");
    }

    private void CheckIfLoginDataRemembered( )
    {
        Debug.Log("weszloo dobrze 2");

      //  yield return new WaitForSeconds(timeDelay);

        Debug.Log("Check if remembered... ");
        LevelData data = SaveSystem.LoadLevel();

        if (data != null)
        {
            rememberedEmail = data.email;
            rememberedPassword = data.password;

            OptionsManager.sfxVolume = data.sfxVolume;
            OptionsManager.musicVolume = data.musicVolume;
            OptionsManager.screenShake = data.shakeScreen;

            FindObjectOfType<AudioManager>().SetMusicVolumeMenu(OptionsManager.musicVolume);
            FindObjectOfType<AudioManager>().SetSfxVolumeMenu(OptionsManager.sfxVolume);

            Debug.Log("Data != null ...");
        }

        if (rememberedPassword == null || rememberedEmail == null)
        {
            Debug.Log("dd1");
            // Activate Login Screen
            UIManager.instance.ClearScreen();
            UIManager.instance.LoginScreen();
        }
        else if (rememberedPassword.Equals("") || rememberedEmail.Equals(""))
        {
            Debug.Log("dd2");
            // Activate Login Screen  
            UIManager.instance.ClearScreen();
            UIManager.instance.LoginScreen();
        }
        else
        {
            Debug.Log("dd3");
            //Login And Activate Main Menu Screen
            StartCoroutine(Login(rememberedEmail, rememberedPassword, false));
            //    UIManager.instance.MainMenuScreen();
        }
        Debug.Log("dd4");
    }

    /*  private IEnumerator CheckIfLoginDataRemembered(float timeDelay)
      {
          Debug.Log("weszloo dobrze 2");

          yield return new WaitForSeconds(timeDelay);

          Debug.Log("Check if remembered... ");
          LevelData data = SaveSystem.LoadLevel();

          if (data != null)
          {
              rememberedEmail = data.email;
              rememberedPassword = data.password;

              OptionsManager.sfxVolume = data.sfxVolume;
              OptionsManager.musicVolume = data.musicVolume;
              OptionsManager.screenShake = data.shakeScreen;

              FindObjectOfType<AudioManager>().SetMusicVolumeMenu(OptionsManager.musicVolume);
              FindObjectOfType<AudioManager>().SetSfxVolumeMenu(OptionsManager.sfxVolume);

              Debug.Log("Data != null ...");
          }

          if (rememberedPassword == null || rememberedEmail == null)
          {
              // Activate Login Screen
              UIManager.instance.LoginScreen();
          }
          else if (rememberedPassword.Equals("") || rememberedEmail.Equals(""))
          {
              // Activate Login Screen
              UIManager.instance.LoginScreen();
          }
          else
          {
              //Login And Activate Main Menu Screen
              StartCoroutine(Login(rememberedEmail, rememberedPassword, false));
              //    UIManager.instance.MainMenuScreen();
          }

      }*/

    private void RememberLoginData()
    {
        if (UIManager.instance.passwordLoginField.text == null || UIManager.instance.emailLoginField.text == null || UIManager.instance.emailLoginField.text.Equals("") || UIManager.instance.passwordLoginField.text.Equals(""))
        {
            //Nic nie rob
        }
        else
        {
            rememberedEmail = UIManager.instance.emailLoginField.text;
            rememberedPassword = UIManager.instance.passwordLoginField.text;
            SaveData();
        }

    }

    public void RegisterButton()
    {
        StartCoroutine(Register(UIManager.instance.emailRegisterField.text, UIManager.instance.passwordRegisterField.text, UIManager.instance.usernameRegisterField.text, UIManager.instance.birthYearRegisterField.text));
    }

    public void SignOutButton()
    {
        auth.SignOut();
        UIManager.instance.LoginScreenDelayed();
        ClearRegisterFeilds();
        ClearLoginFeilds();
        rememberedEmail = "";
        rememberedPassword = "";
        SaveData();

        Debug.Log("User id: " + User.UserId);
    }

    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    public void ResetPasswordButton()
    {
        StartCoroutine(ResetPassword(UIManager.instance.emailResetPasswordField.text));
    }

    private IEnumerator ResetPassword(string _email)
    {
        var ResetPasswordTask = auth.SendPasswordResetEmailAsync(_email);

        yield return new WaitUntil(predicate: () => ResetPasswordTask.IsCompleted);

        if (ResetPasswordTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ResetPasswordTask.Exception}");
            FirebaseException firebaseEx = ResetPasswordTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Ressetting Password Failed!";
            Debug.Log("Error Code: " + errorCode);
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            UIManager.instance.warningResetPasswordTextn.text = message;
            try
            {
                resetPasswordTextAnim = GameObject.FindGameObjectWithTag("ResetPasswordTextLogs").GetComponent<Animator>();
                resetPasswordTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        }
        else
        {
            Debug.Log("Password has been resetted successfully...");
            UIManager.instance.warningResetPasswordTextn.text = "Success! Check your email to finalize resetting";
            UIManager.instance.emailResetPasswordField.text = "";
            try
            {
                resetPasswordTextAnim = GameObject.FindGameObjectWithTag("ResetPasswordTextLogs").GetComponent<Animator>();
                resetPasswordTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }

        }

    }

    public void NoInternetConnection()
    {
        if (!UIManager.instance.noConnectionUI.activeSelf)
        {
            UIManager.instance.ClearScreen();
            UIManager.instance.NoConnectionScreen();
        }
      
            UIManager.instance.warningNoConnectionText.text = "Check your internet connection and try again...";
          
            try
            {
                noConnectionTextAnim = GameObject.FindGameObjectWithTag("NoConnectionTextLogs").GetComponent<Animator>();
                noConnectionTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        

    }

    public void RetryConnectionButton()
    {
        isFirebaseInitialized = false;
        UIManager.instance.isFirstScreenLoaded = false;
        UIManager.instance.firstScreenTimeout = 0.0f;
        InitializeFirebase();
    }

    private IEnumerator Login(string _email, string _password, bool _isSourceButton)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            Debug.Log("Error Code: "+errorCode);
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.WebInternalError:
                    message = "No internet connection";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            Debug.Log(message);

            if (message.Equals("Login Failed!"))
            {
                NoInternetConnection();
                yield break ;
            }

            UIManager.instance.warningLoginText.text = message;
            try
            {
                loginTextAnim = GameObject.FindGameObjectWithTag("LoginTextLogs").GetComponent<Animator>();
                loginTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            UIManager.instance.warningLoginText.text = "";
            UIManager.instance.confirmLoginText.text = "Logged In";
            try
            {
                loginTextAnim = GameObject.FindGameObjectWithTag("LoginTextLogs").GetComponent<Animator>();
                loginTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
            StartCoroutine(LoadUserData());
            StartCoroutine(LoadBestGameData());
            RememberLoginData();



            if (!_isSourceButton)
            {
                UIManager.instance.ClearScreen();
                UIManager.instance.MainMenuScreen(); // Change to user data UI immidatly 
            }
            else
            {
                UIManager.instance.MainMenuScreenDelayed(); // Change to user data UI with animation delay
                UIManager.instance.loginUI.GetComponent<Animator>().SetTrigger("changePanel");
            }

            yield return new WaitForSeconds(2);
            ClearLoginFeilds();
            ClearRegisterFeilds();
        }
    }

    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    private IEnumerator CheckIsUsernameValid(string _username)
    {
        isUsernameValid = true;
        var DBTask = DBreference.Child("users").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                if (childSnapshot.Child("username").Value.ToString().Equals(_username))
                {
                    isUsernameValid = false; 
                }
            }

        }
    }

    private IEnumerator Register(string _email, string _password, string _username, string _birthYear)
    {
        try 
        {
            bool boolBox = int.Parse(_birthYear) > System.DateTime.Now.Year;
        }
        catch (FormatException e)
        {
            UIManager.instance.warningRegisterText.text = "Birth date must be an integer";
            registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
            registerTextAnim.SetTrigger("refresh");

            yield break;
        }

        yield return CheckIsUsernameValid(_username);


        if (_username == "")
        {
            //If the username field is blank show a warning
            UIManager.instance.warningRegisterText.text = "Missing Username";
            try{
                registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                registerTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        }
        else if (!isUsernameValid)
        {
            UIManager.instance.warningRegisterText.text = "Username already in use";
            try
            {
                registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                registerTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        }
        else if (!IsValidEmail(_email))
        {
            UIManager.instance.warningRegisterText.text = "Given email is in incorrect format";
            try
            {
                registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                registerTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        }
        else if (_birthYear == "" || int.Parse(_birthYear) < 1910 || int.Parse(_birthYear) > System.DateTime.Now.Year)
        {
            //If the birthname field is blank show a warning
            UIManager.instance.warningRegisterText.text = "Missing Or Wrong Birth Year";
            try
            {
                registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                registerTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        }
        else if (UIManager.instance.passwordRegisterField.text != UIManager.instance.passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            UIManager.instance.warningRegisterText.text = "Password Does Not Match!";
            try
            {
                registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                registerTextAnim.SetTrigger("refresh");
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Pominieto blad, animator nie aktywny");
            }
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }

                UIManager.instance.warningRegisterText.text = message;
                try
                {
                    registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                    registerTextAnim.SetTrigger("refresh");
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("Pominieto blad, animator nie aktywny");
                }
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        UIManager.instance.warningRegisterText.text = "Username Set Failed!";
                        try
                        {
                            registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                            registerTextAnim.SetTrigger("refresh");
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.Log("Pominieto blad, animator nie aktywny");
                        }
                    }
                    else
                    {
                        //Username is now set
                        //Now set user data and send to database and return to login screen
                        UIManager.instance.warningRegisterText.text = "Successfully registred!";
                        try
                        {
                            registerTextAnim = GameObject.FindGameObjectWithTag("RegistrationTextLogs").GetComponent<Animator>();
                            registerTextAnim.SetTrigger("refresh");
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.Log("Pominieto blad, animator nie aktywny");
                        }
                        SaveUserDataAfterRegistration();
                        UIManager.instance.LoginScreenDelayed();
                        UIManager.instance.registerUI.GetComponent<Animator>().SetTrigger("changePanel");

                        yield return new WaitForSeconds(1);
                        ClearRegisterFeilds();
                        ClearLoginFeilds();
                        //  StartCoroutine(coroutine);

                    }
                }
            }
        }
    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    public IEnumerator GetBestPlayerGame()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();

        var DBTask = DBreference.Child("scoreboard").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            if (snapshot != null)
            {
                bestScore = int.Parse(snapshot.Child("score").Value.ToString());
                bestStars = int.Parse(snapshot.Child("stars").Value.ToString());
                bestKills = int.Parse(snapshot.Child("kills").Value.ToString());
                bestPlayTime = int.Parse(snapshot.Child("play_time").Value.ToString());
                bestTowerHight = int.Parse(snapshot.Child("tower_hight").Value.ToString());
            }
            else
            {
                bestScore = 0;
                bestStars = 0;
                bestKills = 0;
                bestPlayTime = 0;
                bestTowerHight = 0;
            }

        
        }

          
    }



    private IEnumerator LoadScoreboardData()
    {
        var DBTask = DBreference.Child("scoreboard").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            try
            {
                scoreboardContent = GameObject.FindGameObjectWithTag("ScoreboardContent").GetComponent<RectTransform>();
            }
            catch (NullReferenceException e)
            {
                yield break;
            }
                foreach (Transform child in scoreboardContent.transform)
                {
              
                    Destroy(child.gameObject);
              
               }
          //  }
          //  catch (MissingReferenceException e)
          //  {
                //do nothing
          //  }

            int iterator = 0;
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                int towerHight = int.Parse(childSnapshot.Child("tower_hight").Value.ToString());
                int stars = int.Parse(childSnapshot.Child("stars").Value.ToString());
                int kills = int.Parse(childSnapshot.Child("kills").Value.ToString());
                int playTime = int.Parse(childSnapshot.Child("play_time").Value.ToString());
                double timestamp = double.Parse(childSnapshot.Child("timestamp").Value.ToString());

              
                iterator++;

                bool isLoggedUserRow = false;
                if (childSnapshot.Key.Equals(User.UserId))
                    isLoggedUserRow = true;

            //    print("przed instantiate...");
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, score, towerHight, stars, kills, iterator, isLoggedUserRow);
            }

          //  UIManager.instance.ScoreboardScreen();
        }
    }

    public void GetUserGameData()
    {
        StartCoroutine(LoadAllUserGamesData());

      //  return userGamesDataList;
    }


    private IEnumerator LoadAllUserGamesData()
    {
        var DBTask = DBreference.Child("game_data").Child(User.UserId).OrderByChild("timestamp").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            userGamesDataList = new List<GameData>();
            int tak = 0;

            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                //  userGamesDataList.Add(new GameData("1", "2", "3", int.Parse(childSnapshot.Child("score").Value.ToString()), 5,6,7,8888888,9));
                //  Debug.Log("tak:::: "+tak);
             //   try
            //    {
                    GameData oneGameRecord = new GameData();
                    oneGameRecord.UserId = User.UserId;
                    oneGameRecord.GameId = "44";

                    if (childSnapshot.Child("map_name").Value != null)
                        oneGameRecord.MapName = childSnapshot.Child("map_name").Value.ToString();
                    else
                        oneGameRecord.MapName = "No_name";

                if (childSnapshot.Child("score").Value != null)
                    oneGameRecord.Score = int.Parse(childSnapshot.Child("score").Value.ToString());
                else
                    continue; // ignorujemy gry gdzie score był równy  0 pkt
                 //   oneGameRecord.Score = 0;

                if (childSnapshot.Child("kills").Value != null)
                    oneGameRecord.Kills = int.Parse( childSnapshot.Child("kills").Value.ToString() );
                else
                    oneGameRecord.Kills = 0;

                if (childSnapshot.Child("tower_hight").Value != null)
                    oneGameRecord.Towerhight = int.Parse(childSnapshot.Child("tower_hight").Value.ToString());
                else
                    continue; // ignorujemy gry gdzie tower hight był równy  0 pkt
                    //   oneGameRecord.Towerhight = 0;

                if (childSnapshot.Child("stars").Value != null)
                    oneGameRecord.Stars = int.Parse(childSnapshot.Child("stars").Value.ToString());
                else
                    oneGameRecord.Stars = 0;

                oneGameRecord.Timestamp = double.Parse( childSnapshot.Child("timestamp").Value.ToString() );

                if (childSnapshot.Child("play_time").Value != null)
                    oneGameRecord.PlayTime = int.Parse(childSnapshot.Child("play_time").Value.ToString());
                else
                    oneGameRecord.PlayTime = 0;

                oneGameRecord.ConvertTimestampAsDateTime();
                testSprDaty.Add(oneGameRecord.GameDateTime.ToString());

                userGamesDataList.Add(oneGameRecord);     
            }

            isUserDataListLoaded = true;

        }
    }
   
    private IEnumerator LoadUserData()
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            accountBirthDate = snapshot.Child("birth_date").Value.ToString();
            accountExperiencePoints = int.Parse(snapshot.Child("experience_points").Value.ToString());
            accountExperirncePointsToLevelUp = GetExperienceToLevelUp();
            accountLevel = GetLevelInt();

            jumpPowerPoints = int.Parse(snapshot.Child("jump_power_points").Value.ToString());
           // accountLevel = int.Parse(snapshot.Child("level").Value.ToString());
            strengthPoints = int.Parse(snapshot.Child("strength_points").Value.ToString());
            walkSpeedPoints = int.Parse(snapshot.Child("walk_speed_points").Value.ToString());

            try
            {
                starTimerPoints = int.Parse(snapshot.Child("star_timer_points").Value.ToString());
            }
            catch (NullReferenceException e)
            {
                StartCoroutine(UpdateUserDataStarTimerPoints(User.UserId, 0));
            }
             
            try
            {
                starEssencePoints = int.Parse(snapshot.Child("star_essence_points").Value.ToString());
            }
            catch (NullReferenceException e)
            {
                StartCoroutine(UpdateUserDataStarEssencePoints(User.UserId, 0));
            }
            
            try
            {
                timerPenaltyPoints = int.Parse(snapshot.Child("timer_penalty_points").Value.ToString());
            }
            catch (NullReferenceException e)
            {
                StartCoroutine(UpdateUserDataTimerPenaltyPoints(User.UserId, 0));
            }




            Debug.Log("User data loaded...");

         //   StartCoroutine(WriteAchivementTestRecord());
            StartCoroutine(LoadAchievementData());

        }
    }


    private IEnumerator LoadBestGameData()
    {
        var DBTask = DBreference.Child("scoreboard").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            if (snapshot.Exists)
            {
                bestKills = int.Parse(snapshot.Child("kills").Value.ToString());
                bestPlayTime = int.Parse(snapshot.Child("play_time").Value.ToString());
                bestScore = int.Parse(snapshot.Child("score").Value.ToString());
                bestStars = int.Parse(snapshot.Child("stars").Value.ToString());
                bestTowerHight = int.Parse(snapshot.Child("tower_hight").Value.ToString());
                Debug.Log("Best game data loaded...");
            }
         }
    }

    private IEnumerator LoadUserGamesStatisticsData()
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            if (snapshot.Exists)
            {
                allGamesCount = int.Parse(snapshot.Child("all_games_count").Value.ToString());
                allGamesPlayTime = int.Parse(snapshot.Child("all_games_play_time").Value.ToString());
                allGamesTowerHightReached = int.Parse(snapshot.Child("all_games_tower_hight_reached").Value.ToString());
             
                Debug.Log("User games statistics data loaded...");
            }
        }
    }


    //GAME DATA POST METHODES AND SCOREBOARD UPDATE IF NEEDED

    private IEnumerator SetTestGameDataCorutine() {
        //testowanie z statycznych pól przesyłu danych do bazy

        object timestamp = null;
        if (timestampField.text.Equals("") || timestampField.text == null)
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            double cur_time = Math.Floor((double)(System.DateTime.UtcNow - epochStart).TotalMilliseconds);
            
            timestampField.text = cur_time.ToString();
            timestamp = ServerValue.Timestamp;
        }
        else
        {
            timestamp = long.Parse( timestampField.text);
        }

        int howManyDataRows = int.Parse( rowsNumberField.text);

        for (int i = 0; i < howManyDataRows; i++)
        {
           // object _timestamp = null;
         /*   try
            {
                 _timestamp = long.Parse(timestamp.ToString()) - (i * 86400000);
            }catch(Exception e)
            {
                _timestamp = ServerValue.Timestamp;
                timestamp = _timestamp;
            }*/

            int _score = (int) Mathf.Floor( float.Parse(scoreField.text) * (UnityEngine.Random.Range(0.8f, 1.2f) * (1f - ((float)i/100f)) ));
            int _towerHight = (int)Mathf.Floor(float.Parse(towerHightField.text) * (UnityEngine.Random.Range(0.8f, 1.2f) * (1f - ((float)i / 100f))));
            int _kills = (int)Mathf.Floor(float.Parse(killsField.text) * (UnityEngine.Random.Range(0.8f, 1.2f) * (1f - ((float)i / 100f))));
            int _playTime = (int)Mathf.Floor(float.Parse(playTimeField.text) * (UnityEngine.Random.Range(0.8f, 1.2f) * (1f - ((float)i / 100f))));
            int _stars = (int)Mathf.Floor(float.Parse(starsField.text) * (UnityEngine.Random.Range(0.8f, 1.2f) * (1f - ((float)i / 100f))));

          yield return UpdateGameData(timestamp, _score, _towerHight, _kills, _playTime, _stars, "testPush");
        }

      //  Debug.Log("Wyloczone...");
    }

    public void SetTestGameData()
    {
        StartCoroutine(SetTestGameDataCorutine());
    }


    private IEnumerator UpdateGameData(object _timestamp, int _score, int _towerHight, int _kills, int _playTime, int _stars, string _mapName)
    {
        //Set the currently logged in user data about last played game 
        var newKey = DBreference.Child("game_data").Child(User.UserId).Push().Key;
        var DBTaskTimestamp = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("timestamp").SetValueAsync(_timestamp);
      
            yield return new WaitUntil(predicate: () => DBTaskTimestamp.IsCompleted);

            if (DBTaskTimestamp.Exception != null)  {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskTimestamp.Exception}");
            }

        var DBTaskScore = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("score").SetValueAsync(_score);

            yield return new WaitUntil(predicate: () => DBTaskScore.IsCompleted);

            if (DBTaskScore.Exception != null) {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskScore.Exception}");
            }

        var DBTaskTowerHight = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("tower_hight").SetValueAsync(_towerHight);

            yield return new WaitUntil(predicate: () => DBTaskTowerHight.IsCompleted);

            if (DBTaskTowerHight.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerHight.Exception}");
            }

        var DBTaskTowerKills = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("kills").SetValueAsync(_kills);

            yield return new WaitUntil(predicate: () => DBTaskTowerKills.IsCompleted);

            if (DBTaskTowerKills.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerKills.Exception}");
            }

        var DBTaskTowerPlayTime = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("play_time").SetValueAsync(_playTime);

            yield return new WaitUntil(predicate: () => DBTaskTowerPlayTime.IsCompleted);

            if (DBTaskTowerPlayTime.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerPlayTime.Exception}");
            }

        var DBTaskTowerStars = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("stars").SetValueAsync(_stars);

            yield return new WaitUntil(predicate: () => DBTaskTowerStars.IsCompleted);

            if (DBTaskTowerStars.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerStars.Exception}");
            }

        var DBTaskTowerMapName = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("map_name").SetValueAsync(_mapName);

        yield return new WaitUntil(predicate: () => DBTaskTowerStars.IsCompleted);

        if (DBTaskTowerStars.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerStars.Exception}");
        }

        yield return UpdateUserDataAllGamesStatistics(1, _playTime, _towerHight, _score, _timestamp, newKey);

        yield return CheckScoreboardData(User.DisplayName, _timestamp, _score, _towerHight, _kills, _playTime, _stars, _mapName);

     
        
    }

    /* private List<string> CheckIfAchivementGained(int _towerHight)
     {

     }*/

    /* private IEnumerator WriteAchivementTestRecord()
     {
         var DBTask_Test= DBreference.Child("achievement").Child(User.UserId).Child("test_name").SetValueAsync("test_value");

         yield return new WaitUntil(predicate: () => DBTask_Test.IsCompleted);

         if (DBTask_Test.Exception != null)
         {
             Debug.LogWarning(message: $"Failed to register task with {DBTask_Test.Exception}");
         }
     }*/

   
    private IEnumerator LoadAchievementData()
    {
     //   for(int i = 0; i < )
     /*   var DBTask_Test = DBreference.Child("achievement").Child(User.UserId).Child("test_name").SetValueAsync("test_value");

        yield return new WaitUntil(predicate: () => DBTask_Test.IsCompleted);

        if (DBTask_Test.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask_Test.Exception}");
        }*/

        achivementsOfUserList = new List<string>();

        var DBTask_Pull_AchivementNames = DBreference.Child("users").Child(User.UserId).Child("achievements").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask_Pull_AchivementNames.IsCompleted);

        if (DBTask_Pull_AchivementNames.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask_Pull_AchivementNames.Exception}");
        }
        else
        {
          
            DataSnapshot snapshot = DBTask_Pull_AchivementNames.Result;

            if (snapshot.Exists)
            {
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    achivementsOfUserList.Add(childSnapshot.Key);
                }
                Debug.Log("Achoivements List was succesfully filled with name-key...");
            }
        }





    }

    public void GetAchievementCalculationsData()
    {
        StartCoroutine(LoadAchievementCalculations());
        Debug.Log("Odpalilo korutyne z achievement calculations");
    }

    public IEnumerator CalculateAgeCorelation()
    {
        ageCorelationDataList = new List<AgeCorelationData>();

        var DBTask_Pull_All_Users_Id = DBreference.Child("users").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask_Pull_All_Users_Id.IsCompleted);

        if (DBTask_Pull_All_Users_Id.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask_Pull_All_Users_Id.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask_Pull_All_Users_Id.Result;

            foreach (DataSnapshot snapChild in snapshot.Children)
            {
              
                
                string userId = snapChild.Key;
             //   Debug.Log(DateTime.Now.Year);
              //  Debug.Log(int.Parse(snapChild.Child("birth_date").Value.ToString()));

                int currentYear = DateTime.Now.Year;
                int birthDate = 0;

                if (snapChild.Child("birth_date").Value != null)
                    birthDate = int.Parse(snapChild.Child("birth_date").Value.ToString());
                else
                    continue;

                int userAge = currentYear - birthDate;
                int allGamesCount = 0;
                int allGamesScore = 0;
               // int allGamesPlayTime = 0;
               // int allGamesTowerHightReached = 0;

                if (snapChild.Child("all_games_count").Value != null)
                    allGamesCount = int.Parse(snapChild.Child("all_games_count").Value.ToString());
                else
                   continue;
                if (snapChild.Child("all_games_score").Value != null)
                    allGamesScore = int.Parse(snapChild.Child("all_games_score").Value.ToString());
                else
                    continue;
                //   if (snapChild.Child("all_games_play_time").Value != null)
                //       allGamesPlayTime = int.Parse(snapChild.Child("all_games_play_time").Value.ToString());
                //   else
                //        continue;
                //   if (snapChild.Child("all_games_tower_hight_reached").Value != null)
                //       allGamesTowerHightReached = int.Parse(snapChild.Child("all_games_tower_hight_reached").Value.ToString());
                //   else
                //      continue;

                if (allGamesCount == 0)
                    continue;
                if (allGamesScore == 0)
                    continue;

                AgeCorelationData ageCorelationData = new AgeCorelationData(userId, userAge, allGamesCount, allGamesScore, 0, 0); // play time and hight reached not needed in that moment soo its zero both
               
                ageCorelationDataList.Add(ageCorelationData);
              //  Debug.Log("userId: " + snapChild.Key+ " allGamesCount: "+ allGamesCount+ " allGamesScore: "+ allGamesScore);

            }

        }
      //  Debug.Log("Korelacja z "+ ageCorelationDataList.Count+" liczby uzytkownikow...");
        //x = age of user
        //y = average score per game
        Int64 SumOfxy = 0;
        Int64 SumOfxx = 0;
        Int64 SumOfx = 0;
        Int64 SumOfyy = 0;
        Int64 SumOfy = 0;

       // int rejectedUsers = 0;

            for (int i = 0; i < ageCorelationDataList.Count; i++)
            {

              //  try
              //  {
                    SumOfxy += ageCorelationDataList[i].UserAge * ageCorelationDataList[i].AverageScorePerGame;
                    SumOfxx += ageCorelationDataList[i].UserAge * ageCorelationDataList[i].UserAge;
                    SumOfyy += ageCorelationDataList[i].AverageScorePerGame * ageCorelationDataList[i].AverageScorePerGame;
                    SumOfx += ageCorelationDataList[i].UserAge;
                    SumOfy += ageCorelationDataList[i].AverageScorePerGame;
             //   }
              //  catch (DivideByZeroException)
              //  {
              //    rejectedUsers++;
             //     continue;
              //  }

            }
      
      
        double r = (double) ((ageCorelationDataList.Count * SumOfxy) - (SumOfx * SumOfy)) / (double) Math.Sqrt( ((ageCorelationDataList.Count * SumOfxx) - (SumOfx * SumOfx))   * ((ageCorelationDataList.Count * SumOfyy) -(SumOfy * SumOfy)) );

        r = Math.Round(r, 5);

        string interpretacjaKorelacji = "";
        if (r > 0.7f)
        {
            interpretacjaKorelacji = "istnieje bardzo silna korelacja dodatnia. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki przez gracza znacząco rosną.";
        } else if (r > 0.5f)
        {
            interpretacjaKorelacji = "istnieje silna korelacja dodatnia. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki przez gracza rosną.";
        } else if (r > 0.3f)
        {
            interpretacjaKorelacji = "istnieje umiarkowana korelacja dodatnia. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki przez gracza stopniowo rosną.";
        } else if (r > 0.0f)
        {
            interpretacjaKorelacji = "istnieje brak albo znikoma korelacja dodatnia. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki delikatnie rosną lub pozostają bez zmian.";
        } else if (r > -0.3f)
        {
            interpretacjaKorelacji = "istnieje brak albo znikoma korelacja ujemna. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki delikatnie spadają lub pozostają bez zmian.";
        } else if (r > -0.5f)
        {
            interpretacjaKorelacji = "istnieje umiarkowana korelacja ujemna. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki przez gracza stopniowo spadają.";
        } else if (r > -0.7f)
        {
            interpretacjaKorelacji = "istnieje silna korelacja ujemna. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki przez gracza spadają.";
        } else if (r >= -1.0f)
        {
            interpretacjaKorelacji = "istnieje bardzo silna korelacja ujemna. <color=#2bff87> Oznacza to, że wraz ze wzrostem wieku osiągane wyniki przez gracza znacząco spadają.";
        }

        CorelationGraph corelationGraph = FindObjectOfType<CorelationGraph>();

        FindObjectOfType<CorelationManager>().corelationText.text = "Wspolczynnik korelacji liniowej Pearsona (na podstawie danych "+ ageCorelationDataList.Count + " użytkowników ) wynosi: <color=#2bff87>" + r+ "</color> \n\n";
        FindObjectOfType<CorelationManager>().corelationText.text += "Pomiędzy wiekiem gracza a osiąganymi wynikami " + interpretacjaKorelacji+"</color>";

        ageCorelationDataList.Sort();

        corelationGraph.corelationDataList = ageCorelationDataList;

        double x_ = (double)SumOfx / (double)ageCorelationDataList.Count;
        double y_ = (double)SumOfy / (double)ageCorelationDataList.Count;

        double b = (SumOfxy - (ageCorelationDataList.Count * x_ * y_)) / (SumOfxx - (ageCorelationDataList.Count * x_ * x_));
        double a = y_ - (b * x_);

        Vector2 regressionStartingPoint = new Vector2(ageCorelationDataList[0].UserAge, (int)(a + (b * (ageCorelationDataList[0].UserAge))));
        Vector2 regressionEndinggPoint = new Vector2(ageCorelationDataList[ageCorelationDataList.Count - 1].UserAge, (int)(a + (b * (ageCorelationDataList[ageCorelationDataList.Count - 1].UserAge))));


        corelationGraph.regressionStartingPoint = regressionStartingPoint;
        corelationGraph.regressionEndinggPoint = regressionEndinggPoint;
        corelationGraph.LoadGrapgWithData(); 
    }

    public void RunDummyPlayerDataScript()
    {
        Debug.Log("Script was started...");
        StartCoroutine(SetDummyPlayerData());
    }

    private IEnumerator SetDummyPlayerData()
    {
        int accountsNumber = 50;
        for (int i = 0; i < accountsNumber; i++)
        {

            //  var DBTaskTimestamp = DBreference.Child("game_data").Child(User.UserId).Child(newKey).Child("timestamp").SetValueAsync(_timestamp);

           // string idAddon = ;
            string dummyPlayerId = "dummyPlayer" + UnityEngine.Random.Range(0, 500000)+"__"+i;

            string username = dummyPlayerId;
            int birthYear = DateTime.Now.Year - (16 + i);
            int expPoints = UnityEngine.Random.Range(0, 20000);
            int strengthPoints = 0;
            int speedPoints = 0;
            int starTimerpoints = 0;
            int starEssencePoints = 0;
            int timerPenaltyPoints = 0;

            int allGamesCount = UnityEngine.Random.Range(5, 200);
            int allGamesPlayTime = 0;
            int allGamesTowerHightReached = 0;
            int allGamesScore = (int)(allGamesCount * (1 - (0.015f * i)) * UnityEngine.Random.Range(4800, 5200));

            yield return UpdateUserDataUsername(dummyPlayerId, username);
            yield return UpdateUserDataBirthDate(dummyPlayerId, birthYear.ToString());

            yield return UpdateUserDataExperiencePoints(dummyPlayerId, expPoints);
            yield return UpdateUserDataStrengthPoints(dummyPlayerId, strengthPoints);
            yield return UpdateUserDataWalkSpeedPoints(dummyPlayerId, speedPoints);
            yield return UpdateUserDataStarTimerPoints(dummyPlayerId, starTimerpoints);
            yield return UpdateUserDataStarEssencePoints(dummyPlayerId, starEssencePoints);
            yield return UpdateUserDataTimerPenaltyPoints(dummyPlayerId, timerPenaltyPoints); 
            
            yield return UpdateUserDataAllGamesCount(dummyPlayerId, allGamesCount);                  
            yield return UpdateUserDataAllGamesPlayTime(dummyPlayerId, allGamesPlayTime);                  
            yield return UpdateUserDataAllGamesTowerHightReached(dummyPlayerId, allGamesTowerHightReached);                  
            yield return UpdateUserDataAllGamesScore(dummyPlayerId, allGamesScore);                  

        }
    }

    private IEnumerator LoadAchievementCalculations()
    {

        userAchievementDataList = new List<UserAchievementData>[achivementsOfUserList.Count];
     

        var DBTask_Pull_All_Achivements = DBreference.Child("achievement").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask_Pull_All_Achivements.IsCompleted);

        if (DBTask_Pull_All_Achivements.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask_Pull_All_Achivements.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask_Pull_All_Achivements.Result;

            for (int i = 0; i < achivementsOfUserList.Count; i++)
            {
                userAchievementDataList[i] = new List<UserAchievementData>();

                foreach (DataSnapshot snapChild in snapshot.Child(achivementsOfUserList[i]).Children)
                {
                //    Debug.Log("Wartosc i: " + i);
                    string userId = snapChild.Key;
                    string achievementName = achivementsOfUserList[i];
                    int allGamesCount = int.Parse(snapChild.Child("all_games_count").Value.ToString());//all_games_count:
                    int allGamesPlayTime = int.Parse(snapChild.Child("all_games_play_time").Value.ToString());
                    int allGamesTowerHightReached = int.Parse(snapChild.Child("all_games_tower_hight_reached").Value.ToString());
                    string gameKey = snapChild.Child("game_key").Value.ToString();
                    double timestamp = double.Parse(snapChild.Child("timestamp").Value.ToString());


                    UserAchievementData userAchievementData = new UserAchievementData(userId, achievementName, allGamesCount, allGamesPlayTime, allGamesTowerHightReached, gameKey, timestamp);

                    userAchievementDataList[i].Add(userAchievementData);
                   
                }
              //  Debug.Log("Elementy w tablicy z achievementami na pozycji"+i+";   wartosc: "+userAchievementDataList[i].Count);
            }
        }

        List<float> fasterThenRestPercentagePlayedGames = new List<float>();
        List<float> fasterThenRestPercentagePlayTime = new List<float>();

      

        for (int i = 0; i < userAchievementDataList.Length; i++)
        {
            userAchievementDataList[i].Sort();
            userAchievementDataList[i].Reverse();

          //  for (int k = 0; k < userAchievementDataList[i].Count; k++)
           // {
            //    Debug.Log("dddddddd: "+userAchievementDataList[i][k].AllGamesPlayTime);
           // }

            fasterThenRestPercentagePlayTime.Add(0);

            try
            {
                for (int j = 0; j < userAchievementDataList[i].Count; j++)
                {
                    if (userAchievementDataList[i][j].UserId.Equals(User.UserId))
                    {
                        if (userAchievementDataList[i].Count - 1 > 0)
                        {
                            fasterThenRestPercentagePlayTime[i] = (float)j / (float)(userAchievementDataList[i].Count - 1);
                        //    Debug.Log("obliczylo wynik: " + fasterThenRestPercentagePlayTime[i] + " ; j= " + j + " ; list.count= " + userAchievementDataList[i].Count);
                        }
                        else
                        {
                          //  Debug.Log("wszedlo else czyli list count < 0 : "+ userAchievementDataList[i].Count);
                            fasterThenRestPercentagePlayTime[i] = 1;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                continue;
            }

         //   fasterThenRestPercentagePlayedGames[i] = fasterThenRestPercentagePlayedGames[i] / i;
        }


       //  achievementManager = FindObjectOfType<AchievementManager>();

        AchievementComponent[] tempList = achievementManager.achievementComponents;

        for (int i = 0; i < tempList.Length; i++)
        {
            if (achivementsOfUserList.Contains(tempList[i].achievementName))
            {
                double percentage = Math.Round(fasterThenRestPercentagePlayTime[achivementsOfUserList.IndexOf(tempList[i].achievementName)] * 100f, 2);
                tempList[i].onHoverText.text = "Achievement gained faster then " + percentage + "% of total players who also unlocked this achievement";
            }
        }

    }

    private IEnumerator WriteAchievementData(object _timestamp, string _gameId, int _towerHight)
    {
      /*  var DBTask_LoadUserGamnesStatistics = DBreference.Child("user").Child(User.UserId).GetValueAsync();

        int allGamesCount = 0;
        int allGamesPlayTime = 0;
        int allGamesTowerHightReached = 0;

        yield return new WaitUntil(predicate: () => DBTask_LoadUserGamnesStatistics.IsCompleted);

        if (DBTask_LoadUserGamnesStatistics.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask_LoadUserGamnesStatistics.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask_LoadUserGamnesStatistics.Result;

            if (snapshot.Exists)
            {
                allGamesCount = int.Parse(snapshot.Child("all_games_count").Value.ToString());
                allGamesPlayTime = int.Parse(snapshot.Child("all_games_play_time").Value.ToString());
                allGamesTowerHightReached = int.Parse(snapshot.Child("all_games_tower_hight_reached").Value.ToString());
            }
        }*/
        List<string> qualifiedAchivementsToGainList = new List<string>();
        Debug.Log("jeszcze nie weszla wysokosc: " + _towerHight);
        if (_towerHight >= 200) { qualifiedAchivementsToGainList.Add("tower_hight_200"); Debug.Log("Weszla wysokosc: "+ _towerHight); }
        if (_towerHight >= 500) qualifiedAchivementsToGainList.Add("tower_hight_500");
        if (_towerHight >= 800) qualifiedAchivementsToGainList.Add("tower_hight_800");
        if (_towerHight >= 1000) qualifiedAchivementsToGainList.Add("tower_hight_1000");
        if (_towerHight >= 1500) qualifiedAchivementsToGainList.Add("tower_hight_1500");
        if (_towerHight >= 2000) qualifiedAchivementsToGainList.Add("tower_hight_2000");

        if (allGamesTowerHightReached > 5000) qualifiedAchivementsToGainList.Add("all_games_tower_hight_5000");
        if (allGamesTowerHightReached > 10000) qualifiedAchivementsToGainList.Add("all_games_tower_hight_10000");
        if (allGamesTowerHightReached > 25000) qualifiedAchivementsToGainList.Add("all_games_tower_hight_25000");
        if (allGamesTowerHightReached > 50000) qualifiedAchivementsToGainList.Add("all_games_tower_hight_50000");
        if (allGamesTowerHightReached > 100000) qualifiedAchivementsToGainList.Add("all_games_tower_hight_100000");
        Debug.Log(qualifiedAchivementsToGainList);

        for (int i = 0; i < qualifiedAchivementsToGainList.Count; i++)
        {
            if (achivementsOfUserList.Contains(qualifiedAchivementsToGainList[i]))
            {
                qualifiedAchivementsToGainList.RemoveAt(i);
            }
        }
        Debug.Log("ladowanie lisy achivementów... " + allGamesTowerHightReached + " :jjj: " + qualifiedAchivementsToGainList);

        for (int i = 0; i < qualifiedAchivementsToGainList.Count; i++)
        {
          //  var newKey = DBreference.Child("users").Child(User.UserId).Child("achievements").Push().Key;
            var DBTask_User_AchievementName = DBreference.Child("users").Child(User.UserId).Child("achievements").Child(qualifiedAchivementsToGainList[i]).SetValueAsync("true");

            yield return new WaitUntil(predicate: () => DBTask_User_AchievementName.IsCompleted);

            if (DBTask_User_AchievementName.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask_User_AchievementName.Exception}");
            }

            var DBTask_GameId = DBreference.Child("achievement").Child(qualifiedAchivementsToGainList[i]).Child(User.UserId).Child("game_key").SetValueAsync(_gameId);

            yield return new WaitUntil(predicate: () => DBTask_GameId.IsCompleted);

            if (DBTask_GameId.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask_GameId.Exception}");
            }

            var DBTask_Timestamp = DBreference.Child("achievement").Child(qualifiedAchivementsToGainList[i]).Child(User.UserId).Child("timestamp").SetValueAsync(_timestamp);

            yield return new WaitUntil(predicate: () => DBTask_Timestamp.IsCompleted);

            if (DBTask_Timestamp.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask_Timestamp.Exception}");
            }

            var DBTask_AllGamesPlayTime = DBreference.Child("achievement").Child(qualifiedAchivementsToGainList[i]).Child(User.UserId).Child("all_games_play_time").SetValueAsync(allGamesPlayTime);

            yield return new WaitUntil(predicate: () => DBTask_AllGamesPlayTime.IsCompleted);

            if (DBTask_AllGamesPlayTime.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask_AllGamesPlayTime.Exception}");
            }

            var DBTask_AllGamesCount = DBreference.Child("achievement").Child(qualifiedAchivementsToGainList[i]).Child(User.UserId).Child("all_games_count").SetValueAsync(allGamesCount);

            yield return new WaitUntil(predicate: () => DBTask_AllGamesCount.IsCompleted);

            if (DBTask_AllGamesCount.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask_AllGamesCount.Exception}");
            }

            var DBTask_AllGamesTowerHightReached = DBreference.Child("achievement").Child(qualifiedAchivementsToGainList[i]).Child(User.UserId).Child("all_games_tower_hight_reached").SetValueAsync(allGamesTowerHightReached);

            yield return new WaitUntil(predicate: () => DBTask_AllGamesTowerHightReached.IsCompleted);

            if (DBTask_AllGamesTowerHightReached.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask_AllGamesTowerHightReached.Exception}");
            }

            achivementsOfUserList.Add(qualifiedAchivementsToGainList[i]);
            Debug.Log("Dodano achivement do listy klasy firebase...");
        }
    }


    private IEnumerator CheckScoreboardData(string _username, object _timestamp, int _score, int _towerHight, int _kills, int _playTime, int _stars, string _mapName)
    {
        var DBTask = DBreference.Child("scoreboard").Child(User.UserId).Child("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            try
            {
                if (snapshot.Value.ToString() == null || int.Parse(snapshot.Value.ToString()) < _score)
                {      
                    StartCoroutine(UpdateBestGameData(_username, _timestamp, _score, _towerHight, _kills, _playTime, _stars, _mapName));
                }
            }
            catch (NullReferenceException e) {
                Debug.Log("Wykryto null a więc podmiana...");
                StartCoroutine(UpdateBestGameData(_username, _timestamp, _score, _towerHight, _kills, _playTime, _stars, _mapName));
            }
        }
    }

    private IEnumerator UpdateBestGameData(string _username, object _timestamp, int _score, int _towerHight, int _kills, int _playTime, int _stars, string _mapName)
    {
        var DBTaskUsername = DBreference.Child("scoreboard").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTaskUsername.IsCompleted);

        if (DBTaskUsername.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskUsername.Exception}");
        }

        var DBTaskTimestamp = DBreference.Child("scoreboard").Child(User.UserId).Child("timestamp").SetValueAsync(_timestamp);

        yield return new WaitUntil(predicate: () => DBTaskTimestamp.IsCompleted);

        if (DBTaskTimestamp.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTimestamp.Exception}");
        }

        var DBTaskScore = DBreference.Child("scoreboard").Child(User.UserId).Child("score").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTaskScore.IsCompleted);

        if (DBTaskScore.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskScore.Exception}");
        }

        var DBTaskTowerHight = DBreference.Child("scoreboard").Child(User.UserId).Child("tower_hight").SetValueAsync(_towerHight);

        yield return new WaitUntil(predicate: () => DBTaskTowerHight.IsCompleted);

        if (DBTaskTowerHight.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerHight.Exception}");
        }

        var DBTaskTowerKills = DBreference.Child("scoreboard").Child(User.UserId).Child("kills").SetValueAsync(_kills);

        yield return new WaitUntil(predicate: () => DBTaskTowerKills.IsCompleted);

        if (DBTaskTowerKills.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerKills.Exception}");
        }

        var DBTaskTowerPlayTime = DBreference.Child("scoreboard").Child(User.UserId).Child("play_time").SetValueAsync(_playTime);

        yield return new WaitUntil(predicate: () => DBTaskTowerPlayTime.IsCompleted);

        if (DBTaskTowerPlayTime.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerPlayTime.Exception}");
        }

        var DBTaskTowerStars = DBreference.Child("scoreboard").Child(User.UserId).Child("stars").SetValueAsync(_stars);

        yield return new WaitUntil(predicate: () => DBTaskTowerStars.IsCompleted);

        if (DBTaskTowerStars.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerStars.Exception}");
        }

        var DBTaskTowerMapName = DBreference.Child("scoreboard").Child(User.UserId).Child("map_name").SetValueAsync(_mapName);

        yield return new WaitUntil(predicate: () => DBTaskTowerStars.IsCompleted);

        if (DBTaskTowerStars.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTowerStars.Exception}");
        }

       StartCoroutine(LoadBestGameData());
    }

    //USER DATA UPDATE METHODES

    public void SaveUserDataAfterRegistration()
    {
        StartCoroutine(UpdateUserDataUsername(User.UserId, User.DisplayName));          //set username
        StartCoroutine(UpdateUserDataBirthDate(User.UserId, UIManager.instance.birthYearRegisterField.text));             //data urodzenia narazie na sztywno (pewnie przy rejestracji zrobić okienkomz tym)
       // StartCoroutine(UpdateUserDataLevel(1));                            //przy rejestracji level zawsze będzie równe 1 gdyż na koncie nigdy nie zdobyto doświadczenia
        StartCoroutine(UpdateUserDataExperiencePoints(User.UserId, 0));                 //przy rejestracji experience points zawsze będzie równe 0 gdyż na koncie nigdy nie zdobyto doświadczenia
        StartCoroutine(UpdateUserDataStrengthPoints(User.UserId, 0));                   //przy rejestracji strength points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataWalkSpeedPoints(User.UserId, 0));                  //przy rejestracji walk speed  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataJumpPowerPoints(User.UserId, 0));                  //przy rejestracji jump power  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataStarTimerPoints(User.UserId, 0));                  //przy rejestracji star timer  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataStarEssencePoints(User.UserId, 0));                  //przy rejestracji star timer  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataTimerPenaltyPoints(User.UserId, 0));                  //przy rejestracji star timer  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataAllGamesCount(User.UserId, 0));                  //przy rejestracji star timer  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataAllGamesPlayTime(User.UserId, 0));                  //przy rejestracji star timer  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataAllGamesTowerHightReached(User.UserId, 0));                  //przy rejestracji star timer  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
        StartCoroutine(UpdateUserDataAllGamesScore(User.UserId, 0));                  //przy rejestracji star timer  points zawsze będzie równe 0 gdyż konto ma poziom 1 i nie ma żadnych dostępnych punktów
    }

    private IEnumerator UpdateUserDataUsername(string userId, string _username)
    {
        var DBTask = DBreference.Child("users").Child(userId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }


    private IEnumerator UpdateUserDataAllGamesScore(string userId, int _allGamesScore)
    {
        var DBTask = DBreference.Child("users").Child(userId).Child("all_games_score").SetValueAsync(_allGamesScore);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateUserDataAllGamesCount(string userId, int _allGamesCount)
    {
        var DBTask = DBreference.Child("users").Child(userId).Child("all_games_count").SetValueAsync(_allGamesCount);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateUserDataAllGamesPlayTime(string userId, int _allGamesPlayTime)
    {
        var DBTask = DBreference.Child("users").Child(userId).Child("all_games_play_time").SetValueAsync(_allGamesPlayTime);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateUserDataAllGamesTowerHightReached(string userId, int _allGamesTowerHightReached)
    {
        var DBTask = DBreference.Child("users").Child(userId).Child("all_games_tower_hight_reached").SetValueAsync(_allGamesTowerHightReached);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator UpdateUserDataBirthDate(string userId, string _birthDate)
    {
        var DBTask = DBreference.Child("users").Child(userId).Child("birth_date").SetValueAsync(_birthDate);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    /* private IEnumerator UpdateUserDataLevel(int _level)
     {
             var DBTask = DBreference.Child("users").Child(User.UserId).Child("level").SetValueAsync(_level);

             yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

             if (DBTask.Exception != null)
             {
                 Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
             }
     }*/

    private IEnumerator FatUpdateUserGamesStatistics(object _timestamp, string _gameId)
    {
        var DBTask = DBreference.Child("game_data").Child(User.UserId).GetValueAsync();

        int allGamesCount = 0;
        int allGamesPlayTime = 0;
        int allGamesTowerHightReached = 0;
        int allGamesScore = 0;

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                allGamesCount += 1;

                if (childSnapshot.Child("play_time").Value != null)
                {
                    allGamesPlayTime += int.Parse(childSnapshot.Child("play_time").Value.ToString());
                }
                else
                {
                    allGamesPlayTime += 0;
                }

                if (childSnapshot.Child("tower_hight").Value != null)
                {
                    allGamesTowerHightReached += int.Parse(childSnapshot.Child("tower_hight").Value.ToString());
                }
                else
                {
                    allGamesTowerHightReached += 0;
                }

                if (childSnapshot.Child("score").Value != null)
                {
                    allGamesScore += int.Parse(childSnapshot.Child("score").Value.ToString()); ; 
                }

            }

            var DBTaskPush_AllGamesCount = DBreference.Child("users").Child(User.UserId).Child("all_games_count").SetValueAsync(allGamesCount);
            yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesCount.IsCompleted);

            if (DBTaskPush_AllGamesCount.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesCount.Exception}");
            }

            var DBTaskPush_AllGamesPlayTime = DBreference.Child("users").Child(User.UserId).Child("all_games_play_time").SetValueAsync(allGamesPlayTime);
            yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesPlayTime.IsCompleted);

            if (DBTaskPush_AllGamesPlayTime.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesPlayTime.Exception}");
            }

            var DBTaskPush_AllGamesTowerHightReached = DBreference.Child("users").Child(User.UserId).Child("all_games_tower_hight_reached").SetValueAsync(allGamesTowerHightReached);
            yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesTowerHightReached.IsCompleted);

            if (DBTaskPush_AllGamesTowerHightReached.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesTowerHightReached.Exception}");
            }

            var DBTaskPush_AllGamesScore = DBreference.Child("users").Child(User.UserId).Child("all_games_score").SetValueAsync(allGamesScore);
            yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesScore.IsCompleted);

            if (DBTaskPush_AllGamesScore.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesScore.Exception}");
            }
        }

        this.allGamesCount = allGamesCount;
        this.allGamesPlayTime = allGamesPlayTime;
        this.allGamesTowerHightReached = allGamesTowerHightReached;
        this.allGamesScore = allGamesScore;

        StartCoroutine(WriteAchievementData(_timestamp, _gameId, allGamesTowerHightReached));
    }
    /*
    public void IncrementUserDataAllGamesStatistics(int _allGamesCountIncrementValue, int _allGamesPlayTimeIncrementValue, int _allGamesTowerHightReachedIncrementValue)
    {
        StartCoroutine(UpdateUserDataAllGamesStatistics(_allGamesCountIncrementValue, _allGamesPlayTimeIncrementValue, _allGamesTowerHightReachedIncrementValue));
    }*/

    private IEnumerator UpdateUserDataAllGamesStatistics(int _allGamesCountIncrementValue, int _allGamesPlayTimeIncrementValue, int _allGamesTowerHightReachedIncrementValue, int _allGamesScoreIncrementValue, object _timestamp, string _gameId)
    {
        var DBTaskPull = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        int allGamesCount = 0;
        int allGamesPlayTime = 0;
        int allGamesTowerHightReached = 0;
        int allGamesScore = 0;

        yield return new WaitUntil(predicate: () => DBTaskPull.IsCompleted);

        if (DBTaskPull.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPull.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTaskPull.Result;

            if (snapshot.Exists)
            {
                try
                {
                    allGamesCount = int.Parse(snapshot.Child("all_games_count").Value.ToString());
                    allGamesPlayTime = int.Parse(snapshot.Child("all_games_play_time").Value.ToString());
                    allGamesTowerHightReached = int.Parse(snapshot.Child("all_games_tower_hight_reached").Value.ToString());
                    allGamesScore = int.Parse(snapshot.Child("all_games_score").Value.ToString());

                    Debug.Log("User all games coun data loaded...");

                } catch (NullReferenceException e)
                {
                    Debug.Log("No Games statistics fields in database... ");
                    StartCoroutine(FatUpdateUserGamesStatistics(_timestamp, _gameId));
                    Debug.Log("Wykonano FAT UPDATE USER GAMES STATISTICS");
                    yield break;
                }
               

                if (allGamesCount == 0)
                {
                    StartCoroutine(FatUpdateUserGamesStatistics(_timestamp, _gameId));
                    Debug.Log("Wykonano FAT UPDATE USER GAMES STATISTICS");
                    yield break;
                }
            }
            else
            {
                StartCoroutine(FatUpdateUserGamesStatistics(_timestamp, _gameId));
                Debug.Log("Wykonano FAT UPDATE USER GAMES STATISTICS");
                yield break;
            }
        }

        var DBTaskPush_AllGamesCount = DBreference.Child("users").Child(User.UserId).Child("all_games_count").SetValueAsync(allGamesCount +_allGamesCountIncrementValue);
        yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesCount.IsCompleted);

        if (DBTaskPush_AllGamesCount.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesCount.Exception}");
        }

        var DBTaskPush_AllGamesPlayTime = DBreference.Child("users").Child(User.UserId).Child("all_games_play_time").SetValueAsync(allGamesPlayTime + _allGamesPlayTimeIncrementValue);
        yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesPlayTime.IsCompleted);

        if (DBTaskPush_AllGamesPlayTime.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesPlayTime.Exception}");
        }

        var DBTaskPush_AllGamesTowerHightReached = DBreference.Child("users").Child(User.UserId).Child("all_games_tower_hight_reached").SetValueAsync(allGamesTowerHightReached + _allGamesTowerHightReachedIncrementValue);
        yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesTowerHightReached.IsCompleted);

        if (DBTaskPush_AllGamesTowerHightReached.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesTowerHightReached.Exception}");
        }

        var DBTaskPush_AllGamesScore = DBreference.Child("users").Child(User.UserId).Child("all_games_score").SetValueAsync(allGamesScore + _allGamesScoreIncrementValue);
        yield return new WaitUntil(predicate: () => DBTaskPush_AllGamesScore.IsCompleted);

        if (DBTaskPush_AllGamesScore.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPush_AllGamesScore.Exception}");
        }

        this.allGamesCount = allGamesCount + _allGamesCountIncrementValue;
        this.allGamesPlayTime = allGamesPlayTime + _allGamesPlayTimeIncrementValue;
        this.allGamesTowerHightReached = allGamesTowerHightReached + _allGamesTowerHightReachedIncrementValue;
        this.allGamesScore = allGamesScore + _allGamesScoreIncrementValue;

       yield return WriteAchievementData(_timestamp, _gameId, _allGamesTowerHightReachedIncrementValue);
    }
    /*
    private IEnumerator UpdateUserDataAllGamesPlayTime(int _allGamesPlayTimeIncrementValue)
    {
        var DBTaskPull = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        int allGamesPlayTime = 0;

        yield return new WaitUntil(predicate: () => DBTaskPull.IsCompleted);

        if (DBTaskPull.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPull.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTaskPull.Result;

            if (snapshot.Exists)
            {
                allGamesPlayTime = int.Parse(snapshot.Child("all_games_play_time").Value.ToString());

                Debug.Log("User all games coun data loaded...");

                if (allGamesPlayTime == 0)
                { // pull data with fat function...
                }
            }
            else
            {
                allGamesPlayTime = 0;
            }
        }
        var DBTaskPush = DBreference.Child("users").Child(User.UserId).Child("all_games_play_time").SetValueAsync(allGamesPlayTime + _allGamesPlayTimeIncrementValue);
       

        yield return new WaitUntil(predicate: () => DBTaskPush.IsCompleted);

        if (DBTaskPush.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPush.Exception}");
        }
    }

    private IEnumerator UpdateUserDataAllGamesTowerHightReached(int _allGamesTowerHightReachedIncrementValue)
    {
        var DBTaskPull = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        int allGamesTowerHightReached = 0;

        yield return new WaitUntil(predicate: () => DBTaskPull.IsCompleted);

        if (DBTaskPull.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPull.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTaskPull.Result;

            if (snapshot.Exists)
            {
                allGamesTowerHightReached = int.Parse(snapshot.Child("all_games_tower_hight_reached").Value.ToString());

                Debug.Log("User all games coun data loaded...");

                if (allGamesTowerHightReached == 0)
                { // pull data with fat function...
                }
            }
            else
            {
                allGamesTowerHightReached = 0;
            }
        }

        var DBTaskPush = DBreference.Child("users").Child(User.UserId).Child("all_games_tower_hight_reached").SetValueAsync(allGamesTowerHightReached + _allGamesTowerHightReachedIncrementValue);

        yield return new WaitUntil(predicate: () => DBTaskPush.IsCompleted);

        if (DBTaskPush.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskPush.Exception}");
        }
    }
    */

    private IEnumerator UpdateUserDataExperiencePoints(string userId, int _experiencePoints)
    {
            var DBTask = DBreference.Child("users").Child(userId).Child("experience_points").SetValueAsync(_experiencePoints);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
    }
   private IEnumerator UpdateUserDataStrengthPoints(string userId, int _strengthPoints)
   {
            var DBTask = DBreference.Child("users").Child(userId).Child("strength_points").SetValueAsync(_strengthPoints);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
   } 
   private IEnumerator UpdateUserDataStarEssencePoints(string userId, int _starEssencePoints)
   {
            var DBTask = DBreference.Child("users").Child(userId).Child("star_essence_points").SetValueAsync(_starEssencePoints);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
   }
    /*private IEnumerator WriteAchivementTestRecord()
    {
        var DBTask_Test= DBreference.Child("achievement").Child(User.UserId).Child("test_name").SetValueAsync("test_value");

        yield return new WaitUntil(predicate: () => DBTask_Test.IsCompleted);

        if (DBTask_Test.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask_Test.Exception}");
        }
    }*/
   private IEnumerator UpdateUserDataTimerPenaltyPoints(string userId, int _timerPenaltyPoints)
   {
            var DBTask = DBreference.Child("users").Child(userId).Child("timer_penalty_points").SetValueAsync(_timerPenaltyPoints);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
   }

   private IEnumerator UpdateUserDataStarTimerPoints(string userId, int _starTimerPoints)
   {
            var DBTask = DBreference.Child("users").Child(userId).Child("star_timer_points").SetValueAsync(_starTimerPoints);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
   }

   private IEnumerator UpdateUserDataWalkSpeedPoints(string userId, int _walkSpeedPoints)
   {
            var DBTask = DBreference.Child("users").Child(userId).Child("walk_speed_points").SetValueAsync(_walkSpeedPoints);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
   }

   private IEnumerator UpdateUserDataJumpPowerPoints(string userId, int _jumpPowerPoints)
   {
            var DBTask = DBreference.Child("users").Child(userId).Child("jump_power_points").SetValueAsync(_jumpPowerPoints);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
   }

    private IEnumerator UpdateUserDataResetAbilityPoints()
    {
        var DBTaskJumpPower = DBreference.Child("users").Child(User.UserId).Child("jump_power_points").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTaskJumpPower.IsCompleted);

        if (DBTaskJumpPower.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskJumpPower.Exception}");
        }

        var DBTaskWalkSpeed = DBreference.Child("users").Child(User.UserId).Child("walk_speed_points").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTaskWalkSpeed.IsCompleted);

        if (DBTaskWalkSpeed.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskWalkSpeed.Exception}");
        }
       
        var DBTaskStrength = DBreference.Child("users").Child(User.UserId).Child("strength_points").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTaskStrength.IsCompleted);

        if (DBTaskStrength.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskStrength.Exception}");
        }

        var DBTaskStarTimer = DBreference.Child("users").Child(User.UserId).Child("star_timer_points").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTaskStarTimer.IsCompleted);

        if (DBTaskStarTimer.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskStarTimer.Exception}");
        }

        var DBTaskTimerPenalty = DBreference.Child("users").Child(User.UserId).Child("timer_penalty_points").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTaskTimerPenalty.IsCompleted);

        if (DBTaskTimerPenalty.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskTimerPenalty.Exception}");
        }

        var DBTaskStarEssence = DBreference.Child("users").Child(User.UserId).Child("star_essence_points").SetValueAsync(0);

        yield return new WaitUntil(predicate: () => DBTaskStarEssence.IsCompleted);

        if (DBTaskStarEssence.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTaskStarEssence.Exception}");
        }

    }


}


