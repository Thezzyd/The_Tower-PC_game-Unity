
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    private HeroesManager heroesManager;
    private SkillsUpgradeManager skillsUpgradeManager;
    public LevelManager levelManager;

    public GameObject powerBar;
    public float targetScale;
    public float fillSpeed = 0.5f;
    private ParticleSystem ps;
    public bool lvlUpDelay;
    public bool isIncreasing;

    Vector3 locScale;


    void Start()
    {
        skillsUpgradeManager = FindObjectOfType<SkillsUpgradeManager>();
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();

        locScale = new Vector3(0.0f, 1.0f, 1.0f);
        fillSpeed = 0.5f;
        transform.localScale = locScale;
        ps = GetComponentInChildren<ParticleSystem>();

        isIncreasing = true;
    }

    public void ResetAfterPowerChange(){
        locScale.x = ((float)heroesManager.heroesEssenceCollected[levelManager.heroIndexRolledByStar - 1] / (float)skillsUpgradeManager.powerUpProg[levelManager.heroIndexRolledByStar - 1]);
        transform.localScale = locScale;
    }
    public void ResetAfterPowerUp()
    {
        locScale.x = 0.0f;
        transform.localScale = locScale;
    }

    public void ResetAfterPowerDown()
    {
        locScale.x = ((float)heroesManager.heroesEssenceCollected[levelManager.heroIndexRolledByStar - 1] / (float)skillsUpgradeManager.powerUpProg[levelManager.heroIndexRolledByStar - 1]);
        transform.localScale = locScale;
    }

    public void FixedUpdate()
    {
        if(!lvlUpDelay && levelManager.heroIndexRolledByStar != 0){
            targetScale = ((float)heroesManager.heroesEssenceCollected[levelManager.heroIndexRolledByStar - 1] / (float)skillsUpgradeManager.powerUpProg[levelManager.heroIndexRolledByStar - 1]);
        }
        if(transform.localScale.x < targetScale && isIncreasing){

            locScale = transform.localScale;
            locScale.x += fillSpeed * Time.fixedDeltaTime;
            if(locScale.x > 1.0f){
                locScale.x = 1.0f;
            }
            transform.localScale = locScale;
            ps.Play();
        }
        else if(transform.localScale.x > targetScale && !isIncreasing){
            locScale = transform.localScale;
            locScale.x -= fillSpeed * Time.fixedDeltaTime;
            if(locScale.x < 0.0f){
                locScale.x = 0.0f;
            }
            transform.localScale = locScale;
            ps.Play();
        }else{
            //Debug.Log("Power bar rownyyyyyyyyyyyyyyyyyyyy");
            ps.Stop();
            if(lvlUpDelay && isIncreasing){
                ResetAfterPowerUp();
                lvlUpDelay = false;
            }else if(lvlUpDelay && !isIncreasing){
                ResetAfterPowerDown();
                lvlUpDelay = false;
            }
        }



       /* if (!lvlUpDelay && levelManager.heroIndexRolledByStar != 0)
        {
            targetScale = ((float)heroesManager.heroesEssenceCollected[levelManager.heroIndexRolledByStar - 1] / (float)skillsUpgradeManager.powerUpProg[levelManager.heroIndexRolledByStar - 1]);
        }
        else if(lvlUpDelay)
        {
            targetScale = 1;
        }

        if (isIncreasing)
        {
            if (locScale.x < targetScale)
            {
                locScale.x += fillSpeed * Time.fixedDeltaTime;
                transform.localScale = locScale;
                if (!ps.isPlaying)
                    ps.Play();
            }
            else
            {
                if (lvlUpDelay)
                {
                    lvlUpDelay = false;
                    ResetAfterPowerUp();
                }

                ps.Stop();
            }
        }
        else
        {
            if (locScale.x > targetScale)
            {
                locScale.x -= fillSpeed * Time.fixedDeltaTime;
                transform.localScale = locScale;
                if (!ps.isPlaying)
                    ps.Play();
            }
            else
            {
                if (lvlUpDelay)
                {
                    lvlUpDelay = false;
                    ResetAfterPowerUp();
                }

                ps.Stop();
            }
        }*/
    
    }




    /*
    public void PowerBarRefresh()
    {
        levelManager = FindObjectOfType<LevelManager>();
        locScale = transform.localScale;
       // Debug.Log(levelManager.heroesEnergyStack[levelManager.ranNumInheritrdFromStar]/levelManager.powerUpProg[levelManager.ranNumInheritrdFromStar]+"   lllllllllllll");
        locScale.x = (1f * ((float)levelManager.heroesEnergyStack[levelManager.ranNumInheritrdFromStar] / (float)levelManager.powerUpProg[levelManager.ranNumInheritrdFromStar]));
        transform.localScale = locScale;
    }*/
}
