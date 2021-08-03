using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBullet : BulletBase
{
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _maxLifeTime = 0.2f;
        _speed = 180f;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        UpdateBullet();
    }
}
