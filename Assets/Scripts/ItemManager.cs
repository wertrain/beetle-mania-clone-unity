using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public float ItemSpawnSpan { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    private GameObject _prefab;

    /// <summary>
    /// 
    /// </summary>
    private float _spawnTime;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        ItemSpawnSpan = 30f;
        _prefab = (GameObject)Resources.Load("Prefabs/Heart");
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if ((_spawnTime += Time.deltaTime) > ItemSpawnSpan)
        {
            var item = Instantiate(_prefab, new Vector3(Random.Range(-95f, 95f), 100f, 0f), Quaternion.identity);
            _spawnTime = 0;
        }
    }
}
