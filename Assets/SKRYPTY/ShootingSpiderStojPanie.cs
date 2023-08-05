using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpiderStojPanie : MonoBehaviour
{


    public void AllowWalking()
    {
        GetComponentInParent<EnemyShootingSpider>().stojPanie = false;
     //   stojPanie = false;
    }
}
