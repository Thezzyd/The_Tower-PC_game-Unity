using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1LaserDirection : MonoBehaviour
{
    private HeroesManager heroesManager;
    public float angle;

    void Start()
    {
        heroesManager = FindObjectOfType<HeroesManager>();
    }

    void FixedUpdate()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        //Angle between mouse and this object
        angle = AngleBetweenPoints(transform.position, mouseWorldPosition);

        //Ta daa
        if (heroesManager.isHeroHeadingRight)
        {
            if (angle > 80f)
                angle = 80f;
            else if (angle < -80f)
                angle = -80f;
        }
        else
        {
            if (angle < 100f && angle > 0f)
                angle = 100f;
            else if (angle > -100f && angle < 0f)
                angle = -100f;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }


    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }


}
