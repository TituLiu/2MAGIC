using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminManager : MonoBehaviour
{
    public GameObject castleObj;
    private ICommand commandTakeDamage;
    private void Awake()
    {
        var builder = new Builder();
        builder.SetCasle(castleObj);
        commandTakeDamage = builder.Build();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            commandTakeDamage.Execute();
        }
    }
}
