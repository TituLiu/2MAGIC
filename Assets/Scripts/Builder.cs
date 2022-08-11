using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder 
{
    GameObject castleObj;
    public void SetCasle(GameObject castle)
    {
        castleObj = castle;
    }
    public CommandTakeDamage Build()
    {
        return new CommandTakeDamage(castleObj);
    }
}
