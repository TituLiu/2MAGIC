using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeightPointer : MonoBehaviour
{
    public static FlyWeight simpleEnemyStats = new FlyWeight()
    {
        maxLife = 1, 
        maxSpeed = 30,
        minDistance = 25,
        maxDistance = 56
    };
    public static FlyWeight kamikazeEnemyStats = new FlyWeight()
    {
        maxLife = 1,
        maxSpeed = 45,
        minDistance = 15,
        maxDistance = 36
    };
}
