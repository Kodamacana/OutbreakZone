using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemiesToSpawn;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {

        
            Vector3 randomSpawnPosition = new Vector3(Random.Range(transform.position.x -50, transform.position.x+ 50), 2, Random.Range(transform.position.z- 50, transform.position.z+50));
            Instantiate(enemiesToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }
}
