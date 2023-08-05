/*
using UnityEngine;

public class BasePortalBar : MonoBehaviour
{

   // [HideInInspector] public float lvltemp;

    public GameObject baseHealthBar;

    [HideInInspector] public LevelManager levelManager;

    [HideInInspector] Vector3 locScale;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
      //  lvltemp = 1f;

        locScale = baseHealthBar.transform.localScale;
        //  locScaleStala = locScale;
    }

    public void BasePortalHealthBarRefresh()
    {
        levelManager = FindObjectOfType<LevelManager>();
        locScale = baseHealthBar.transform.localScale;

        locScale.x = (levelManager.basePortalHealthPoints / levelManager.basePortalHealthPointsStala);
        baseHealthBar.transform.localScale = locScale;
       // levelManager.expInfoText.text = MainMenu.currentExp + " / " + MainMenu.expToLvlUp + " Exp";
    }
}
*/