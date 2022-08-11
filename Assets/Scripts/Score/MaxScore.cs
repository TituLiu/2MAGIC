using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaxScore : MonoBehaviour
{
    int maxScore;
    public TextMeshProUGUI maxScoreText;
    void Start()
    {
        SaveScore.instance.Save();
        SaveScore.instance.OnLoadLevelData += LoadData;
        SaveScore.instance.Load();
    }
    private void LoadData(Data data)
    {
        if (data.score == 0)
        {
            maxScore = 0;
        }
        maxScore = data.score;
        maxScoreText.text = "Max Score: " + maxScore.ToString("F0");
    }
}
