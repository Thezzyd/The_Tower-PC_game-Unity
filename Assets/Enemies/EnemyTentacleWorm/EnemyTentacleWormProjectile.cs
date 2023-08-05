using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTentacleWormProjectile : MonoBehaviour
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

        moveDirection = (target + new Vector3(0f, Random.Range(-1.8f, 1.6f), 0f) - transform.position).normalized * enemiesManager.enemyTentacleWormProjectileMoveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, enemiesManager.enemyTentacleWormProjectileLifetime);
    }

    public void DestroyBullet()
    {
        var bulletBreak = Instantiate(enemiesManager.enemyTentacleWormProjectileDestryedEffect, gameObject.transform.position, enemiesManager.enemyTentacleWormProjectileDestryedEffect.transform.rotation);
        var bulletShatred = Instantiate(enemiesManager.enemyTentacleWormProjectileShatredEffect, gameObject.transform.position, enemiesManager.enemyTentacleWormProjectileShatredEffect.transform.rotation);

        levelManager.audioManager.Play("EnemyBigSpiderProjectileCrash", transform.position);

        Destroy(bulletBreak.gameObject, 0.8f);
        Destroy(bulletShatred, 6f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string coliderTag = collision.gameObject.tag;

        if (OptionsManager.screenShake)
            CinemachineCameraShake.Instance.ShakeCamera(3f, 0.1f);

        if (!collision.CompareTag("LeftEdge") && !collision.CompareTag("RightEdge"))
        {
            if (collision.gameObject.layer == 18 || coliderTag.Equals("AttackHero2") || collision.gameObject.layer == 8 ||
                coliderTag.Equals("AttackHero1") || coliderTag.Equals("AttackHero3") || coliderTag.Equals("AttackHero6"))
            {
                var bulletBreak = Instantiate(enemiesManager.enemyTentacleWormProjectileDestryedEffect, gameObject.transform.position, enemiesManager.enemyTentacleWormProjectileDestryedEffect.transform.rotation);
                var bulletShatred = Instantiate(enemiesManager.enemyTentacleWormProjectileShatredEffect, gameObject.transform.position, enemiesManager.enemyTentacleWormProjectileShatredEffect.transform.rotation);

                levelManager.audioManager.Play("EnemyBigSpiderProjectileCrash", transform.position);

                Destroy(bulletBreak.gameObject, 0.8f);
                Destroy(bulletShatred, 6f);
                Destroy(gameObject);
            }
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            var bulletBreak = Instantiate(enemiesManager.enemyTentacleWormProjectileDestryedEffect, gameObject.transform.position, enemiesManager.enemyTentacleWormProjectileDestryedEffect.transform.rotation);
            var bulletShatred = Instantiate(enemiesManager.enemyTentacleWormProjectileShatredEffect, gameObject.transform.position, enemiesManager.enemyTentacleWormProjectileShatredEffect.transform.rotation);

            levelManager.audioManager.Play("EnemyBigSpiderProjectileCrash", transform.position);

            Destroy(bulletBreak.gameObject, 0.8f);
            Destroy(bulletShatred, 6f);

            Destroy(gameObject);
        }

    }
}
