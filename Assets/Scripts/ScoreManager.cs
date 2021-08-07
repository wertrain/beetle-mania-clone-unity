using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private GameObject _prefab;

    /// <summary>
    /// 
    /// </summary>
    private UIController _controller;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _prefab = (GameObject)Resources.Load("Prefabs/Score");
        _controller = GameObject.Find("UIController").GetComponent<UIController>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public void Scored(int index, Vector3 position)
    {
        var score = Instantiate(_prefab);
        score.GetComponent<Score>().Popup(index, position);
        _controller.AddScore((ulong)Mathf.Pow(2, index + 1));
    }
}
