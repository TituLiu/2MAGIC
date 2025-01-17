﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int simpleEnemyChanse;
    public int spawnPointsNum;
    public int spawnPointsDistanceOffset;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<GameObject> spawnPointList;
    public int enemyCounter;
    public float spawnRate, spawnRateIncrease;
    bool alreadySpawn = false;
    bool canSpawn = true;

    public Pool<SimpleEnemy> enemyPool;
    public Pool<Kamikaze> enemyKamikazePool;

    public EnemySpawner enemySpawner = new EnemySpawner();
    public KamikazeSpawner enemyKamikazeSpawner = new KamikazeSpawner();

    private void Awake()
    {
        spawnPointList.Add(spawnPoint);
        for (int i = 0; i < spawnPointsNum - 1; i++)
        {
            var currentPos = spawnPointList[i];
            var obj = Instantiate(spawnPoint, transform);
            obj.transform.position = new Vector3
                (currentPos.transform.position.x + spawnPointsDistanceOffset, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
            spawnPointList.Add(obj);
        }
    }
    private void Start()
    {
        enemyPool = new Pool<SimpleEnemy>(enemySpawner.Create, enemySpawner.TurnOffObject, enemySpawner.TurnOnObject, 8);
        enemyKamikazePool = new Pool<Kamikaze>(enemyKamikazeSpawner.Create, enemyKamikazeSpawner.TurnOffObject, enemyKamikazeSpawner.TurnOnObject, 4);
        EventManager.Instance.Subscribe("OnEnemyKilled", EnemyKilled);
        EventManager.Instance.Subscribe("OnStopWaveSpawner", StopSpawn);
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        if (enemyCounter < 8 && !alreadySpawn && canSpawn)
        {
            if (Random.Range(1,11) <= simpleEnemyChanse)
            {
                var enemy = enemyPool.Get();
                enemy.transform.position = spawnPointList[Random.Range(0, spawnPointList.Count)].transform.position;
                enemyCounter++;
            }
            else
            {
                var enemy = enemyKamikazePool.Get();
                enemy.transform.position = spawnPointList[Random.Range(0, spawnPointList.Count)].transform.position;
                enemyCounter++;
            }
        }
        StartCoroutine(WaveSpawner());
    }
    IEnumerator WaveSpawner()
    {
        alreadySpawn = true;
        yield return new WaitForSeconds(spawnRate);
        if (spawnRate > 3 && canSpawn)
        {
            spawnRate -= spawnRateIncrease;
        }       
        alreadySpawn = false;
        SpawnEnemy();
    }
    private void EnemyKilled(object[] obj)
    {
        enemyCounter--;
    }
    void StopSpawn(object[] obj)
    {
        canSpawn = false;
        StartCoroutine(RestarSpawner(10));
    }
    IEnumerator RestarSpawner(float time)
    {
        yield return new WaitForSeconds(time);
        canSpawn = true;
    }
}


