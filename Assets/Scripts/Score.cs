using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    /// <summary>
    /// スコア画像スプライトリスト
    /// </summary>
    public List<Sprite> Sprites = new List<Sprite>();

    /// <summary>
    /// 最大生存時間
    /// </summary>
    private float _maxLifeTime;

    /// <summary>
    /// 生存時間
    /// </summary>
    private float _lifeTime;

    /// <summary>
    /// 方向
    /// </summary>
    private Vector3 _velocity;

    /// <summary>
    /// 速度
    /// </summary>
    private float _speed;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _speed = 0.2f;
        _maxLifeTime = 0.7f;
        _velocity = new Vector3(0f, 1f, 0f);
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
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
    /// <param name="index"></param>
    /// <param name="position"></param>
    public void Popup(int index, Vector3 position)
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[Mathf.Min(index, Sprites.Count - 1)];

        transform.position = position;
    }
}
