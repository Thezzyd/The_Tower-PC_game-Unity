using UnityEngine;

public class HeroKillTrigger : MonoBehaviour
{

    private LevelManager levelManager;
    private HeroesManager heroesManager;


    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();

    }

    public void PlayerDeath()
    {
        heroesManager.heroTrailEffect.gameObject.SetActive(false);
        heroesManager.emittingEssenceEffect.gameObject.SetActive(false);
        heroesManager.emittingSteamEffect.gameObject.SetActive(false);

        heroesManager.heroSmokeEffectEmissionModule.rateOverDistance = 0f;

        var effect = Instantiate(heroesManager.shatredHeroEffect[levelManager.heroIndexRolledByStar - 1], transform.position + new Vector3(0, 1.8f, 0), heroesManager.shatredHeroEffect[levelManager.heroIndexRolledByStar - 1].transform.rotation);
        Destroy(effect, 5f);

        levelManager.audioManager.Stop("Hero4TendrilsRunning");

        heroesManager.HeroesHealth = 0;

        heroesManager.isHeroDefeated = true;
        heroesManager.isHeroAlive = false;

        levelManager.HealthCheck();

        GameObject[] players = GameObject.FindGameObjectsWithTag("PLAYER");
        foreach (GameObject player in players)
        {
            Destroy(player);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer.Equals(29) && !heroesManager.isHeroDefeated && !other.gameObject.CompareTag("EnemyEggSpider") && !other.CompareTag("EnemyPortal"))
        {
            if (!other.CompareTag("NotHarmfull"))
            {
                PlayerDeath();
            }
        }
    }


}
  


