using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeSpawner : MonoBehaviour
{
    KamikazeFactory _kamikazeFactory = new KamikazeFactory();
    public GameObject kamikazeEnemy;

    public Kamikaze TurnOnObject(Kamikaze b)
    {
        b.gameObject.SetActive(true);
        return b;
    }
    public Kamikaze TurnOffObject(Kamikaze b)
    {
        b.gameObject.SetActive(false);
        return b;
    }
    public Kamikaze Create()
    {
        return _kamikazeFactory.Create(kamikazeEnemy);
    }
}
