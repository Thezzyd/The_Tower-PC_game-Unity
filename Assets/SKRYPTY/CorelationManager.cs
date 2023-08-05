using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorelationManager : MonoBehaviour
{
    public TextMeshProUGUI corelationText;

    public void Start()
    {
       StartCoroutine( FindObjectOfType<FirebaseManager>().CalculateAgeCorelation() );
    }
}
