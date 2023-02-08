using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private GameObject dualWeapon;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private GameObject gunSprite;
    [SerializeField] private SpriteRenderer gunSprite2;

    [SerializeField] private Image i_currWeapon;
    [SerializeField] private Image i_nextWeapon;
    private float colorChangeTime = 3f;

    public enum ShootingMode
    {
        Single,
        Dual
    };
    private ShootingMode shootingMode;

    [Tooltip("Color change time of of guns")]
    //float colorChangeTime = ga;

    float currTime_2 = 2.0f;

    //[SerializeField]
    //GameManager gameManager;

    float currTime = 0f;
    float minTime = 0.2f;
    private Vector2 mousePos;
    private Vector2 shootDir;
    private Vector3 aimDir;

    private Camera cam;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        shootingMode = ShootingMode.Single;
    }

    private void Update()
    {
        RotateGun();
        if (Input.GetButton("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;


            if (currTime > minTime)
            {
                Shoot();
                if (shootingMode == ShootingMode.Single) { Shoot(); }
                else if (shootingMode == ShootingMode.Dual) { DualShoot(); }
                currTime = 0;
            }

            else
            {
                currTime += Time.deltaTime;
            }
        }
        if (currTime_2 > colorChangeTime)
        {
            currTime_2 = 0;
            SetGunColour();
        }
        else
        {
            currTime_2 += Time.deltaTime;
            i_nextWeapon.fillAmount = currTime_2 / colorChangeTime;
        }
    }

    private void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        GameManager.SetColor(bullet.GetComponentInChildren<SpriteRenderer>(), gunSprite.GetComponent<SpriteRenderer>().color, null);
        Rigidbody2D rb = bullet.GetComponentInChildren<Rigidbody2D>();
        //shootDir = 
        //RotateGun();


        rb.AddForce(aimDir.normalized * bulletForce, ForceMode2D.Impulse);
    }

    private void RotateGun()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        aimDir = mousePos - new Vector2(firePoint1.position.x, firePoint1.position.y);
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gunSprite.transform.rotation = Quaternion.Slerp(gunSprite.transform.rotation, rotation, 5f * Time.deltaTime);
    }
    private void DualShoot()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        GameManager.SetColor(bullet1.GetComponentInChildren<SpriteRenderer>(), gunSprite.GetComponent<SpriteRenderer>().color, null);
        Rigidbody2D rb1 = bullet1.GetComponentInChildren<Rigidbody2D>();
        rb1.AddForce(firePoint1.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        GameManager.SetColor(bullet2.GetComponentInChildren<SpriteRenderer>(), gunSprite2.color, null);
        Rigidbody2D rb2 = bullet2.GetComponentInChildren<Rigidbody2D>();
        rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);
    }

    public void ChangeShootingMode(ShootingMode mode)
    {
        if (mode == ShootingMode.Single)
        {
            dualWeapon.SetActive(false);
        }
        else
        {
            dualWeapon.SetActive(true);
            StartCoroutine(ResetShootingMode());
        }
        shootingMode = mode;
    }

    private IEnumerator ResetShootingMode()
    {
        yield return new WaitForSeconds(15f);
        ChangeShootingMode(ShootingMode.Single);
    }

    private void SetGunColour()
    {
        Color color = GameManager.ChooseRandomColor();
        i_currWeapon.color = i_nextWeapon.color;
        i_nextWeapon.color = color;
        i_nextWeapon.fillAmount = 0f;

        gunSprite.GetComponent<SpriteRenderer>().color = i_currWeapon.color;
        gunSprite2.color = i_currWeapon.color;
    }
}