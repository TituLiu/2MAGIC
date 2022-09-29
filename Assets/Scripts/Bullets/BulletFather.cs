using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {Fire, Water, Ice};
public class BulletFather : MonoBehaviour, IPublisher
{
    public MeshRenderer mr;
    public Rigidbody rb;
    public GameObject meshObject;
    public delegate void ChangeTypeBullet();
    public ChangeTypeBullet _MyDelegate;

    [Header("Stats")]
    public float speedBullet = 2;
    public float searchTargetRadius = 2;
    public float activateRadius = 2;
    public int timer = 10;
    public float counter = 10;
    
    GameObject targetObj;
    [SerializeField] protected LayerMask enemyLayerMask;
    [SerializeField] protected LayerMask _bounceMask;

    public Element bulletElement;
    private List<ISubscriber> _subscribers = new List<ISubscriber>();

    private void Awake()
    {
        counter = timer;
    }
    protected virtual void Start()
    {
        EventManager.Instance.Subscribe("OnRevive", Reset);
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void Reset(params object[] parameters)
    {
        _MyDelegate = SimpleMovement;
        gameObject.SetActive(false);
    }
    #region Functions
    protected void SimpleMovement()
    {
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        rb.velocity = transform.forward * speedBullet;
    }
    protected void FollorTargetMovement()
    {
        Vector3 dir = targetObj.transform.position - transform.position;
        dir.y = 0;
        dir.Normalize();
        transform.forward = dir;
        rb.velocity = dir * speedBullet;
    }
    #region BulletFunctions
    protected void SearchTarget()
    {
        Collider[] searchTarget = Physics.OverlapSphere(transform.position, searchTargetRadius, enemyLayerMask);
        if (searchTarget.Length != 0)
        {
            targetObj = searchTarget[0].gameObject;
            _MyDelegate -= SearchTarget;
            _MyDelegate += FollorTargetMovement;
        }
    }
    protected void WaitForActivate()
    {
        Collider[] activate = Physics.OverlapSphere(transform.position, activateRadius, enemyLayerMask);
        if (activate.Length != 0)
        {
            BulletElementalAttack();
        }
    }
    protected virtual void BulletElementalAttack(){ }
    #endregion 

    #endregion
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bounce") //Cambiar esto
        {
            transform.forward = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
        }
        var damageable = collision.gameObject.GetComponent<IDamagable>();
        if (damageable != null)
        {
            //damageable.Damage(1, Element.Fire);
            gameObject.SetActive(false);
        }
        var reflectable = collision.gameObject.GetComponent<IAffect>();
        if (reflectable != null)
        {
            var direction = Vector3.Reflect(transform.forward, collision.GetContact(0).normal);
            reflectable.Touch(direction, transform.position, 0);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "MapLimit")
        {
            gameObject.SetActive(false);
            Reset();
        }
        //else StartCoroutine(ColliderOff());
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
    #endregion Observer
}
