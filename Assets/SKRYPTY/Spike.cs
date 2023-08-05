/*using UnityEngine;

public class Spike : MonoBehaviour
{
    //public GameObject player;
    public LevelManager levelManager;
    public float licznikSpike;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        
            licznikSpike = 10f;
        
    }
    
    private void Update()
    {
        licznikSpike -= Time.deltaTime;
        if (licznikSpike <= 0)
        { licznikSpike = 10f; }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (licznikSpike <= 5f && levelManager.tarcza == false)
        {
            

            if (other.name == "PLAYER")
            {

                LevelManager.health -= 1;
                levelManager.RespawnPlayer();
                levelManager.tarcza = true;

            }

            if (other.name == "PLAYER2")
            {

                LevelManager.health -= 1;
                levelManager.RespawnPlayer2();
                levelManager.tarcza = true;

            }

            if (other.name == "PLAYER3")
            {

                LevelManager.health -= 1;
                levelManager.RespawnPlayer3();
                levelManager.tarcza = true;

            }

        }
    }

}
*/