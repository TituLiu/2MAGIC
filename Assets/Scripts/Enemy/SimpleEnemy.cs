using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    delegate void Delegate();
    Delegate _MyDelegate;
    [SerializeField] float shootDir;
    [SerializeField] MeshRenderer enemyMat;
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

        ChangeTargetDir();
        StartCoroutine(ShootCD());
        _MyDelegate = Movement;
    }
    private void FixedUpdate()
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
        rb.velocity = Vector3.zero;
        _MyDelegate = Stay;
        StartCoroutine(TimeUntilContinue());
    }
    IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(Random.Range(7,11));
        _MyDelegate = Shoot;
    }
    IEnumerator TimeUntilContinue()
    {
        yield return new WaitForSeconds(3);
        ChangeTargetDir();
        _MyDelegate = Movement;
        StartCoroutine(ShootCD());
    }
    //public override void CheckDistance()
    //{
    //    base.CheckDistance();
    //    if (distance <= Random.Range(FlyWeightPointer.simpleEnemyStats.minDistance, FlyWeightPointer.simpleEnemyStats.maxDistance))
    //    {
    //        _MyDelegate = Shoot;
    //    }
    //}
    public override void Reset()
    {
        base.Reset();
        _MyDelegate = CheckDistance;
        dead = false;
    }
    protected override void OnEnable()
    {
        int randomElement = Random.Range(0, 3);
        switch (randomElement)
        {
            case 0:
                bulletElement = Element.Fire;
                enemyMat.material.color = Color.red;
                break;
            case 1:
                bulletElement = Element.Ice;
                enemyMat.material.color = Color.cyan;
                break;
            case 2:
                bulletElement = Element.Water;
                enemyMat.material.color = Color.blue;
                break;
            default:
                Debug.LogError("Innexistent Element");
                break;
        }
    }
}
