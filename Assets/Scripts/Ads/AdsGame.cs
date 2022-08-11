using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsGame : MonoBehaviour
{
    //No llegamos a terminar los ads

    string id = "4590773";
    public static AdsGame instance;
    Action _CallFinish, _CallFailed;
    private void Awake()
    {
//#if UNITY_ANDROID
//        _id = "47";
//#endif
//#if UNITY_IOS
//        _id = "";
//#endif
        Advertisement.Initialize(id);
        instance = this;
    }

    public void Active(AdsType nameAds, Action methodFinish, Action methodFailed)
    {
        try
        {
            if (Advertisement.IsReady(nameAds.ToString()) && !Advertisement.isShowing)
            {
                ShowOptions options = new ShowOptions();
                options.resultCallback = HandleShowResult;
                _CallFailed = methodFailed;
                _CallFinish = methodFinish;
                Advertisement.Show(nameAds.ToString(), options);
            }
            else
                methodFailed();
        }
        catch
        {
            methodFailed();
        }
    }

    public void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
            _CallFinish?.Invoke();
        else
            _CallFailed?.Invoke();
    }

    public enum AdsType
    {
        Interstitial_Android,
        Interstitial_iOS,
        Rewarded_Android,
        Rewarded_iOS
    }
}

