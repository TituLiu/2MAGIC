using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeFactory : IFactory<Kamikaze, GameObject>
{
    public Kamikaze Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<Kamikaze>();
        return obj;
    }
}
