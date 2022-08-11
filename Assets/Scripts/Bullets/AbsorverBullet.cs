using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorverBullet : BulletFather
{
    public GameObject absorverArea;
    private void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Reset);
        bulletIntensity = 3;
        rb = GetComponent<Rigidbody>();
        _MyDelegate = Movement;
        _MyDelegate += BulletBehaviour;
    }
    void Update()
    {
        _MyDelegate();
    }
    public void BulletBehaviour()
    {
        StartCoroutine(TimeUntilStopMoving());
    }
    IEnumerator TimeUntilStopMoving()
    {
        yield return new WaitForSeconds(2.5f);
        _MyDelegate -= Movement;
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
    public override void Reset(params object[] parameters)
    {
        base.Reset();
        _MyDelegate += BulletBehaviour;
        rb.constraints = RigidbodyConstraints.None;
        absorverArea.SetActive(false);
    }
}
