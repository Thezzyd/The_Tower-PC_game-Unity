using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementComponent : MonoBehaviour
{
    private FirebaseManager firebaseManager;

    public string achievementName;
    private bool isUnlocked;

    private TextMeshProUGUI achievementText;
    private TextMeshProUGUI achievementDescriptionText;
    private Image achievementImage;

    public TextMeshProUGUI onHoverText;
    

    void Start()
    {
        RefreshAchievement();
     
        onHoverText.gameObject.SetActive(false);

        SetOnHoverTextValues();

    }

    public void OnEnable()
    {
        RefreshAchievement();
        SetOnHoverTextValues();
    }

    public void RefreshAchievement()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();

        achievementText = transform.Find("AchievementText").GetComponent<TextMeshProUGUI>();
        achievementDescriptionText = transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
        onHoverText = transform.Find("OnHoverText").GetComponent<TextMeshProUGUI>();
        achievementImage = GetComponent<Image>();

        if (firebaseManager.achivementsOfUserList.Contains(achievementName))
        {
            achievementText.text = "Achievement unlocked";
            achievementImage.color = new Color(0, 0.5377f, 0.2297f);
            isUnlocked = true;
        }
        else
        {
            achievementText.text = "Achievement locked";
            achievementImage.color = new Color(0.6415f, 0.0963f, 0);
            isUnlocked = false;
        }
       
    }

    public void SetOnHoverTextValues()
    { 
        
    }

    public void OnHoverEnterAchievement()
    {
        if (isUnlocked)
        {
            achievementText.gameObject.SetActive(false);
            achievementDescriptionText.gameObject.SetActive(false);

            onHoverText.gameObject.SetActive(true);
        }
    }

    public void OnHoverExitAchievement()
    {
        onHoverText.gameObject.SetActive(false);

        achievementText.gameObject.SetActive(true);
        achievementDescriptionText.gameObject.SetActive(true); 
    }


}
