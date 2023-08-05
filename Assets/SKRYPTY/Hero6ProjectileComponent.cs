using System;
using UnityEngine;


public class Hero6ProjectileComponent : MonoBehaviour
{
    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private Vector3 homingProjectileDestinationPosition;
    private Vector3 target;
    private Rigidbody2D rb;

    private float homingProjectileVelocityMultiplier;
    private float forwardProjectileVelocityMultiplier;

    private bool isTurningBack;
    private float passingTimeMultiplier;
    private bool isRigibodyChanged;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        target= levelManager.cameraAndEnemiesTargetPoint.position;
        rb = GetComponentInChildren<Rigidbody2D>();
        homingProjectileVelocityMultiplier = 1f;
        forwardProjectileVelocityMultiplier = 1f;
        passingTimeMultiplier = heroesManager.Hero6ProjectileSpeedValue / 2f;
    }


    void FixedUpdate()
    {
        transform.Rotate(0, 0, 50 * Time.fixedDeltaTime);

        if (Math.Abs(rb.velocity.x) <= 2f && Math.Abs(rb.velocity.y) <= 2f && !isTurningBack)
        {
            isTurningBack = true;

        }
        else if(!isTurningBack)
        {
            forwardProjectileVelocityMultiplier -= Time.fixedDeltaTime / 5;
            rb.velocity = new Vector2(rb.velocity.x * forwardProjectileVelocityMultiplier, rb.velocity.y * forwardProjectileVelocityMultiplier);
          
        }

        if (isTurningBack)
        {
            if (!isRigibodyChanged)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                isRigibodyChanged = true;
            }

            homingProjectileVelocityMultiplier += Time.fixedDeltaTime * passingTimeMultiplier;
            target = levelManager.cameraAndEnemiesTargetPoint.position + new Vector3(0f, -0.2f, 0f);
            homingProjectileDestinationPosition = (target - transform.position) ;
            rb.velocity = new Vector2(homingProjectileDestinationPosition.x * homingProjectileVelocityMultiplier, homingProjectileDestinationPosition.y * homingProjectileVelocityMultiplier );

            if (Vector2.Distance(target, transform.position) <= 1.4f)
                  Destroy(gameObject);
        }
    }

 
}
