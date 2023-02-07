using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float stoppingDistance;

    private Rigidbody2D rb;
    private Vector2 lookDir;
    private float angle;    //to point at player

    Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (!GameManager.isRewinding)
            FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (Vector2.Distance(target.position, transform.position) > stoppingDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (anim != null)
            anim.SetTrigger("isWalking");
        //RotateEnemy();
    }

    private void Rotate()
    {
        lookDir = target.position - transform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
