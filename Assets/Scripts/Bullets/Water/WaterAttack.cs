using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            var enemy = other.gameObject.GetComponents<IDamagable>();
            if (enemy.Length == 0) return;
            foreach (var item in enemy)
            {
                item.Damage(1);
            }
        }
    }
}
