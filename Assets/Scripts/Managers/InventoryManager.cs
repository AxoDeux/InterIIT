using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }       //singleton

    [SerializeField] private TMP_Text t_waterBalloon;
    [SerializeField] private TMP_Text t_horn;
    [SerializeField] private TMP_Text t_stone;
    [SerializeField] private TMP_Text t_coins;

    private int waterBalloonCount;
    private int hornCount;
    private int stoneCount;
    private int coinCount;
    private bool isVeggieBagBought;

    public enum Items {
        none,
        waterBalloon,
        horn,
        stone,
        coins,
        veggieBag
    };

    private Items activeItem;


    private void Awake() {
        if(Instance!=null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        waterBalloonCount = 1;
        hornCount = 1;
        stoneCount = 1;
        coinCount = 1;
        isVeggieBagBought = false;

        activeItem = Items.none;
    }

    public void OnCollectCoin() {
        coinCount++;
        t_coins.text = coinCount.ToString();
    }

    public void OnClickWaterBalloon() {
        activeItem = Items.waterBalloon;
        Debug.Log(activeItem);
        waterBalloonCount--;
        t_waterBalloon.text = waterBalloonCount.ToString();

        if(waterBalloonCount == 0) {
            //deactivate button
        }
    }

    public void OnClickHorn() {
        activeItem = Items.horn;
        Debug.Log(activeItem);

        hornCount--;
        t_horn.text = hornCount.ToString();

        if(hornCount == 0) {
            //deactivate horn button
        }
    }

    public void OnClickStone() {
        activeItem = Items.stone;
        Debug.Log(activeItem);

        stoneCount--;
        t_stone.text = stoneCount.ToString();

        if(stoneCount == 0) {
            //deactivate stone button
        }

    }

    public void OnClickCoin() {
        activeItem = Items.coins;
        Debug.Log(activeItem);

    }

    public void OnClickVeggieBag() {
        activeItem = Items.veggieBag;
        Debug.Log(activeItem);

    }


}
