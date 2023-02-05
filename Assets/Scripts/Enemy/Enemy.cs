using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer enemyCircle;


    public void Start()
    {
        Color rndColor = GameManager.ChooseRandomColor();
        GameManager.SetColor(enemyCircle, rndColor, null);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        //Debug.Log("Enemy circle color " + enemyCircle.color + ", bullet color" + collision.gameObject.GetComponentInChildren<SpriteRenderer>().color);
        if (collision.gameObject.CompareTag("Bullet") && (enemyCircle.color == collision.gameObject.GetComponentInChildren<SpriteRenderer>().color))
        {
            //check bullet type and then destroy
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            //reduce Health
        }
    }
}
