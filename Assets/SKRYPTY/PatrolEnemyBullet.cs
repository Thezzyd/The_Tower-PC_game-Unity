/*
using UnityEngine;

public class PatrolEnemyBullet : MonoBehaviour
{
   public float moveSpeed = 16f;
     public float bulletTimeLife = 4f;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public LevelManager levelManager;


  //  [HideInInspector] public Vector3 target;
    [HideInInspector] public Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("ColliderOff", 3.7f);
        Destroy(gameObject, bulletTimeLife);
        levelManager = FindObjectOfType<LevelManager>();
        moveDirection = (levelManager.cameraTarget.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        

        var particle = Instantiate(levelManager.patrolEnemyBulletEffect, transform.position, transform.rotation);
        Destroy(particle.gameObject, 0.5f);
        if (collision.gameObject.CompareTag("POCISK"))
        {
            var crash1 = Instantiate(levelManager.laserCrashEffect, transform.position, transform.rotation);
            Destroy(crash1, 0.5f);
            Destroy(collision.gameObject);
        }

    }

    public void ColliderOff()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
*/