/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowOrange : MonoBehaviour
{

    public float timeToGlow;
    public bool rosnie = true;

    void Start()
    {
        timeToGlow = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (rosnie == true)
        {
            timeToGlow += Time.deltaTime ;
        }
        else if (rosnie == false)
        {
            timeToGlow -= Time.deltaTime ;
        }

        if (timeToGlow >= 0.8f)
        {
            rosnie = false;
        }

        else if (timeToGlow <= 0.2f )
        {
            rosnie = true;
        }

        gameObject.transform.localScale = new Vector3(timeToGlow, timeToGlow, 0f);

       
    }
}
*/