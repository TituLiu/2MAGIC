using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BulletFather
{
    [SerializeField] float explotionRadius;
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
        _MyDelegate -= SimpleMovement;
        _MyDelegate -= WaitForActivate;

        Collider[] explotion = Physics.OverlapSphere(transform.position, explotionRadius, enemyLayerMask);
        if (explotion != null)
        {
            foreach (var enemy in explotion)
            {
                var enemyLife = enemy.GetComponent<IDamagable>();
                if (enemyLife != null)
                {
                    enemyLife.Damage(1);
                }
            }
        }

        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(.5f);
        Reset();
        gameObject.SetActive(false);
    }
    protected override void Reset(params object[] parameters)
    {
        base.Reset();
        particlesOn = false;
    }
    private void OnDrawGizmos()
    {
        
    }
}
