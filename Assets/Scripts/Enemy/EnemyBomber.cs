using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy
{
    [SerializeField] private float explosionDist;
    [SerializeField] private GameObject toxicZone;
    [SerializeField] private GameObject bomberModel;

    private bool isCoroutineStarted = false;

    private EnemyFollow followScript;
    private TimeBody timeScript;
    private GameObject player;
    private Vector2 distance;

    private void Awake()
    {
        timeScript = GetComponent<TimeBody>();
        followScript = GetComponent<EnemyFollow>();
        //player = followScript.target.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        distance = player.transform.position - transform.position;
        if (distance.magnitude < explosionDist && !isCoroutineStarted)
        {
            //Debug.Log("We explode");
            //***
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        isCoroutineStarted = true;

        toxicZone.SetActive(true);
        bomberModel.SetActive(false);
        //toxicZone.GetComponent<SpriteRenderer>().color = rndColor;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        timeScript.enabled = false;
        followScript.StopFollowing();
        toxicZone.GetComponent<Animator>().SetBool("shouldBlast", true);
        //Debug.Log("We set the shouldblast true here");
        yield return new WaitForSeconds(5f);
        toxicZone.GetComponent<Animator>().SetBool("shouldBlast", false);
        toxicZone.SetActive(false);

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
