using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2f;

    private float tmpSpeed;
    private float infectedSpeed = 2.5f;

    private void Start() {
        tmpSpeed = speed;
    }
    private void OnEnable() {
        ToxicZone.PlayerEnterToxicZoneEvent += HandleToxicZoneEffect;
    }
    private void OnDisable() {
        ToxicZone.PlayerEnterToxicZoneEvent -= HandleToxicZoneEffect;
    }

    void Update()
    {
        if(isDashing) { return; }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetButtonDown("Dash") && canDash) {
            StartCoroutine(Dash());
        }
        
    }

    private void FixedUpdate() {
        if(isDashing) {
            rb.MovePosition(rb.position + movement * dashingPower * Time.fixedDeltaTime);
            return; 
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;        
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    public void HandleToxicZoneEffect(bool isInToxicZone) {   //changing speed variable because its used in update methods
        if(isInToxicZone) {
            speed = infectedSpeed;
            Debug.Log(speed);
            //deal damage
        } else {
            speed = tmpSpeed;
            Debug.Log(speed);

        }
    }

}
