using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    delegate void Delegate();
    Delegate _MyDelegate;
    [SerializeField] float shootDir;
    private void Awake()
    {
        target = FindObjectOfType<Castle>().gameObject;
    }
    void Start()
    {
        int randomElement = Random.Range(0, 3);
        switch (randomElement)
        {
            case 0:
                bulletElement = Element.Fire;
                break;
            case 1:
                bulletElement = Element.Water;
                break;
            case 2:
                bulletElement = Element.Ice;
                break;
            default:
                break;
        }
        EventManager.Instance.Subscribe("OnRevive", Die);
        dead = false;
        revive = false;
        bulletPool = new Pool<BulletFather>(bulletSpawner.Create, bulletSpawner.TurnOffObject, bulletSpawner.TurnOnObject, 2);
        life = FlyWeightPointer.simpleEnemyStats.maxLife;
        speed = FlyWeightPointer.simpleEnemyStats.maxSpeed;
        _MyDelegate = CheckDistance;
        _MyDelegate += Movement;
    }
    private void Update()
    {
        _MyDelegate();
    }
    public override void Shoot()
    {
        transform.rotation = Quaternion.Euler(0, -180 + Random.Range(-shootDir, shootDir), 0);
        var bullet = bulletPool.Get();
        bullet.bulletElement = bulletElement;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position;

        _MyDelegate = Stay;
        StartCoroutine(TimeUntilContinue());
    }
    IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(Random.Range(7,11));
        _MyDelegate = Shoot;
    }
    public override void LateralMovement()
    {
        base.LateralMovement();    
    }
    IEnumerator TimeUntilContinue()
    {
        yield return new WaitForSeconds(3);
        _MyDelegate = LateralMovement;
        StartCoroutine(ShootCD());
    }
    public override void CheckDistance()
    {
        base.CheckDistance();
        if (distance <= Random.Range(FlyWeightPointer.simpleEnemyStats.minDistance, FlyWeightPointer.simpleEnemyStats.maxDistance))
        {
            _MyDelegate = Shoot;
        }
    }
    public override void Reset()
    {
        base.Reset();
        _MyDelegate = CheckDistance;
        dead = false;
    }
}
