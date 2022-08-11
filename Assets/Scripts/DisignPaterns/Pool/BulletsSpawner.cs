using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsSpawner : MonoBehaviour
{
    BulletFactory _bulletFactory = new BulletFactory();
    public GameObject _bullet;

    public BulletFather TurnOnObject(BulletFather b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public BulletFather TurnOffObject(BulletFather b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public BulletFather Create()
    {
        return _bulletFactory.Create(_bullet);
    }
}
