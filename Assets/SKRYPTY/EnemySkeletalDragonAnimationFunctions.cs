using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletalDragonAnimationFunctions : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemySkeletalDragon enemySkeletalDragon;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        enemySkeletalDragon = transform.parent.parent.GetComponent<EnemySkeletalDragon>();
    }

    public void PlayWingSoundEffect()
    {
        levelManager.audioManager.Play("EnemySkeletalDragonWings", transform.position);
    }

    public void BeginAttack() 
    {
        enemySkeletalDragon.BeginAttack();
    }

    public void SpawnProjectile()
    {
        enemySkeletalDragon.SpawnProjectile();
    }

}
