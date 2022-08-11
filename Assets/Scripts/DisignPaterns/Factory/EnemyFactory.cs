using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : IFactory<SimpleEnemy, GameObject>
{
    public SimpleEnemy Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<SimpleEnemy>();
        return obj;
    }
}
