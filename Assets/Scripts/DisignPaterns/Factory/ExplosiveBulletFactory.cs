using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletFactory : IFactory<FireBullet, GameObject>
{
    public FireBullet Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<FireBullet>();
        return obj;
    }
}
