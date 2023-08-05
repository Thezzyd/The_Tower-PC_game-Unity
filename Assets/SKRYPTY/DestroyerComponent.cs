using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerComponent : MonoBehaviour
{

    public ParticleSystem splashEffectBig;
    public ParticleSystem splashEffectNormal;
    public ParticleSystem splashEffectSmall;

    private TowerSpawnManager towerSpawnManager;

    private void Start()
    {
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        string tag = collision.gameObject.tag;

        ParticleSystem splash;


        if (tag.Equals("EnemyBigSpider") || tag.Equals("EnemyHangingSpider") || tag.Equals("EnemySkeletalDragon"))
            splash = splashEffectBig;
        else if (tag.Equals("EnemyNormalSpider") || tag.Equals("SpiderEgg"))
            splash = splashEffectNormal;
        else
            splash = splashEffectSmall;

        splash = Instantiate(splash);
        splash.transform.position = new Vector3(collision.transform.position.x, transform.position.y, 0);
        Destroy(splash.gameObject, 3f);

        Destroy(collision.gameObject);
    }

 /*   private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }*/

}
