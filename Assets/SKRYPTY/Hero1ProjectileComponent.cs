
using UnityEngine;

public class Hero1ProjectileComponent : MonoBehaviour
{

    private LevelManager levelManager;
    private float collisionCounter;
    private HeroesManager heroesManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        collisionCounter = 0f;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer.Equals(29) && !col.gameObject.CompareTag("medusaOrb") && !col.gameObject.CompareTag("EnemyPortal"))
        {
         //   levelManager = FindObjectOfType<LevelManager>();
            collisionCounter++;

        //    if (collisionCounter >= heroesManager.Hero1ProjectilePierceValue)
        //    { 
             //   var crash1 = Instantiate(heroesManager.projectileCrashEffectHero1, transform.position, transform.rotation);
                FindObjectOfType<AudioManager>().Play("Hero1ProjectileCrash", col.transform.position);
         //       Destroy(crash1, 1f);
                Destroy(gameObject);
          //  }
        }
    }


}
