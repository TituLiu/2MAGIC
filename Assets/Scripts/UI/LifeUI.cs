using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] RawImage[] hearts;
    [SerializeField] RawImage[] emptyHearts;
    [SerializeField] int currentLife = 0;
    [SerializeField] int maxLife = 5;

    [Header("Life")]
    [SerializeField] private RawImage[] shield;
    [SerializeField] private RawImage[] emptyShield;
    [SerializeField] private int currentShield = 0;
    [SerializeField] private int maxShield = 3;
    private void Start()
    {
        EventManager.Instance.Subscribe("OnUpdateLife", OnUpdateLife);
        EventManager.Instance.Subscribe("OnUpdateShield", OnUpdateShield);
        currentLife = maxLife;
    }
    private void OnUpdateLife(params object[] parameters)
    {
        var life = (int)parameters[0];
        currentLife = life;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLife)
            {
                hearts[i].enabled = true;
                emptyHearts[i].enabled = false;
            }
            else
            {
                hearts[i].enabled = false;
                emptyHearts[i].enabled = true;
            }
        }
    }
    private void OnUpdateShield(params object[] parameters)
    {
        var shieldd = (int)parameters[0];
        currentShield = shieldd;
        for (int i = 0; i < shield.Length; i++)
        {
            if (i < currentShield)
            {
                shield[i].enabled = true;
                shield[i].gameObject.SetActive(true);
            }
            else
            {
                shield[i].enabled = false;
                shield[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnRevive(params object [] parameters)
    {

    }
}
