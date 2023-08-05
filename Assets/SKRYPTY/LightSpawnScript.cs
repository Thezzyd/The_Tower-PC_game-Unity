using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawnScript : MonoBehaviour
{
    private TowerSpawnManager towerSpawnManager;

    void Start()
    {
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
        SpawnLIghtInLAntern();
    }

    public void SpawnLIghtInLAntern() {

        int whichPrefab = Random.Range(0, towerSpawnManager.lanternLightPrefabs.Length);

       GameObject light = Instantiate(towerSpawnManager.lanternLightPrefabs[whichPrefab]);
       light.transform.SetParent(gameObject.transform);
       light.transform.position = gameObject.transform.position;
       float animSpeedValue = Random.Range(0.4f, 1.1f);
       light.GetComponent<Animator>().SetFloat("Speed", animSpeedValue);

    }

}
