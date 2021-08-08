using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBulletsController : MonoBehaviour
{
    /// <summary>
    /// ���ˊԊu
    /// </summary>
    public float ShotSpan { get; set; } = 0.1f;

    /// <summary>
    /// ���˃^�C�}�[
    /// </summary>
    private float _shotTime;

    /// <summary>
    /// ���˃^�C�}�[
    /// </summary>
    private float _shotResetTime;

    /// <summary>
    /// �V���b�g��
    /// </summary>
    private int _shotCount;

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

        if ((_shotResetTime += Time.deltaTime) > 2f)
        {
            _shotResetTime = 0;
            _shotCount = 0;
        }
    }

    public void Shot(Vector3 start)
    {
        if (_shotCount > 5) return;

        if (_shotTime <= 0f)
        {
            ++_shotCount;

            Instantiate(_prefab, start, Quaternion.identity);
            _shotTime = ShotSpan;
        }
    }
}
