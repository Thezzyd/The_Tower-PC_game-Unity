using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShakeOnOffOptions : MonoBehaviour
{
     void Start()
    {
        GetComponent<Toggle>().isOn = OptionsManager.screenShake;
    }

    public void ScreenShakeOptionRefreash()
    {
       GetComponent<Toggle>().isOn = OptionsManager.screenShake;

    }
}
