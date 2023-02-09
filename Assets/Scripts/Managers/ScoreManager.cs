using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField]
    private float MAX_INFECTION = 100f;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Image i_timeBattery;
    [SerializeField] private Image i_infectionBar;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private GameObject damagePopUp;
    [SerializeField] private GameObject scorePopUp;


    private int score;
    private int highScore = 0;
    private Dictionary<Enemy.EnemyType, int> EnemyToPointsMap;

    private float time = 0f;
    private int minutes;
    private int seconds;
    private int milliSeconds;
    private string t_minutes, t_seconds, t_milliSeconds;

    private float timer15 = 15f;
    private float timer60 = 60f;
    Transform target;

    public static bool isGameOver = false;

    private void Awake()
    {

        if (!PlayerPrefs.HasKey(GameManager.name))
        {
            PlayerPrefs.SetInt(GameManager.name, 0);
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
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SetClockTime();
        SetScore();
        if (i_timeBattery.fillAmount >= 1f)
        {
            GameManager.canRewind = true;
        }
        else if (i_timeBattery.fillAmount == 0f)
        {
            GameManager.canRewind = false;
        }
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
    }

    public void OnBatteryDischarged()
    {
        i_timeBattery.fillAmount = 0f;
    }

    public void DealDamage(float damage)
    {
        i_infectionBar.fillAmount += damage / MAX_INFECTION;
        //damage
        //GameObject dmgPopUp = Instantiate(damagePopUp, target.position, Quaternion.identity);
        //dmgPopUp.GetComponentInChildren<TextMeshProUGUI>().text = "-" + damage.ToString();
        InstantiateEffeect(damagePopUp, (int)damage, "-");
        if (i_infectionBar.fillAmount >= 1 && !isGameOver)
        {
            OnGameOver();
        }
    }

    public void HealInfection(float amount) {
        i_infectionBar.fillAmount -= amount / MAX_INFECTION;
        InstantiateEffeect(scorePopUp, (int)amount, "+");
    }

    public void OnKillEnemy(Enemy.EnemyType type)
    {
        //update score as per enemy type

        switch (type)
        {
            case Enemy.EnemyType.Contagious:
                {
                    int inc = EnemyToPointsMap[Enemy.EnemyType.Contagious];
                    score += inc;
                    InstantiateEffeect(scorePopUp, inc, "+");
                }
                break;
            case Enemy.EnemyType.Shooter:
                {
                    int inc = EnemyToPointsMap[Enemy.EnemyType.Shooter];
                    score += inc;
                    InstantiateEffeect(scorePopUp, inc, "+");

                }
                break;
            case Enemy.EnemyType.Bomber:
                {
                    int inc = EnemyToPointsMap[Enemy.EnemyType.Bomber];
                    score += inc;
                    InstantiateEffeect(scorePopUp, inc, "+");

                }
                break;
        }
    }

    private void TimeScoreIncrease(int scoreIncrease)
    {
        score += scoreIncrease;
        InstantiateEffeect(scorePopUp, scoreIncrease, "+");
    }

    private void InstantiateEffeect(GameObject gameObj, int score, string mlplr)
    {
        GameObject tmp = Instantiate(gameObj, target.position, Quaternion.identity);
        tmp.GetComponentInChildren<TextMeshProUGUI>().text = mlplr + score.ToString();
    }

    private void SetScore()
    {
        scoreText.text = score.ToString();
        SetHighScore(score);

    }
    void SetHighScore(int currScore)
    {
        //Debug.Log($"for {PlayerPrefs.GetInt(GameManager.name)} Highscore is {}");
        if (currScore > PlayerPrefs.GetInt(GameManager.name))
        {
            PlayerPrefs.SetInt(GameManager.name, currScore);
            highScore = PlayerPrefs.GetInt(GameManager.name);
            PlayerPrefs.Save();
        }
    }

    private void OnGameOver()
    {
        //time scale 0
        Time.timeScale = 0f;
        //stop all active coroutines and set isDead booleans to true;
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.gameOver);
        //high
        gameOverScreen.GetComponent<GameOverScreen>().SetScores(score, PlayerPrefs.GetInt(GameManager.name), timeText.text);
        HighScoreTable.AddHighScoreEntry(PlayerPrefs.GetInt(GameManager.name), GameManager.name);

        isGameOver = true;
    }

    public void OnClickPause()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        pauseScreen.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.UIButtonScifi);
    }

    public void OnClickResume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        pauseScreen.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.UIButtonScifi);
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1f;
        SoundManager.PlaySound(SoundManager.Sound.UICloseButton) ;
        Invoke(nameof(LoadLevel), 1f);
    }

    private void LoadLevel() {
        SceneManager.LoadScene(0);
    }
}
