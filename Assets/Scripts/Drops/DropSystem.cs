using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{
    [SerializeField] int total;
    [SerializeField] int[] table;
    [SerializeField] GameObject[] drop;
    private void Start()
    {
        EventManager.Instance.Subscribe("OnDropPickUp", Drop);
        foreach (var item in table)
        {
            total += item;
        }
    }
    public void Drop(params object[] parameters)
    {
        int randomNumber = Random.Range(0, total);
        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                Vector3 pos = (Vector3)parameters[0];
                var pickUp = Instantiate(drop[i]);
                pickUp.transform.position = pos;
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
    }
}
