/*using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class LevelManager_Wieza : LevelManager
{

    void Start()
    {
     
    }

    void Update()
    {

    }

    public override void HealthCheck()
    {
        switch (health)
        {
            case 10:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(true);
                hearts_GameOver[4].gameObject.SetActive(true);
                hearts_GameOver[5].gameObject.SetActive(true);
                hearts_GameOver[6].gameObject.SetActive(true);
                hearts_GameOver[7].gameObject.SetActive(true);
                hearts_GameOver[8].gameObject.SetActive(true);
                hearts_GameOver[9].gameObject.SetActive(true);
                break;
            case 9:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(true);
                hearts_GameOver[4].gameObject.SetActive(true);
                hearts_GameOver[5].gameObject.SetActive(true);
                hearts_GameOver[6].gameObject.SetActive(true);
                hearts_GameOver[7].gameObject.SetActive(true);
                hearts_GameOver[8].gameObject.SetActive(true);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 8:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(true);
                hearts_GameOver[4].gameObject.SetActive(true);
                hearts_GameOver[5].gameObject.SetActive(true);
                hearts_GameOver[6].gameObject.SetActive(true);
                hearts_GameOver[7].gameObject.SetActive(true);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 7:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(true);
                hearts_GameOver[4].gameObject.SetActive(true);
                hearts_GameOver[5].gameObject.SetActive(true);
                hearts_GameOver[6].gameObject.SetActive(true);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 6:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(true);
                hearts_GameOver[4].gameObject.SetActive(true);
                hearts_GameOver[5].gameObject.SetActive(true);
                hearts_GameOver[6].gameObject.SetActive(false);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 5:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(true);
                hearts_GameOver[4].gameObject.SetActive(true);
                hearts_GameOver[5].gameObject.SetActive(false);
                hearts_GameOver[6].gameObject.SetActive(false);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 4:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(true);
                hearts_GameOver[4].gameObject.SetActive(false);
                hearts_GameOver[5].gameObject.SetActive(false);
                hearts_GameOver[6].gameObject.SetActive(false);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 3:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(true);
                hearts_GameOver[3].gameObject.SetActive(false);
                hearts_GameOver[4].gameObject.SetActive(false);
                hearts_GameOver[5].gameObject.SetActive(false);
                hearts_GameOver[6].gameObject.SetActive(false);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 2:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(true);
                hearts_GameOver[2].gameObject.SetActive(false);
                hearts_GameOver[3].gameObject.SetActive(false);
                hearts_GameOver[4].gameObject.SetActive(false);
                hearts_GameOver[5].gameObject.SetActive(false);
                hearts_GameOver[6].gameObject.SetActive(false);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 1:
                hearts_GameOver[0].gameObject.SetActive(true);
                hearts_GameOver[1].gameObject.SetActive(false);
                hearts_GameOver[2].gameObject.SetActive(false);
                hearts_GameOver[3].gameObject.SetActive(false);
                hearts_GameOver[4].gameObject.SetActive(false);
                hearts_GameOver[5].gameObject.SetActive(false);
                hearts_GameOver[6].gameObject.SetActive(false);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                break;
            case 0:
                hearts_GameOver[0].gameObject.SetActive(false);
                hearts_GameOver[1].gameObject.SetActive(false);
                hearts_GameOver[2].gameObject.SetActive(false);
                hearts_GameOver[3].gameObject.SetActive(false);
                hearts_GameOver[4].gameObject.SetActive(false);
                hearts_GameOver[5].gameObject.SetActive(false);
                hearts_GameOver[6].gameObject.SetActive(false);
                hearts_GameOver[7].gameObject.SetActive(false);
                hearts_GameOver[8].gameObject.SetActive(false);
                hearts_GameOver[9].gameObject.SetActive(false);
                GameOverSecwenction();



                break;
        }
    }
}
*/