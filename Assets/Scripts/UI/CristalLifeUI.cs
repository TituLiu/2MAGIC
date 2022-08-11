using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CristalLifeUI : MonoBehaviour
{
    public RawImage[] rubiHearts;
    public RawImage[] emptyRubiHearts;
    public int currentCristalLife = 0;
    public int maxCristalLife = 5;
    private void Start()
    {
        EventManager.Instance.Subscribe("OnCristalDamaged", OnCristalDamaged);
        currentCristalLife = maxCristalLife;
    }
    private void OnCristalDamaged(params object[] parameters)
    {
        var life = (int)parameters[0];
        currentCristalLife = life;
        for (int i = 0; i < rubiHearts.Length; i++)
        {
            if (i < currentCristalLife)
            {
                rubiHearts[i].enabled = true;
                emptyRubiHearts[i].enabled = false;
            }
            else
            {
                rubiHearts[i].enabled = false;
                emptyRubiHearts[i].enabled = true;
            }
        }
    }
    private void OnRevive(params object [] parameters)
    {

    }
}
