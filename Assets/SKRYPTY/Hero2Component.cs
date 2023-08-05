using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class Hero2Component : MonoBehaviour
{
    private LevelManager levelManager;
    private HeroesManager heroesManager;
    private HeroBaseComponent heroBaseComponent;

    private float attackSpeedTimer;
    private bool isDirectionSwap;
    private bool slashSet;

   // private ParticleSystem.MainModule lifetimeSlashRight;
 //   public ParticleSystem.MainModule[] lifetimeSlash;
  // private ParticleSystem.MainModule lifetimeSlashLeft;

 //   private ParticleSystem[] particles;
    private GameObject slash;

  //  public ParticleSystem[] slashParticles;

    private float lookAngle;
    public Transform firePoint;

    public Animator anim;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        heroBaseComponent = transform.parent.GetComponent<HeroBaseComponent>();

     //   lifetimeSlash = new ParticleSystem.MainModule[slashParticles.Length];

        attackSpeedTimer = 0.0f;

        heroBaseComponent.PlayerEnergySizeRefresh(1);

        isDirectionSwap = heroesManager.isHeroHeadingRight;

     //   for (int i = 0; i < slashParticles.Length; i++)
     //   {
     //       lifetimeSlash[i] = slashParticles[i].main;
     //   }

    }

    void FixedUpdate()
    {
       // SlashRotationSetter();

        if (heroBaseComponent.isAndroidAttacking )
        {
            anim.SetBool("isAttacking", true);
          
       //     SpawnHero2Slash();
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }


        if (heroesManager.isHeroHeadingRight)
        {
            firePoint.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            firePoint.localScale = new Vector3(-1, 1, 1);

        }


        //  AdjustSlashScale();
    }

    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }

   /* private void SlashRotationSetter()
    {
       

        if (heroesManager.isHeroHeadingRight != isDirectionSwap)
        {
           // lookAngle = Mathf.Abs(lookAngle);
            firePoint.localScale = new Vector3(-firePoint.localScale.x, firePoint.localScale.y, firePoint.localScale.z);
            firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, -lookAngle));
            isDirectionSwap = heroesManager.isHeroHeadingRight;
        }
      //  else
      //  {
          //  lookAngle = -Mathf.Abs(lookAngle);
       //     firePoint.localScale = new Vector3(1, -1, firePoint.localScale.z);
       //     firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, -tempAngle));
     //   }
    }*/



    public void SpawnHero2Slash()
    {
       // firePoint.localScale = new Vector3(1, 1, 1);

        levelManager.audioManager.Play("Hero2Slash");
        anim.SetFloat("animSpeed", 1 / heroesManager.Hero2AttackSpeedValue);

   /*     Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        lookAngle = AngleBetweenPoints(firePoint.position, mouseWorldPosition);

        firePoint.position = heroBaseComponent.spawnPocisku.position;

        if (heroesManager.isHeroHeadingRight)
        {
            if (lookAngle > 55)
                lookAngle = 55;
            else if (lookAngle < -55)
                lookAngle = -55;

            firePoint.localScale = new Vector3(firePoint.localScale.x, 1, firePoint.localScale.z);
        }
        else
        {
            if (lookAngle < 125f && lookAngle > 0f)
                lookAngle = 125f;
            else if (lookAngle > -125f && lookAngle < 0f)
                lookAngle = -125f;

            firePoint.localScale = new Vector3(firePoint.localScale.x, -1, firePoint.localScale.z);
        }

        firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, lookAngle));
        */
    }

}
