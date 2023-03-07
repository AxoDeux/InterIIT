using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField] private TrailRenderer tr;

    private Vector2 movement;
    private Vector2 mousePos;
    private Vector2 lookDir;

    public FloatingJoystick movementJoystick;
    //public FloatingJoystick shooT;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2f;

    private float tmpSpeed;
    private float infectedSpeed = 2.5f;

    [SerializeField]
    Animator playerAnim;
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private Light2D light2D;

    private void Start()
    {
        tmpSpeed = speed;
        playerAnim.GetComponentInChildren<Animator>();
        worldCanvas.worldCamera = cam;
    }
    private void OnEnable()
    {
        ToxicZone.PlayerEnterToxicZoneEvent += HandleToxicZoneEffect;
    }
    private void OnDisable()
    {
        ToxicZone.PlayerEnterToxicZoneEvent -= HandleToxicZoneEffect;
    }

    void Update()
    {
        if (isDashing) { return; }

        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        movement.x = movementJoystick.Horizontal + Input.GetAxisRaw("Horizontal");
        movement.y = movementJoystick.Vertical + Input.GetAxisRaw("Vertical");
        if (movement.x == 0 && movement.y == 0) playerAnim.SetBool("isWalking", false);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            //rb.MovePosition(rb.position + movement * dashingPower * Time.fixedDeltaTime);
            MovePlayer(dashingPower);
            return;
        }

        MovePlayer(speed);

        //lookDir = mousePos - rb.position;

        //float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;
    }

    private void MovePlayer(float speed)
    {
        playerAnim.SetBool("isWalking", true);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;
        light2D.intensity = Mathf.Lerp(1, 0, dashingTime);
        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        isDashing = false;
        light2D.intensity = Mathf.Lerp(0, 1, dashingCooldown);
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    public void HandleToxicZoneEffect(bool isInToxicZone)
    {   //changing speed variable because its used in update methods
        if (isInToxicZone)
        {
            speed = infectedSpeed;
            //deal damage
        }
        else
        {
            speed = tmpSpeed;

        }
    }

}
