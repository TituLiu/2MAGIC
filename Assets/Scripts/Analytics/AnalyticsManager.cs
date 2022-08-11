using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager
{
    static Dictionary<string, object> analyticDictionary = new Dictionary<string, object>();

    public static void SendLevelEnd(string levelName, float survivedTime)
    {
        if (analyticDictionary.ContainsKey("level_name"))
            analyticDictionary["level_name"] = levelName;
        else
            analyticDictionary.Add("level_name", levelName);
        
        if (analyticDictionary.ContainsKey("player_level"))
            analyticDictionary["survivedTime"] = survivedTime;
        else
            analyticDictionary.Add("survivedTime", survivedTime);

        Analytics.SendEvent("LevelStarted", analyticDictionary);
    }
}
