using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }       //singleton

    [SerializeField] private TMP_Text t_timeCell;
    [SerializeField] private TMP_Text t_coins;

    private int timeCellCount;
    private int coinCount;

    public enum Items
    {
        none,
        weaponUpgrade,
        timeCell,
        coins,
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        timeCellCount = 0;
        coinCount = 0;
    }

    public void OnCollectCoin()
    {
        coinCount++;
        //SoundManager.PlaySound(SoundManager.Sound.coin);
        t_coins.text = coinCount.ToString();
    }

    public void OnCollectTimeCell()
    {
        timeCellCount++;
        t_timeCell.text = timeCellCount.ToString();
    }

    public void OnClickTimeCell()
    {
        //subtract time cells or energybar
        t_timeCell.text = timeCellCount.ToString();
        Debug.Log("Rewind time");
    }


    public void OnClickCoin()
    {

    }

}
