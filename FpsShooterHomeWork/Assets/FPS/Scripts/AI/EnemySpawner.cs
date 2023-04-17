using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemiesToSpawn;
    [SerializeField] int value= 3;

    void Start()
    {
        for (int i = 0; i < value; i++)
        {        
            Vector3 randomSpawnPosition = new Vector3(Random.Range(transform.position.x -5, transform.position.x+ 5),
                Random.Range(transform.position.y - 1, transform.position.y + 1), Random.Range(transform.position.z- 5,
                transform.position.z+5));
            Instantiate(enemiesToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }
}
