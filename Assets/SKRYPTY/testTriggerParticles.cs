using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTriggerParticles : MonoBehaviour
{

    public void OnParticleCollision(GameObject other)
    {
        Debug.Log("O ciurala uderzyloo...");
    }

}
