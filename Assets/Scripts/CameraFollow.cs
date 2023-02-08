using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float xPosBound;
    [SerializeField] private float xNegBound;
    [SerializeField] private float yPosBound;
    [SerializeField] private float yNegBound;

    [SerializeField] private Vector3 offset;

    private float smoothSpeed = 12.5f;
    private Vector3 desiredPos, smoothedPos;

    private float playerX;
    private float playerY;

    private void FixedUpdate() {
        if(IsPlayerOutOfBounds()) { return; }
        desiredPos = player.position + offset;
        smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }

    private bool IsPlayerOutOfBounds() {
        playerX = player.position.x;
        playerY = player.position.y;

        if(playerX > xPosBound || playerX < xNegBound || playerY > yPosBound || playerY < yNegBound) return true;
        else return false;
    }
}
