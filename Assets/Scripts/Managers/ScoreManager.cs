using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private const float MAX_INFECTION = 1000f;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image i_timeBattery;
    [SerializeField] private Image i_infectionBar;
    [SerializeField] private GameObject gameOverScreen;

    private int score;
    public int highScore = 0;
    private Dictionary<Enemy.EnemyType, int> EnemyToPointsMap;

    private float time = 0f;
    private int minutes;
    private int seconds;
    private int milliSeconds;
    private string t_minutes, t_seconds, t_milliSeconds;

    private float timer15 = 15f;
    private float timer60 = 60f;

    private void Awake()
    {

        if (!PlayerPrefs.HasKey("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", 0);
        }

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        EnemyToPointsMap = new Dictionary<Enemy.EnemyType, int>() {
            {Enemy.EnemyType.Contagious, 10 },
            {Enemy.EnemyType.Shooter, 20 },
            {Enemy.EnemyType.Bomber, 30 },

        };
    }

    private void Start()
    {
        time = 0f;
        score = 0;
        i_timeBattery.fillAmount = 0f;
        i_infectionBar.fillAmount = 0f;
    }

    private void Update()
    {
        SetClockTime();
        SetScore();
    }

    private void SetClockTime()
    {
        time += Time.deltaTime;

        minutes = (int)time / 60;
        seconds = (int)time % 60;
        milliSeconds = (int)((time - (minutes * 60 + seconds)) * 100);


        if (minutes < 10)
        {
            t_minutes = "0" + minutes.ToString();
        }
        else t_minutes = minutes.ToString();

        if (seconds < 10)
        {
            t_seconds = "0" + seconds.ToString();
        }
        else t_seconds = seconds.ToString();

        if (milliSeconds < 10)
        {
            t_milliSeconds = "0" + milliSeconds.ToString();
        }
        else t_milliSeconds = milliSeconds.ToString();

        if (minutes == 0)
        {
            timeText.text = $"{t_seconds}:{t_milliSeconds}";
        }
        else
        {
            timeText.text = $"{t_minutes}:{t_seconds}:{t_milliSeconds}";
        }

        timer15 -= Time.deltaTime;
        if (timer15 <= 0)
        {
            TimeScoreIncrease(10);
            timer15 = 15f;
        }

        timer60 -= Time.deltaTime;
        if (timer60 <= 0)
        {
            TimeScoreIncrease(20);
            timer60 = 60f;
        }
    }

    public void OnCollectTimeCell()
    {
        if (i_timeBattery.fillAmount >= 1f) { return; }
        i_timeBattery.fillAmount += 0.25f;

        if (i_timeBattery.fillAmount >= 1f)
        {
            GameManager.canRewind = true;
        }
    }

    public void OnBatteryDischarged()
    {
        i_timeBattery.fillAmount = 0f;
    }

    public void DealDamage(float damage)
    {
        i_infectionBar.fillAmount += damage / MAX_INFECTION;
        if (i_infectionBar.fillAmount >= 1)
        {
            OnGameOver();
        }
    }

    public void OnKillEnemy(Enemy.EnemyType type)
    {
        //update score as per enemy type

        switch (type)
        {
            case Enemy.EnemyType.Contagious:
                score += EnemyToPointsMap[Enemy.EnemyType.Contagious];
                break;
            case Enemy.EnemyType.Shooter:
                score += EnemyToPointsMap[Enemy.EnemyType.Shooter];
                break;
            case Enemy.EnemyType.Bomber:
                score += EnemyToPointsMap[Enemy.EnemyType.Bomber];
                break;
        }
    }
    void SetHighScore(int currScore)
    {
        if (currScore > PlayerPrefs.GetInt("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", currScore);
            highScore = PlayerPrefs.GetInt("HIGHSCORE");
            Debug.Log($"Highscore is {highScore}");
            PlayerPrefs.Save();
        }
    }

    private void TimeScoreIncrease(int scoreIncrease)
    {
        score += scoreIncrease;
    }

    private void SetScore()
    {
        scoreText.text = score.ToString();
        SetHighScore(score);

    }

    private void OnGameOver()
    {
        //time scale 0
        //stop all active coroutines and set isDead booleans to true;
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverScreen>().SetScores(score, highScore, timeText.text);
    }
}
