using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    [Tooltip("Color change time of of guns")]
    //float colorChangeTime = ga;

    float currTime = 2.0f;

    //[SerializeField]
    GameManager gameManager;

    Color colorChosen;
    [SerializeField]
    [Tooltip("Slider for the circle")]
    Slider slider;

    float currTime_2 = 0;
    float minTime = 0.2f;
    private Vector2 mousePos;
    private Vector2 shootDir;

    private Camera cam;

    private void Awake() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

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
        if (Input.GetButton("Fire1"))
        {
            //if(EventSystem.current.IsPointerOverGameObject()) { return; }
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if(currTime>minTime) {
                Shoot();
                currTime = 0;
            } else {
                currTime += Time.deltaTime;
            }
        }
        if (currTime_2 > GameManager.colorChangeTime)
        {
            currTime_2 = 0;
            gameManager.StartColorChoosingSequence(gunSprite);

            //GameManager.
            //SetBulletGunColor();
        }
        else
        {
            currTime_2 += Time.deltaTime;
        }
        slider.value = currTime_2 / GameManager.colorChangeTime;

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameManager.SetColor(bullet.GetComponentInChildren<SpriteRenderer>(), gameObject.GetComponent<SpriteRenderer>().color, null);
        Rigidbody2D rb = bullet.GetComponentInChildren<Rigidbody2D>();
        shootDir = mousePos - new Vector2(firePoint.position.x, firePoint.position.y);
        rb.AddForce(shootDir.normalized * bulletForce, ForceMode2D.Impulse);
        //change gun color on shoot
        //SetBulletGunColor();
    }


}
