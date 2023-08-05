
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public void PauseTime()
    {
       // Debug.Log("Pauza");
        Time.timeScale = 0.0f;
    }

    public void NormalTime()
    {
       // Debug.Log("NormalTime");
        Time.timeScale = 1.0f;
    }

    
}
