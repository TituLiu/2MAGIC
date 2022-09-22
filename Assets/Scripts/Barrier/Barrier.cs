using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, ISubscriber, IAffect, IBarrier
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
    [SerializeField]
    List<Transform> playerPos = new List<Transform>();
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
        //_myDelegate += CheckDistance; 
        //_myDelegate += Movement;
        //_myDelegate += Resize; 
        //_myDelegate += ChangeColor; 
        mr = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        //_myDelegate();
    }
    //private void CheckDistance()
    //{
    //    distance = Vector3.Distance(playerPos[0].position, playerPos[1].position);
    //}
    //private void Resize()
    //{
    //    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance);
    //    if (distance > distanceLimit*3) foreach (var subscriber in _subscribers) subscriber.OnNotify("OnLimitDistance");
    //}
    //private void Movement()
    //{
    //    transform.position = (playerPos[0].position + playerPos[1].position) / 2;
    //    var dir = playerPos[0].position - playerPos[1].position;
    //    transform.forward = dir;
    //}
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
        //if (distance > 1 && distance <= distanceLimit)
        //{
        //    mr.material.color = Color.red;
        //    barrierIntensity = 3;
        //}
        //else if (distance > distanceLimit && distance <= distanceLimit * 2)
        //{
        //    mr.material.color = Color.yellow;
        //    barrierIntensity = 2;
        //}
        //else if (distance > distanceLimit * 2)
        //{
        //    mr.material.color = Color.green;
        //    barrierIntensity = 1;
        //}
    }
    private void PowerUp()
    {
        //_myDelegate -= ChangeColor;
        mr.material.color = Color.red;
        barrierIntensity = 3;
        StartCoroutine(PowerUpTime());
    }
    IEnumerator PowerUpTime()
    {
        yield return new WaitForSeconds(5);
        //_myDelegate += ChangeColor;
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
        //switch (barrierIntensity)
        //{
        //    case 1:
        //        var homingBullet = hommingBulletPool.Get();
        //        homingBullet.transform.position = bulletPos;
        //        homingBullet.transform.forward = dirBarrier;
        //        break;
        //    case 2:
        //        var explosiveBullet = explosiveBulletPool.Get();
        //        explosiveBullet.transform.position = bulletPos;
        //        explosiveBullet.transform.forward = dirBarrier;
        //        break;
        //    case 3:
        //        var absorverBullet = absorverBulletPool.Get();
        //        absorverBullet.transform.position = bulletPos;
        //        absorverBullet.transform.forward = dirBarrier;
        //        break;
        //}
    }
    public int Intensity()
    {
        int intensity;
        intensity = barrierIntensity;
        return intensity;
    }
    #region Observer
    public void OnNotify(string eventId)
    {
        if (eventId == "OnKillStreak5")
        {
            PowerUp();
        }
    }
    #endregion
}
