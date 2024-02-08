using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float spawnTimer = 1f;
    

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 0f;
    [SerializeField] private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!GameMenagerScript.Instance.IsRunning) return;

        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = UnityEngine.Random.Range(5f, 7f) - spawnRate;
            spawnRate = Mathf.Clamp(spawnTimer + 0.25f, 0f, 5f);
        }
    }

    private void SpawnEnemy()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        enemy.gameObject.SetActive(true);
        //enemy.transform.parent = transform;
    }
}
