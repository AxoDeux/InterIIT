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
                    InventoryManager.Instance.OnCollectCoin();
                    break;
                case InventoryManager.Items.timeCell:
                    InventoryManager.Instance.OnCollectTimeCell();
                    break;
                case InventoryManager.Items.weaponUpgrade:
                    InventoryManager.Instance.OnCollectAdvancedWeapon();
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
