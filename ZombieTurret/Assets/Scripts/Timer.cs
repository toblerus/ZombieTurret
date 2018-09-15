using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timer;
    public float minutes = 0;
    public float seconds = 5;
    public float miliseconds = 0;

    void Update()
    {

        if (miliseconds <= 0)
        {
            if (seconds <= 0)
            {
                minutes--;
                seconds = 59;
            }
            else if (seconds >= 0)
            {
                seconds--;
            }

            miliseconds = 100;
        }

        miliseconds -= Time.deltaTime * 100;

        timer.text = "Timeleft: " + string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
        if(minutes <= 0.0f && seconds <= 0.0f && miliseconds <= 0.0f)
        {
            Debug.Log("The Timer ran out!");
                this.enabled = false;
        }
    }
}