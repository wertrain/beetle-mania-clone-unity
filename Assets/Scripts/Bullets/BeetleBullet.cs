using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBullet : BulletBase
{
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _velocity = new Vector3(0f, 1f);
        _maxLifeTime = 3f;
        _speed = 160f;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        UpdateBullet();
    }
}
