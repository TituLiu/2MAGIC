using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float pullForce;
    Vector3 direction;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            direction = transform.position - other.transform.position;
            other.transform.position += direction.normalized * pullForce * Time.deltaTime;
        }
    }
}
