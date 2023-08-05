using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Hero6Component : MonoBehaviour
{

    private HeroesManager heroesManager;
    private HeroBaseComponent heroBaseComponent;
    private LevelManager levelManager;

    private float attackSpeedTimer;
  
    private float lookAngle;
    public Transform firePoint;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        heroBaseComponent = transform.parent.GetComponent<HeroBaseComponent>();
        levelManager = FindObjectOfType<LevelManager>();

        attackSpeedTimer = 0.0f;

        heroBaseComponent.PlayerEnergySizeRefresh(5);

       
    }

  
    void FixedUpdate()
    {
        attackSpeedTimer -= Time.fixedDeltaTime;

        /* shootDirection = Input.mousePosition;
         shootDirection.z = 0.0f;
         shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
         shootDirection = shootDirection - heroBaseComponent.spawnPocisku.position;
    */

        //   shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //   lookAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        lookAngle = AngleBetweenPoints(firePoint.position , mouseWorldPosition);

        firePoint.position = heroBaseComponent.spawnPocisku.position;
        //Ta daa
        if (heroesManager.isHeroHeadingRight)
        {
            if (lookAngle > 80f)
                lookAngle = 80f;
            else if (lookAngle < -80f)
                lookAngle = -80f;
        }
        else
        {
            if (lookAngle < 100f && lookAngle > 0f)
                lookAngle = 100f;
            else if (lookAngle > -100f && lookAngle < 0f)
                lookAngle = -100f;
        }

        firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, lookAngle));

        if (heroBaseComponent.isAndroidAttacking && attackSpeedTimer <= 0f)
        {
            SpawnHero6Projectile();
        }

       
      
    }
    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }

    private void SpawnHero6Projectile()
    {
        attackSpeedTimer = heroesManager.Hero6AttackSpeedValue;
        var projectile = Instantiate(heroesManager.Hero6ProjectilePrefab);
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        var bulletRigidbody1 = projectile.GetComponentInChildren<Rigidbody2D>();
        var projectileSize = projectile.GetComponent<Transform>();

        levelManager.audioManager.Play("Hero6Projectile");
       // if (!heroesManager.isHeroHeadingRight)
      //  {
            projectileSize.localScale *= (heroesManager.Hero6ProjectileSizeValue);
        bulletRigidbody1.velocity = firePoint.right * heroesManager.Hero6ProjectileSpeedValue;
        
      //  } 
     /*   else
        {
            projectileSize.localScale *= (heroesManager.Hero6ProjectileSizeValue);
            bulletRigidbody1.velocity = heroBaseComponent.spawnPocisku.right * heroesManager.Hero6ProjectileSpeedValue * Time.fixedDeltaTime;

        }*/
    }

}

