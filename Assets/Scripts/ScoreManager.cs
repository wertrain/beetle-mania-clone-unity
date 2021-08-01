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
    void Start()
    {
        _prefab = (GameObject)Resources.Load("Prefabs/Score");
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
    }
}
