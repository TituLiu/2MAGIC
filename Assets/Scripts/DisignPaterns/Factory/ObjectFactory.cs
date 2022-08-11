using UnityEngine;

public class ObjectFactory : IFactory<GameObject, GameObject>
{
    public GameObject Create(GameObject prefab)
    {
        var obj = Object.Instantiate(prefab).GetComponent<GameObject>();
        return obj;
    }
}
