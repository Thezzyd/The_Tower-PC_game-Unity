
using UnityEngine;

public class StarTimerBar : MonoBehaviour
{
    public GameObject powerBar;

    private LevelManager levelManager;
    private HeroesManager heroesManager;

    Vector3 locScale;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        locScale = transform.localScale;

    }

    public void FixedUpdate()
    {
        // levelManager = FindObjectOfType<LevelManager>();
        if (levelManager.starTimer >= 0.0f)
        {
            locScale = transform.localScale;
            locScale.x = ((float)levelManager.starTimer / heroesManager.HeroesStarTimerValue);
            transform.localScale = locScale;
        }
    }
}
