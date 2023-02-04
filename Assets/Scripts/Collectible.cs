using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private InventoryManager.Items itemType;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            switch(itemType) {
                case InventoryManager.Items.coins:
                    //update coins
                    Debug.Log("Coin Collected");
                    InventoryManager.Instance.OnCollectCoin();
                    break;

            }
            gameObject.SetActive(false);
        }
    }
}
