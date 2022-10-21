using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : BulletFather
{
    GameObject target;
    [SerializeField] GameObject _watterAttackObj;
    [SerializeField] float _speedChange;

    protected override void Start()
    {
        base.Start();
        _MyDelegate = SimpleMovement;
        BulletElementalAttack();
        speedBullet *= _speedChange;
    }
    void Update()
    {
        _MyDelegate();
    }

    protected override void BulletElementalAttack()
    {
        _watterAttackObj.SetActive(true);
    }
    public override void Reset(params object[] parameters)
    {
        base.Reset();
        _watterAttackObj.SetActive(false);
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bounce") //Cambiar esto
        {
            transform.forward = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
        }
        if (collision.gameObject.tag == "MapLimit") 
        {
            gameObject.SetActive(false);
            Reset();
        }
    }
}
