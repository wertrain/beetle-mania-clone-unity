using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// キャラ画像スプライトリスト
    /// </summary>
    public List<Sprite> Sprites = new List<Sprite>();

    /// <summary>
    /// ステートマシン
    /// </summary>
    private IceMilkTea.Core.ImtStateMachine<Player> _stateMachine;

    /// <summary>
    /// タイマー
    /// </summary>
    private float _stepTime;

    /// <summary>
    /// 
    /// </summary>
    private float _damageTime;

    /// <summary>
    /// 
    /// </summary>
    private float _invincibleTime;

    /// <summary>
    /// 画像インデックス
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

        _speed = 90f;
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

        int baseIndex = 0;
        if (_stateMachine.Running && _stateMachine.IsCurrentState<DamageState>())
            baseIndex = 2;

        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[baseIndex + _stepSpriteIndex];

        if (_invincibleTime > 0)
        {
            sr.color = new Color(255, 255, 255, 128);
            if ((_invincibleTime -= Time.deltaTime) < 0f)
            {
                sr.color = new Color(255, 255, 255, 255);
                _invincibleTime = 0;
            }
        }

        _stateMachine.Update();
    }

    /// <summary>
    /// Idle ステート
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

            var position = Context.transform.position;

            float width = 100f - 8f;
            if (position.x < -width) position.x = -width;
            else if (position.x > width) position.x = width;

            Context.transform.position = position;
        }
    }

    /// <summary>
    /// Move ステート
    /// </summary>
    private class DamageState : IceMilkTea.Core.ImtStateMachine<Player>.State
    {
        protected internal override void Update()
        {
            if ((Context._damageTime += Time.deltaTime) > 3f)
            {
                Context._stateMachine.SendEvent((int)StateEventId.Idle);
                Context._damageTime = 0;

                Context._invincibleTime = 10.0f;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_invincibleTime > 0) return;

        var shell = collision.gameObject.GetComponent<Shell>();
        if (shell)
        {
            _stateMachine.SendEvent((int)StateEventId.Damage);
        }
    }
}
