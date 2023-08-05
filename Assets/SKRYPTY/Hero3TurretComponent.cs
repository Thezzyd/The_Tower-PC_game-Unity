/*
using UnityEngine;

public class Hero3TurretComponent : MonoBehaviour
{
    private LevelManager levelManager;
    private HeroesManager heroesManager;
    public Transform shootingPoint;

    private Vector2 lookAndShootDirection;
    private float lookAngle;
    public GameObject forCannonAudio;

    private Vector2 enemyPos;
    private Vector3 target;

    private bool isCurrentTargetKilled;

    private float attackSpeedTimer;

    private GameObject currentTarget;
    private Vector2 bulletMoveDirection;

    private RaycastHit2D raycastHit;
    private Animator anim;
    private bool canShoot;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        anim = GetComponentInParent<Animator>();

        isCurrentTargetKilled = true;
        attackSpeedTimer = heroesManager.Hero3TurretAttackSpeedValue;

        Invoke("ShootingActivation", 0.8f);
        Invoke("CannonShrink", (heroesManager.Hero3TurretLifeTimeValue) - 0.5f);

        Destroy(gameObject.transform.parent.gameObject, heroesManager.Hero3TurretLifeTimeValue);
    }


    public void Destruction()
    {
        canShoot = false;
        anim.SetBool("isDestroyed", true);
        Destroy(gameObject.transform.parent.gameObject, 0.5f);
    }


    public void CannonShrink()
    {
        canShoot = false;
        anim.SetBool("isDestroyed", true);
    }


    public void ShootingActivation()
    {
        canShoot = true;
    }


    void FixedUpdate()
    {
        attackSpeedTimer -= Time.fixedDeltaTime;

        if (attackSpeedTimer <= 0f && isCurrentTargetKilled == false)
        {

            if (currentTarget == null) { isCurrentTargetKilled = true; attackSpeedTimer = 0f; }
            else if (!gameObject.GetComponent<Collider2D>().IsTouching(currentTarget.GetComponent<Collider2D>())) { isCurrentTargetKilled = true; attackSpeedTimer = 0f; }
            else if (raycastHit)
            {
                if (raycastHit.collider.gameObject.layer == 8) 
                    isCurrentTargetKilled = true;

                attackSpeedTimer = 0f;
            }

            ShootTowardCurrentTarget();
        }
    }


    public void SpawnTurretProjectile()
    {
        var cannonBullet = Instantiate(heroesManager.Hero3TurretProjectilePrefab, shootingPoint.position, Quaternion.identity);
      
        bulletMoveDirection = (target - cannonBullet.transform.position) ;
        cannonBullet.GetComponent<Rigidbody2D>().velocity = bulletMoveDirection * heroesManager.Hero3TurretProjectileSpeed * Time.fixedDeltaTime;
        Destroy(cannonBullet, heroesManager.Hero3TurretProjectileLifetime);

        levelManager.audioManager.Play("Hero3TurretProjectile", transform.position);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
       
        enemyPos = new Vector2(collision.transform.position.x, collision.transform.position.y);
        enemyPos = new Vector2(collision.transform.position.x - shootingPoint.transform.position.x, collision.transform.position.y - shootingPoint.transform.position.y);
       
        var layerMask = ((1 << 8) | (1 << 17) | (1 << 13) | (1 << 22));
        float circleRadius = 0.218f;

        raycastHit = Physics2D.CircleCast(shootingPoint.position, circleRadius, enemyPos, Mathf.Infinity, layerMask);

        if (raycastHit)
        {
            if (isCurrentTargetKilled == true && raycastHit.collider.gameObject.layer != 8)
            {
                currentTarget = collision.gameObject;
                isCurrentTargetKilled = false;
            }
        }
    }

    public void ShootTowardCurrentTarget()
    {
        if (currentTarget != null && canShoot)
        { 
            target = currentTarget.transform.position;
            lookAndShootDirection = target - transform.position;
            lookAngle = Mathf.Atan2(lookAndShootDirection.y, lookAndShootDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
           
            attackSpeedTimer = heroesManager.Hero3TurretAttackSpeedValue ;

            SpawnTurretProjectile();
        } 
    }


}
*/