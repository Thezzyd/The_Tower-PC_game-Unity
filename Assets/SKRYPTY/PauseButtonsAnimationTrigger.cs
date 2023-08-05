using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonsAnimationTrigger : MonoBehaviour
{

    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PointerEnter01()
    {
        anim.SetTrigger("enter");
    }

    public void PointerExit01()
    {
        anim.SetTrigger("exit");
    }

}
