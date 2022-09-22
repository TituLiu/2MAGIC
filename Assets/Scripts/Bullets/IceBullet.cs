using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : BulletFather
{
    public GameObject absorverArea;

    void Update()
    {
        _MyDelegate();
    }
    protected override void BulletElementalAttack()
    {
        //Ataque de hielo
    }
    IEnumerator TimeUntilStopMoving()
    {
        yield return new WaitForSeconds(2.5f);
        _MyDelegate -= SimpleMovement;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        absorverArea.SetActive(true);
        StartCoroutine(TimeUntilAbsorverExplote());
    }
    IEnumerator TimeUntilAbsorverExplote()
    {
        yield return new WaitForSeconds(5);
        Reset();
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamagable>();
        if (damageable != null)
        {
            damageable.Damage(10);
        }
    }
    protected override void Reset(params object[] parameters)
    {
        base.Reset();
        rb.constraints = RigidbodyConstraints.None;
        absorverArea.SetActive(false);
    }
}
