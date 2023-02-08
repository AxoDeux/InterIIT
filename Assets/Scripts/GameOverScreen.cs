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

    }

    public void SetScores(int score, int highScore, string time)
    {
        t_score.text = score.ToString();
        t_highScore.text = highScore.ToString();
        t_time.text = time;
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void OnClickExit()
    {
        leaderboard.SetActive(false);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
