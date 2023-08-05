
using UnityEngine.EventSystems;
using UnityEngine;


public class ItemsInfo : MonoBehaviour, IPointerDownHandler
{
    // public GameObject childTextNames;
    // public GameObject childTextValues;
    //    public GameObject background;
    [HideInInspector] public int tapCounter;
    public GameObject gemInfo;

    public void Start()
    {
        //background.SetActive(false);
        gemInfo.SetActive(false);
        tapCounter = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tapCounter == 0)
        {
            gemInfo.SetActive(true);
            tapCounter++;
        }
        else {
            gemInfo.SetActive(false);
            tapCounter = 0 ;
        }
        
       

    }
    /*
    public void OnPointerExit(PointerEventData eventData)
    {
      //  Debug.Log("OnMouseExit");
        // childText.SetActive(false);
        gemInfo.SetActive(false);
    }
    */
}
