using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public Button showAdButton;
    string androidAdId = "Rewarded_Android";
    private void Start()
    {
        showAdButton.interactable = true;  
    }
    public void LoadAd()
    {
        Advertisement.Load(androidAdId, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == androidAdId)
        {
            showAdButton.onClick.AddListener(ShowAd);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    public void ShowAd()
    {
        Advertisement.Show(androidAdId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("ERROR");
    }
    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //recompensa
        showAdButton.interactable = false;
    }
}
