using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISubscriber
{
    public Transform player;
    public PlayerModel model;
    public PlayerView view;
    public JoyStick myStick;
    public Transform barrier;
    private event Action action;
    [SerializeField]
    private int playerNumber;
    private void Awake()
    {
        //model.slowTimePower = new CommandTakeDamage();
    }
    void Start()
    {
        myStick.OnDragStick += Movement;
        myStick.OnEndDragStick += Movement;
        action = PlayerAxis;
        //switch (playerNumber)
        //{
        //    case 1:
        //        action = FirstPlayerControlUnity;
        //        break;
        //    case 2:
        //        action = SecondPlayerControlUnity;
        //        break;
        //}
        //action += CheckDistance;
    }
    void Update()
    {
        action();
    }
    void PlayerAxis()
    {   
        float x = Input.GetAxis(model.firstPlayerHorizontalAxis);
        float z = Input.GetAxis(model.firstPlayerVerticalAxis);

        Debug.Log(Input.acceleration.x);

        if (x != 0 || z != 0)
        {
            Movement(x, z);
        }
    }
    //public void FirstPlayerControlUnity()
    //{
    //    float x = Input.GetAxis(model.firstPlayerHorizontalAxis);
    //    float z = Input.GetAxis(model.firstPlayerVerticalAxis);
    //    if (x != 0 || z != 0)
    //    {
    //        Movement(x, z);
    //    }
    //}
    //public void SecondPlayerControlUnity()
    //{
    //    float x = Input.GetAxis(model.secondPlayerhorizontalAxis);
    //    float z = Input.GetAxis(model.secondPlayerverticalAxis);
    //    if (x != 0 || z != 0)
    //    {
    //        Movement(x, z);
    //    }
    //}
    //public void CheckDistance()
    //{
    //    model.distance = Vector3.Distance(player.position, transform.position);
    //    if (model.distance > 1 && model.distance <= model.distanceLimit)
    //    {
    //        view.ChangeColorRed();
    //    }
    //    else if (model.distance > model.distanceLimit && model.distance <= model.distanceLimit * 2)
    //    {
    //        view.ChangeColorYellow();
    //    }
    //    else if (model.distance > model.distanceLimit * 2)
    //    {
    //        view.ChangeColorGreen();
    //    }
    //}

    public void Movement(float x, float z)
    {
        //transform.rotation = Quaternion.LookRotation(barrier.right);
        Vector3 movedir = new Vector3(x, 0, 0);
        Vector3 newDir = Vector3.ClampMagnitude(movedir, 1);
        transform.position += newDir * model.movementSpeed * Time.deltaTime;
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
