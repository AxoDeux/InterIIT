using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy
{
    [SerializeField] private float explosionDist;
    [SerializeField] private GameObject toxicZone;

    private EnemyFollow followScript;
    private TimeBody timeScript;
    private GameObject player;
    private Vector2 distance;

    private void Awake()
    {
        timeScript = GetComponent<TimeBody>();
        followScript = GetComponent<EnemyFollow>();
        player = followScript.target.gameObject;
    }

    private void Update()
    {
        distance = player.transform.position - transform.position;
        if (distance.magnitude < explosionDist)
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        toxicZone.SetActive(true);
        toxicZone.GetComponent<SpriteRenderer>().color = rndColor;
        //enemyCircle.gameObject.SetActive(false);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        timeScript.enabled = false;
        followScript.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        toxicZone.GetComponent<Animator>().SetBool("shouldBlast", true);

        yield return new WaitForSeconds(5f);
        toxicZone.SetActive(false);
        toxicZone.GetComponent<Animator>().SetBool("shouldBlast", false);

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
