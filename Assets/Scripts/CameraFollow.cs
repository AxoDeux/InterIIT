using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 offset;

    private float smoothSpeed = 12.5f;
    private Vector3 desiredPos, smoothedPos;

    private void FixedUpdate() {
        desiredPos = player.position + offset;
        smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }
}
