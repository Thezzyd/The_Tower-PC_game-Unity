using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero2AttackAnimationFunctions : MonoBehaviour
{
    private Hero2Component hero2Component;

    private void Start()
    {
        hero2Component = FindObjectOfType<Hero2Component>();

    }

    private void AttackAndRotation()
    {
        hero2Component.SpawnHero2Slash();
    }

}
