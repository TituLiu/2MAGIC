using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBulletSpawner : MonoBehaviour
{
    HominingBulletFactory _bulletFactory = new HominingBulletFactory();   
    public GameObject homingBullet;

    public WaterBullet TurnOnObject(WaterBullet b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public WaterBullet TurnOffObject(WaterBullet b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public WaterBullet Create()
    {
        return _bulletFactory.Create(homingBullet);
    }
}
