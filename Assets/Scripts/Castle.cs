using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IDamagable
{
    [Header("Gem Rotation")]
    [SerializeField] private GameObject _gem;
    [SerializeField] private float _rotationSpeed;
    
    [Header("Gem Life")]
    public int life;
    public int maxLife;
    void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Revive);
        life = maxLife;
    }
    public void Damage(int damageTaken)
    {
        life -= damageTaken;
        if (life > 0)
        {
            EventManager.Instance.Trigger("OnCristalDamaged", life);
        }
        else
        {
            EventManager.Instance.Trigger("OnLevelEnd");
        }
    }
    public void Revive(params object [] parameters)
    {
        life = maxLife / 2 + 1;
        EventManager.Instance.Trigger("OnCristalDamaged", life);
    }

    private void Update()
    {
        _gem.transform.Rotate(Vector3.up * _rotationSpeed);
    }
}
