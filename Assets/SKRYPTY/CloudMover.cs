
using UnityEngine;
/*
public class CloudMover : MonoBehaviour
{

    [HideInInspector] public float speed;
    [HideInInspector] public LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
       // transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);

        if (gameObject.transform.position.x >= 135)
        {
            
            Instantiate(levelManager.cloudPrefab, levelManager.cloudResps[Random.Range(0, 8)].transform.position, gameObject.transform.rotation);
            Destroy(gameObject);

           
        }
    }

  
}
*/