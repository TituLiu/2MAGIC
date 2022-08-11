using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : BulletFather
{
    public GameObject explotionArea;
    private void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Reset);
        bulletIntensity = 2;
        _MyDelegate = Movement;
        _MyDelegate += BulletBehaviour;
    }
    void Update()
    {
        _MyDelegate();
    }
    public void BulletBehaviour()
    {
        StartCoroutine(TimeUntilExplotion());
    }
    IEnumerator TimeUntilExplotion()
    {
        yield return new WaitForSeconds(2);
        if (!particlesOn)
        {
            EventManager.Instance.Trigger("OnExplotionParticle", transform.position, 0);
            particlesOn = true;
        }
        Explote();
    }
    private void Explote()
    {
        _MyDelegate -= Movement;
        explotionArea.SetActive(true);
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(.5f);
        Reset();
        gameObject.SetActive(false);
    }
    public override void Reset(params object[] parameters)
    {
        base.Reset();
        _MyDelegate += BulletBehaviour;
        particlesOn = false;
        explotionArea.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamagable>();
        if (damageable != null)
        {
            Explote();
        }
    }
}
