using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformComponent : MonoBehaviour
{

    private HeroesManager heroesManager;
    private LevelManager levelManager;

    public bool isHeroOnThisPlatform;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
     /*   if (col.gameObject.CompareTag("AttackHero1"))
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

        if (col.gameObject.CompareTag("PLAYER"))
        {
            isHeroOnThisPlatform = true;
        }

        if (col.gameObject.CompareTag("EnemyHangingSpider"))
        {
            col.gameObject.GetComponent<EnemyHangingSpider>().platformComponentThisSpiderIsStandingOn = this;
        }

    }

    private void OnParticleCollision(GameObject other)
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PLAYER"))
        {
            isHeroOnThisPlatform = false;
        }
    }
}
