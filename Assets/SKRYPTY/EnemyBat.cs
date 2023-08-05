using UnityEngine;
using Pathfinding;
using TMPro;
using System;
using System.Collections;

public class EnemyBat : MonoBehaviour
{
    private EnemiesManager enemiesManager;
    private LevelManager levelManager;

    private float startingLifePool;
    private float actualLifePool;

    public TextMeshPro healthLeftText;
    public GameObject healthBar;
    private AIPath aiPath;
    //  private AIDestinationSetter batTarget;
    private float scaleMultiplier;

    private Vector3 locScale;
    private Vector3 scalePlus;
    private Vector3 scaleMinus;

    public Transform scalableObject;
    //  private float serachTargetRange;
    private float bloodEffectTimer;

    private void FindInMapObjects()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        FindInMapObjects();

        actualLifePool = enemiesManager.enemyBatLifePool;
        startingLifePool = actualLifePool;

        scaleMultiplier = UnityEngine.Random.Range(enemiesManager.enemyBatScaleMultiplierMin, enemiesManager.enemyBatScaleMultiplierMax);
        transform.localScale = new Vector3(transform.localScale.x * scaleMultiplier, transform.localScale.y * scaleMultiplier, 1f);

        scalePlus = new Vector3(1f, 1f, 1f);
        scaleMinus = new Vector3(-1f, 1f, 1f);

        locScale = healthBar.transform.localScale;
        healthLeftText.text = actualLifePool+" / "+startingLifePool+" HP";

     //   serachTargetRange = 20f;

      //  StartCoroutine(ActivateSeraching(3.0f));

        aiPath = GetComponent<AIPath>();
     //   batTarget = GetComponent<AIDestinationSetter>();
      //  batTarget.target = levelManager.cameraAndEnemiesTargetPoint;
        aiPath.maxSpeed = UnityEngine.Random.Range(enemiesManager.enemyBatMoveSpeedMin, enemiesManager.enemyBatMoveSpeedMax);

        StartCoroutine(TurnOffSerachSystem(1.0f));
        StartCoroutine(TurnOnSerachSystem(2.0f));
        bloodEffectTimer = 0;
    }

    private IEnumerator TurnOffSerachSystem(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        aiPath.canSearch = false;
    }

    private IEnumerator TurnOnSerachSystem(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        aiPath.canSearch = true;
    }

    void FixedUpdate()
    {
        bloodEffectTimer -= Time.fixedDeltaTime;

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            scalableObject.localScale = scaleMinus;
        }
        else
        {
            scalableObject.localScale = scalePlus;
        }

        aiPath.destination = levelManager.cameraAndEnemiesTargetPoint.position;
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

    private IEnumerator TurnOnMovement(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        aiPath.canMove = true;
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
            enemiesManager.HitBloodEffect(enemiesManager.enemyBatHitBloodEffect, transform.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0, 0));
            bloodEffectTimer = enemiesManager.bloodHitEffectReload;
        }

        if (actualLifePool <= 0)
            Death();
        else
            levelManager.audioManager.Play("EnemyBatHit", transform.position);

    }

   
    public void Death()
    {
        levelManager.audioManager.Play("EnemyBatDeath", transform.position);

        levelManager.ExpGain("EnemyBat");

        enemiesManager.DeathBloodEffect(enemiesManager.enemyBatDeathBloodEffect, transform.position);

        enemiesManager.DropOfEssence(enemiesManager.enemyBatEssenceDropMinQuantity, enemiesManager.enemyBatEssenceDropMaxQuantity, transform.position);
        
        Destroy(gameObject);
    }


}
