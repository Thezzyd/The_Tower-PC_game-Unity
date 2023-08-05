
using UnityEngine;

public class EnemyHangingSpiderAnimationFunctions : MonoBehaviour
{
    public void AttackFunction()
    {
        transform.parent.parent.GetComponent<EnemyHangingSpider>().SpawnProjectile();
    }

}
