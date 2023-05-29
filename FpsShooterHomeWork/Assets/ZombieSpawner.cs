using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnArea;
    public int zombieCount = 1;

    private void Start()
    {
        SpawnZombies();
    }

    private void SpawnZombies()
    {
        for (int i = 0; i < zombieCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(zombiePrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 spawnCenter = spawnArea.position;
        Vector3 spawnExtents = spawnArea.localScale / 2f;

        float randomX = Random.Range(spawnCenter.x - spawnExtents.x, spawnCenter.x + spawnExtents.x);
        float randomY = Random.Range(spawnCenter.y - spawnExtents.y, spawnCenter.y + spawnExtents.y);
        float randomZ = Random.Range(spawnCenter.z - spawnExtents.z, spawnCenter.z + spawnExtents.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
