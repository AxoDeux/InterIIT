using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

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
    public static bool canRewind;


    void Start()
    {
        isRewinding = false;
    }


    public void StartColorChoosingSequence(SpriteRenderer sprite)
    {
        StartCoroutine("ChangeColor", sprite);
    }

    IEnumerator ChangeColor(SpriteRenderer sprite = null)
    {
        //choose next color
        Color nextColor = ChooseRandomColor();

        //set the image color here it's circle
        SetColor(null, nextColor, circleColorUI);
        yield return new WaitForSeconds(colorChangeTime);

        //set sprite color probably gun or bullet
        SetColor(sprite, nextColor, null);
    }

    //set color of the sprite and return the color of the sprite
    public static void SetColor(SpriteRenderer sprite, Color colorChosen, Image img)
    {
        if(sprite != null) { sprite.color = colorChosen; }
        if(img != null) { img.color = colorChosen; }
    }

    public static Color ChooseRandomColor()
    {
        int rnd = Random.Range(0, GameManager.colorCode.Length);
        return GameManager.colorCode[rnd];
    }

}
