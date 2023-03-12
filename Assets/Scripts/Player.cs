using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Player : SingletonMonobehaviour <Player>{
    public float moveSpeed = 1f;
    public UnityEvent playerHealthChanged;
    public UnityEvent playerHealthMaxChanged;
    public UnityEvent playerDied;
    public UnityEvent playerGotXP;

    [SerializeField] float maxHealth = 5;
    [SerializeField] float currentHealth = 5;
    [SerializeField] float damageCooldown = 0.5f;
    [SerializeField] float currentXP = 0.0f;
    [SerializeField] int currentLevel = 1;

    [SerializeField] int[] levels;

    private float currentDamageCooldown = 0.0f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private bool isRunning;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentXP { get => currentXP; set => currentXP = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        if (levels.Length < 1) {
            populateLevels();
        }

        currentHealth = maxHealth;
        playerHealthMaxChanged.Invoke();
        playerHealthChanged.Invoke();
        playerGotXP.Invoke();
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

    void takeDamage(float damage) {
        Debug.Log("Player took damage.");
        CurrentHealth -= damage;
        currentDamageCooldown = 0.0f;
        playerHealthChanged.Invoke();

        if (CurrentHealth <= 0) {
            die();
        }
    }

    void die() {
        Debug.Log("Player Died, oh no!");
        playerDied.Invoke();
    }

    //Desired Collision behavior:
    // When player comes in contact with an enemy they take damage from that enemy, then get a duration of invincibility == damageCooldown.
    //
    // When player comes in contact with a gem, pick it up and increment XP.

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) {
            currentDamageCooldown = 0.0f;
            takeDamage(enemyComponent.HitDamage);
        } else if (collision.gameObject.TryGetComponent<Gem>(out Gem gemComponent)) {
            Debug.Log("Hit Gem!  Adding " + gemComponent.XpValue + " xp!");
            currentXP += gemComponent.XpValue;
            if (levels[currentLevel] <= currentXP) {
                Debug.Log("Current Level: " + currentLevel);
                Debug.Log("Current XP = " + currentXP + ".  XP to next level = " + levels[currentLevel]);
                currentXP = currentXP - levels[currentLevel];
                currentLevel++;
            }
            playerGotXP.Invoke();
            gemComponent.Pickup();
        }
    }

    private void OnCollisionStay2D (Collision2D collision) {
        Debug.Log("Collision with Player");
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) {
            if (currentDamageCooldown < damageCooldown) {
                Debug.Log("damage cooldown: " + currentDamageCooldown);
                currentDamageCooldown += Time.deltaTime;
            } else {
                Debug.Log("Player Hit Enemy");
                takeDamage(enemyComponent.HitDamage);
            }
        } 
    }

    private void populateLevels() {
        levels = new int[500];
        for (int i = 0; i < 500; i++) {
            if (i == 0) {
                levels[i] = 0;
            }
            if (i < 20) {
                levels[i] = (i * 10) - 5;
            } else if (i < 40) {
                levels[i] = (i * 13) - 6;
            } else {
                levels[i] = (i * 16) - 8;
            }
        }
    }
}
