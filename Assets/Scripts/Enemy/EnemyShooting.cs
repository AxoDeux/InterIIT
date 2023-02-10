using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public static bool pauseShoot = false;
    private bool canShoot = true;


    public Transform bulletParent;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        RotateGun();
        if (!canShoot || pauseShoot) { return; }
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        bullet = Instantiate(enemyBulletPrefab, firePoint);
        bullet.transform.parent = bulletParent;
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        Vector3 dir = target.position - firePoint.position;
        dir.Normalize();
        bulletRb.AddForce(dir * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    private void RotateGun()
    {
        Vector3 direction = (target.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunRb.transform.rotation = Quaternion.Euler(0, 0, angle - 15f);
        if (angle < -90 || angle > 90)
        {
            if (direction.x < 0)
            {
                gunRb.transform.localRotation = Quaternion.Euler(180, 0, -angle);
            }
            else if (direction.x > 0)
            {
                gunRb.transform.localRotation = Quaternion.Euler(180, 180, -angle);
            }
        }
    }
}
