using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public TextMeshProUGUI progressText;

    private void Start()
    {
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameplayScene");
        //var WatiForEndOfFrame = new WaitForEndOfFrame();
        loadOperation.allowSceneActivation = false;

        while (loadOperation.progress < 0.9f)
        {
            progressText.text = ((loadOperation.progress / 0.9f) * 100) + "%";
            yield return new WaitForEndOfFrame();
        }
        progressText.text = "Presiona ESPACIO para jugar";
        while (!Input.GetKey(KeyCode.Mouse0))
        {
            yield return new WaitForEndOfFrame();
        }

        loadOperation.allowSceneActivation = true;
    }
}
