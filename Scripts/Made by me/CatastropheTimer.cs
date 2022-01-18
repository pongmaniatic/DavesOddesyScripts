using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatastropheTimer : MonoBehaviour
{
    public int totalTime = 313;
    private float currentTime;
    public bool timerOn = false;
    public TextMeshProUGUI TimerText;

    private void Start() { currentTime = totalTime; }

    void Update()
    {
        if (timerOn)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0){TimeIsUp();}
            if ((int)(currentTime % 60)  > 9f){ TimerText.text = (int)(currentTime /60) + ":" + (int)(currentTime%60); }
            else { TimerText.text =(int)(currentTime / 60) + ":0" + (int)(currentTime % 60); }
            
        }
    }

    public void TimeStart(){timerOn = true; TimeRestart(); }
    public void TimeRestart(){currentTime = totalTime;}
    public void TimeIsUp(){timerOn = false;}
}
