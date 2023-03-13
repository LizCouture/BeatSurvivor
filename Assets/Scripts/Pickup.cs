using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [SerializeField] CircleCollider2D pickupCollider;
    [SerializeField] float pickupRadius = 0.5f;

    private void Start() {
        if (!pickupCollider) {
            pickupCollider = GetComponentInChildren<CircleCollider2D>();
        }
        pickupCollider.radius = pickupRadius;
    }

    private void Update() {
        transform.position = Player.Instance.gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Pickup collider hit");
        if (collision.gameObject.TryGetComponent<Gem>(out Gem gemComponent)) {
            Player.Instance.PickupGem(gemComponent);
        }
    }
}
