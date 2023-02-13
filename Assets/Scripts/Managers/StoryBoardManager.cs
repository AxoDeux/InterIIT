using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryBoardManager : MonoBehaviour {
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;


    [Header("Scene1")]
    [SerializeField] private GameObject scene1;
    [SerializeField] private GameObject colour1;
    [SerializeField] private GameObject colour2;
    [SerializeField] private GameObject colour3;
    [SerializeField] private GameObject i_scene1;

    [Header("Scene2")]
    [SerializeField] private GameObject scene2;
    [SerializeField] private GameObject skull;
    [SerializeField] private GameObject i_scene2;

    [Header("Scene3")]
    [SerializeField] private GameObject scene3;
    [SerializeField] private GameObject enemyFace;
    [SerializeField] private GameObject i_scene3;

    [Header("Scene4")]
    [SerializeField] private GameObject scene4;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject timeCell1;
    [SerializeField] private GameObject timeCell2;
    [SerializeField] private GameObject i_scene4;

    private int sceneNum;


    private void OnEnable() {
        sceneNum = 1;
        ChangeScene(sceneNum);
    }

    private void ChangeScene(int num) {
        switch(num) {
            case 1:
                scene1.SetActive(true);
                i_scene1.SetActive(true);
                LeanTween.moveLocalY(colour1, 4f, 2f).setEaseInOutCubic().setLoopPingPong();
                LeanTween.moveLocalY(colour2, 4f, 2.5f).setEaseInOutCubic().setLoopPingPong();
                LeanTween.moveLocalY(colour3, 4f, 2.2f).setEaseInOutCubic().setLoopPingPong();
                break;
            case 2:
                scene2.SetActive(true);
                i_scene2.SetActive(true);
                LeanTween.scale(skull, new Vector3(1.5f, 1.5f), 2f).setEaseInOutCirc().setLoopPingPong();
                break;
            case 3:
                scene3.SetActive(true);
                i_scene3.SetActive(true);
                LeanTween.scale(enemyFace, new Vector3(1.25f, 1.25f), 2f).setEaseInOutCirc().setLoopPingPong();
                break;
            case 4:
                scene4.SetActive(true);
                i_scene4.SetActive(true);
                LeanTween.moveLocalY(timeCell1, -3f, 1.8f).setEaseInOutCubic().setLoopPingPong();
                LeanTween.moveLocalY(timeCell2, -3f, 1.8f).setEaseInOutCubic().setLoopPingPong();
                LeanTween.rotateZ(gun, 10f, 2f).setEaseInOutCirc().setLoopPingPong();
                break;
        }
    }

    private void CloseScene(int num) {
        switch(num) {
            case 1:
                scene1.SetActive(false);
                i_scene1.SetActive(false);
                break;
            case 2:
                scene2.SetActive(false);
                i_scene2.SetActive(false);
                break;
            case 3:
                scene3.SetActive(false);
                i_scene3.SetActive(false);
                break;
            case 4:
                scene4.SetActive(false);
                i_scene4.SetActive(false);
                break;
        }
        LeanTween.cancelAll();
    }

    public void OnClickNext() {
        CloseScene(sceneNum);
        sceneNum++;
        ChangeScene(sceneNum);

        if(sceneNum == 4) {
            nextButton.SetActive(false);
        } else if(sceneNum == 2) {
            backButton.SetActive(true);
        }
        SoundManager.PlaySound(SoundManager.Sound.UIButtonWood);
    }

    public void OnClickBack() {
        CloseScene(sceneNum);
        sceneNum--;
        ChangeScene(sceneNum);

        if(sceneNum == 1) {
            backButton.SetActive(false);
        } else if(sceneNum == 3) {
            nextButton.SetActive(true);
        }
        SoundManager.PlaySound(SoundManager.Sound.UIButtonWood);
    }

    public void OnClickMenu() {
        SoundManager.PlaySound(SoundManager.Sound.UICloseButton);

        CloseScene(sceneNum);
        sceneNum = 1;
        nextButton.SetActive(true);
        backButton.SetActive(false);
        mainMenuCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

    


}
