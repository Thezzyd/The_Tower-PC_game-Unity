using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarFillComponent : MonoBehaviour
{
    private Image expBarFillImageComponent;
    private LevelManager levelManager;
    private FirebaseManager firebaseManager;
    public Transform expBarProgressionEffectParent;
    public float targetFillAmountOnBar;
    public Vector3 localScaleOfProgressionEffectParent;

    public ParticleSystem progressionEffect;
  
    public bool isLevelUpAfterEndGameExperienceAdded;
    private float timeElapsedForFillAmounUpdationt;
    public float timeElapsedForExpTextUpdation;
    private int expAmountCalculated;
    private float lerpDuration;
    private float startingPointOfFillAmount;
    private float experienceAmountBeforeEndGameExperianceAdded;
    private float levelBeforeEndGameExperianceAdded;
    private int experiencToLevelUpAmountBeforeEndGameExperienceAdded;
    public bool isEndGameExpCalculated;


    void Start()
    {
        
        expBarFillImageComponent = GetComponent<Image>();
        levelManager = FindObjectOfType<LevelManager>();
        firebaseManager = FindObjectOfType<FirebaseManager>();

        isEndGameExpCalculated = false;

        expBarProgressionEffectParent.localScale = new Vector3((firebaseManager.GetLevelFloat() - firebaseManager.GetLevelInt() ), 1f, 1f);
        expBarFillImageComponent.fillAmount = firebaseManager.GetLevelFloat() - firebaseManager.GetLevelInt();
        localScaleOfProgressionEffectParent = expBarProgressionEffectParent.localScale;

        timeElapsedForFillAmounUpdationt = 0.0f;
        startingPointOfFillAmount = localScaleOfProgressionEffectParent.x;
        experienceAmountBeforeEndGameExperianceAdded = firebaseManager.GetExperience();
        experiencToLevelUpAmountBeforeEndGameExperienceAdded = firebaseManager.GetExperienceToLevelUp();

        levelBeforeEndGameExperianceAdded = firebaseManager.GetLevelInt();

        if (name.Equals("FillGameOverBar"))
        {
            levelManager.currentExperiencePointsInfoTextGameOverCanvas.text = firebaseManager.GetExperience() + " I " + firebaseManager.GetExperienceToLevelUp() + " EXP";
            levelManager.currentLevelTextGameOverCanvas.text = "Lvl : " + levelBeforeEndGameExperianceAdded;
            lerpDuration = 3f;
        }
        else
        {
            lerpDuration = 0.4f;
        }
    }

    public void Update()
    {
            targetFillAmountOnBar = firebaseManager.GetLevelFloat() - firebaseManager.GetLevelInt();


        if (isLevelUpAfterEndGameExperienceAdded)
        {

            ExpBarFillAmountCalculations(1.0f, experiencToLevelUpAmountBeforeEndGameExperienceAdded);

            if (expBarFillImageComponent.fillAmount >= 1.0f)
            {
                isLevelUpAfterEndGameExperienceAdded = false;
                ResetAfterLevelUp();
            }
          

            if (!progressionEffect.isPlaying)
                progressionEffect.Play();

        }
        else if (localScaleOfProgressionEffectParent.x < targetFillAmountOnBar)
        {

            ExpBarFillAmountCalculations(targetFillAmountOnBar, firebaseManager.GetExperienceToLevelUp());

            if (!progressionEffect.isPlaying)
                progressionEffect.Play();

        }

        else
        {
            timeElapsedForFillAmounUpdationt = 0f;
            startingPointOfFillAmount = localScaleOfProgressionEffectParent.x;
            progressionEffect.Stop();
        }

     /*   if (!name.Equals("FillGameOverBar") && expBarFillImageComponent.fillAmount >= 0.9f && levelBeforeEndGameExperianceAdded != firebaseManager.GetLevelInt())
        {
            levelBeforeEndGameExperianceAdded = firebaseManager.GetLevelInt();
            ResetAfterLevelUp();
        }*/

    }

    private void ExpBarFillAmountCalculations(float targetFillAmount, int expToLvlUp)
    {
        timeElapsedForFillAmounUpdationt += Time.unscaledDeltaTime;

        expBarFillImageComponent.fillAmount = Mathf.Lerp(startingPointOfFillAmount, targetFillAmount, timeElapsedForFillAmounUpdationt / lerpDuration);
        localScaleOfProgressionEffectParent.x = Mathf.Lerp(startingPointOfFillAmount, targetFillAmount, timeElapsedForFillAmounUpdationt / lerpDuration);
        expBarProgressionEffectParent.localScale = localScaleOfProgressionEffectParent;

        if (isEndGameExpCalculated)
        {
            ExperienceAmountTextCalculations(expToLvlUp);
        }
    }

    private void ExperienceAmountTextCalculations(int expToLvlUp)
    {
        timeElapsedForExpTextUpdation += Time.unscaledDeltaTime;
        expAmountCalculated = (int) Mathf.Lerp(experienceAmountBeforeEndGameExperianceAdded, firebaseManager.GetExperience(), timeElapsedForExpTextUpdation / lerpDuration);
        levelManager.currentExperiencePointsInfoTextGameOverCanvas.text = expAmountCalculated.ToString() + " I " + expToLvlUp + " EXP";
    }

    public void ResetAfterLevelUp()
    {
       // Debug.Log("jakos dziala coss");
        timeElapsedForFillAmounUpdationt = 0f;
        startingPointOfFillAmount = 0f;
        localScaleOfProgressionEffectParent.x = 0f;
        expBarFillImageComponent.fillAmount = 0f;
        expBarProgressionEffectParent.localScale = localScaleOfProgressionEffectParent;
        levelManager.ExpGain("NaN");
        targetFillAmountOnBar = firebaseManager.GetLevelFloat() - firebaseManager.GetLevelInt();
        levelManager.currentLevelTextGameOverCanvas.text = "Lvl : " + firebaseManager.GetLevelInt();

    }

    public void ExpBarRefresh()
    {

        levelManager = FindObjectOfType<LevelManager>();

        levelManager.currentExperiencePointsInfoTextGameViewCanvas.text = firebaseManager.GetExperience() + " I " + firebaseManager.GetExperienceToLevelUp() + " Exp";
      //  levelManager.currentLevelTextGameOverCanvas.text = "Lvl : "+ firebaseManager.GetLevelInt();
    }

}
