using UnityEngine;

public class EnemyHangingSpiderProjectileComponent : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 18 || collision.gameObject.layer == 25)
        {
            GameObject spawnedCrash = FindObjectOfType<EnemiesManager>().enemyHangingSiderProjectileCrashPrefab;

            spawnedCrash = Instantiate(spawnedCrash);
            spawnedCrash.transform.position = transform.position;
            FindObjectOfType<AudioManager>().Play("EnemyHangingSpiderProjectileCrash", transform.position);

            Destroy(spawnedCrash, 3f);
        }

        Destroy(gameObject);
    }


}
