using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : Enemy
{
    delegate void Delegate();
    Delegate _MyDelegate;
    public int timeUntilKamikaze;
    void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Die);
        revive = false;
        dead = false;
        target = FindObjectOfType<Life>().gameObject;
        life = FlyWeightPointer.simpleEnemyStats.maxLife;
        speed = FlyWeightPointer.simpleEnemyStats.maxSpeed; //sacar
        _MyDelegate = Movement;
        _MyDelegate += CheckDistance;
    }   
    void Update()
    {
        _MyDelegate();
    }
    public override void CheckDistance()
    {
        base.CheckDistance();
        if (distance <= Random.Range(FlyWeightPointer.kamikazeEnemyStats.minDistance, FlyWeightPointer.kamikazeEnemyStats.maxDistance))
        {
            _MyDelegate = Stay;
            StartCoroutine(TimeUntilKamikaze());
        }
    }
    IEnumerator TimeUntilKamikaze()
    {
        yield return new WaitForSeconds(timeUntilKamikaze);        
        _MyDelegate = Movement;
    }
    private void Explote()
    {
        dead = true;
        Reset();
        gameObject.SetActive(false);
    }
    public override void Reset()
    {
        base.Reset();
        _MyDelegate = CheckDistance;
        dead = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Damage(1, Element.Fire);
        }
        var damageable = collision.gameObject.GetComponent<IDamagable>();
        if (damageable != null)
        {
            _MyDelegate = Stay;
            Explote();
            damageable.Damage(1, Element.Fire);
            EventManager.Instance.Trigger("OnExplotionParticle", transform.position, 0);
        }
    }   
}
