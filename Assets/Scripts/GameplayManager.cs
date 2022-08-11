using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour, IPublisher
{
    List<ISubscriber> _subscriber = new List<ISubscriber>();
    public float killStreak = 0;
    public float survivedTime = 0;
    public Castle castle;
    public static GameplayManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(this);
            Debug.LogWarning($"Found duplicate of 'EventManager' on {gameObject.name}");
        }
    }
    void Start()
    {
        EventManager.Instance.Subscribe("OnEnemyKilled", KillStreak);
        EventManager.Instance.Subscribe("OnRevive", Revive);
    }
    private void Update()
    {
        survivedTime += 1 * Time.deltaTime;
    }
    private void KillStreak(params object[] parameters)
    {
        killStreak++;
        if (killStreak >= 5)
        {
            foreach (var subscriber in _subscriber)
            {
                subscriber.OnNotify("OnKillStreak5");
            }
        }
    }
    public void Revive(params object [] parameters)
    {
        Debug.Log("OnRevive");
    }
    public void Subscribe(ISubscriber subscriber)
    {
        _subscriber.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber subscriber)
    {
        _subscriber.Remove(subscriber);
    }
}
