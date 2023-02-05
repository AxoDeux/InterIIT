using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer enemyCircle;

    [SerializeField] private float damage = 10f;
    [SerializeField] private int hitsRequired = 1;
    private int hitCount;
    public enum EnemyType {
        Contagious,
        Shooter,
        Bomber
    };
    [SerializeField] private EnemyType type;

    public void Start()
    {
        Color rndColor = GameManager.ChooseRandomColor();
        GameManager.SetColor(enemyCircle, rndColor, null);
        hitCount = 0;
    }
    public EnemyType GetEnemyType() {
        return type;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer bullet = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (collision.gameObject.CompareTag("Bullet") && (enemyCircle.color == bullet.color))
        {
            hitCount++;
            if(hitCount >= hitsRequired) {
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
