using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HominingBulletFactory : IFactory<HomingBullet, GameObject>
{
    public HomingBullet Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<HomingBullet>();
        return obj;
    }
}
