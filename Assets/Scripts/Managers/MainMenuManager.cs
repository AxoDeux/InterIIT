using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject HowToPlay;
    [SerializeField] private GameObject Story;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject Quit;

    public enum MenuButtonType {
        start,
        howtoplay,
        story,
        options,
        leaderboard,
        quit
    };

    public void OnDestroyButton(MenuButtonType type) {
        switch(type) {
            case MenuButtonType.start:
                SceneManager.LoadScene(1);
                break;
            case MenuButtonType.howtoplay:
                //enable how to play canvas
                break;
            case MenuButtonType.story:
                //start story level
                break;
            case MenuButtonType.options:
                //load options screen
                break;
            case MenuButtonType.leaderboard:
                //load leaderboard screen
                break;
            case MenuButtonType.quit:
                Application.Quit();
                break;
        }
    }
}
