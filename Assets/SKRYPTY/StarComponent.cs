using UnityEngine;

public class StarComponent : MonoBehaviour
{
    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private TowerSpawnManager towerSpawnManager;
    private FirebaseManager firebaseManager;
    private PowerBar powerBar;
    private HeroBaseComponent heroBaseComponent;
    private bool isRollingNewHeroSucceded;
    private GameObject heroObject;
   // public Transform parentPlatform;
    //private bool isSequenceOver;

    public void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
        firebaseManager = FindObjectOfType<FirebaseManager>();
      //  powerBar = FindObjectOfType<PowerBar>();
      //  isSequenceOver = true;
    }

    private void FixedUpdate()
    {
        DestroyStarWhenItsPositionIsTooLow();
       
    }

    private void DestroyStarWhenItsPositionIsTooLow()
    {
        if (transform.position.y < towerSpawnManager.waterInGameObject.transform.position.y)
        {
            towerSpawnManager.StarSpawner(true);
            Destroy(transform.parent.gameObject);
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CoinCollector"))
        {   
            //  levelManager.RampageEssenceDropBonusCalculations();
            float indexOfCollectingStarHero = levelManager.heroIndexRolledByStar;
            if(!heroBaseComponent){
                heroBaseComponent = FindObjectOfType<HeroBaseComponent>();
                heroObject = heroBaseComponent.gameObject;
            }

            heroBaseComponent.DisableAllHeroComponents();

            if (indexOfCollectingStarHero == 4)
                levelManager.audioManager.Stop("Hero4TendrilsRunning");

            int temporaryRolledHeroIndex = 0;
            while (!isRollingNewHeroSucceded)
            {
                temporaryRolledHeroIndex = Random.Range(1, 7);

                if (temporaryRolledHeroIndex == 1 && indexOfCollectingStarHero != 1)      { heroBaseComponent.hero1Component.enabled = true; heroObject.name = "Hero1Prefab";  isRollingNewHeroSucceded = true; }
                else if (temporaryRolledHeroIndex == 2 && indexOfCollectingStarHero != 2) { heroBaseComponent.hero2Component.enabled = true; heroObject.name = "Hero2Prefab";  isRollingNewHeroSucceded = true; }
                else if (temporaryRolledHeroIndex == 3 && indexOfCollectingStarHero != 3) { heroBaseComponent.hero3Component.enabled = true; heroObject.name = "Hero3Prefab";  isRollingNewHeroSucceded = true; }
                else if (temporaryRolledHeroIndex == 4 && indexOfCollectingStarHero != 4) { heroBaseComponent.hero4Component.enabled = true; heroObject.name = "Hero4Prefab";  isRollingNewHeroSucceded = true; }
                else if (temporaryRolledHeroIndex == 5 && indexOfCollectingStarHero != 5) { heroBaseComponent.hero5Component.enabled = true; heroObject.name = "Hero5Prefab";  isRollingNewHeroSucceded = true; }
                else if (temporaryRolledHeroIndex == 6 && indexOfCollectingStarHero != 6) { heroBaseComponent.hero6Component.enabled = true; heroObject.name = "Hero6Prefab";  isRollingNewHeroSucceded = true; }
            }

            levelManager.heroIndexRolledByStar = temporaryRolledHeroIndex;

            //DelayedEssenceOrbsDrop(heroesManager.HeroesStarEssenceValue);
            //StartCoroutine(DropEssenceFromStar(heroesManager.HeroesStarEssenceValue));
           
            if (OptionsManager.screenShake)
            {
                CinemachineCameraShake.Instance.ShakeCamera(5f, 0.2f);
            }

            towerSpawnManager.StarSpawner(false);
            levelManager.starEssenceSpawnPosition = transform.position;
            levelManager.isStarCollectedSequenceOver = false;
           // StartCoroutine(OnStarCollectedSequence());

            Destroy(transform.parent.gameObject);
        }
    }

   /* private IEnumerator DropEssenceFromStar(float starEssenceValue){

        GameObject[] essence = new GameObject[3];
        bool lastEssenceChance = false;

        essence[0] = levelManager.essence_x1_prefab;
        essence[1] = levelManager.essence_x2_prefab;
        essence[2] = levelManager.essence_x3_prefab;

        GameObject essenceGO = null;

        while (starEssenceValue > 0)
        {
            int whichEssence;

            if (starEssenceValue >= 3)
                whichEssence = Random.Range(0, 3);
            else if (starEssenceValue >= 2)
                whichEssence = Random.Range(0, 2);
            else
                whichEssence = 0;

            switch (whichEssence)
            {
                case 0: essenceGO = Instantiate(essence[0]); starEssenceValue -= 1; break;
                case 1: essenceGO = Instantiate(essence[1]); starEssenceValue -= 2; break;
                case 2: essenceGO = Instantiate(essence[2]); starEssenceValue -= 3; break;
            }

            essenceGO.transform.position = transform.position;
            essenceGO.layer = 0;
            essenceGO.AddComponent<EssenceOrbDelayed>();
            essenceGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(7f, 12f)), ForceMode2D.Impulse);

            Destroy(essenceGO, levelManager.essenceLifeTime);

            if (!lastEssenceChance && starEssenceValue > 0.0f && starEssenceValue < 1.0f)
            {
                float lastEssenceChanceValue = Random.Range(0.0f, 1.0f);
                if (lastEssenceChanceValue < starEssenceValue)
                {
                    starEssenceValue = 1;
                }
                else
                {
                    starEssenceValue = 0;
                }
                lastEssenceChance = true;
            }
            yield return null; 
        }
    }*/

    private void DelayedEssenceOrbsDrop(float starEssenceValue)
    {
        GameObject[] essence = new GameObject[3];
        bool lastEssenceChance = false;

        essence[0] = levelManager.essence_x1_prefab;
        essence[1] = levelManager.essence_x2_prefab;
        essence[2] = levelManager.essence_x3_prefab;

        GameObject essenceGO = null;

        while (starEssenceValue > 0)
        {
            int whichEssence;

            if (starEssenceValue >= 3)
                whichEssence = Random.Range(0, 3);
            else if (starEssenceValue >= 2)
                whichEssence = Random.Range(0, 2);
            else
                whichEssence = 0;

            switch (whichEssence)
            {
                case 0: essenceGO = Instantiate(essence[0]); starEssenceValue -= 1; break;
                case 1: essenceGO = Instantiate(essence[1]); starEssenceValue -= 2; break;
                case 2: essenceGO = Instantiate(essence[2]); starEssenceValue -= 3; break;
            }

            essenceGO.transform.position = transform.position;
            essenceGO.layer = 0;
            essenceGO.AddComponent<EssenceOrbDelayed>();
            essenceGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(7f, 12f)), ForceMode2D.Impulse);

            Destroy(essenceGO, levelManager.essenceLifeTime);

            if (!lastEssenceChance && starEssenceValue > 0.0f && starEssenceValue < 1.0f)
            {
                float lastEssenceChanceValue = Random.Range(0.0f, 1.0f);
                if (lastEssenceChanceValue < starEssenceValue)
                {
                    starEssenceValue = 1;
                }
                else
                {
                    starEssenceValue = 0;
                }
                lastEssenceChance = true;
            }

        }
    }



        /*


        for (int i = 0; i < 5; i++)
        {
            var ranNum = Random.Range(1, 101);

            if (ranNum <= levelManager.rampageEssenceDropBonusX1Value)
                ranNum = 0;
            else if (ranNum <= levelManager.rampageEssenceDropBonusX2Value)
                ranNum = 1;
            else ranNum = 2;

            orbs[i] = Instantiate(essence[ranNum]);
         //   orbs[i].GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            orbs[i].transform.position = transform.position;
            orbs[i].layer = 0;
            orbs[i].AddComponent<EssenceOrbDelayed>();
            Destroy(orbs[i], levelManager.essenceLifeTime);
          
        }

        orbs[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(12f,14f)), ForceMode2D.Impulse);
        orbs[1].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(9f, 12f)), ForceMode2D.Impulse);
        orbs[2].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(9f, 12f)), ForceMode2D.Impulse);
        orbs[3].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), Random.Range(7f, 9f)), ForceMode2D.Impulse);
        orbs[4].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), Random.Range(7f, 9f)), ForceMode2D.Impulse);
    */
    
    //}

}
