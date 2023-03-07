using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private bool isRunning;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
        AnimateRun();
    }

    void AnimateRun() {
        isRunning = (moveInput.x > 0.1f || moveInput.x < -0.1f || moveInput.y > 0.1f || moveInput.y < -0.1f);
        if (moveInput.x < 0) {
            sprite.flipX = true;
        } else if (moveInput.x > 0){
            sprite.flipX = false;
        }
        animator.SetBool("isRunning", isRunning);
    }
}
