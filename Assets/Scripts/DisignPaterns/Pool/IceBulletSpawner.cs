using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBulletSpawner : MonoBehaviour
{
    AbsorverBulletFactory _bulletFactory = new AbsorverBulletFactory();
    public GameObject absorverBullet;

    public IceBullet TurnOnObject(IceBullet b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public IceBullet TurnOffObject(IceBullet b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public IceBullet Create()
    {
        return _bulletFactory.Create(absorverBullet);
    }
}
