using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform pos1, pos2;
    [SerializeField] protected float dist;
    public Element bulletElement;
    public GameObject target;
    public GameObject bulletPrefab;

    public Pool<BulletFather> bulletPool;
    public BulletsSpawner bulletSpawner;

    public float distance, speed, counterTime;
    public int life, minDistance, maxDistance;
    public bool dead;
    public bool revive;
    public float offset;
    [SerializeField] protected Vector3 targetDir;
    public void Movement()
    {
        //transform.forward = -target.transform.forward;
        //transform.position = new Vector3(transform.position.x, offset, transform.position.z);
        //transform.position += transform.forward * speed * Time.deltaTime;

        rb.velocity = transform.forward * speed;
        if (Vector3.Distance(transform.position, targetDir) < dist)
        {
            //Shoot();
            ChangeTargetDir();
        }
    }
    public void ChangeTargetDir()
    {
        targetDir = new Vector3(Random.Range(pos1.position.x , pos2.position.x), transform.position.y, Random.Range(pos1.position.z, pos2.position.z));
        var newDir = targetDir - transform.position;
        transform.forward = newDir;
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
