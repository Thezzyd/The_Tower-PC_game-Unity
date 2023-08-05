using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTextAnim : MonoBehaviour
{
  
    public void HoverInAnim()
    {
      //  if (CompareTag("UpgradeBtn"))
       //     name = "TemporaryNameHoverIn";

        GetComponent<Animator>().SetBool("isHover", true);
    }

    public void HoverOutAnim()
    {
       // if (CompareTag("UpgradeBtn"))
          //  name = startName;

        GetComponent<Animator>().SetBool("isHover", false);

    }

}
