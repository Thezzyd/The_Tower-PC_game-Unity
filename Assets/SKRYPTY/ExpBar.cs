/*
using UnityEngine;

public class ExpBar : MonoBehaviour
{

    [HideInInspector] public float lvltemp;

    public GameObject expBar;

    [HideInInspector] public LevelManager levelManager;

    [HideInInspector] Vector3 locScale;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        lvltemp = 1f;
        
        locScale = expBar.transform.localScale;
      //  locScaleStala = locScale;
    }

    public void ExpBarRefresh()
    {
        levelManager = FindObjectOfType<LevelManager>();
        locScale = expBar.transform.localScale;
        locScale.x = (1f * (FirebaseManager.accountExperiencePoints / FirebaseManager.accountExperirncePointsToLevelUp));
            expBar.transform.localScale = locScale;
        levelManager.expInfoText.text = FirebaseManager.accountExperiencePoints + " / " + FirebaseManager.accountExperirncePointsToLevelUp + " Exp";
    }
}
*/