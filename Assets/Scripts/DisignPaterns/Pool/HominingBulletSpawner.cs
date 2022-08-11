using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HominingBulletSpawner : MonoBehaviour
{
    HominingBulletFactory _bulletFactory = new HominingBulletFactory();   
    public GameObject homingBullet;

    public HomingBullet TurnOnObject(HomingBullet b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public HomingBullet TurnOffObject(HomingBullet b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public HomingBullet Create()
    {
        return _bulletFactory.Create(homingBullet);
    }
}
