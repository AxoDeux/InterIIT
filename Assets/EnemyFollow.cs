using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float stoppingDistance;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (Vector2.Distance(target.position, transform.position) > stoppingDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
