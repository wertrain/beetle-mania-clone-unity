using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    /// <summary>
    /// キャラ画像スプライトリスト
    /// </summary>
    public List<Sprite> Sprites = new List<Sprite>();

    /// <summary>
    /// タイマー
    /// </summary>
    protected float _stepTime;

    /// <summary>
    /// タイマー
    /// </summary>
    protected float _stepSpan;

    /// <summary>
    /// 画像インデックス
    /// </summary>
    protected int _stepSpriteIndex;

    /// <summary>
    /// 
    /// </summary>
    protected float _speed;

    /// <summary>
    /// 最大生存時間
    /// </summary>
    protected float _maxLifeTime;

    /// <summary>
    /// 生存時間
    /// </summary>
    protected float _lifeTime;

    /// <summary>
    /// 
    /// </summary>
    protected void UpdateStep()
    {
        if ((_stepTime += Time.deltaTime) > _stepSpan)
        {
            if (++_stepSpriteIndex >= Sprites.Count)
                _stepSpriteIndex = 0;
            _stepTime = 0;
        }

        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[_stepSpriteIndex];
    }

    /// <summary>
    /// 
    /// </summary>
    protected void UpdateLifeTime()
    {
        if ((_lifeTime += Time.deltaTime) > _maxLifeTime)
        {
            _lifeTime = 0;
            Destroy(gameObject);
        }
    }
}
