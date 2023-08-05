using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text scoreText;
    public TMP_Text towerHightText;
    public TMP_Text starsText;
    public TMP_Text killsText;
    public TMP_Text ordinalNumberText;

    public bool isLoggedUserRow;

    public void NewScoreElement (string _username, int _score, int _towerHight, int _stars, int _kills, int _ordinalNumber, bool _isLoggedUserRow)
    {
        usernameText.text = _username;
        scoreText.text = _score.ToString();
        towerHightText.text = _towerHight.ToString();
        starsText.text = _stars.ToString();
        killsText.text = _kills.ToString();
        ordinalNumberText.text = _ordinalNumber.ToString();

        isLoggedUserRow = _isLoggedUserRow;
        if (isLoggedUserRow)
        {
            GetComponent<Image>().color = new Color(0, 1, 0.6557f, 015f);
        }
    }

}
