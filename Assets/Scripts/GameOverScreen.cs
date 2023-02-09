using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text t_score;
    [SerializeField] private TMP_Text t_highScore;
    [SerializeField] private TMP_Text t_time;
    [SerializeField] private GameObject leaderboard;

    private void OnEnable()
    {

        t_highScore.text = PlayerPrefs.GetInt(GameManager.name).ToString();

    }

    public void SetScores(int score, int highScore, string time)
    {
        t_score.text = "Score: " + score.ToString();
        t_highScore.text = "HScore:" + highScore.ToString();
        t_time.text = time;
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1;
        SoundManager.PlaySound(SoundManager.Sound.UICloseButton);
        Invoke(nameof(LoadLevel), 1f);
    }
    private void LoadLevel() {
        SceneManager.LoadScene(0);
    }

    public void OnClickExit()
    {
        leaderboard.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.UIClick);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1;
        SoundManager.PlaySound(SoundManager.Sound.UIButtonScifi);
        ScoreManager.isGameOver = false;
        Invoke(nameof(ReloadScene), 1f);
    }

    private void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
