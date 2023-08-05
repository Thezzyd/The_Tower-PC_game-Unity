using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIExtensions;

public class StatsShine : MonoBehaviour
{
    public ShinyEffectForUGUI[] upgradeBtn;
    public float licznik;

    void Start()
    {
        licznik = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        licznik += Time.fixedDeltaTime;

        if (licznik >= 4f)
            for (int i = 0; i < upgradeBtn.Length; i++)
            {
             //   upgradeBtn[i].Play(1, );
            }
    }
}
