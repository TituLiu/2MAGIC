using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public GameObject target;
    public GameObject bulletPrefab;

    public Pool<BulletFather> bulletPool;
    public BulletsSpawner bulletSpawner;

    public float distance, speed, counterTime;
    public int life, minDistance, maxDistance;
    public bool dead;
    public bool revive;
    public float offset;
    public void Movement()
    {
        transform.LookAt(target.transform.position);
        transform.position = new Vector3(transform.position.x, offset, transform.position.z);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    public virtual void LateralMovement()
    {
        counterTime += 1 * Time.deltaTime;
        transform.LookAt(target.transform.position);
        transform.RotateAround(target.transform.position, transform.up, speed / 2 * Time.deltaTime);
        if (counterTime >= 3)
        {
            speed *= -1;
            counterTime = 0;
        }
    }
    public void Stay() { }
    public virtual void CheckDistance()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
    }
    public virtual void Shoot()
    {
        var bullet = bulletPool.Get();
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position;
    }
    public void Die(params object[] parameters)
    {
        bool param = (bool)parameters[0];
        revive = param;
        dead = param;
        if (dead)
        {
            Debug.Log("Revivi");
            Reset();
            gameObject.SetActive(false);
            if (revive == false)
            {
                EventManager.Instance.Trigger("OnEnemyKilled");
                EventManager.Instance.Trigger("OnEnemyDeath", Random.Range(8, 11));
            }          
        }
    }
    public void Damage(int damageTaken)
    {
        life -= damageTaken;
        if (life <= 0)
        {
            dead = true;
            Die(true);
        }
    }
    public virtual void Reset()
    {
        dead = false;       
        revive = false;
        life = FlyWeightPointer.simpleEnemyStats.maxLife;
    }
}
