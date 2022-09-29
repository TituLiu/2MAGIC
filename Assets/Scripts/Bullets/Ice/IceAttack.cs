using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    [SerializeField] float _speed;
    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.gameObject.GetComponent<IDamagable>();
        if (damageable != null) damageable.Damage(1, Element.Ice);
        Destroy(gameObject);
    }
}
