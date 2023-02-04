using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public static bool isRewinding;

    //all functions subscribed to this get invoked when we are rewinding
    //public static Action OnStartRewinding = delegate { };
    //all functions subscribed to this get invoked when we are not rewinding
    //public static Action OnStopRewinding = delegate { };

    //publi
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();

            return _instance;
        }
    }

    void Start()
    {
        isRewinding = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //set color of the sprite and return the color of the sprite
    public static Color SetColor(SpriteRenderer sprite)
    {
        int rnd = Random.Range(0, GameManager.colorCode.Length);
        sprite.color = GameManager.colorCode[rnd];
        return sprite.color;
    }

    public Color getGunColor()
    {
        Debug.Log("GUN COLOR IS " + gunSprite.color);
        return gunSprite.color;
    }
}
