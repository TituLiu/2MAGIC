using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : BulletFather
{
    [SerializeField] Transform[] iceAttackDir;
    [SerializeField] GameObject iceAttackObj;
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
        foreach (var dir in iceAttackDir)
        {
            var iceAttack = Instantiate(iceAttackObj);
            iceAttack.transform.position = dir.position;
            iceAttack.transform.forward = dir.forward;
        }
        Reset();
    }
    public override void Reset(params object[] parameters)
    {
        base.Reset();
        rb.constraints = RigidbodyConstraints.None;
    }
}
