using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : ItemBase
{
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _stepSpan = 0.25f;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        UpdateStep();
    }
}
