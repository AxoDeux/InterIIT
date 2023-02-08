using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyFollow : MonoBehaviour
{
    //public float stoppingDistance;
    //public float speed = 2f;

    private Vector2 lookDir;
    private float angle;    //to point at player
    Animator anim;

    [Header("Pathfinding")]
    public AIPath aiPath;
    public Transform target;
    private AIDestinationSetter destinationSetter;

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start() {
        destinationSetter.target = target;
        float randSpeed = Random.Range(1.5f, 3f);
        aiPath.maxSpeed = randSpeed;
    }

    private void Update() {
        //flips object as per player position *CHECK SCALE
        if(aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if(aiPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    //private void Awake()
    //{
    //    anim = GetComponentInChildren<Animator>();
    //    rb = GetComponent<Rigidbody2D>();
    //    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    //}

    //void Update()
    //{
    //    if (!GameManager.isRewinding)
    //        FollowPlayer();
    //}

    //private void FollowPlayer()
    //{
    //    if (Vector2.Distance(target.position, transform.position) > stoppingDistance)
    //        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //    if (anim != null)
    //        anim.SetTrigger("isWalking");
    //    //RotateEnemy();
    //}

    //private void Rotate()
    //{
    //    lookDir = target.position - transform.position;
    //    angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
    //    rb.rotation = angle;
    //}
}
