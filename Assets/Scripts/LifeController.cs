using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private GameObject _prefab;

    /// <summary>
    /// 
    /// </summary>
    private List<GameObject> _symbols;

    /// <summary>
    /// 最大ライフ
    /// </summary>
    private static readonly int MaxLife = 5;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _prefab = (GameObject)Resources.Load("Prefabs/Life");
        _symbols = new List<GameObject>();

        for (int index = 0; index < MaxLife; ++index)
        {
            var symbol = Instantiate(_prefab, 
                new Vector3(transform.position.x + (10 * index), transform.position.y, transform.position.z), 
                Quaternion.identity);
            _symbols.Add(symbol);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {

    }
}
