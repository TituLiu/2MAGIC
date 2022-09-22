using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : BulletFather
{
    GameObject target;
    bool targetAquired = false;

    void Update()
    {
        _MyDelegate();
    }
    public void BulletBehaviour()
    {
        transform.forward = target.transform.position;
        transform.position += transform.forward * speedBullet * Time.deltaTime;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(8);
        Reset();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && !targetAquired)
        {
            targetAquired = true;
            target = other.gameObject;     
            _MyDelegate = BulletBehaviour;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamagable>();
        if (damageable != null)
        {
            damageable.Damage(10);
            Reset();
            gameObject.SetActive(false);
        }
    }
}
