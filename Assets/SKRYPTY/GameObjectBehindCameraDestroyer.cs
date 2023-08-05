using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectBehindCameraDestroyer : MonoBehaviour
{
    private TowerSpawnManager towerSpawnManager;
    private LevelManager levelManager;
    private Transform thisTransform;

    void Start()
    {
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
        levelManager = FindObjectOfType<LevelManager>();

        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisTransform.position.y < levelManager.highestTowerYaxisValueReached - 30 - 40)
        {
            Destroy(gameObject);
        }
    }
}
