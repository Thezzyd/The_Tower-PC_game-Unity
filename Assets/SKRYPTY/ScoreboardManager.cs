
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public void OnEnable()
    {
        FindObjectOfType<FirebaseManager>().ScoreboardButton();
    }

}
