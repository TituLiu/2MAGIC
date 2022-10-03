using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour, IDrop
{
    public enum Type
    {
        Health,
        Shield,
        Speed,
        BlackHoleAttack
    }
    public Type type;
    public void GetPowerUp()
    {
        switch (type)
        {
            case Type.Health:
                GameplayManager.Instance.PickUp_Health();      
                break;
            case Type.Shield:
                GameplayManager.Instance.PickUp_Shield();
                break;
            case Type.Speed:
                GameplayManager.Instance.PickUp_Speed();
                break;
            case Type.BlackHoleAttack:
                GameplayManager.Instance.PickUp_BlackHole();
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}
