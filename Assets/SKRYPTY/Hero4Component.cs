using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Hero4Component : MonoBehaviour
{
    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private HeroBaseComponent heroBaseComponent;

    private float frameCounter;

    public ParticleSystem hero4Particle;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;
    private ParticleSystem.MainModule beamSizeModule;
    private ParticleSystem.TrailModule trialModule;
    private ParticleSystem.EmissionModule emissionModule;


    private float lookAngle;
    public Transform firePoint;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        heroBaseComponent = transform.parent.GetComponent<HeroBaseComponent>();

    //    hero4Particle = Instantiate(heroesManager.Hero4Particle);
      //  hero4Particle.transform.SetParent(transform);
      
     //   hero4Particle.transform.position = heroBaseComponent.spawnPocisku.position;

        velocityModule = hero4Particle.velocityOverLifetime;
        beamSizeModule = hero4Particle.main;
        trialModule = hero4Particle.trails;
        emissionModule = hero4Particle.emission;

        heroBaseComponent.PlayerEnergySizeRefresh(3);
    }

    void FixedUpdate()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        lookAngle = AngleBetweenPoints(firePoint.position, mouseWorldPosition);

        firePoint.position = heroBaseComponent.spawnPocisku.position;

        if (heroesManager.isHeroHeadingRight)
        {
            if (lookAngle > 65f)
                lookAngle = 65f;
            else if (lookAngle < -65f)
                lookAngle = -65f;
        }
        else
        {
            if (lookAngle < 115f && lookAngle > 0f)
                lookAngle = 115f;
            else if (lookAngle > -115f && lookAngle < 0f)
                lookAngle = -115f;
        }

        firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, lookAngle));

        if (heroBaseComponent.isAndroidAttacking)
        {
            SpawnHero4Tendrils();
        }
        else
        {
            levelManager.audioManager.Stop("Hero4TendrilsRunning");
            emissionModule.rateOverTime = 0;
        }

        if(Time.timeScale <= 0.0f)
            levelManager.audioManager.Stop("Hero4TendrilsRunning");
    }

    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }

    public void SpawnHero4Tendrils()
    {
        hero4Particle.transform.position = heroBaseComponent.spawnPocisku.position;

        if (!levelManager.audioManager.isPlaying("Hero4TendrilsRunning", gameObject.transform.position))
            levelManager.audioManager.Play("Hero4TendrilsRunning", gameObject.transform.position);

        beamSizeModule.startSize = heroesManager.Hero4BeamSizeValue ;

        trialModule.widthOverTrail = new ParticleSystem.MinMaxCurve(0.2f, heroesManager.Hero4BeamTrailSizeValue);
        emissionModule.rateOverTime = heroesManager.Hero4BeamQuantityValue;
     //   frameCounter += 1;
  
            velocityModule.zMultiplier = heroesManager.Hero4BeamLengthValue;

     //  hero4Particle.Emit(heroesManager.Hero4BeamQuantityTransitionValue);

     //   if (frameCounter % 10f == heroesManager.Hero4BeamQuantityValue)
     //   {
     //       hero4Particle.Emit(1);
     //       frameCounter = 0f;
     //   }

        
    }

}
