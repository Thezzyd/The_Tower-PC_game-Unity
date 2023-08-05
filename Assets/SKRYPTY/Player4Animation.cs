/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player4Animation : MonoBehaviour
{

    [System.NonSerialized]
    private Animator anim;

    public Player4 player4;
    private bool groundeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player4 = GetComponentInParent<Player4>();
    }

    // Update is called once per frame
    void Update()
    {
        groundeed = player4.grounded;
        anim.SetBool("Grounded", groundeed);

      

        anim.SetFloat("Speed", Mathf.Abs(player4.GetComponent<Rigidbody2D>().velocity.x));

        if (player4.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
        else if (player4.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
*/