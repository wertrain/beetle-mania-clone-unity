using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFactory : MonoBehaviour
{
    /// <summary>
    /// 最大の生成甲羅数
    /// </summary>
    public int MaxShellCount { get; set; }

    /// <summary>
    /// 甲羅の生成スパン
    /// </summary>
    public float ShellSpawnSpan { get; set; }

    /// <summary>
    /// タイマー
    /// </summary>
    private float _spawnTime;

    /// <summary>
    /// 
    /// </summary>
    private GameObject _prefab;

    /// <summary>
    /// 
    /// </summary>
    private List<GameObject> _shells;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        MaxShellCount = 40;
        ShellSpawnSpan = 0.25f;

        _prefab = (GameObject)Resources.Load("Prefabs/Shell");
        _shells = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shells.Count < MaxShellCount)
        {
            if ((_spawnTime += Time.deltaTime) > ShellSpawnSpan)
            {
                var shell = Instantiate(_prefab, new Vector3(Random.Range(-95f, 95f), 95f, 0f), Quaternion.identity);
                //shell.GetComponent<Shell>().VelocityX = 50.0f * (Random.Range(0.0f, 1.0f) > 0.5f ? -1.0f : 1.0f);
                shell.GetComponent<Rigidbody2D>().velocity = new Vector2(50f * (Random.Range(0f, 1f) > 0.5f ? -1f : 1f), 0);
                 _shells.Add(shell);
                _spawnTime = 0;
            }
        }

        for (int index = _shells.Count - 1; index >= 0; --index)
        {
            var shell = _shells[index];
            if (!shell) _shells.Remove(shell);
        }
    }
}
