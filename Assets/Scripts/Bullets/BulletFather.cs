using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {Fire, Water, Ice};
public class BulletFather : MonoBehaviour, IPublisher
{
    public Element bulletElement;
    private List<ISubscriber> _subscribers = new List<ISubscriber>();
    public int bulletIntensity, intensity, randomNumber;
    public float speedBullet = 2;
    public int timer = 10;
    public float counter = 10;
    public bool particlesOn = false;
    public MeshRenderer mr;
    public Rigidbody rb;
    public delegate void ChangeTypeBullet();
    public ChangeTypeBullet _MyDelegate;

    public GameObject meshObject;
    private void Awake()
    {
        counter = timer;
    }
    private void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Reset);
    }
    public virtual void Reset(params object[] parameters)
    {
        _MyDelegate = Movement;
        randomNumber = Random.Range(1, 11);
    }
    #region Functions
    public void Movement()
    {
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        transform.position += transform.forward * speedBullet * Time.deltaTime;
    }
    #region BulletFunctions
    public void NormalBullet()
    {
        mr.material.color = Color.green;
    }
    public IEnumerator TimeUntilDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    //private void ChangeBulletType(params object[] parameters) //Preguntar si hay una forma mejor
    //{
    //    float type = (int)parameters[0];
    //    changeBullet = (TypeOfBullet)(int)type;
    //    switch (changeBullet)
    //    {
    //        case TypeOfBullet.Normal:
    //            _MyDelegate = NormalBullet;
    //            _MyDelegate += Movement;
    //            counter = timer;
    //            _MyDelegate += Destroy;
    //            break;
    //        case TypeOfBullet.Explosive:
    //            _MyDelegate = ExplosiveBullet;
    //            _MyDelegate += Movement;
    //            break;
    //        case TypeOfBullet.Absorver:
    //            _MyDelegate = AbsorverBullet;
    //            _MyDelegate += Movement;
    //            break;
    //    }
    //}
    #endregion 

    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamagable>();
        if (damageable != null)
        {
            damageable.Damage(1);
            gameObject.SetActive(false);
        }
        var reflectable = collision.gameObject.GetComponent<IAffect>();
        var barrierIntensity = collision.gameObject.GetComponent<IBarrier>();
        if (reflectable != null )
        {
            if (barrierIntensity.Intensity() >= bulletIntensity)
            {
                var direction = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
                reflectable.Touch(direction, transform.position, bulletIntensity);
                gameObject.SetActive(false);
            }
            else StartCoroutine(ColliderOff());
        }
    }
    IEnumerator ColliderOff()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(.5f);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
    #region
    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }
    public void Unsubscribe(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }
    #endregion
}
