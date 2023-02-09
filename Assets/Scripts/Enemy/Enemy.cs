using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer[] enemyColoredParts;

    [SerializeField] private float damage = 10f;
    [SerializeField] private int hitsRequired = 1;
    private int hitCount;
    public enum EnemyType
    {
        Contagious,
        Shooter,
        Bomber
    };
    [SerializeField] private EnemyType type;

    public Color rndColor;

    public void Start()
    {
        Color rndColor = GameManager.ChooseRandomColor();
        //Debug.Log("Enemy chosen color");
        foreach (SpriteRenderer sprite in enemyColoredParts)
        {
            GameManager.SetColor(sprite, rndColor, null);
        }
        hitCount = 0;
    }
    public EnemyType GetEnemyType()
    {
        return type;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer bullet = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (enemyColoredParts.Length > 0 && collision.gameObject.CompareTag("Bullet") && (enemyColoredParts[0].color == bullet.color))
        {
            Debug.Log(collision.gameObject.name + "Hit us");
            //Debug.Log("We hit");
            hitCount++;
            if (hitCount >= hitsRequired)
            {
                ScoreManager.Instance.OnKillEnemy(type);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.DealDamage(damage);
        }
    }
}
