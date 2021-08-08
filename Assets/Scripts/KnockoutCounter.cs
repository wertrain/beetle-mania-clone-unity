using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockoutCounter : MonoBehaviour
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
    /// 画像インデックス
    /// </summary>
    protected int _stepSpriteIndex;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if ((_stepTime += Time.deltaTime) > 1f)
        {
            if (++_stepSpriteIndex >= Sprites.Count)
            {
                Destroy(gameObject);
                return;
            }
            _stepTime = 0;
        }

        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[_stepSpriteIndex];
    }
}
