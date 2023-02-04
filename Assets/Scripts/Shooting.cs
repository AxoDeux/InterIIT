using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    float currTime = 0;
    float minTime = 0.2f;

    private void Start()
    {
        SetBulletGunColor();
        //GameManager.SetColor(bulletPrefab.GetComponent<SpriteRenderer>());
    }

    private void SetBulletGunColor()
    {
        Color clr = GameManager.SetColor(gunSprite);
        bulletPrefab.GetComponentInChildren<SpriteRenderer>().color = clr;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //if(EventSystem.current.IsPointerOverGameObject()) { return; }
            if(currTime>minTime) {
            Shoot();
                currTime = 0;
            } else {
                currTime += Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponentInChildren<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        //change gun color on shoot
        SetBulletGunColor();
        //GameManager.SetColor(gunSprite);
    }


}
