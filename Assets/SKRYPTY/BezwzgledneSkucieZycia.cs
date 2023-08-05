/*using UnityEngine;

public class BezwzgledneSkucieZycia : MonoBehaviour
{

    [HideInInspector] public LevelManager levelManager;

    public Spazz spazzScript;



    void Start()
    {

       
        levelManager = FindObjectOfType<LevelManager>();

    }

    

    public void OnTriggerEnter2D(Collider2D other)
    {



        if (other.name == "PLAYER1" )
        {

            LevelManager.health -= 1;
            if (LevelManager.health < 0)
                LevelManager.health = 0;
            if (LevelManager.health > 0)
            {
                levelManager.RespawnPlayer();
                levelManager.tarcza = true;
            }
            levelManager.HealthCheck();
            Destroy(other.gameObject);
          /* if (LevelManager.health <= 0)
                levelManager.GameOver();*/
            // spazzScript = FindObjectOfType<Spazz>();
            //  spazzScript.Resurect();
  /*      }

     else   if (other.name == "PLAYER2")
        {

            LevelManager.health -= 1;
            if (LevelManager.health < 0)
                LevelManager.health = 0;
            if (LevelManager.health > 0)
            {
                levelManager.RespawnPlayer2();
                levelManager.tarcza = true;
            }
            levelManager.HealthCheck();
            Destroy(other.gameObject);
          /*  if (LevelManager.health <= 0)
                levelManager.GameOver();*/
            //  spazzScript = FindObjectOfType<Spazz>();
            // spazzScript.Resurect();
   /*     }

      else  if (other.name == "PLAYER3")
        {

            LevelManager.health -= 1;
            if (LevelManager.health < 0)
                LevelManager.health = 0;
            if (LevelManager.health > 0)
            {
                levelManager.RespawnPlayer3();
                levelManager.tarcza = true;
            }
            levelManager.HealthCheck();
            Destroy(other.gameObject);
           /* if (LevelManager.health <= 0)
                levelManager.GameOver();*/
            //  spazzScript = FindObjectOfType<Spazz>();
            //  spazzScript.Resurect();
      /*  }

        else if (other.name == "PLAYER4")
        {

            LevelManager.health -= 1;
            if (LevelManager.health < 0)
                LevelManager.health = 0;
            if (LevelManager.health > 0)
            {
                levelManager.RespawnPlayer4();
                levelManager.tarcza = true;
            }

            levelManager.HealthCheck();
            Destroy(other.gameObject);
           /* if (LevelManager.health <= 0)
                levelManager.GameOver();*/
            // spazzScript = FindObjectOfType<Spazz>();
            // spazzScript.Resurect();
     /*   }

        else if (other.name == "PLAYER5")
        {

            LevelManager.health -= 1;
            if (LevelManager.health < 0)
                LevelManager.health = 0;
            if (LevelManager.health > 0)
            {
                levelManager.RespawnPlayer5();
                levelManager.tarcza = true;
            }
            levelManager.HealthCheck();
            Destroy(other.gameObject);
           /* if (LevelManager.health <= 0)
                levelManager.GameOver();*/
            //  spazzScript = FindObjectOfType<Spazz>();
            // spazzScript.Resurect();
    /*    }
        else if (other.name == "PLAYER6")
        {

            LevelManager.health -= 1;
            if (LevelManager.health < 0)
                LevelManager.health = 0;
            if (LevelManager.health > 0)
            {
                levelManager.RespawnPlayer6();
                levelManager.tarcza = true;
            }
            levelManager.HealthCheck();
            Destroy(other.gameObject);
            //  spazzScript = FindObjectOfType<Spazz>();
            // spazzScript.Resurect();
           /* if (LevelManager.health <= 0)
                levelManager.GameOver();*/
     /*   }
        else Destroy(other.gameObject);

    }

}*/
