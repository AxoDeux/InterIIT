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
    private bool isXOutOfBound = false;
    private bool isYOutOfBound = false;

    private void FixedUpdate() {
        GetDesiredPos();
        smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }

    private void GetDesiredPos() {
        playerX = player.position.x;
        playerY = player.position.y;


        if(playerX > xPosBound || playerX < xNegBound) isXOutOfBound = true;
        else isXOutOfBound = false;

        if(playerY > yPosBound || playerY < yNegBound) isYOutOfBound = true;
        else isYOutOfBound = false;

        if(isXOutOfBound && isYOutOfBound) {
            desiredPos = transform.position;
        }else if(isXOutOfBound) {
            desiredPos = new Vector3(transform.position.x, player.position.y, 0) + offset;
        }else if(isYOutOfBound) {
            desiredPos = new Vector3(player.position.x, transform.position.y, 0) + offset;
        } else {
            desiredPos = player.position + offset;
        }
    }
}
