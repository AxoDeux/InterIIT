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
                    ScoreManager.Instance.OnCollectTimeCell();
                    break;
                case InventoryManager.Items.weaponUpgrade:
                    Shooting shootingScript = collision.GetComponent<Shooting>();
                    shootingScript.ChangeShootingMode(Shooting.ShootingMode.Dual);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
