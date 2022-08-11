using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private ObjectFactory particleFactory = new ObjectFactory();
    public GameObject[] particles;
    public void Start()
    {
        EventManager.Instance.Subscribe("OnExplotionParticle", Reposition);
    }
    public void Reposition(params object[] parameters)
    {
        var reposition = (Vector3)parameters[0];
        var typeOfParticle = (int)parameters[1];
        particles[typeOfParticle].transform.position = reposition;
        particleFactory.Create(particles[typeOfParticle]);
    }

}
