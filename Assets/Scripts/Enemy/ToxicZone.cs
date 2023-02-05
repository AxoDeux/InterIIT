using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicZone : MonoBehaviour
{
    public delegate void PlayerEnterHandler(bool isInToxicZone);
    public static event PlayerEnterHandler PlayerEnterToxicZoneEvent;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            //deal infection

            //Slow player movement
            PlayerEnterToxicZoneEvent.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            PlayerEnterToxicZoneEvent.Invoke(false);
        }
    }
}
