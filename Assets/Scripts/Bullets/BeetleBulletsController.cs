using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBulletsController : MonoBehaviour
{
    /// <summary>
    /// 発射間隔
    /// </summary>
    public float ShotSpan { get; set; } = 0.1f;

    /// <summary>
    /// 発射タイマー
    /// </summary>
    private float _shotTime;

    /// <summary>
    /// 
    /// </summary>
    private GameObject _prefab;

    /// <summary>
    /// 
    /// </summary>
    private List<GameObject> _bullets;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        _prefab = (GameObject)Resources.Load("Prefabs/BeetleBullet");
        _bullets = new List<GameObject>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (_shotTime > 0f)
        {
            if ((_shotTime -= Time.deltaTime) < 0f)
            {
                _shotTime = 0f;
            }
        }
    }

    public void Shot(Vector3 start)
    {
        if (_shotTime <= 0f)
        {
            Instantiate(_prefab, start, Quaternion.identity);
            _shotTime = ShotSpan;
        }
    }
}
