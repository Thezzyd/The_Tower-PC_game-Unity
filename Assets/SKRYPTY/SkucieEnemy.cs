using UnityEngine;

public class SkucieEnemy : MonoBehaviour
{
    //public GameObject player;
    [HideInInspector] public LevelManager levelManager;
   // public SpawnerMedusy spawnerMedusy;
   // public SpawnerMedusy2 spawnerMedusy2;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

       // spawnerMedusy = FindObjectOfType<SpawnerMedusy>();
       // spawnerMedusy2 = FindObjectOfType<SpawnerMedusy2>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.name == "Medusa")
        {
            Destroy(other.gameObject);
            //spawnerMedusy.InvokeSpawnerMedusa();
        }

       else if (other.name == "Medusa2")
        {
            Destroy(other.gameObject);
            //spawnerMedusy2.InvokeSpawnerMedusa2();
        }

        else if (other.tag == "BLOOBENEMY")
        {
            Destroy(other.gameObject);
            
        }

    }
}
