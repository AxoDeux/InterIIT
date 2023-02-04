using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isJumping = false;

    private bool isPlayerInInfectionZone = false;

    private float moveDirection;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update() {
        ProcessInput();
        //flip direction - check inputaxis value (-1/1) and facing right bool
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void OnEnable() {
        InfectionZone.OnGetInfected += HandleInfection;
    }

    private void OnDisable() {
        InfectionZone.OnGetInfected -= HandleInfection;
    }

    private void ProcessInput() {
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && (rb.velocity.y < 0.5 && rb.velocity.y > -0.5)) {
            isJumping = true;
        }
    }

    private void MovePlayer() {
        if(!isPlayerInInfectionZone) {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(moveSpeed/2, rb.velocity.y);
        }

        if(isJumping && !isPlayerInInfectionZone) {             //infected player doesnt jump
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;
    }

    private void FlipCharacter() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void HandleInfection(bool isPlayerInfected) {
        if(isPlayerInfected) {
            isPlayerInInfectionZone = true;
        } else {
            StartCoroutine(RecoverFromInfection());
        }
    }

    IEnumerator RecoverFromInfection() {
        yield return new WaitForSeconds(2f);
        isPlayerInInfectionZone = false;
    }
}

