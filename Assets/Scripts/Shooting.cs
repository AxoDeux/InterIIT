using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private GameObject dualWeapon;
    [SerializeField] private GameObject singleWeapon;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private GameObject gunSprite;
    [SerializeField] private GameObject gunSprite1;
    [SerializeField] private GameObject gunSprite2;

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

    bool mustFlipLeft = false;
    bool isFacingLeft = false;

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
        RotateGun(shootingMode);
        if (Input.GetButton("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;


            if (currTime > minTime)
            {
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

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameManager.SetColor(bullet.GetComponentInChildren<SpriteRenderer>(), gunSprite.GetComponent<SpriteRenderer>().color, null);
        Rigidbody2D rb = bullet.GetComponentInChildren<Rigidbody2D>();
        rb.AddForce(aimDir.normalized * bulletForce, ForceMode2D.Impulse);

        SoundManager.PlaySound(SoundManager.Sound.shoot);
    }

    private void RotateGun(ShootingMode mode)
    {
        //taking firePoint as common for both modes
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        aimDir = mousePos - new Vector2(firePoint.position.x, firePoint.position.y);
        aimDir.Normalize();
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (mode == ShootingMode.Single)
        {
            ChangeGunRotation(gunSprite, angle);

        }
        else
        {
            ChangeGunRotation(gunSprite1, angle);
            ChangeGunRotation(gunSprite2, angle);
        }

    }
    private void ChangeGunRotation(GameObject go, float angle)
    {
        go.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (angle < -90 || angle > 90)
        {
            if (aimDir.x < 0)
            {
                go.transform.localRotation = Quaternion.Euler(180, 0, -angle);
            }
            else if (aimDir.x > 0)
            {
                go.transform.localRotation = Quaternion.Euler(180, 180, -angle);
            }
        }
    }
    private void DualShoot()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        GameManager.SetColor(bullet1.GetComponentInChildren<SpriteRenderer>(), gunSprite1.GetComponent<SpriteRenderer>().color, null);
        Rigidbody2D rb1 = bullet1.GetComponentInChildren<Rigidbody2D>();
        rb1.AddForce(aimDir * bulletForce, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        GameManager.SetColor(bullet2.GetComponentInChildren<SpriteRenderer>(), gunSprite2.GetComponent<SpriteRenderer>().color, null);
        Rigidbody2D rb2 = bullet2.GetComponentInChildren<Rigidbody2D>();
        rb2.AddForce(aimDir * bulletForce, ForceMode2D.Impulse);
    }

    public void ChangeShootingMode(ShootingMode mode)
    {
        if (mode == ShootingMode.Single)
        {
            dualWeapon.SetActive(false);
            singleWeapon.SetActive(true);
        }
        else
        {
            dualWeapon.SetActive(true);
            singleWeapon.SetActive(false);
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
        //Debug.Log(color);
        i_currWeapon.color = i_nextWeapon.color;
        i_nextWeapon.color = color;
        i_nextWeapon.fillAmount = 0f;

        gunSprite.GetComponent<SpriteRenderer>().color = i_currWeapon.color;
        gunSprite1.GetComponent<SpriteRenderer>().color = i_currWeapon.color;
        gunSprite2.GetComponent<SpriteRenderer>().color = i_currWeapon.color;
    }
}