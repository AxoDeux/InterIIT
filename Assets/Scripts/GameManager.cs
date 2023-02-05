using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
//using UnityEngine.core
public class GameManager : MonoBehaviour
{
    //public Color red = Color.blue

    public static Color[] colorCode = {
        new Color(63f, 235f, 200f),
        Color.red,
        Color.black,
        Color.blue,
        Color.grey
};
    [SerializeField]
    SpriteRenderer gunSprite;

    [SerializeField]
    Image circleColorUI;

    [SerializeField]
    [Tooltip("Color change time of the gun")]
    public static float colorChangeTime = 3f;

    public static bool isRewinding;

    //all functions subscribed to this get invoked when we are rewinding
    //public static Action OnStartRewinding = delegate { };
    //all functions subscribed to this get invoked when we are not rewinding
    //public static Action OnStopRewinding = delegate { };

    //publi
    //private static GameManager _instance;
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //            _instance = new GameManager();

    //        return _instance;
    //    }
    //}

    void Start()
    {
        isRewinding = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartColorChoosingSequence(SpriteRenderer sprite)
    {
        //Color rndColor = ChooseRandomColor();
        //SetColor(sprite, rndColor, null);
        //while (true)
        StartCoroutine("ChangeColor", sprite);
        //StopCoroutine("ChangeColor");
    }
    //changes color after some seconds
    IEnumerator ChangeColor(SpriteRenderer sprite = null)
    {
        //choose next color

        Color nextColor = ChooseRandomColor();
        //set the sprite color
        //SetColor(sprite, nextColor, null);
        //set the image color here it's circle
        SetColor(null, nextColor, circleColorUI);

        yield return new WaitForSeconds(colorChangeTime);
        //set sprite color probably gun or bullet
        SetColor(sprite, nextColor, null);
        //SetColor()


    }
    //set color of the sprite and return the color of the sprite
    public static Color SetColor(SpriteRenderer sprite, Color colorChosen, Image img)
    {
        if (img == null)
        {
            //Debug.Log(img.color);
            sprite.color = colorChosen;
            return sprite.color;
        }
        if (img != null)
        {
            Debug.Log("We change color of circle");
            img.color = colorChosen;
            return img.color;
        }
        else return Color.yellow;
    }

    public static Color ChooseRandomColor()
    {
        int rnd = Random.Range(0, GameManager.colorCode.Length);
        return GameManager.colorCode[rnd];
    }


    //public Color getGunColor()
    //{
    //    Debug.Log("GUN COLOR IS " + gunSprite.color);
    //    return gunSprite.color;
    //}
}
