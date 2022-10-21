using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlackHole : MonoBehaviour
{
    Action _BeheaviourAction;   

    [Header("Stats")]
    [SerializeField] float _speed;
    [SerializeField] float _maxForce;
    [SerializeField] float _radius;
    [SerializeField] float _arriveRadius;
    [SerializeField] float _aborveForceToCenter;
    [SerializeField] float _aborveForceToRight;
    [SerializeField] float _implotionTime;
    [SerializeField] LayerMask _enemiesMask;
    [SerializeField] LayerMask _enemiesBulletMask;
    [Header("Direction")]
    Vector3 _velocity;
    [SerializeField] Vector3 _targetDir;
    [Header("Other")]
    Rigidbody _rb;
    Animator _anim;
    [SerializeField] bool _viewRange;
    bool _activate;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        EventManager.Instance.Trigger("OnStopWaveSpawner", (float)10);
        _BeheaviourAction += Movement;
        _BeheaviourAction += CheckDistance;
    }
    private void Update()
    {
        EventManager.Instance.Trigger("OnBlackHole", (float)10);
    }
    private void FixedUpdate()
    {
        _BeheaviourAction();             
    }
    void Movement()
    {
        ApplyForce(Arrive());
        _rb.velocity = _velocity;
    }
    void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _speed);
    }
    Vector3 Arrive()
    {
        Vector3 desired = _targetDir - transform.position;
        if (desired.magnitude < _arriveRadius)
        {
            float currSpeed = _speed * (desired.magnitude / _arriveRadius);
            desired.Normalize();
            desired *= currSpeed;
        }
        else
        {
            desired.Normalize();
            desired *= _speed;
        }
        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        return steering;
    }
    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, _targetDir) < .5f)
        {
            _BeheaviourAction = Absorve;
            StartCoroutine(TimeUntilImplotion());
        }
    }
    void Absorve()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, _radius, _enemiesMask);
        if (enemies == null) return;
        foreach (var enemy in enemies)
        {
            var dir = transform.position - enemy.transform.position;
            dir.y = 0;
            var enemyRb = enemy.GetComponent<Rigidbody>();
            enemyRb.AddForce(dir.normalized * _aborveForceToCenter, ForceMode.Acceleration);
            enemyRb.AddForce((dir.normalized - Vector3.right) * _aborveForceToRight, ForceMode.Acceleration);
        }

        Collider[] bullets = Physics.OverlapSphere(transform.position, _radius, _enemiesBulletMask);
        if (bullets == null) return;
        foreach (var bullet in bullets)
        {
            var dir = transform.position - bullet.transform.position;
            dir.y = 0;
            var bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(dir.normalized * _aborveForceToCenter, ForceMode.Acceleration);           
        }
    }
    IEnumerator TimeUntilImplotion()
    {
        yield return new WaitForSeconds(_implotionTime);
        _anim.SetTrigger("Implotion");
        Destroy(gameObject, 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((_enemiesMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            var damageable = other.GetComponent<IDamagable>();
            damageable.Damage(10, Element.BlackHole);
        }
        if ((_enemiesBulletMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other.GetComponent<EnemyBullet>().Reset();
        }
    }
    private void OnDrawGizmos()
    {
        if (_viewRange)
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }        
    }
}
