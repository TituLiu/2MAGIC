using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Element currentElement;
    [SerializeField] GameObject[] playerModel;
    MeshRenderer[] mr;
    private void Start()
    {       
        for (int i = 0; i < playerModel.Length; i++)
        {
            mr[i] = playerModel[i].GetComponent<MeshRenderer>(); 
        }
    }
    public void ChangeElement(Element element)
    {
        currentElement = element;
        switch (currentElement)
        {
            case Element.Fire:
                foreach (var player in mr)
                {
                    player.material.color = Color.red;
                    player.material.SetColor("_EmissionColor", Color.red);
                }                
                break;
            case Element.Water:
                foreach (var player in mr)
                {
                    player.material.color = Color.blue;
                    player.material.SetColor("_EmissionColor", Color.blue);
                }
                break;
            case Element.Ice:
                foreach (var player in mr)
                {
                    player.material.color = Color.cyan;
                    player.material.SetColor("_EmissionColor", Color.cyan);
                }
                break;
            default:
                break;
        }
    }
}
