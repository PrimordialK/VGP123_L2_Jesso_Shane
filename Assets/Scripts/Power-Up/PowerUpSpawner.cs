using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Assign your power-up prefabs here")]
    public GameObject[] powerUpPrefabs;

    [Header("Assign your spawn points here")]
    public Transform[] spawnPoints;

    private void Start()
    {
        int spawnCount = Mathf.Min(5, spawnPoints.Length); // Only spawn up to the number of available spawn points

        // Create a list of indices and shuffle them
        int[] indices = new int[spawnPoints.Length];
        for (int i = 0; i < indices.Length; i++)
            indices[i] = i;

        // Fisher-Yates shuffle
        for (int i = indices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }

        // Spawn at unique spots
        for (int i = 0; i < spawnCount; i++)
        {
            int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
            int spawnPointIndex = indices[i];

            Instantiate(
                powerUpPrefabs[powerUpIndex],
                spawnPoints[spawnPointIndex].position,
                Quaternion.identity
            );
        }
    }

    // Call this to spawn a random power-up at a random spawn point
    public void SpawnRandomPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || spawnPoints.Length == 0)
            return;

        int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(
            powerUpPrefabs[powerUpIndex],
            spawnPoints[spawnPointIndex].position,
            Quaternion.identity
        );
    }
}
