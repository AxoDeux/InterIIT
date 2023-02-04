using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer enemyCircle;
    public void Start()
    {
        GameManager.SetColor(enemyCircle);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Bullet color {GameManager.Instance.getGunColor()}, Enemy color = {enemyCircle.color}");
        if (collision.gameObject.CompareTag("Bullet") && (enemyCircle.color == GameManager.Instance.getGunColor()))
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
