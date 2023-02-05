using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletForce;

    [SerializeField]
    SpriteRenderer gunSprite;

    [Tooltip("Color change time of of guns")]
    //float colorChangeTime = ga;

    float currTime = 2.0f;

    //[SerializeField]
    GameManager gameManager;

    Color colorChosen;
    [SerializeField]
    [Tooltip("Slider for the circle")]
    Slider slider;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //StartColorChoosingSequence();
        //GameManager.SetColor(bulletPrefab.GetComponent<SpriteRenderer>());
    }

    //private void StartColorChoosingSequence()
    //{
    //    colorChosen = GameManager.ChooseRandomColor();
    //    SetBulletGunColor(colorChosen);
    //}

    private void SetBulletGunColor(Color colorChosen)
    {
        Color clr = GameManager.SetColor(gunSprite, colorChosen, null);
        bulletPrefab.GetComponentInChildren<SpriteRenderer>().color = clr;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (currTime > GameManager.colorChangeTime)
        {
            currTime = 0;
            gameManager.StartColorChoosingSequence(gunSprite);

            //GameManager.
            //SetBulletGunColor();
        }
        else
        {
            currTime += Time.deltaTime;
        }
        slider.value = currTime / GameManager.colorChangeTime;

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameManager.SetColor(bullet.GetComponentInChildren<SpriteRenderer>(), gameObject.GetComponent<SpriteRenderer>().color, null);
        Rigidbody2D rb = bullet.GetComponentInChildren<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        //change gun color on shoot
        //SetBulletGunColor();
    }


}
