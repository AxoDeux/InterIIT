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

    [Header("How to play")]
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject collectibles;
    [SerializeField] private GameObject next;
    [SerializeField] private GameObject back;


    private int h2pNum;


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
                howToPlay.SetActive(true);
                h2pNum = 1;
                SetInstruction(h2pNum);
                break;
            case MenuButtonType.story:
                storyBoard.SetActive(true);
                mainMenuCanvas.SetActive(false);
                break;
            case MenuButtonType.options:
                //load options screen
                break;
            case MenuButtonType.leaderboard:
                Leaderboard.SetActive(true);
                break;
            case MenuButtonType.quit:
                Application.Quit();
                break;
        }
    }

    public void OnClickEditName()
    {
        inputField.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.UIClick);
    }

    public void OnEndEditName(TMP_Text name)
    {
        //save name for leaderboard
        playerName.text = name.text;

        GameManager.name = name.text;

        PlayerPrefs.SetInt(name.text, PlayerPrefs.GetInt(GameManager.name));
        PlayerPrefs.Save();
        inputField.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.UIClick);
    }

    public void OnClickNext() {
        CloseInstruction(h2pNum);
        h2pNum++;
        SetInstruction(h2pNum);
        SoundManager.PlaySound(SoundManager.Sound.UIButtonWood);
    }
    public void OnClickBack() {
        CloseInstruction(h2pNum);
        h2pNum--;
        SetInstruction(h2pNum);
        SoundManager.PlaySound(SoundManager.Sound.UIButtonWood);
    }

    public void OnClickClose() {
        howToPlay.SetActive(false);
        CloseInstruction(h2pNum);
        h2pNum = 1;
        SoundManager.PlaySound(SoundManager.Sound.UICloseButton);
    }

    private void SetInstruction(int num) {
        if(num == 1) {
            controls.SetActive(true);
            back.SetActive(false);
        }else if(num == 2) {
            enemies.SetActive(true);
        }else if (num == 3) {
            collectibles.SetActive(true);
            next.SetActive(false);
        }
    }

    private void CloseInstruction(int num) {
        if(num == 1) {
            controls.SetActive(false);
            back.SetActive(true);
        } else if(num == 2) {
            enemies.SetActive(false) ;
        } else if(num == 3) {
            collectibles.SetActive(false);
            next.SetActive(true);
        }
    }

}
