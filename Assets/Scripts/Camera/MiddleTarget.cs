using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleTarget : MonoBehaviour
{
    [SerializeField]
    private List<Transform> playerPos = new List<Transform>();
    private Vector3 direction;

    [SerializeField]
    private Transform target;

    void Update()
    {
        direction = (playerPos[0].position + playerPos[1].position) / 2;
        transform.position = direction;

        transform.LookAt(target);
    }
}
