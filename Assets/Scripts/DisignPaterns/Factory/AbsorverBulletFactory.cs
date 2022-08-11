using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorverBulletFactory : IFactory<AbsorverBullet, GameObject>
{
    public AbsorverBullet Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<AbsorverBullet>();
        return obj;
    }
}
