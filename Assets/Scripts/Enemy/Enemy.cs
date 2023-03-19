using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public SpriteRenderer[] enemyColoredParts;

    [SerializeField] private float damage = 10f;
    [SerializeField] private int hitsRequired = 1;
    [SerializeField] GameObject splashEffect;
    private int hitCount;
    public enum EnemyType
    {
        Contagious,
        Shooter,
        Bomber
    };
    [SerializeField] private EnemyType type;

    public Color rndColor;
    Transform _player;

    public void Start()
    {
        Color rndColor = GameManager.ChooseRandomColor();
        //Debug.Log("Enemy chosen color");
        foreach (SpriteRenderer sprite in enemyColoredParts)
        {
            GameManager.SetColor(sprite, rndColor, null);
        }
        hitCount = 0;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
    public EnemyType GetEnemyType()
    {
        return type;
    }
    private void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer bullet = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (enemyColoredParts.Length > 0 && collision.gameObject.CompareTag("Bullet") && (enemyColoredParts[0].color == bullet.color))
        {
            hitCount++;
            Vector2 collisionPoint = collision.GetContact(0).point;
            Vector2 collisionNormal = collision.GetContact(0).normal;

            // Rotate the splash to face the direction of the collision
            float angle = Mathf.Atan2(collisionNormal.y, collisionNormal.x) * Mathf.Rad2Deg;

            if (hitCount >= hitsRequired)
            {
                ScoreManager.Instance.OnKillEnemy(type);
                //Instantiate(splashEffect, bullet.transform.position, Quaternion.LookRotation(transform.position - collision.transform.position));
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        //-370.9 -163.88
        else if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.DealDamage(damage);
            PostProcessingManager.Instance.PlayerHurting();
        }
    }
}
