using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeightPointer : MonoBehaviour
{
    public static FlyWeight simpleEnemyStats = new FlyWeight()
    {
        maxLife = 3, 
        maxSpeed = 10,
        minDistance = 90,
        maxDistance = 100
    };
    public static FlyWeight kamikazeEnemyStats = new FlyWeight()
    {
        maxLife = 3,
        maxSpeed = 45,
        minDistance = 15,
        maxDistance = 20
    };
}
