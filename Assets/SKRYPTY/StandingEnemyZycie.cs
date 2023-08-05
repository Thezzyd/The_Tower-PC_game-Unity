
//using UnityEngine;

//public class StandingEnemyZycie : MonoBehaviour
//{
    /*
    public float lifePool = 100f;
    public LevelManager levelManager;
    public SpawnerMedusy spawnerMedusy;
    public SpawnerMedusy2 spawnerMedusy2;*/
    /*
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        spawnerMedusy = FindObjectOfType<SpawnerMedusy>();
        spawnerMedusy2 = FindObjectOfType<SpawnerMedusy2>();

    }
    public void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "POCISK")
        {


            lifePool -= 100;
            Debug.Log("Trafiloooooooooooooooo" + lifePool);
            // Blood();

            if (lifePool <= 0)
            {

                Debug.Log("ZNISZCZONO!");

                levelManager.killCounter += 1;
                levelManager.KillCounter();

                if (gameObject.name == "Medusa")
                {
                    Debug.Log("yuuuup!");
                    spawnerMedusy.InvokeSpawnerMedusa();
                }

                else
                {
                    Debug.Log("yuup!");
                    spawnerMedusy2.InvokeSpawnerMedusa2();
                }

                Destroy(gameObject);
            }

        }




    }*/



//}
