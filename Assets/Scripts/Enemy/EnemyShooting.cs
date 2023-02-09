using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShooting : Enemy
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private float bulletForce;
    private GameObject bullet;
    private Rigidbody2D bulletRb;

    public Transform target;
    private Vector3 lookDir;
    private float angle;    //to point at player
    public GameObject gunRb;

    private bool canShoot = true;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        RotateGun();
        if (!canShoot) { return; }
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        bullet = Instantiate(enemyBulletPrefab, firePoint);
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    private void RotateGun()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunRb.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
