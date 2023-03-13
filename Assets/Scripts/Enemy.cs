using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 2f;
    [SerializeField] float hitDamage = 1f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject gemDropped;

    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    SpriteRenderer sprite;

    public float HitDamage { get => hitDamage; set => hitDamage = value; }
    public GameObject GemDropped { get => gemDropped; set => gemDropped = value; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        health = maxHealth;
        target = GameObject.Find("Player").transform;
    }

    private void Update() {
        if (target) {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            AnimateRun();
        } else {
            Debug.Log("No target found for enemy");
        }
    }

    private void FixedUpdate() {
        if (target) {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void AnimateRun() {
        if (moveDirection.x < 0) {
            sprite.flipX = true;
        } else if (moveDirection.x > 0) {
            sprite.flipX = false;
        }
    }

    public void TakeDamage (float damage) {
        health = health - damage;
        if (health <= maxHealth) {
            die();
        }
    }

    private void die() {
        EnemyManager.Instance.onEnemyDie(gameObject);
    }
}
