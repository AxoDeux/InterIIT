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
    Transform player;

    public void Start()
    {
        Color rndColor = GameManager.ChooseRandomColor();
        //Debug.Log("Enemy chosen color");
        foreach (SpriteRenderer sprite in enemyColoredParts)
        {
            GameManager.SetColor(sprite, rndColor, null);
        }
        hitCount = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }
    public EnemyType GetEnemyType()
    {
        return type;
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer bullet = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (enemyColoredParts.Length > 0 && collision.gameObject.CompareTag("Bullet") && (enemyColoredParts[0].color == bullet.color))
        {
            //Debug.Log(collision.gameObject.name + "Hit us");
            //Debug.Log("We hit");
            hitCount++;
            //float angle = Mathf.Atan2((player.position.y - transform.position.y), (player.position.x - transform.position.x)) * Mathf.Rad2Deg;
            //Instantiate(splashEffect, transform.position, Quaternion.Euler(0, 0, angle));
            // Get the collision point and normal
            Vector2 collisionPoint = collision.GetContact(0).point;
            Vector2 collisionNormal = collision.GetContact(0).normal;

            // Instantiate the splash prefab at the collision point
            GameObject splash = Instantiate(splashEffect, collisionPoint, Quaternion.identity);

            splash.GetComponentInChildren<SpriteRenderer>().color = bullet.color;
            // Rotate the splash to face the direction of the collision
            float angle = Mathf.Atan2(collisionNormal.y, collisionNormal.x) * Mathf.Rad2Deg;
            splash.transform.rotation = Quaternion.AngleAxis(angle + 54.05f, Vector3.forward);

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
