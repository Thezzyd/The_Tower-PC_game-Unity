
using UnityEngine;

public class WallComponent : MonoBehaviour
{

    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private AudioSource Hero4TendrilsAudioSource;
    private AudioClip Hero4TendrilsAudioClip;

    private TowerSpawnManager towerSpawnManager;
    private EnemiesManager enemiesManager;

    [SerializeField]
    private Transform upperSpawnPoint;
    [SerializeField]
    private Transform lowerSpawnPoint;
    [SerializeField]
    private Transform enemiesParent;

    private int  enemyTentacleWormSpawnTries;
    public int numberOfEnemies;
    private bool isLeftWall;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
        enemiesManager = FindObjectOfType<EnemiesManager>();
        numberOfEnemies = 0;
        enemyTentacleWormSpawnTries = 0;
        // Hero4TendrilsAudioSource = levelManager.audioManager.sounds[15].source;
        //  Hero4TendrilsAudioClip = Hero4TendrilsAudioSource.clip;
        if (gameObject.tag.Equals("LEFTWALL"))
        {
            isLeftWall = true;
        }
        else
        {
            isLeftWall = false;
        }
      

    }

    public void TentacleEnemySpawn() {

        if (numberOfEnemies < 1)
        {
            GenerateEnemyTentacleWorm(lowerSpawnPoint.position.y, upperSpawnPoint.position.y, lowerSpawnPoint.position.x);
        }
    }

    private void GenerateEnemyTentacleWorm(float lowerPositionY, float upperPositionY, float worldPositionX)
    {
        int howManyEnemies = 1;

        for (int i = 1; i <= howManyEnemies; i++)
        {
           

            GameObject spawnedEnemy = null;
            Vector3 enemyPosition = new Vector3(0, 0, 0);

            spawnedEnemy = enemiesManager.enemyTentacleWormPrefab as GameObject;
            enemyPosition = new Vector3(worldPositionX, Random.Range(lowerPositionY, upperPositionY), 0);

            spawnedEnemy = Instantiate(spawnedEnemy) as GameObject;
            spawnedEnemy.transform.SetParent(enemiesParent);
            spawnedEnemy.transform.position = enemyPosition;

            if (spawnedEnemy.transform.position.y < 0)
            {
                Destroy(spawnedEnemy);
                continue;
                Debug.Log("destroy tentacle");
            }
            Debug.Log("spawn tentacle");

            var layerMask = (1 << 8); //ground layer
            Collider2D groundColider = Physics2D.OverlapCircle(spawnedEnemy.transform.position, 4f, layerMask);

            if (groundColider) {
                Destroy(spawnedEnemy);
                continue;
            }

            spawnedEnemy.GetComponent<EnemyTentacleWorm>().SetStartingDirectionScale(isLeftWall);


            enemiesManager.enemiesTentacleWormObjectsList.Add(spawnedEnemy);
            numberOfEnemies += 1;
            //Invoke("CheckIfPlatformOnCollision", 0.5f);
        }
        
    }

 /*   public void CheckIfPlatformOnCollision(GameObject spawnedEnemy, float lowerPositionY, float upperPositionY, float worldPositionX) 
    {
        enemyTentacleWormSpawnTries += 1;
        var layerMask = (1 << 8); //ground layer
        Collider2D groundColider = Physics2D.OverlapCircle(spawnedEnemy.transform.position, 4f, layerMask);

        if (groundColider && enemyTentacleWormSpawnTries <= 4)
        {
            Destroy(spawnedEnemy);
            numberOfEnemies -= 1;
            GenerateEnemyTentacleWorm(lowerPositionY, upperPositionY, worldPositionX);
        }
        else 
        {
            Destroy(spawnedEnemy);
        }
    }
*/

    public void OnCollisionEnter2D(Collision2D col)
    {
      /*  if (col.gameObject.CompareTag("AttackHero1"))
        {
         
            var crash1 = Instantiate(heroesManager.projectileCrashEffectHero1.gameObject, col.transform.position, col.transform.rotation);  
            CinemachineCameraShake.Instance.ShakeCamera(2.2f, 0.1f);

            Destroy(crash1, 1f);
            levelManager.audioManager.Play("Hero1ProjectileCrash", col.transform.position);
            Destroy(col.gameObject);
        }*/

        if (col.gameObject.CompareTag("AttackHero3"))
        { 
            CinemachineCameraShake.Instance.ShakeCamera(1.1f, 0.1f);
        }

    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("AttackHero4"))
        {
            CinemachineCameraShake.Instance.ShakeCamera(1.3f, 0.1f);

            if (heroesManager.isHero4TendrilsClipReseted)
                levelManager.audioManager.sounds[15].source.PlayOneShot(levelManager.audioManager.sounds[15].source.clip);
            else
            {
                levelManager.audioManager.Play("Hero4TendrilsHit", other.transform.position);
                heroesManager.isHero4TendrilsClipReseted = true;
            }
        }

        else if (other.gameObject.CompareTag("AttackHero5"))
        {
            CinemachineCameraShake.Instance.ShakeCamera(2f, 0.1f);
        }

    }

   
}
