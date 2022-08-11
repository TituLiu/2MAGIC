using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damagable<T>
{
    void GetHit(T damageTaken);
}
