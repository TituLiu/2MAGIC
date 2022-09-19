using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemyFactory _enemyFactory = new EnemyFactory();
    public GameObject enemy;

    public SimpleEnemy TurnOnObject(SimpleEnemy b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public SimpleEnemy TurnOffObject(SimpleEnemy b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public SimpleEnemy Create()
    {
        return _enemyFactory.Create(enemy);
    }
}
