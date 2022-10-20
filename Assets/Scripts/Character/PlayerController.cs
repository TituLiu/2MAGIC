using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISubscriber
{
    [SerializeField] int mapLimitMask;
    [SerializeField] int[] mapLimit;
    Rigidbody rb;
    public Transform player;
    public PlayerModel model;
    public PlayerView view;
    public JoyStick myStick;
    public Transform barrier;
    private event Action action;
    private float powerUpTime;
    private float powerUpDuration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        model.movementSpeed = model.baseMovementSpeed;
        myStick.OnDragStick += Movement;
        myStick.OnEndDragStick += Movement;
        action = PlayerAxis;
    }
    void Update()
    {
        action();
    }
    void PlayerAxis()
    {   
        float x = Input.GetAxis(model.firstPlayerHorizontalAxis);
        float z = Input.GetAxis(model.firstPlayerVerticalAxis);

        if (x != 0 || z != 0)
        {
            Movement(x, z);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public void Movement(float x, float z)
    {
        //Vector3 movedir = new Vector3(x * model.movementSpeed, 0, 0);
        float movedir = x * model.movementSpeed;
        float newMovedir = Mathf.Clamp(movedir, -model.maxMovementSpeed, model.maxMovementSpeed);
        //transform.position += movedir * model.movementSpeed * Time.deltaTime;
        rb.velocity = new Vector3(newMovedir, 0 ,0);
    }
    public void SpeedPowerUp(float speed, float duration)
    {
        model.movementSpeed *= speed;
        powerUpTime = duration;
        if (powerUpTime == powerUpDuration)
        {
            action += SpeedDesaleration;
        }       
    }
    public void SpeedDesaleration()
    {
        powerUpDuration -= Time.deltaTime * 1;
        if (powerUpTime <= powerUpDuration)
        {
            model.movementSpeed = model.baseMovementSpeed;
            action -= SpeedDesaleration;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == mapLimitMask)
        {
            //model.movementSpeed = 0;

        }
    }
    public void OnNotify(string eventId)
    {
        if (eventId == "OnLimitDistance") 
        {
            model.movementSpeed *= -1;
            StartCoroutine(CD());
        }        
    }
    IEnumerator CD()
    {
        yield return new WaitForSeconds(0.2f);
        model.movementSpeed *= -1;
    }
}
