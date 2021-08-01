using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// �L�����摜�X�v���C�g���X�g
    /// </summary>
    public List<Sprite> Sprites = new List<Sprite>();

    /// <summary>
    /// �X�e�[�g�}�V��
    /// </summary>
    private IceMilkTea.Core.ImtStateMachine<Player> _stateMachine;

    /// <summary>
    /// �^�C�}�[
    /// </summary>
    private float _stepTime;

    /// <summary>
    /// �摜�C���f�b�N�X
    /// </summary>
    private int _stepSpriteIndex;

    /// <summary>
    /// 
    /// </summary>
    private float _speed;

    /// <summary>
    /// 
    /// </summary>
    private enum StateEventId : int
    {
        Idle,
        Damage,
        Max
    }

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new IceMilkTea.Core.ImtStateMachine<Player>(this);
        _stateMachine.AddAnyTransition<IdleState>((int)StateEventId.Idle);
        _stateMachine.AddAnyTransition<DamageState>((int)StateEventId.Damage);
        _stateMachine.SetStartState<IdleState>();

        _speed = 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_stepTime += Time.deltaTime) > 0.075f)
        {
            if (++_stepSpriteIndex >= 2)
                _stepSpriteIndex = 0;
            _stepTime = 0;
        }

        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[_stepSpriteIndex];

        _stateMachine.Update();
    }

    /// <summary>
    /// Idle �X�e�[�g
    /// </summary>
    private class IdleState : IceMilkTea.Core.ImtStateMachine<Player>.State
    {
        protected internal override void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Context.transform.Translate(-Context._speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Context.transform.Translate(Context._speed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                Context.GetComponent<BeetleBulletsController>().Shot(Context.transform.position);
            }
        }
    }

    /// <summary>
    /// Move �X�e�[�g
    /// </summary>
    private class DamageState : IceMilkTea.Core.ImtStateMachine<Player>.State
    {
        protected internal override void Update()
        {
        }
    }
        }
