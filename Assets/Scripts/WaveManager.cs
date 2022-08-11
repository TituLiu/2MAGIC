using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int simpleEnemyChanse;
    public Transform[] spawnPoint;
    public int enemyCounter;
    public float spawnRate, spawnRateIncrease;
    public bool alreadySpawn = false;

    public Pool<SimpleEnemy> enemyPool;
    public Pool<Kamikaze> enemyKamikazePool;

    public EnemySpawner enemySpawner = new EnemySpawner();
    public KamikazeSpawner enemyKamikazeSpawner = new KamikazeSpawner();


    private void Start()
    {
        enemyPool = new Pool<SimpleEnemy>(enemySpawner.Create, enemySpawner.TurnOffObject, enemySpawner.TurnOnObject, 8);
        enemyKamikazePool = new Pool<Kamikaze>(enemyKamikazeSpawner.Create, enemyKamikazeSpawner.TurnOffObject, enemyKamikazeSpawner.TurnOnObject, 4);
        EventManager.Instance.Subscribe("OnEnemyKilled", EnemyKilled);
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        if (enemyCounter < 8 && !alreadySpawn)
        {
            if (Random.Range(1,11) <= simpleEnemyChanse)
            {
                var enemy = enemyPool.Get();
                enemy.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
                enemyCounter++;
                StartCoroutine(WaveSpawner());
            }
            else
            {
                var enemy = enemyKamikazePool.Get();
                enemy.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
                enemyCounter++;
                StartCoroutine(WaveSpawner());
            }
        }
    }
    IEnumerator WaveSpawner()
    {
        alreadySpawn = true;
        yield return new WaitForSeconds(spawnRate);
        if (spawnRate > 3)
        {
            spawnRate -= spawnRateIncrease;
        }       
        alreadySpawn = false;
        SpawnEnemy();
    }
    private void EnemyKilled(object[] obj)
    {
        enemyCounter--;
        SpawnEnemy();
    }
}


