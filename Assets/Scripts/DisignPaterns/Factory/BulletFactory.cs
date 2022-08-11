using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : IFactory<BulletFather, GameObject>
{
    public BulletFather Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<BulletFather>();
        return obj;
    }
}
