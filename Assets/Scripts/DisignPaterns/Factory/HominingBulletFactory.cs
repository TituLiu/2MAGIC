using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HominingBulletFactory : IFactory<WaterBullet, GameObject>
{
    public WaterBullet Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<WaterBullet>();
        return obj;
    }
}
