
using UnityEngine;

public class EnemySkeletalDragonProjectile : MonoBehaviour
{

    private EnemiesManager enemiesManager;
    //  public float moveSpeed = 7f;
    //  public float bulletTimeLife = 3f;
    private Rigidbody2D rb;

    private LevelManager levelManager;

    private Vector3 target;
    private Vector2 moveDirection;
    private Vector2 facingDirection;
    private float lookAngle;
    private Quaternion rotation;


    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();

        rb = GetComponent<Rigidbody2D>();

        target = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0, -1.5f, 0);

        facingDirection = target - transform.position;
        lookAngle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle );

        moveDirection = (target - transform.position).normalized * enemiesManager.enemySkeletalDragonProjectileSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

         Destroy(gameObject, 30f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (OptionsManager.screenShake)
            CinemachineCameraShake.Instance.ShakeCamera(4.5f, 0.15f);


        if (collision.gameObject.layer == 8)
        {
            Vector3 position = gameObject.transform.position;
            position.y = gameObject.transform.position.y;
            //   rotacja = levelManager.rotationObject.transform;
            if (collision.transform.position.y > transform.position.y)
                rotation = Quaternion.Euler(0f, 0f, 180f);
            else
                rotation = Quaternion.Euler(0f, 0f, 0f);

            enemiesManager = FindObjectOfType<EnemiesManager>();
            Instantiate(enemiesManager.enemySkeletalDragonStage2Projectile, position, rotation);
            //  gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Destroy(gameObject);
            rotation = Quaternion.Euler(0f, 0f, 0f);
            levelManager.audioManager.Play("EnemySkeletalDragonProjectileImpact", transform.position);

        }
        else if (collision.gameObject.tag == "LEFTWALL")
        {
            Vector3 position = gameObject.transform.position;
            position.y = gameObject.transform.position.y;
            // rotacja = levelManager.rotationObject.transform;

            rotation = Quaternion.Euler(0f, 0f, -90f);
            Instantiate(enemiesManager.enemySkeletalDragonStage2Projectile, position, rotation);
            Destroy(gameObject);
            rotation = Quaternion.Euler(0f, 0f, 0f);
            levelManager.audioManager.Play("EnemySkeletalDragonProjectileImpact", transform.position);

        }
        else if (collision.gameObject.tag == "RIGHTWALL")
        {
            Vector3 position = gameObject.transform.position;
            position.y = gameObject.transform.position.y;
            // rotacja = levelManager.rotationObject.transform;   jak nie diała coś t pewnie to !!!!!!!!!!!!!!!!!!

            rotation = Quaternion.Euler(0f, 0f, 90f);
            Instantiate(enemiesManager.enemySkeletalDragonStage2Projectile, position, rotation);
            Destroy(gameObject);
            rotation = Quaternion.Euler(0f, 0f, 0f);
            levelManager.audioManager.Play("EnemySkeletalDragonProjectileImpact", transform.position);
        }

        else if (collision.gameObject.layer == 24)
        { 
          // do nothing
        }else
          //  Debug.Log("KOLIZJAAAAAAAA: "+collision.gameObject.tag);
        Destroy(gameObject);
       // Debug.Log("KOLIZJAAAAAAAA: " + collision.gameObject.tag);
    }

}
