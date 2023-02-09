using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private InventoryManager.Items itemType;

    private void Start() {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay() {
        float rnd = Random.Range(0f, 1f);
        yield return new WaitForSeconds(rnd);
        LeanTween.moveLocalY(gameObject, 0.5f, 1f).setEaseInOutCubic().setLoopPingPong();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (itemType)
            {
                case InventoryManager.Items.coins:
                    {
                        InventoryManager.Instance.OnCollectCoin();
                        SoundManager.PlaySound(SoundManager.Sound.coin);
                        ScoreManager.Instance.HealInfection(10);
                    }
                    break;
                case InventoryManager.Items.timeCell:
                    {
                        InventoryManager.Instance.OnCollectTimeCell();
                        ScoreManager.Instance.OnCollectTimeCell();
                        SoundManager.PlaySound(SoundManager.Sound.TimeCellCollect);

                    }
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
