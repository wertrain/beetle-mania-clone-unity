using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    /// <summary>
    /// キャラ画像スプライトリスト
    /// </summary>
    public List<Sprite> Sprites = new List<Sprite>();

    /// <summary>
    /// コンボ数
    /// </summary>
    public int ComboCount { get; set; }

    /// <summary>
    /// 方向
    /// </summary>
    protected Vector3 _velocity;

    /// <summary>
    /// 最大生存時間
    /// </summary>
    protected float _maxLifeTime;

    /// <summary>
    /// 生存時間
    /// </summary>
    protected float _lifeTime;

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
    protected float _speed;

    /// <summary>
    /// 
    /// </summary>
    protected void UpdateBullet()
    {
        if ((_stepTime += Time.deltaTime) > 0.075f)
        {
            if (++_stepSpriteIndex >= Sprites.Count)
                _stepSpriteIndex = 0;
            _stepTime = 0;
        }

        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[_stepSpriteIndex];

        if ((_lifeTime += Time.deltaTime) > _maxLifeTime)
        {
            _lifeTime = 0;
            Destroy(gameObject);
        }

        var position = transform.position;
        position += _velocity * _speed * Time.deltaTime;
        transform.position = position;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var shell = collision.gameObject.GetComponent<Shell>();
        if (shell)
        {
            shell.Explode(ComboCount);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
