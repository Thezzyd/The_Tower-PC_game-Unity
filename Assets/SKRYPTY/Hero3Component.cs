using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class Hero3Component : MonoBehaviour
{
    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private HeroBaseComponent heroBaseComponent;

    private int builtCanoons;

    private bool isAttackingCheck;
    private float attackSpeedTimer;
    private float lookAngle;
    public Transform firePoint;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        heroBaseComponent = transform.parent.GetComponent<HeroBaseComponent>();

        isAttackingCheck = true;
        builtCanoons = 0;

        heroBaseComponent.PlayerEnergySizeRefresh(2);
    }

    void FixedUpdate()
    {
        attackSpeedTimer -= Time.fixedDeltaTime;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        lookAngle = AngleBetweenPoints(firePoint.position , mouseWorldPosition);
        firePoint.position = heroBaseComponent.spawnPocisku.position;

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
            SpawnHero3Projectile();
        }


        /*if (!heroBaseComponent.isAndroidAttacking)
        {
            isAttackingCheck = true;
        }

        if (heroBaseComponent.isAndroidAttacking && isAttackingCheck)
        {
            SpawnHero3Turret();
            isAttackingCheck = false;
        }*/

    }

    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }

   /* public void SpawnHero3Turret()
    {
        if (heroesManager.isHeroGrounded)
        {
            var canoonPosition = heroBaseComponent.transform.position;
            canoonPosition.y += 1.4f;

            try
            {
                if (heroesManager.Hero3ActiveTurrets[builtCanoons])
                {
                    heroesManager.Hero3ActiveTurrets[builtCanoons].GetComponentInChildren<Hero3TurretComponent>().Destruction();
                }

            } catch (ArgumentOutOfRangeException e)
            {
                heroesManager.Hero3ActiveTurrets.Add(new GameObject());
            }

           

            heroesManager.Hero3ActiveTurrets[builtCanoons] = Instantiate(heroesManager.Hero3TurretPrefab, canoonPosition, transform.rotation);
            levelManager.audioManager.Play("Hero3TurretBuilt");
    
            builtCanoons += 1;
            if (builtCanoons >= heroesManager.Hero3TurretMaxQuantityValue) { builtCanoons = 0; }
          
        }
    }
*/
    public void SpawnHero3Projectile(){

        attackSpeedTimer = heroesManager.Hero3AttackSpeedValue;


        var projectile = Instantiate(heroesManager.Hero3ProjectilePrefab, firePoint.position, Quaternion.Euler(0, 0, lookAngle));
        var bulletRigidbody1 = projectile.GetComponent<Rigidbody2D>();
        bulletRigidbody1.velocity = firePoint.right * heroesManager.Hero3ProjectileSpeedValue ; 

        for(int i = 1; i < heroesManager.Hero3BarrageQuantity; i++){
            if(heroesManager.Hero3BarrageQuantity <=5){
                Invoke("BarrageProjectile", 0.1f * i);
            }else{
                Invoke("BarrageProjectile", (0.5f / heroesManager.Hero3BarrageQuantity) * i);
            }
        }

    }

    public void BarrageProjectile(){
            var projectile = Instantiate(heroesManager.Hero3ProjectilePrefab, firePoint.position, Quaternion.Euler(0, 0, lookAngle + UnityEngine.Random.Range(-5.0f, 5.0f)));
            projectile.GetComponent<Rigidbody2D>().velocity = firePoint.right * heroesManager.Hero3ProjectileSpeedValue * UnityEngine.Random.Range(0.8f, 0.99f);         
    }

}
