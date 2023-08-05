/*using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [HideInInspector]  public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PLAYER")
        {
            levelManager.currCheckpoint = gameObject;
            //Debug.Log("Activated Check " + transform.position);
        }
    }

}
*/