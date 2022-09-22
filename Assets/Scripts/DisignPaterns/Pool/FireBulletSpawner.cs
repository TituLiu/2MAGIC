using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletSpawner : MonoBehaviour
{
    ExplosiveBulletFactory _bulletFactory = new ExplosiveBulletFactory();
    public GameObject explosiveBullet;

    public FireBullet TurnOnObject(FireBullet b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public FireBullet TurnOffObject(FireBullet b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public FireBullet Create()
    {
        return _bulletFactory.Create(explosiveBullet);
    }
}

