using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime;

    [SerializeField]
    private float nextSpawn = 0f;

    
    void Update()
    {
        SpawnEnemy(); 
    }

    public void SpawnEnemy()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;

            Instantiate(enemy, transform.position, enemy.transform.rotation);
        }
    }
}
