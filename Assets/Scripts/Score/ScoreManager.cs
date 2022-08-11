using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    private void Start()
    {
        finalScoreText.enabled = false;
        EventManager.Instance.Subscribe("OnLevelEnd", OnLevelEnd);
        EventManager.Instance.Subscribe("OnEnemyDeath", OnEnemyDeath);
    }
    private void OnEnemyDeath(params object[] parameters)
    {
        var points = (int)parameters[0];
        score += points;
        ScoreCount();
    }
    private void ScoreCount()
    {
        scoreText.text = score.ToString("F0");
    }
    private void OnLevelEnd(params object[] parameters)
    {
        finalScoreText.text = "Your Score: " + score.ToString("F0");
        finalScoreText.enabled = true;
        //SaveScore.instance.levelData.score = score;
        //SaveScore.instance.Save();
    }
}
