using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] float xpValue = 1.0f;

    public float XpValue { get => xpValue; set => xpValue = value; }

    public void Pickup() {
        GemManager.Instance.RemoveGem(gameObject);
        GameObject.Destroy(gameObject);
    }
}
