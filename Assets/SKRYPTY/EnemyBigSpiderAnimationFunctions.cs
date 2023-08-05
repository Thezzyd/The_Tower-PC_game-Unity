using UnityEngine;

public class EnemyBigSpiderAnimationFunctions : MonoBehaviour
{
    private EnemyBigSpider enemyBigSpider;

    private void Start()
    {
        enemyBigSpider = transform.parent.parent.GetComponent<EnemyBigSpider>();
    }

    public void SpawnProjectile()
    {
        enemyBigSpider.SpawnProjectile();
    }

    public void AllowToMove()
    {
        enemyBigSpider.AllowToMove();
    }

    public void ForbidToMove()
    {
        enemyBigSpider.ForbidToMove();
    }


}
