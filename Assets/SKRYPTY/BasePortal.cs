/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePortal : MonoBehaviour
{

    [HideInInspector] public LevelManager levelManager;
    [HideInInspector] public BasePortalBar basePortalBar;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        basePortalBar = FindObjectOfType<BasePortalBar>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BLOOBENEMY"))
        {
            levelManager.basePortalHealthPoints -= 2;
            CheckBaseHealthLeft();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("BigSpiderEnemy"))
        {
            levelManager.basePortalHealthPoints -= 5;
            CheckBaseHealthLeft();
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("Bat"))
        {
            levelManager.basePortalHealthPoints -= 3;
            CheckBaseHealthLeft();
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("JumpingSpider"))
        {
            levelManager.basePortalHealthPoints -= 4;
            CheckBaseHealthLeft();
            Destroy(collision.gameObject);

        }
    }

    public void CheckBaseHealthLeft()
    {
        basePortalBar.BasePortalHealthBarRefresh();
        if (levelManager.basePortalHealthPoints <= 0f)
            levelManager.GameOver();
    }
}
*/