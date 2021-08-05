using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : ItemBase
{
    /// <summary>
    ///
    /// </summary>
    void Start()
    {
        _stepSpan = 0.12f;
        _speed = 10f;
        _maxLifeTime = 8f;
    }

    /// <summary>
    ///
    /// </summary>
    void Update()
    {
        UpdateStep();
        UpdateLifeTime();

        var position = transform.position;
        position += -Vector3.up * _speed * Time.deltaTime;
        transform.position = position;
    }
}
