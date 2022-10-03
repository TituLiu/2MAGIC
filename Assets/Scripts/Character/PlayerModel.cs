using System;
using UnityEngine;
public class PlayerModel : MonoBehaviour
{
    public bool slowTime = false;
    public int playerNumber;
    public float movementSpeed = 15;
    public float baseMovementSpeed = 15;
    public float distance;
    public float distanceLimit = 10;
    public string firstPlayerHorizontalAxis = "1PHorizontal";
    public string firstPlayerVerticalAxis = "1PVertical";
    public string secondPlayerhorizontalAxis = "2PHorizontal";
    public string secondPlayerverticalAxis = "2PVertical";
}
