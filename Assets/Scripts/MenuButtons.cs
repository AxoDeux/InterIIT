using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private MainMenuManager menuManager;
    [SerializeField] private List<Sprite> buttonSprites;
    private const int REQ_HITS = 3;
    private int hitCount;

    [SerializeField] private MainMenuManager.MenuButtonType buttonType;

    private void Start()
    {
        hitCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;
            ChangeSprite();
            Destroy(collision.gameObject, 0.2f);
            if (hitCount == REQ_HITS)
            {
                menuManager.OnDestroyButton(buttonType);
            }
        }
    }

    private void ChangeSprite()
    {
        if (hitCount > REQ_HITS)
        {
            hitCount = 0;
            return;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprites[hitCount];
    }
}
