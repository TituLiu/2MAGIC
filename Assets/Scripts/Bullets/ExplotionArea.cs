using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.gameObject.GetComponent<IDamagable>();
        if (damageable != null)
        {
            damageable.Damage(10);
            //gameObject.SetActive(false);
        }
    }
}
