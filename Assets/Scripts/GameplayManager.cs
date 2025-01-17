﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour, IPublisher
{
    [SerializeField] DropSystem dropSyst;
    [SerializeField] Life playerLife;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerView playerView;
    [SerializeField] Barrier barrier;

    List<ISubscriber> _subscriber = new List<ISubscriber>();
    public float killStreak = 0;
    public float survivedTime = 0;
    public static GameplayManager Instance { get; private set; }

    EnumElement element;

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
        element = GetComponent<EnumElement>();
        EventManager.Instance.Subscribe("OnEnemyKilled", KillStreak);
        EventManager.Instance.Subscribe("OnRevive", Revive);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUp_BlackHole();
        }
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
    #region PickUps
    public void PickUp_Health()
    {
        playerLife.HealLife();
    }
    public void PickUp_Shield()
    {
        playerLife.GetShield();
    }
    public void PickUp_Speed()
    {
        playerController.SpeedPowerUp(1.5f, 5);
    }
    public void PickUp_BlackHole()
    {
        barrier.ChangeElement(element);
        playerView.ChangeElement(element);
    }
    #endregion
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
