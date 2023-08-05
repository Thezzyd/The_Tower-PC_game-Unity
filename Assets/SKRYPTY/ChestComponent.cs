using System.Collections;
using UnityEngine;


public class ChestComponent : MonoBehaviour
{
    private Animator anim;
    private LevelManager levelManager;
    public bool isUsed;
    public Transform spawnPoint;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
    }

    public IEnumerator DropEssenceFromChestSmall(float delayTime)
    {
        for (float i = 0f; i < levelManager.chestSmallEssenceQuantity; i += 0.1f){
            var randNum = Random.Range(1, 4);
            GameObject essencePrefab = null;
            switch (randNum)
            {
                case 1:
                    essencePrefab = levelManager.essence_x1_prefab; break;
                case 2:
                    essencePrefab = levelManager.essence_x2_prefab; break;
                case 3:
                    essencePrefab = levelManager.essence_x3_prefab; break;
            }

            var essenceOrb1 = Instantiate(essencePrefab);
            var essenceOrb2 = Instantiate(essencePrefab);
            var essenceOrb3 = Instantiate(essencePrefab);

        //  essenceOrb1.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb1.layer = 0;
            essenceOrb1.transform.position = spawnPoint.position + new Vector3(0f, 0f, 0f);
            essenceOrb1.AddComponent<EssenceOrbDelayed>();

        //  essenceOrb2.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb2.layer = 0;
            essenceOrb2.transform.position = spawnPoint.position + new Vector3(0.1f, 0f, 0f);
            essenceOrb2.AddComponent<EssenceOrbDelayed>();

        //   essenceOrb3.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb3.layer = 0;
            essenceOrb3.transform.position = spawnPoint.position + new Vector3(-0.1f, 0f, 0f);
            essenceOrb3.AddComponent<EssenceOrbDelayed>();

            essenceOrb1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            essenceOrb2.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 8), ForceMode2D.Impulse);
            essenceOrb3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 8), ForceMode2D.Impulse);

            Destroy(essenceOrb1, levelManager.essenceLifeTime);
            Destroy(essenceOrb2, levelManager.essenceLifeTime);
            Destroy(essenceOrb3, levelManager.essenceLifeTime);
            
            yield return new WaitForSeconds(delayTime);
        }

    }

    public IEnumerator DropEssenceFromChestNormal(float delayTime)
    {
        for (float i = 0f; i < levelManager.chestNormalEssenceQuantity; i += 0.1f){
            var randNum = Random.Range(1, 4);
            GameObject essencePrefab = null;
            switch (randNum)
            {
                case 1:
                    essencePrefab = levelManager.essence_x1_prefab; break;
                case 2:
                    essencePrefab = levelManager.essence_x2_prefab; break;
                case 3:
                    essencePrefab = levelManager.essence_x3_prefab; break;
            }

            var essenceOrb1 = Instantiate(essencePrefab);
            var essenceOrb2 = Instantiate(essencePrefab);
            var essenceOrb3 = Instantiate(essencePrefab);

        //  essenceOrb1.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb1.layer = 0;
            essenceOrb1.transform.position = spawnPoint.position + new Vector3(0f, 0f, 0f);
            essenceOrb1.AddComponent<EssenceOrbDelayed>();

        //  essenceOrb2.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb2.layer = 0;
            essenceOrb2.transform.position = spawnPoint.position + new Vector3(0.1f, 0f, 0f);
            essenceOrb2.AddComponent<EssenceOrbDelayed>();

        //    essenceOrb2.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb3.layer = 0;
            essenceOrb3.transform.position = spawnPoint.position + new Vector3(-0.1f, 0f, 0f);
            essenceOrb3.AddComponent<EssenceOrbDelayed>();

            essenceOrb1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            essenceOrb2.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 8), ForceMode2D.Impulse);
            essenceOrb3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 8), ForceMode2D.Impulse);

            Destroy(essenceOrb1, 10f);
            Destroy(essenceOrb2, 10f);
            Destroy(essenceOrb3, 10f);
            
            yield return new WaitForSeconds(delayTime);
        }

    }

    public IEnumerator DropEssenceFromChestBig(float delayTime)
    {
        for (float i = 0f; i < levelManager.chestNormalEssenceQuantity; i += 0.1f){
            var randNum = Random.Range(1,4);
            GameObject essencePrefab = null;
            switch (randNum)
            {
                case 1:
                    essencePrefab = levelManager.essence_x1_prefab; break;
                case 2:
                    essencePrefab = levelManager.essence_x2_prefab; break;
                case 3:
                    essencePrefab = levelManager.essence_x3_prefab; break;
            }

            var essenceOrb1 = Instantiate(essencePrefab);
            var essenceOrb2 = Instantiate(essencePrefab);
            var essenceOrb3 = Instantiate(essencePrefab);
            var essenceOrb4 = Instantiate(essencePrefab);
            var essenceOrb5 = Instantiate(essencePrefab);

        //    essenceOrb1.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb1.layer = 0;
            essenceOrb1.transform.position = spawnPoint.position + new Vector3(0f, 0f, 0f);
            essenceOrb1.AddComponent<EssenceOrbDelayed>();

        // essenceOrb2.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb2.layer = 0;
            essenceOrb2.transform.position = spawnPoint.position + new Vector3(0.1f, 0f, 0f);
            essenceOrb2.AddComponent<EssenceOrbDelayed>();
        
        //  essenceOrb3.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb3.layer = 0;
            essenceOrb3.transform.position = spawnPoint.position + new Vector3(-0.1f, 0f, 0f);
            essenceOrb3.AddComponent<EssenceOrbDelayed>();
        
        // essenceOrb4.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb4.layer = 0;
            essenceOrb4.transform.position = spawnPoint.position + new Vector3(0.2f, 0f, 0f);
            essenceOrb4.AddComponent<EssenceOrbDelayed>();
        
        //  essenceOrb5.GetComponent<EssenceHeroIdComponent>().heroId = levelManager.heroIndexRolledByStar;
            essenceOrb5.layer = 0;
            essenceOrb5.transform.position = spawnPoint.position + new Vector3(-0.2f, 0f, 0f);
            essenceOrb5.AddComponent<EssenceOrbDelayed>();

            essenceOrb1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
            essenceOrb2.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 13), ForceMode2D.Impulse);
            essenceOrb3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 13), ForceMode2D.Impulse);
            essenceOrb4.GetComponent<Rigidbody2D>().AddForce(new Vector2(3, 12), ForceMode2D.Impulse);
            essenceOrb5.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3, 12), ForceMode2D.Impulse);

            Destroy(essenceOrb1, 10f);
            Destroy(essenceOrb2, 10f);
            Destroy(essenceOrb3, 10f);
            Destroy(essenceOrb4, 10f);
            Destroy(essenceOrb5, 10f);
            
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void OpenChest(string chestTag)
    {
        float chestEssenceQuantity = 0.0f; 
        float timeDelay = 0.15f;
        switch (chestTag)
        {
            case "ChestSmall": chestEssenceQuantity = levelManager.chestSmallEssenceQuantity; break;
            case "ChestNormal": chestEssenceQuantity = levelManager.chestNormalEssenceQuantity; break;
            case "ChestBig": chestEssenceQuantity = levelManager.chestBigEssenceQuantity; break;
        }

        if (chestTag.Equals("ChestBig"))
            StartCoroutine(DropEssenceFromChestBig(timeDelay));
        else if (chestTag.Equals("ChestNormal"))
            StartCoroutine(DropEssenceFromChestNormal(timeDelay));
        else if (chestTag.Equals("ChestSmall"))
            StartCoroutine(DropEssenceFromChestSmall(timeDelay));

        StartCoroutine(FadeChestAnimation(chestEssenceQuantity));
        Destroy(gameObject, (chestEssenceQuantity + 3f));
    }

    public IEnumerator FadeChestAnimation(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        anim.SetTrigger("isFading");
        Debug.Log("Wszedl triggerr");
    }

}
