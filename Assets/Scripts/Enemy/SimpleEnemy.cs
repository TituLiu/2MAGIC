using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    delegate void Delegate();
    Delegate _MyDelegate;
    [SerializeField] float shootDir;
    [SerializeField] MeshRenderer[] enemyMat;
    private void Awake()
    {
        target = FindObjectOfType<Life>().gameObject;
    }
    void Start()
    {
        ChangeElement();
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
    private void Update()
    {
        healthBar.transform.forward = Camera.main.transform.forward;
    }
    private void FixedUpdate()
    {
        _MyDelegate();
    }
    public override void Shoot()
    {
        transform.rotation = Quaternion.Euler(0, -180 + Random.Range(-shootDir, shootDir), 0);
        var bullet = bulletPool.Get();
        bullet.bulletElement = myElement;
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
    private void ChangeElement()
    {
        int randomElement = Random.Range(0, 3);
        switch (randomElement)
        {
            case 0:
                myElement = Element.Fire;
                foreach (var mat in enemyMat)
                {
                    Color color = mat.material.color;
                    color = Color.red;
                    color.a = 0.5f;
                    mat.material.color = color;
                }
                break;
            case 1:
                myElement = Element.Ice;
                foreach (var mat in enemyMat)
                {
                    Color color = mat.material.color;
                    color = Color.cyan;
                    color.a = 0.5f;
                    mat.material.color = color;
                }
                break;
            case 2:
                myElement = Element.Water;
                foreach (var mat in enemyMat)
                {
                    Color color = mat.material.color;
                    color = Color.blue;
                    color.a = 0.5f;
                    mat.material.color = color;
                }
                break;
            default:
                Debug.LogError("Innexistent Element");
                break;
        }
    }
    protected override void OnEnable()
    {
        ChangeElement();
    }
}
