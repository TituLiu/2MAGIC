using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    ObjectFactory _bulletFactory = new ObjectFactory();
    public GameObject _bullet;
    public GameObject _homingBullet;

    public GameObject TurnOnObject(GameObject b)
    {
        b = gameObject;
        b.gameObject.SetActive(true);
        return b;
    }
    public GameObject TurnOffObject(GameObject b)
    {
        b = gameObject;
        b.SetActive(false);
        return b;
    }
    public GameObject Create()
    {
        return _bulletFactory.Create(_homingBullet);
    }
}
