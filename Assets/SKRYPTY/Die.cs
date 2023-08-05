/*using UnityEngine;

public class Die : MonoBehaviour
{

    public LevelManager levelManager;


    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PLAYER")
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            levelManager.RespawnPlayer();
        }
    }
}
*/