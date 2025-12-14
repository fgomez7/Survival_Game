using UnityEngine;

public class EnemySpawnBoundary : MonoBehaviour
{
    public EnemySpawner spawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED SPAWN ZONE");
            spawner.StartSpawning();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER LEFT SPAWN ZONE");
            spawner.StopSpawning();
        }
    }
}
