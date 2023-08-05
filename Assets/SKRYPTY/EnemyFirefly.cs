using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Pathfinding;
using TMPro;

public class EnemyFirefly : MonoBehaviour
{
    private LevelManager levelManager;
    private EnemiesManager enemiesManager;
    public EnemyFireflyHive enemyFireflyHive;

    public Animator fireflyHolderAnim;
    public Transform fireflyHolderTransform;
    private Animator anim;
    public Transform scalableObject;

    public TextMeshPro healthLeftText;
    public GameObject healthBar;

    private float startingLifePool;
    private float actualLifePool;

    private float scaleMultiplier;

    private Vector3 locScale;
    private Vector3 scalePlus;
    private Vector3 scaleMinus;

    public bool isTriggered;

    public AIPath aiPath;
    public bool isMovingAnimFinished;
    public bool isRoomingAnimFinished;

    private bool transition;

    private float xPositionBox;
    private float bloodEffectTimer;


    void Start()
    {
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        enemiesManager = FindObjectOfType<EnemiesManager>();

        actualLifePool = enemiesManager.enemyFireflyLifePool;
        startingLifePool = actualLifePool;

        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemyFireflyScaleMultiplierMin, enemiesManager.enemyFireflyScaleMultiplierMax);
        transform.localScale = new Vector3(transform.localScale.x * scaleMultiplier, transform.localScale.y * scaleMultiplier, 1f);

        scalePlus = new Vector3(1, 1, 1);
        scaleMinus = new Vector3(-1, 1, 1);

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";

        aiPath.maxSpeed = UnityEngine.Random.Range(enemiesManager.enemyFireflyMoveSpeedMin, enemiesManager.enemyFireflyMoveSpeedMax);

        aiPath.canMove = false;
        aiPath.canSearch = true;

        xPositionBox = transform.position.x;
        fireflyHolderAnim.SetFloat("animSpeed", UnityEngine.Random.Range(0.8f, 1.5f)) ;

        float zRotationValue = UnityEngine.Random.Range(0.0f, 180.0f);

        fireflyHolderTransform.rotation = Quaternion.Euler(fireflyHolderTransform.eulerAngles.x, fireflyHolderTransform.eulerAngles.y, fireflyHolderTransform.eulerAngles.z + zRotationValue);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - zRotationValue);

        anim.SetFloat("animSpeed", UnityEngine.Random.Range(0.8f, 1.4f));
        bloodEffectTimer = 0;
    }


    void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;

        if (isTriggered)
        {
            if (!transition)
            {
                fireflyHolderAnim.enabled = false;
                anim.SetBool("isMoving", true);

                aiPath.canMove = true;

                FixPositionWhenTriggered();

                transition = true;
            }

            if (aiPath.canMove && aiPath.desiredVelocity.x >= 0.01f)
            {
                scalableObject.localScale = scalePlus;
             //   transform.eulerAngles = new Vector3(0, 0, -30);
            }
            else if (aiPath.canMove)
            {
                scalableObject.localScale = scaleMinus;
              //  transform.eulerAngles = new Vector3(0, 0, 30);
            }


            if (Physics2D.OverlapCircle(transform.position, 3f, (1 << 25)))
            {
                anim.SetTrigger("explozion");
            }

        }

        else
        {

            if (xPositionBox > transform.position.x)
            {
                scalableObject.localScale = scaleMinus;
                // transform.eulerAngles = new Vector3(0, 0, 30);
                xPositionBox = transform.position.x;
            }
            else
            {
                scalableObject.localScale = scalePlus;
                // transform.eulerAngles = new Vector3(0, 0, -30);
                xPositionBox = transform.position.x;
            }
        }

        aiPath.destination = levelManager.cameraAndEnemiesTargetPoint.position;

    }

    private IEnumerator TurnOnMovement(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        aiPath.canMove = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero1") || coliderTag.Equals("AttackHero2") || coliderTag.Equals("AttackHero3")
            || coliderTag.Equals("AttackHero6"))
        {

            DamageTaken(other.gameObject, 1);
        }

    }

    public void OnParticleCollision(GameObject other)
    {
        string coliderTag = other.tag;

        if (coliderTag.Equals("AttackHero4") || coliderTag.Equals("AttackHero5"))
        {
            DamageTaken(other, 1);
        }
    }

    public void DamageTaken(GameObject colliderGameObject, float damageMultiplier)
    {

        actualLifePool -= enemiesManager.TakeDamageAfterHeroAttack(colliderGameObject.tag, gameObject, colliderGameObject, damageMultiplier);
        actualLifePool = (float)Math.Round(actualLifePool, 2);


        if (colliderGameObject.Equals("AttackHero1") || colliderGameObject.Equals("AttackHero2"))
        {
            aiPath.canMove = false;
            StartCoroutine(TurnOnMovement(0.15f));
        }


        locScale.x = (actualLifePool / startingLifePool);

        if (locScale.x >= 0f)
            healthBar.transform.localScale = locScale;
        else
        {
            locScale.x = 0f;
            healthBar.transform.localScale = locScale;
        }

        healthLeftText.text = actualLifePool + " / " + startingLifePool + " HP";



        if (bloodEffectTimer <= 0)
        {
            enemiesManager.HitBloodEffect(enemiesManager.enemyFireflyHitBloodEffectPrefab, transform.position);
            bloodEffectTimer = enemiesManager.bloodHitEffectReload;
        }

        if (actualLifePool <= 0)
            Death();
        else
            levelManager.audioManager.Play("EnemyFireflyHit", transform.position);

        if (!isTriggered)
        {
            if (enemyFireflyHive != null)
            {
                enemyFireflyHive.TriggerAllMembersInHive();
            }
            else 
            {
                isTriggered = true;
            }
        }

    }

    public void Death()
    {
        levelManager.audioManager.Play("EnemyFireflyDeath", transform.position);

        levelManager.ExpGain("EnemyFirefly");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyFireflyExplozionEffectPrefab, transform.position);

        enemiesManager.DropOfEssence(enemiesManager.enemyFireflyEssenceDropMinQuantity, enemiesManager.enemyFireflyEssenceDropMaxQuantity, transform.position);

        Destroy(fireflyHolderTransform.gameObject);
    }

    public void FixPositionWhenTriggered()
    {
        transform.parent.position = transform.position;
        transform.localPosition = new Vector3(0, 0, 0);

        Debug.Log("fixingPosition");
    }

  
    public void FireflyExplozion()
    {
        if (Physics2D.OverlapCircle(transform.position, 3.2f, (1 << 25)))
        {
            StartCoroutine(KillHero(0.08f));

            levelManager.audioManager.Play("EnemyFireflyDeath", transform.position);

            enemiesManager.DeathBloodEffect(enemiesManager.enemyFireflyBigExplozionEffectPrefab, transform.position);

        }
        else 
        {
            levelManager.audioManager.Play("EnemyFireflyDeath", transform.position);

            enemiesManager.DeathBloodEffect(enemiesManager.enemyFireflyBigExplozionEffectPrefab, transform.position);

            Destroy(fireflyHolderTransform.gameObject);
        }
    }

    private IEnumerator KillHero(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        FindObjectOfType<HeroKillTrigger>().PlayerDeath();

        Destroy(fireflyHolderTransform.gameObject);
    }


}
