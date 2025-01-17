﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BulletFather
{
    [SerializeField] float explotionRadius;
    bool particlesOn;
    protected override void Start()
    {
        base.Start();
        _MyDelegate = SimpleMovement;
        _MyDelegate += SearchTarget;
        _MyDelegate += WaitForActivate;
    }
    void Update()
    {
        _MyDelegate();
    }
    protected override void BulletElementalAttack()
    {
        if (!particlesOn)
        {
            EventManager.Instance.Trigger("OnExplotionParticle", transform.position, 0);
            particlesOn = true;
        }
        _MyDelegate = delegate { };

        Collider[] explotion = Physics.OverlapSphere(transform.position, explotionRadius, enemyLayerMask);
        if (explotion != null)
        {
            foreach (var enemy in explotion)
            {
                var enemyLife = enemy.GetComponent<IDamagable>();
                if (enemyLife != null)
                {
                    enemyLife.Damage(1, Element.Fire);
                }
            }
        }
        Reset();
    }
    public override void Reset(params object[] parameters)
    {
        base.Reset();
        particlesOn = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, searchTargetRadius);
    }
}
