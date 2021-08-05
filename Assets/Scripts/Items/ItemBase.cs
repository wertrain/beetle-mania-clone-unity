using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    /// <summary>
    /// �L�����摜�X�v���C�g���X�g
    /// </summary>
    public List<Sprite> Sprites = new List<Sprite>();

    /// <summary>
    /// �^�C�}�[
    /// </summary>
    protected float _stepTime;

    /// <summary>
    /// �^�C�}�[
    /// </summary>
    protected float _stepSpan;

    /// <summary>
    /// �摜�C���f�b�N�X
    /// </summary>
    protected int _stepSpriteIndex;

    /// <summary>
    /// 
    /// </summary>
    protected float _speed;

    /// <summary>
    /// �ő吶������
    /// </summary>
    protected float _maxLifeTime;

    /// <summary>
    /// ��������
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
