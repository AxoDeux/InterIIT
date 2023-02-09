using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject HowToPlay;
    [SerializeField] private GameObject Story;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject Leaderboard;
    [SerializeField] private GameObject Quit;
    [SerializeField] private GameObject inputField;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject storyBoard;


    public enum MenuButtonType
    {
        start,
        howtoplay,
        story,
        options,
        leaderboard,
        quit
    };

    public void OnDestroyButton(MenuButtonType type)
    {
        switch (type)
        {
            case MenuButtonType.start:
                SceneManager.LoadScene(1);
                break;
            case MenuButtonType.howtoplay:
                //enable how to play canvas
                break;
            case MenuButtonType.story:
                storyBoard.SetActive(true);
                mainMenuCanvas.SetActive(false);
                break;
            case MenuButtonType.options:
                //load options screen
                break;
            case MenuButtonType.leaderboard:
                {
                    Leaderboard.SetActive(true);

                    //Debug.Log("We hit leaderboard");
                    //SoundManager.PlaySound()
                }
                //load leaderboard screen
                break;
            case MenuButtonType.quit:
                Application.Quit();
                break;
        }
    }

    public void OnClickEditName()
    {
        inputField.SetActive(true);
    }

    public void OnEndEditName(TMP_Text name)
    {
        //save name for leaderboard
        playerName.text = name.text;

        //PlayerPrefs.SetInt(name.text, PlayerPrefs.GetInt(name.text));
        PlayerPrefs.Save();
        inputField.SetActive(false);
    }
}
