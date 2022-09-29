using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public enum PlayerElement
    {
        Fire,
        Water,
        Ice
    }
    public PlayerElement currentElement;
    [SerializeField] MeshRenderer[] playerMr;
    private void Start()
    {
        foreach (var player in playerMr)
        {
            player.material.color = Color.red;
            player.material.SetColor("_EmissionColor", Color.red);
        }
    }
    public void ChangeElement(EnumElement element)
    {
        currentElement = element.PlayerElement;
        switch (currentElement)
        {
            case PlayerElement.Fire:
                foreach (var player in playerMr)
                {
                    player.material.color = Color.red;
                    player.material.SetColor("_EmissionColor", Color.red);
                }
                break;
            case PlayerElement.Water:
                foreach (var player in playerMr)
                {
                    player.material.color = Color.blue;
                    player.material.SetColor("_EmissionColor", Color.blue);
                }
                break;
            case PlayerElement.Ice:
                foreach (var player in playerMr)
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
