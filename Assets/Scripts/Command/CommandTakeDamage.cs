using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTakeDamage : ICommand
{
    GameObject castleObj;
    public CommandTakeDamage(GameObject castle)
    {
        castleObj = castle;
    }
    public void Execute()
    {
        castleObj.GetComponent<Castle>().Damage(1);
    }
}
