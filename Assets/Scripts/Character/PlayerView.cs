using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    MeshRenderer mr;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    public void ChangeColorGreen()
    {
        mr.material.color = Color.green;
    }    
    public void ChangeColorYellow()
    {
        mr.material.color = Color.yellow;
    }    
    public void ChangeColorRed()
    {
        mr.material.color = Color.red;
    }
}
