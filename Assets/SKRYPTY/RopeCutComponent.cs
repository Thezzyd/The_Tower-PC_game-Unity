using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCutComponent : MonoBehaviour
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 8 && collision.gameObject.layer != 18)
        {
            GetComponent<HingeJoint2D>().enabled = false;
        }
    }

    private void OnParticleCollision(GameObject collision)
    {
        if (collision.CompareTag("AttackHero4") || collision.CompareTag("AttackHero5"))
        {

            GetComponent<HingeJoint2D>().enabled = false;
        }
    }
}
