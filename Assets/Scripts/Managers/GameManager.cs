using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static UnityEngine.Color[] colorCode = {
        new UnityEngine.Color(63/255f, 235/255f, 200/255f),
        new UnityEngine.Color(229/255f, 204/255f, 102/255f),
        new UnityEngine.Color(243/255f, 227/255f, 178/255f),
        new UnityEngine.Color(195/255f, 152/255f, 105/255f),
        new UnityEngine.Color(182/255f, 179/255f, 213/255f),
        new UnityEngine.Color(195/255f, 152/255f, 105/255f),
        new UnityEngine.Color(237/255f, 235/255f, 125/255f)
        //Color.red,
        //Color.black,
        //Color.blue,
        //Color.grey
};
    [SerializeField]
    SpriteRenderer gunSprite;



    [SerializeField]
    Image circleColorUI;

    [SerializeField]
    [Tooltip("Color change time of the gun")]
    public static float colorChangeTime = 3f;
    public static float rewindTime = 5f;
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
        if (sprite != null) { sprite.color = colorChosen; }
        if (img != null) { img.color = colorChosen; }
    }

    public static Color ChooseRandomColor()
    {
        int rnd = Random.Range(0, GameManager.colorCode.Length);
        //Debug.Log($"Color chosen {GameManager.colorCode[rnd]}");
        return GameManager.colorCode[rnd];
    }

}
