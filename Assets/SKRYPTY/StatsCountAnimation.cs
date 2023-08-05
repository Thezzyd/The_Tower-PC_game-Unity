using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsCountAnimation : MonoBehaviour
{
    private LevelManager levelManager;
    private TextMeshProUGUI textNumber;

    private float animationTime;
    public float initialNumber;
    public float currentNumber;
    public float desiredNumber;

    public void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        textNumber = GetComponent<TextMeshProUGUI>();
        animationTime = 0.33f;
        StartCoroutine(StartCountingAnimation(1f));

    }

    public IEnumerator StartCountingAnimation(float delay)
    {
        yield return StartCoroutine(LevelManager.WaitForRealSeconds(delay));

        switch (name)
        {
            case "ScoreValue":
                SetNumber(levelManager.scoreFinalCounter);
                break;
            case "StarsValue":
                SetNumber(levelManager.starFinalCounter);
                break;
            case "KillsValue":
                SetNumber(levelManager.killFinalCounter - 1); //nie wiem czemu + 1 ale tak ma być :/
                break;
            case "TimeValue":
                SetNumber(levelManager.timeFinalCounter);
                break;
            case "HightValue":
                SetNumber(levelManager.towerHightReachedFinalCounter);
                break;
        }



    }

    public void SetNumber(float value)
    {
        initialNumber = currentNumber;
        desiredNumber = value;
    }

    public void AddToNumber(float value)
    {
        initialNumber = currentNumber;
        desiredNumber += value;
    }

    void Update()
    {
        if (currentNumber < desiredNumber)
        {
            if (initialNumber < desiredNumber)
            {
                currentNumber += (animationTime * Time.unscaledDeltaTime) * (desiredNumber - initialNumber);
                if (currentNumber >= desiredNumber)
                    currentNumber = desiredNumber;
            }
            else
            {
                currentNumber -= (animationTime * Time.unscaledDeltaTime) * (initialNumber - desiredNumber);
                if (currentNumber <= desiredNumber)
                    currentNumber = desiredNumber;


            }

            if (name == "TimeValue")
            {
                string mn;
                string sn;

                int minutesNow = ((int)currentNumber / 60);
                if (minutesNow < 10)
                    mn = "0" + minutesNow.ToString("0");
                else
                    mn = minutesNow.ToString("0");

                float secondsNow = (currentNumber % 60);
                if (secondsNow < 10)
                    sn = "0" + secondsNow.ToString("0");
                else sn = secondsNow.ToString("0");

                textNumber.text = mn + " : " + sn;
            }
            else
            textNumber.text = currentNumber.ToString("0");

        }
    }
}
