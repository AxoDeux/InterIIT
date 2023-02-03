using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionZone : MonoBehaviour {

    public delegate void OnGetInfectedHandler(bool isPlayerInInfectionZone);
    public static event OnGetInfectedHandler OnGetInfected;

    //public delegate bool InverseGravityHandler(bool isGravityEvent);
    //public static event InverseGravityHandler inverseGravityEvent;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            //Slow player speed
            OnGetInfected.Invoke(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            //increase player speed
            OnGetInfected.Invoke(false);
        }
    }
}
