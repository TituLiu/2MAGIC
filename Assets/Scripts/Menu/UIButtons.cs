using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public AdsGame.AdsType nameAds;
    /*private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }*/
    public void MainMenuScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
    public void StartAds()
    {
        AdsGame.instance.Active(nameAds, Revive, Pause);
    }
    public void Pause()
    {
        EventManager.Instance.Trigger("OnPause");
        Debug.Log("pause");
    }
    public void UnPause()
    {
        EventManager.Instance.Trigger("OnUnPause");
        Debug.Log("unpause");
    }
    public void Revive()
    {
        EventManager.Instance.Trigger("OnRevive", true);
    }
    public void PlayScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void HowToPlayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void CreditsScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void LoadingScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
