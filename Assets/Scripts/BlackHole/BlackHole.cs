using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask enemiesMask;
    [SerializeField] float aborveForceToCenter;
    [SerializeField] float aborveForceToRight;
    bool _activate;

    private void FixedUpdate()
    {
        Absorve();
        if (_activate)
        {
            
        }
    }
    void Absorve()
    {
        EventManager.Instance.Trigger("OnBlackHole");
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemiesMask);
        if (enemies == null) return;
        foreach (var enemy in enemies)
        {
            var dir = transform.position - enemy.transform.position;
            dir.y = 0;
            var enemyRb = enemy.GetComponent<Rigidbody>();
            enemyRb.AddForce(dir.normalized * aborveForceToCenter, ForceMode.Acceleration);
            enemyRb.AddForce((dir.normalized - Vector3.right) * aborveForceToRight, ForceMode.Acceleration);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((enemiesMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            var damageable = other.GetComponent<IDamagable>();
            damageable.Damage(10, Element.Fire);
            damageable.Damage(10, Element.Water);
            damageable.Damage(10, Element.Ice);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
