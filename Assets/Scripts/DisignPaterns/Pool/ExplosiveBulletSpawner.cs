using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletSpawner : MonoBehaviour
{
    ExplosiveBulletFactory _bulletFactory = new ExplosiveBulletFactory();
    public GameObject explosiveBullet;

    public ExplosiveBullet TurnOnObject(ExplosiveBullet b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public ExplosiveBullet TurnOffObject(ExplosiveBullet b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public ExplosiveBullet Create()
    {
        return _bulletFactory.Create(explosiveBullet);
    }
}

