using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject endMenu;
    private void Start()
    {
        EventManager.Instance.Subscribe("OnPause", Pause);
        EventManager.Instance.Subscribe("OnUnPause", UnPause);
        EventManager.Instance.Subscribe("OnLevelEnd", LevelEnd);
        EventManager.Instance.Subscribe("OnRevive", Revive);
    }
    private void Pause(params object[] parameters)
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    private void UnPause(params object[] parameters)
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        endMenu.SetActive(false);
    }
    private void Revive(params object[] parameters)
    {
        Time.timeScale = 1f;
        endMenu.SetActive(false);
    }
    private void LevelEnd(params object[] parameters)
    {
        Time.timeScale = 0f;
        endMenu.SetActive(true);
        //AnalyticsManager.SendLevelEnd("level1", GameplayManager.Instance.survivedTime);
    }
}
