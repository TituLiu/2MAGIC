using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInteract : MonoBehaviour
{
    [SerializeField] LayerMask dropMask;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, dropMask))
            {
                var pickUp = hit.collider.GetComponent<IDrop>();
                if (pickUp == null) return;
                pickUp.GetPowerUp();          
            }
        }
    }
}
