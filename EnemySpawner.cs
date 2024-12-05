using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;
    private float spawnTime;
    public Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        setSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            int i = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[i]);
            setSpawnTime();
        }
    }
    private void setSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
