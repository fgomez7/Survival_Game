using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Mushroom, Skeleton, etc
    public BoxCollider2D spawnArea;

    public float spawnInterval = 3f;
    public int maxEnemies = 10;

    private bool spawning;
    private int currentEnemies;

    public void StartSpawning()
    {
        if (spawning) return;
        spawning = true;
        StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        spawning = false;
        StopAllCoroutines();
    }

    IEnumerator SpawnLoop()
    {
        while (spawning && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector2 pos = GetRandomPoint(spawnArea.bounds);
        GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemy, pos, Quaternion.identity);
        currentEnemies++;
    }

    Vector2 GetRandomPoint(Bounds b)
    {
        return new Vector2(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y)
        );
    }
}
