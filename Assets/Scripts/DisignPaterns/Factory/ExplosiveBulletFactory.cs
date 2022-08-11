using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletFactory : IFactory<ExplosiveBullet, GameObject>
{
    public ExplosiveBullet Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<ExplosiveBullet>();
        return obj;
    }
}
