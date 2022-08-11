using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorverBulletSpawner : MonoBehaviour
{
    AbsorverBulletFactory _bulletFactory = new AbsorverBulletFactory();
    public GameObject absorverBullet;

    public AbsorverBullet TurnOnObject(AbsorverBullet b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public AbsorverBullet TurnOffObject(AbsorverBullet b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public AbsorverBullet Create()
    {
        return _bulletFactory.Create(absorverBullet);
    }
}
