
using UnityEngine;
using TMPro;


public class HeroObjectCollector : MonoBehaviour
{
    private HeroesManager heroesManager;
    private LevelManager levelManager;
    private HeroBaseComponent heroBaseComponent;

    public ParticleSystem orbCollectedEffect;


    public void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
        heroBaseComponent = FindObjectOfType<HeroBaseComponent>();
    }

    public void SpawnEssenceText(Vector3 spawnPosition, int essenceQuantity){
        Vector3 textMove = Vector3.zero;
        int ranNum = Random.Range(1, 4);
        if(ranNum == 1){
            textMove = new Vector3(Random.Range(-2f, 2f), Random.Range(0.5f, 1.3f), 0); //above Hero
        }else if(ranNum == 2){
            textMove = new Vector3(Random.Range(-2f, -1.2f), Random.Range(-1.5f, 1.3f), 0); //left to Hero
        }else{
            textMove = new Vector3(Random.Range(1.2f, 2f), Random.Range(-1.5f, 1.3f), 0); //right to Hero
        }

        var essenceText = Instantiate(heroesManager.HeroEssenceTextPrefab, spawnPosition + textMove, transform.rotation);
        essenceText.GetComponent<TextMeshPro>().SetText("+"+essenceQuantity.ToString());
        Destroy(essenceText, 0.5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
      

       


        if (collision.CompareTag("EssenceOrbX1"))
        {
       //     int whichHero = collision.gameObject.GetComponent<EssenceHeroIdComponent>().heroId ;

         
          //  heroBaseComponent.PlayerOrbCollectedEffectColorRefresh(whichHero);
            Destroy(collision.gameObject);

            heroesManager.HeroPowerSetRefresh(100, levelManager.heroIndexRolledByStar);
            FindObjectOfType<AudioManager>().Play("EssenceCollected");
            orbCollectedEffect.Emit(30);
            SpawnEssenceText(transform.position, 100);

        }

        else if (collision.CompareTag("EssenceOrbX2"))
        {
          //  int whichHero = collision.gameObject.GetComponent<EssenceHeroIdComponent>().heroId;

           // heroBaseComponent.PlayerOrbCollectedEffectColorRefresh(whichHero);
            Destroy(collision.gameObject);
        
            heroesManager.HeroPowerSetRefresh(200, levelManager.heroIndexRolledByStar);
            FindObjectOfType<AudioManager>().Play("EssenceCollected");
            orbCollectedEffect.Emit(60);
            SpawnEssenceText(transform.position, 200);

        }

        else if (collision.CompareTag("EssenceOrbX3"))
        {
           // int whichHero = collision.gameObject.GetComponent<EssenceHeroIdComponent>().heroId;

          //  heroBaseComponent.PlayerOrbCollectedEffectColorRefresh(whichHero);
            Destroy(collision.gameObject);
          
            heroesManager.HeroPowerSetRefresh(300, levelManager.heroIndexRolledByStar);
            FindObjectOfType<AudioManager>().Play("EssenceCollected");
            orbCollectedEffect.Emit(90);
            SpawnEssenceText(transform.position, 300);
        
        }

        else if (collision.CompareTag("ChestNormal") || collision.CompareTag("ChestSmall") || collision.CompareTag("ChestBig"))
        {
            if (!collision.GetComponent<ChestComponent>().isUsed)
            {
                collision.GetComponent<ChestComponent>().isUsed = true;
                collision.GetComponent<ChestComponent>().OpenChest(collision.tag);
            }
        }

    }

}


