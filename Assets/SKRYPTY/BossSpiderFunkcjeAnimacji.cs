using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiderFunkcjeAnimacji : MonoBehaviour
{
    public EnemyBossSpider spiderBoss;

    public void Start()
    {
        spiderBoss = GetComponentInParent<EnemyBossSpider>();
    }

    public void Attack()
    {
        spiderBoss.Attack();
    }

    public void Jumping()
    {
        spiderBoss.Skakanie();
    }

    public void WalkingDeactivation()
    {
        spiderBoss.WalkingDeactivation();
    }

    public void WalkingActivation()
    {
        spiderBoss.WalkingActivation();
    }

}
