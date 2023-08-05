
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float speed = 8.0f;
    public float distanceFromCamera = 5.0f;
    public Transform directionPosition;
    public LevelManager levelManager;
    private bool isTurning;
    public float translateSpeed;
    private bool isDone;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
      //  rb = GetComponent<Rigidbody2D>();
     //   directionPosition = levelManager.spazzPosition;
       // translateSpeed = 1;
       // rb.AddForce(new Vector2(Ratrandom.Range(-1f, 1f), Random.Range(12f, 14f)), ForceMode2D.Impulse);

    }
    void Update()
    {
        translateSpeed -= (Time.deltaTime / 0.3f);
        directionPosition = levelManager.cameraAndEnemiesTargetPoint;
        if (!isTurning)
        {
            transform.Translate(Vector2.up*35 * translateSpeed * Time.deltaTime);
              if(translateSpeed <= 0f)
                isTurning = true;
        }

        if (isTurning)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = distanceFromCamera;
            speed += (Time.deltaTime * 0.7f);
            //Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3 position = Vector3.Lerp(transform.position, directionPosition.position, 1.0f - Mathf.Exp(-speed * Time.deltaTime));

            transform.position = position;

            if (!isDone && Vector2.Distance(directionPosition.position, transform.position) <= 0.2f)
            {
               
              //  var effect = Instantiate(levelManager.spazzCollectedStarEffect, levelManager.cameraTarget.position, levelManager.spazzCollectedStarEffect.transform.rotation);
             //   effect.transform.SetParent(levelManager.cameraTarget);
             //   Destroy(effect, 3f);
                isDone = true;
                Destroy(gameObject);
            }
        }
    }
}
