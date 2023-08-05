using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Hero5Component : MonoBehaviour
{
    private LevelManager levelManager;
    private HeroesManager heroesManager;
    private HeroBaseComponent heroBaseComponent;

    private ParticleSystem.MainModule startSpeedModule;
    private ParticleSystem.MainModule starLifeTimeModule;
    private ParticleSystem.EmissionModule burstEmissionModule;

    private float attackSpeedTimer;

    public GameObject hero5ParticlesGameObject;
    public ParticleSystem hero5ParticlesEffect;

    private float lookAngle;
    public Transform firePoint;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        heroBaseComponent = transform.parent.GetComponent<HeroBaseComponent>();
       
        attackSpeedTimer = 0f;

        heroBaseComponent.PlayerEnergySizeRefresh(4);

        startSpeedModule = hero5ParticlesEffect.main;
        starLifeTimeModule = hero5ParticlesEffect.main;
        burstEmissionModule = hero5ParticlesEffect.emission;
    }

    void FixedUpdate()
    {
        attackSpeedTimer -= Time.fixedDeltaTime;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        lookAngle = AngleBetweenPoints(firePoint.position, mouseWorldPosition);

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
           SpawnHero5Projectiles();
        }

    }
    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }

    public void SpawnHero5Projectiles()
    {

        //  if (heroesManager.isHeroHeadingRight)
        //     attackHERO5 = Instantiate(heroesManager.Heero5ProjectilesRightPrefab, heroBaseComponent.spawnPocisku.position, heroBaseComponent.spawnPocisku.rotation);
        //   else
        //     attackHERO5 = Instantiate(heroesManager.Hero5ProjectilesLeftPrefab, heroBaseComponent.spawnPocisku.position, heroBaseComponent.spawnPocisku.rotation);

        starLifeTimeModule.startLifetime = new ParticleSystem.MinMaxCurve(heroesManager.Hero5ProjectileLifeTimeMinValue, heroesManager.Hero5ProjectileLifeTimeMaxValue);
        startSpeedModule.startSpeed = new ParticleSystem.MinMaxCurve(heroesManager.Hero5ProjectileSpeedMinValue, heroesManager.Hero5ProjectileSpeedMaxValue);

        hero5ParticlesGameObject.transform.position = heroBaseComponent.spawnPocisku.position;
        hero5ParticlesEffect.Emit(heroesManager.Hero5ProjectileQuantityValue);

    
       // burstEmissionModule.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst (0.0f, heroesManager.Hero5ProjectileQuantityValue ) });

        levelManager.audioManager.Play("Hero5Projectile", transform.position);

        attackSpeedTimer = heroesManager.Hero5AttackSpeedValue;
    }


}
