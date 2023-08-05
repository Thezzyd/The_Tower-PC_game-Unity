
using UnityEngine;

public class EnemySkeletalDragonProjectileExplosion : MonoBehaviour
{
    private float timeToDestroy;

    void Start()
    {
        timeToDestroy = 2f;
        Invoke("CollDeactivation", 0.2f);
        Destroy(gameObject, timeToDestroy);

    }

    private void CollDeactivation()
    {
        GetComponent<Collider2D>().enabled = false;
    }

}
