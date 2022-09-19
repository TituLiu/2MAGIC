using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletFather
{
    void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Reset);
        _MyDelegate = Movement;
        mr = meshObject.GetComponent<MeshRenderer>();
        randomNumber = Random.Range(1, 11);
        //if (randomNumber <= 5) bulletIntensity = 1;
        //else if (randomNumber > 5 && randomNumber <= 8) bulletIntensity = 2;
        //else bulletIntensity = 3;

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
        switch (bulletIntensity)
        {
            case 1:
                mr.material.color = Color.green;
                mr.material.SetColor("_EmissionColor", Color.green);
                break;
            case 2:
                mr.material.color = Color.yellow;
                mr.material.SetColor("_EmissionColor", Color.yellow);
                break;
            case 3:
                mr.material.color = Color.red;
                mr.material.SetColor("_EmissionColor", Color.red);
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
