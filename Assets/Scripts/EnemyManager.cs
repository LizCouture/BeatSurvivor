using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonobehaviour <EnemyManager> {
    public GameObject enemyPrefab;
    public float spawnMaxDistance = 10, spawnMinDistance= 7, timeBetweenSpawns = 2.5f;
    private float currentTime;

    private GameObject player;
    //TODO: FIX THIS
    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        currentTime = 0;
        player = GameObject.Find("Player");
    }
     
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeBetweenSpawns) {
            spawnEnemy();
            currentTime = 0;
        }
    }

    private void spawnEnemy() {
        bool spawnLeft = randomBool();
        bool spawnDown = randomBool();
        float spawnOffsetX = Random.Range(spawnMinDistance, spawnMaxDistance);
        float spawnOffsetY = Random.Range(spawnMinDistance, spawnMaxDistance);
        Vector2 spawnPos;
        if (spawnLeft) {
            spawnOffsetX = -spawnOffsetX;
        }
        if (spawnDown) {
            spawnOffsetY = -spawnOffsetY;
        }

        spawnPos = new Vector2 (player.transform.position.x + spawnOffsetX, player.transform.position.y + spawnOffsetY);
        GameObject enemyToSpawn = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemies.Add(enemyToSpawn);
    }

    private bool randomBool() {
        if (Random.value >= 0.5) return true;
        return false;
    }

    public void onEnemyDie(GameObject enemy) {
        enemies.Remove(enemy);

        if (enemy.TryGetComponent<Enemy>( out Enemy enemyScript)){
            GameObject gemToSpawn = GameObject.Instantiate(enemyScript.GemDropped, enemy.transform.position, Quaternion.identity, GemManager.Instance.transform);
            Debug.Log("gemToSpawn " + gemToSpawn);
            GemManager.Instance.AddGem(gemToSpawn);
        }
        GameObject.Destroy(enemy);
    }
}
