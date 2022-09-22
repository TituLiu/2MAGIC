using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorverBulletFactory : IFactory<IceBullet, GameObject>
{
    public IceBullet Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<IceBullet>();
        return obj;
    }
}
