using System;
using System.Collections.Generic;
using UnityEngine;

public class Hero1LaserComponent : MonoBehaviour
{
    private HeroesManager heroesManager;
    private HeroBaseComponent heroBaseComponent;
    private RaycastHit2D hit;

    public Transform rayCastStartPoint;
    public ParticleSystem laserHitEffect;
    public LineRenderer laserLineRenderer;
    public float length = 100f;

    private float attackcounter;
    private List<Collider2D> objectsInRadius;
    private List<Transform> objectsInRadiusTransform;
    private int actualActivatedChains;

    public List<Transform> lockedEnemiesList;
    public Transform firstTarget;
    public Transform nearestEnemyTransform;

    private void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        heroBaseComponent = FindObjectOfType<HeroBaseComponent>();
        attackcounter = 0f;
        actualActivatedChains = 0;

        lockedEnemiesList = new List<Transform>();
        objectsInRadius = new List<Collider2D>();
        objectsInRadiusTransform = new List<Transform>();

    }


    void FixedUpdate()
    {
        attackcounter -= Time.fixedDeltaTime;

        Vector3 forwardVel = transform.parent.forward;
        Vector3 horizontalVel = transform.parent.right;

        hit = Physics2D.Raycast(rayCastStartPoint.position, (forwardVel + horizontalVel) * 5f, length, (1 << 8) | (1 << 13) | (1 << 17) | (1 << 18) | (1 << 22) | (1 << 24) | (1 << 29));

      
        if (hit.collider)
        {
            laserLineRenderer.SetPosition(0, heroBaseComponent.spawnPocisku.position);
            laserLineRenderer.SetPosition(1, hit.point);

            if (attackcounter <= 0f)
            {
                laserHitEffect.transform.position = hit.point;
                laserHitEffect.Emit(6);    
            }

            if ((hit.collider.gameObject.layer == 17 || hit.collider.gameObject.layer == 22 || hit.collider.gameObject.layer == 13) && attackcounter <= 0f)
            {
                GameObject enemyObject = hit.collider.gameObject;
                attackcounter = 0.05f;

                firstTarget = enemyObject.transform;

                switch (enemyObject.tag)
                {
                    case "EnemyBat": enemyObject.GetComponent<EnemyBat>().DamageTaken(gameObject, 1); break;
                    case "EnemyFirefly": enemyObject.GetComponent<EnemyFirefly>().DamageTaken(gameObject, 1); break;
                    case "EnemyBabySpider": enemyObject.GetComponent<EnemyBabySpider>().DamageTaken(gameObject, 1); break;
                    case "EnemyNormalSpider": enemyObject.GetComponent<EnemyNormalSpider>().DamageTaken(gameObject, 1); break;
                    case "EnemyJumpingSpider": enemyObject.GetComponent<EnemyJumpingSpider>().DamageTaken(gameObject, 1); break;
                    case "EnemyBigSpider": enemyObject.GetComponent<EnemyBigSpider>().DamageTaken(gameObject, 1); break;
                    case "EnemyEggSpider": enemyObject.GetComponent<EnemyEggEmpty>().HatchEgg(); break;
                    case "EnemyHangingSpider": enemyObject.GetComponent<EnemyHangingSpider>().DamageTaken(gameObject, 1); break;
                    case "EnemySkeletalDragon": enemyObject.GetComponent<EnemySkeletalDragon>().DamageTaken(gameObject, 1); break;
                    case "EnemyTentacleWorm": enemyObject.GetComponent<EnemyTentacleWorm>().DamageTaken(gameObject, 1); break;
                    case "EnemyTentacleWormProjectile": enemyObject.transform.parent.GetComponent<EnemyTentacleWormProjectile>().DestroyBullet(); break;
                }

                if (heroesManager.Hero1maxChainsNumber >= 1)
                    ChainSetter(hit.collider.transform.position, 1);

                Chain();
            }
            else if (hit.collider.gameObject.layer == 24)
            {
                laserLineRenderer.positionCount = 2;
                lockedEnemiesList.Clear();
                GameObject webSegmentObject = hit.collider.gameObject;
                webSegmentObject.GetComponent<RopeSegmentBehaviourComponent>().WebSplit();
            }
            else if (attackcounter <= 0f)
            {
                lockedEnemiesList.Clear();
                laserLineRenderer.positionCount = 2;
             //   Debug.Log("trafiloooo w layer: " + hit.collider.gameObject.layer);
                CinemachineCameraShake.Instance.ShakeCamera(0.8f, 0.1f);
            }
        }
        else {
            lockedEnemiesList.Clear();
            laserLineRenderer.positionCount = 2;
        }
        
     
    }

    private void Chain()
    {
        for (int i = 0; i < lockedEnemiesList.Count; i++)
        {
            if (lockedEnemiesList[i] == null)
            {
                laserLineRenderer.positionCount = 2 + i;
                break;
            }

            laserLineRenderer.positionCount = 3 + i;
            laserLineRenderer.SetPosition(2 + i, lockedEnemiesList[i].position); // 2 + bo indeksowanie od 0

            float dmgMultiplier = Mathf.Pow(heroesManager.Hero1chainEfficiency, (i + 1));

            switch (lockedEnemiesList[i].tag)
            {
                case "EnemyBat": lockedEnemiesList[i].GetComponent<EnemyBat>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyFirefly": lockedEnemiesList[i].GetComponent<EnemyFirefly>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyBabySpider": lockedEnemiesList[i].GetComponent<EnemyBabySpider>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyNormalSpider": lockedEnemiesList[i].GetComponent<EnemyNormalSpider>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyJumpingSpider": lockedEnemiesList[i].GetComponent<EnemyJumpingSpider>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyBigSpider": lockedEnemiesList[i].GetComponent<EnemyBigSpider>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyEggSpider": lockedEnemiesList[i].GetComponent<EnemyEggEmpty>().HatchEgg(); break;
                case "EnemyHangingSpider": lockedEnemiesList[i].GetComponent<EnemyHangingSpider>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemySkeletalDragon": lockedEnemiesList[i].GetComponent<EnemySkeletalDragon>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyTentacleWorm": lockedEnemiesList[i].GetComponent<EnemyTentacleWorm>().DamageTaken(gameObject, dmgMultiplier); break;
                case "EnemyTentacleWormProjectile": lockedEnemiesList[i].transform.parent.GetComponent<EnemyTentacleWormProjectile>().DestroyBullet(); break;
            }

            try
            {
                if (heroesManager.Hero1chainHitEffect[i] != null)
                {
                    heroesManager.Hero1chainHitEffect[i].transform.position = lockedEnemiesList[i].position;
                    heroesManager.Hero1chainHitEffect[i].Emit(6);
                }
                else
                {
                    heroesManager.Hero1chainHitEffect.Add(Instantiate(heroesManager.Hero1chainHitEffectPrefab));
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                heroesManager.Hero1chainHitEffect.Add(Instantiate(heroesManager.Hero1chainHitEffectPrefab));
            }
        }

        if(lockedEnemiesList.Count == 0)
            laserLineRenderer.positionCount = 2;

    }

    private void PutItemIntoLockedEnemiesList(Transform objectItem, int indexInList)
    {
        if (!lockedEnemiesList.Contains(objectItem))
        {
          //  Debug.Log("nieposiada weszloo");
            if (lockedEnemiesList.Count > indexInList)
            {
                if (lockedEnemiesList[indexInList] == null)
                    lockedEnemiesList[indexInList] = objectItem;
            }
            else
            {
                lockedEnemiesList.Add(objectItem);
            }
        }

        for (int i = 0; i < lockedEnemiesList.Count; i++)
        {
            if (lockedEnemiesList[i] == null)
            {
                lockedEnemiesList.RemoveAt(i);
            }
        }

    }

    private void ChainSetter(Vector2 StartingWorldPoint, int whichChain)
    {
        objectsInRadiusTransform.Clear();
        objectsInRadius = new List<Collider2D>( Physics2D.OverlapCircleAll(StartingWorldPoint, heroesManager.Hero1chainRadius,  (1 << 17) | (1 << 13) | (1 << 22)));


        for (int i = 0; i < objectsInRadius.Count; i++)
        {

            objectsInRadiusTransform.Add(objectsInRadius[i].transform);

            Vector2 raycastDir = new Vector2(objectsInRadius[i].transform.position.x, objectsInRadius[i].transform.position.y) - StartingWorldPoint;

            hit = Physics2D.Raycast(StartingWorldPoint, raycastDir * 5f, Vector2.Distance(StartingWorldPoint, new Vector2(objectsInRadius[i].transform.position.x, objectsInRadius[i].transform.position.y)), (1 << 8) | (1 << 18));
            if (hit.collider)
                objectsInRadiusTransform[i] = null; 
        }


        for (int i = 0; i < lockedEnemiesList.Count; i++)
        {

            if (lockedEnemiesList[i] != null)
            {
                if (!objectsInRadiusTransform.Contains(lockedEnemiesList[i]))
                {
                    lockedEnemiesList[i] = null;
                }
            }
        }


        if (objectsInRadiusTransform.Contains(firstTarget))
        {
            objectsInRadiusTransform.Remove(firstTarget);
        }

        foreach (Transform tra in lockedEnemiesList)
        {
            if (tra != null)
            {
                if (objectsInRadiusTransform.Contains(tra))
                {
                    objectsInRadiusTransform.Remove(tra);
                }
            }  
        }

   

        float distanceMinBox = 10000;
        nearestEnemyTransform = null;

        
        foreach (Transform tra in objectsInRadiusTransform)
        {
            if (tra != null)
            {

                float distance = Vector2.Distance(StartingWorldPoint, tra.position);

                if (distanceMinBox > distance)
                {
                    distanceMinBox = distance;
                    nearestEnemyTransform = tra;
                }
            }
        }

      

        if (lockedEnemiesList.Contains(firstTarget))
        {
            int index = lockedEnemiesList.IndexOf(firstTarget);
            lockedEnemiesList[index] = null;
        }

        if (nearestEnemyTransform != null)
        {

            PutItemIntoLockedEnemiesList(nearestEnemyTransform, whichChain - 1);

            if (heroesManager.Hero1maxChainsNumber > whichChain)
                ChainSetter(nearestEnemyTransform.position, whichChain + 1);
        }
        else
        {
            PutItemIntoLockedEnemiesList(null, whichChain - 1);
        }

        /*   if (nearestEnemyTransform != null)
           {

               if(lockedEnemiesList[whichChain] == null ||)
                  laserLineRenderer.positionCount = 2 + whichChain;
                  laserLineRenderer.SetPosition(1 + whichChain, nearestEnemyTransform.position); // 1+ bo indeksowanie od 0


               if (heroesManager.maxChainsNumber > whichChain + 1)
                   Chain(nearestEnemyTransform.position, whichChain + 1);
           }
           else
           {
               laserLineRenderer.positionCount = 2 + whichChain -1;
           }*/

    }

  

}
