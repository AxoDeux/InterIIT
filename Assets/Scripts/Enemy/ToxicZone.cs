using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicZone : MonoBehaviour
{
    public delegate void PlayerEnterHandler(bool isInToxicZone);
    public static event PlayerEnterHandler PlayerEnterToxicZoneEvent;

    [SerializeField] private float damage = 5f;
    [SerializeField] private float damageInterval = 1f;
    private float currTime = 0f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            //deal infection

            //Slow player movement
            PlayerEnterToxicZoneEvent.Invoke(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            if(currTime > damageInterval) {
                ScoreManager.Instance.DealDamage(damage);
                PostProcessingManager.Instance.PlayerHurting();
                currTime = 0f;
            } else {
                currTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            PlayerEnterToxicZoneEvent.Invoke(false);
        }
    }
}
