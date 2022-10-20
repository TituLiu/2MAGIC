using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, ISubscriber, IAffect
{
    public enum BarrierElement
    {
        Fire,
        Water,
        Ice
    }
    public BarrierElement currentElement;

    MeshRenderer mr;
    [SerializeField]
    int distanceLimit;
    [SerializeField] Transform barrierPos;
    private List<ISubscriber> _subscribers = new List<ISubscriber>();
    public IPublisher powerUp;
    private delegate void Delegate();
    Delegate _myDelegate;
    public int barrierIntensity;
    private float distance;
    public GameObject[] bullets;
    public Vector3 reflect;

    public Pool<WaterBullet> waterBulletPool;
    public WaterBulletSpawner waterSpawner;

    public Pool<FireBullet> fireBulletPool;
    public FireBulletSpawner fireBulletSpawner;

    public Pool<IceBullet> IceBulletPool;
    public IceBulletSpawner iceBulletSpawner;

    private void Start()
    {
        waterBulletPool = new Pool<WaterBullet>(waterSpawner.Create, waterSpawner.TurnOffObject, waterSpawner.TurnOnObject, 2);
        fireBulletPool = new Pool<FireBullet>(fireBulletSpawner.Create, fireBulletSpawner.TurnOffObject, fireBulletSpawner.TurnOnObject, 2);
        IceBulletPool = new Pool<IceBullet>(iceBulletSpawner.Create, iceBulletSpawner.TurnOffObject, iceBulletSpawner.TurnOnObject, 2);

        powerUp = GameplayManager.Instance.GetComponent<IPublisher>();
        powerUp.Subscribe(this);
        mr = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        transform.position = barrierPos.position;
    }
    public void ChangeElement(EnumElement element)
    {
        currentElement = element.BarrierElement;
        switch (currentElement)
        {
            case BarrierElement.Fire:               
                mr.material.color = Color.red;
                mr.material.SetColor("_EmissionColor", Color.red);
                break;
            case BarrierElement.Water:
                mr.material.color = Color.blue;
                mr.material.SetColor("_EmissionColor", Color.blue);
                break;
            case BarrierElement.Ice:
                mr.material.color = Color.cyan;
                mr.material.SetColor("_EmissionColor", Color.cyan);
                break;
            default:
                break;
        }
    }
    public void Touch(Vector3 dirBarrier, Vector3 bulletPos, int intensity)
    {
        switch (currentElement)
        {
            case BarrierElement.Fire:
                var fireBullet = fireBulletPool.Get();
                fireBullet.transform.position = bulletPos;
                fireBullet.transform.forward = dirBarrier;
                break;
            case BarrierElement.Water:
                var waterBullet = waterBulletPool.Get();
                waterBullet.transform.position = bulletPos;
                waterBullet.transform.forward = dirBarrier;
                break;
            case BarrierElement.Ice:
                var iceBullet = IceBulletPool.Get();
                iceBullet.transform.position = bulletPos;
                iceBullet.transform.forward = dirBarrier;
                break;
            default:
                break;
        }
    }
    #region Observer
    public void OnNotify(string eventId)
    {

    }
    #endregion
}
