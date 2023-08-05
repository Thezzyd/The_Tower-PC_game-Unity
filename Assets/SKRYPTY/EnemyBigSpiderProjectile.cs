using UnityEngine;

public class EnemyBigSpiderProjectile : MonoBehaviour
{

    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
    private Rigidbody2D rb;
    public Vector3 target;
    private Vector2 moveDirection;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();

        rb = GetComponent<Rigidbody2D>();

        moveDirection = (target + new Vector3(0f, -0.2f, 0f) - transform.position).normalized * enemiesManager.enemyBigSpiderProjectileMoveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (OptionsManager.screenShake)
            CinemachineCameraShake.Instance.ShakeCamera(3f, 0.1f);
        if (!collision.CompareTag("LeftEdge") && !collision.CompareTag("RightEdge"))
        {
            if (collision.gameObject.layer == 18 || collision.gameObject.CompareTag("AttackHero2") || collision.gameObject.layer == 8)
            {
                var bulletBreak = Instantiate(enemiesManager.bigSpiderProjectileDestryedEffect, gameObject.transform.position, enemiesManager.bigSpiderProjectileDestryedEffect.transform.rotation);
                levelManager.audioManager.Play("EnemyBigSpiderProjectileCrash", transform.position);

                Destroy(bulletBreak, 0.8f);
                Destroy(gameObject);
            }
        }
    }
}
