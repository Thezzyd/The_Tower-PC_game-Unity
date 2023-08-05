using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    private FirebaseManager firebaseManager;
    public AchievementComponent[] achievementComponents;

    public Window_Graph window_Graph;
   

  /*  void Start()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();

       // firebaseManager.GetAchievementCalculationsData();
    }*/
    /*
    public void RefreshAchievementsCalculations()
    {

        firebaseManager.GetAchievementCalculationsData();
    }

    public void RefreshProgressionCalculations()
    {
       
        window_Graph.LoadUserData();
      
    }*/

    void OnEnable()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();
        firebaseManager.achievementManager = this;
    

         firebaseManager.GetAchievementCalculationsData();
     //   Debug.Log("PrintOnEnable: script was enabled");
    }


}
