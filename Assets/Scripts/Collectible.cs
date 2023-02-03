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
                case InventoryManager.Items.waterBalloon:
                    //update waterballoons
                    Debug.Log("WaterBalloon collected");
                    break;
                case InventoryManager.Items.veggieBag:
                    Debug.Log("VeggieBag collected");
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
