using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public ulong Score { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public static ulong HighScore { get; private set; } = 5000;

    /// <summary>
    /// 
    /// </summary>
    private static readonly ulong MaxScore = 99999999999999;

    /// <summary>
    /// 
    /// </summary>
    private GameObject _scoreObject;

    /// <summary>
    /// 
    /// </summary>
    private GameObject _highScoreObject;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _scoreObject = GameObject.Find("Score");
        _highScoreObject = GameObject.Find("Hi-Score");
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        _scoreObject.GetComponent<Text>().text = $"{Score:00}";
        _highScoreObject.GetComponent<Text>().text = $"{HighScore:00}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(ulong score)
    {
        Score += score;

        if (Score > MaxScore)
            Score = MaxScore;

        if (Score > HighScore)
            HighScore = Score;
    }
}
