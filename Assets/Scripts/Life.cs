using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour, IDamagable
{   
    [Header("Life")]
    [SerializeField] private int life;
    [SerializeField] private int maxLife;
    [SerializeField] private int shield;
    [SerializeField] private int maxShield;
    void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Revive);
        life = maxLife;
    }
    public void HealLife()
    {
        life += 1;
        EventManager.Instance.Trigger("OnUpdateLife", life);
    }
    public void GetShield()
    {
        Debug.Log("GetShield");
        shield += 1;
        EventManager.Instance.Trigger("OnUpdateShield", shield);
    }
    public void Damage(int damageTaken, Element elem)
    {
        if (shield > 0)
        {
            shield -= 1;
            EventManager.Instance.Trigger("OnUpdateShield", shield);
            return;
        }
        life -= 1;
        EventManager.Instance.Trigger("OnUpdateLife", life);
        if (life == 0)
        {
            EventManager.Instance.Trigger("OnLevelEnd");
        }
    }
    public void Revive(params object [] parameters)
    {
        life = maxLife / 2 + 1;
        EventManager.Instance.Trigger("OnUpdateLife", life);
    }
}
