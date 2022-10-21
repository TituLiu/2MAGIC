using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletFather
{
    protected override void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Reset);
        EventManager.Instance.Subscribe("OnBlackHole", BlackHoleAbsorved);
        _MyDelegate = SimpleMovement;
        mr = meshObject.GetComponent<MeshRenderer>();

        switch (bulletElement)
        {
            case Element.Fire:
                mr.material.color = Color.red;
                mr.material.SetColor("_EmissionColor", Color.red);
                break;
            case Element.Water:
                mr.material.color = Color.blue;
                mr.material.SetColor("_EmissionColor", Color.blue);
                break;
            case Element.Ice:
                mr.material.color = Color.cyan;
                mr.material.SetColor("_EmissionColor", Color.cyan);
                break;
            default:
                break;
        }
    }
    public override void Reset(params object[] parameters)
    {
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        _MyDelegate();
    }
}
