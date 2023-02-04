using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Bullet")) {
            //check bullet type and then destroy
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }else if(collision.gameObject.CompareTag("Player")) {
            //reduce Health
        }
    }
}
