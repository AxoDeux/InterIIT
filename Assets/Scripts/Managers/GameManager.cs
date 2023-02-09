using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static UnityEngine.Color[] colorCode = {

        new UnityEngine.Color(5/255f, 93/255f, 1/255f),

        new UnityEngine.Color(60/255f, 57/255f, 96/255f),

        new UnityEngine.Color(94/255f, 50/255f, 86/255f),

        new UnityEngine.Color(88/255f, 144/255f, 162/255f),
};
    [SerializeField]
    SpriteRenderer gunSprite;

    public static string name = "Player";


    [SerializeField]
    Image circleColorUI;

    [SerializeField]
    [Tooltip("Color change time of the gun")]
    public static float colorChangeTime = 3f;
    //public static float rewindTime = 10f;
    public static bool isRewinding = false;
    public static bool canRewind;


    void Start()
    {
        canRewind = false;
        //isRewinding = false;
    }

    private void Update()
    {
        Debug.Log($"can rewind {canRewind}");
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
