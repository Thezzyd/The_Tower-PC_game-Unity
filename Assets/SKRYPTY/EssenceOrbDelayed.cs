using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceOrbDelayed : MonoBehaviour
{
    void Start()
    {
        Invoke("EssenceOrbColliderActivation", 0.8f);
    }

    public void EssenceOrbColliderActivation()
    {
        gameObject.layer = 21;
    }


}
