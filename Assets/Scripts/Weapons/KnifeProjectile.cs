using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3;
    public GameObject target;
    [SerializeField] float timeToDestroy = 20;
    [SerializeField] float damage = 2;
    
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Knife knifeManager;
    private GameObject spriteObject;
    private float timeAlive;

    void Start()
    {
        knifeManager = GetComponentInParent<Knife>();
        rb = GetComponent<Rigidbody2D>();
        moveDirection = (target.transform.position - transform.position).normalized;
        rotateToFaceTarget();
    }

    void Update()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        if (target) {
            rotateToFaceTarget();
        }
        timeAlive += Time.deltaTime;
        if (timeAlive >= timeToDestroy) {
            Destroy(gameObject);
        }
    }

    // For debug purposes only.
    //void OnDrawGizmos() {
       // Gizmos.DrawLine(transform.position, target.transform.position);
   // }

    private void rotateToFaceTarget() {
        Vector3 relativePos = target.transform.position - transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    private float findQuaternionAngleToLookAt(Vector2 pos) {
        float x = pos.x - transform.position.x;
        float y = pos.y - transform.position.y;

        return Mathf.Atan2(x, y) * Mathf.Rad2Deg - 90;
    }
    //TODO: On collision, if target is not enemy, destroy it immediately.

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision with KnifeProjectile");
        if (collision.gameObject.tag == "Player") return;

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) {
            Debug.Log("Hit Enemy");
            enemyComponent.TakeDamage(damage);
        } else if (collision.gameObject == target) {
            // If the target isn't an enemy it is a BS target created to send it in a random direction.  Destroy it.
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }

    void OnDestroy() {
        knifeManager.knifeProjectiles.Remove(gameObject);

        // If the target isn't an enemy it is a bs target created to send it in a random direction.  Destroy it.
        if (target && target.tag != "Enemy") {
            Destroy(target);
        }
    }
}
