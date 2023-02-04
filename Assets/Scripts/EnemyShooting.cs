using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private float bulletForce;
    private GameObject bullet;
    private Rigidbody2D bulletRb;

    private bool canShoot = true;

    private void Update() {
        if(!canShoot) { return; }
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot() {
        canShoot = false;
        bullet = Instantiate(enemyBulletPrefab, firePoint);
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
}
