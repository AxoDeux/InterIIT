using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    const float MAX_TIME = 300f; //seconds
    [SerializeField] private TMP_Text timeText;

    private float time = 0f;
    private int minutes;
    private int seconds;
    private int milliSeconds;
    private string t_minutes, t_seconds, t_milliSeconds;

    private void Start() {
        time = MAX_TIME;
    }

    private void Update() {
       SetClockTime();  
    }

    private void SetClockTime() {
        time -= Time.deltaTime;

        minutes = (int)time / 60;
        seconds = (int)time % 60;
        milliSeconds = (int)((time - (minutes * 60 + seconds)) * 100);


        if(minutes < 10) {
            t_minutes = "0" + minutes.ToString();
        } else t_minutes = minutes.ToString();

        if(seconds < 10) {
            t_seconds = "0" + seconds.ToString();
        } else t_seconds = seconds.ToString();

        if(milliSeconds < 10) {
            t_milliSeconds = "0" + milliSeconds.ToString();
        } else t_milliSeconds = milliSeconds.ToString();

        timeText.text = $"{t_minutes}:{t_seconds}:{t_milliSeconds}";
    }
}
