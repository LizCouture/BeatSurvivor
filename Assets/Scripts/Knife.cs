using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: I called this class Knife because it will remind me that this only works for this one weapon.  This class needs to be implemented in an object oriented way.
// Normally composition is better over inheritence, but we might actually prefer a weapon base class that overwrites specific methods.
// Every weapon needs to define its rhythm, how it reacts with the upgrade system, what it does on fire, what it does when it moves,
// what it does when it collides with an enemy.  Some of that can be defined in data with data files, but I think a lot will be their own methods and I think that's ok.
// I can't imagine we'll have more than 8-10 of them.  

// I think we'll also need 2 things, the game object + script representing the knife weapon the player controls, and an object that represents a knife in the world,
// one of many the player will throw.
public class Knife : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float timeBetweenFire;

    public List<GameObject> knifeProjectiles;

    private float currentTime;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeBetweenFire) {
            GameObject target = findTarget();
            GameObject newKnife = Instantiate(prefab, transform.position, Quaternion.identity);
            newKnife.GetComponent<KnifeProjectile>().target = target;
            newKnife.transform.SetParent(transform);
            knifeProjectiles.Add(newKnife);
            currentTime = 0;
        }
    }

    // Finds a random enemy and travels towards that enemy.  If no enemy is found, travel in a random direction.
    private GameObject findTarget() {
        GameObject target;
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0) {
            target = enemies[Random.Range(0, enemies.Length)];
        } else {
            target = new GameObject("Empty");
            target.transform.position = Random.insideUnitCircle.normalized;
        }
        return target;
    }
}
