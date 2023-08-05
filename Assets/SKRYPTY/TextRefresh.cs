using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextRefresh : MonoBehaviour
{
    public float timer = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // print(logInfoTextValueRegistration);

        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            gameObject.SetActive(false);
           
        }

        if (timer <= 1f)
        {
            gameObject.SetActive(true);
        }

        if (timer >= 4f)
            timer = 0f;
    }
}
