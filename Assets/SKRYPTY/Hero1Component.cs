using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Hero1Component : MonoBehaviour
{
    private LevelManager levelManager;
    private HeroesManager heroesManager;
    private HeroBaseComponent heroBaseComponent;
 //   public ParticleSystem laserParticles;

    private float attackSpeedTimer;
    public GameObject hero1Laser;
    public Hero1LaserComponent hero1LaserComponent;
    public LineRenderer laserLineRenderer;

    private bool isAttacking;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        heroBaseComponent = transform.parent.GetComponent<HeroBaseComponent>();
      //  laserParticles = FindObjectOfType<>

        isAttacking = true;
        heroBaseComponent.PlayerEnergySizeRefresh(0);
    }


    void FixedUpdate()
    {
   

        if (heroBaseComponent.isAndroidAttacking && isAttacking)
        {  

            ActivateLaser();
            isAttacking = false;
        }
        else if (!heroBaseComponent.isAndroidAttacking)
        {
            isAttacking = true;
            DeactivateLaser();
        }

    }

    /* private void SpawnHero1Projectile()
     {
         GameObject hero1Projectile = Instantiate(heroesManager.Hero1ProjectilePrefab, heroBaseComponent.spawnPocisku.position, heroBaseComponent.spawnPocisku.rotation);

         levelManager.audioManager.Play("Hero1Projectile");

         var hero1ProjectileRigidbody = hero1Projectile.GetComponent<Rigidbody2D>();

         if (!heroesManager.isHeroHeadingRight)
         {
             hero1Projectile.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
             hero1ProjectileRigidbody.velocity = -heroBaseComponent.spawnPocisku.right * (heroesManager.Hero1ProjectileSpeedValue) * Time.fixedDeltaTime;
         }
         else
         {
             hero1ProjectileRigidbody.velocity = heroBaseComponent.spawnPocisku.right * (heroesManager.Hero1ProjectileSpeedValue) * Time.fixedDeltaTime;
         }

         attackSpeedTimer = heroesManager.Hero1AttackSpeedValue;
         Destroy(hero1Projectile, heroesManager.Hero1ProjectileLifetime);
     }*/

   private void ActivateLaser()
   {
        hero1Laser.transform.parent.position = heroBaseComponent.spawnPocisku.position;
        laserLineRenderer.enabled = true;
        laserLineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        laserLineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        hero1Laser.SetActive(true);

     //   laserParticles.Stop();
       // laserParticles.Play();

     //  levelManager.audioManager.Play("Hero1Projectile");
   }

    private void DeactivateLaser()
    {
        laserLineRenderer.positionCount = 2;
        hero1LaserComponent.lockedEnemiesList.Clear();

        laserLineRenderer.enabled = false;
        hero1Laser.SetActive(false);

        //  levelManager.audioManager.Play("Hero1Projectile");
    }

}

