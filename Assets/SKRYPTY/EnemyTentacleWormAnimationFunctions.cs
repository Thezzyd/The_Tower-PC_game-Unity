using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTentacleWormAnimationFunctions : MonoBehaviour
{
    private EnemyTentacleWorm enemyTentacleWorm;

    private void Start()
    {
        enemyTentacleWorm = transform.parent.parent.GetComponent<EnemyTentacleWorm>();
    }

    public void SpawnProjectile()
    {
        enemyTentacleWorm.SpawnProjectile();
    }
}
