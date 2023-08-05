
using UnityEngine;

public class SpiderBossBullet : MonoBehaviour
{

    private EnemiesManager enemiesManager;
    private HeroesManager heroesManager;
    private SkillsUpgradeManager skillsUpgradeManager;

    public float moveSpeed = 8f;
    [HideInInspector] public float bulletTimeLife = 8f;
    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public LevelManager levelManager;

    public Transform target;
    [HideInInspector] public Vector2 moveDirection;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();
        levelManager = FindObjectOfType<LevelManager>();

        rb = GetComponent<Rigidbody2D>();


        target = levelManager.cameraAndEnemiesTargetPoint;

        moveSpeed = Random.Range(7f, 9f);


        moveDirection = (target.transform.position + new Vector3(0f, -0.2f, 0f) - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, bulletTimeLife);
    }

    /* private void OnParticleCollision(GameObject other)
     {
         if (other.CompareTag("MIECZ"))
         {
             var bulletBreak = Instantiate(levelManager.medusaBallDestryed, gameObject.transform.position, levelManager.medusaBallDestryed.transform.rotation);
             Destroy(bulletBreak, 0.8f);
             Destroy(gameObject);
             CinemacineCameraShake.Instance.ShakeCamera(3f, 0.1f);
         }

     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("MIECZ"))
        {
           var bulletBreak = Instantiate(levelManager.medusaBallDestryed, gameObject.transform.position, levelManager.medusaBallDestryed.transform.rotation);
            Destroy(bulletBreak, 0.8f);
        }*/
        if (OptionsManager.screenShake)
            CinemachineCameraShake.Instance.ShakeCamera(3f, 0.1f);
        if (collision.gameObject.CompareTag("LEFTWALL") || collision.gameObject.CompareTag("MIECZ") || collision.gameObject.CompareTag("RIGHTWALL") || collision.gameObject.CompareTag("GROUND"))
        {
            var bulletBreak = Instantiate(enemiesManager.bigSpiderProjectileDestryedEffect, gameObject.transform.position, enemiesManager.bigSpiderProjectileDestryedEffect.transform.rotation);
            Destroy(bulletBreak, 0.8f);
            Destroy(gameObject);
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
