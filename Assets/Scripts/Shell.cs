using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    /// <summary>
    /// キャラ画像スプライトリスト
    /// </summary>
    public List<Sprite> Sprites = new List<Sprite>();

    /// <summary>
    /// 
    /// </summary>
    private enum StateEventId : int
    {
        Idle,
        Move,
        Max
    }

    /// <summary>
    /// ステートマシン
    /// </summary>
    private IceMilkTea.Core.ImtStateMachine<Shell> _stateMachine;

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
    private float _velocityX;

    /// <summary>
    /// 
    /// </summary>
    private float _velocityY;

    /// <summary>
    /// 
    /// </summary>
    public float VelocityX { set { _velocityX = value; } }

    /// <summary>
    /// 
    /// </summary>
    public bool _exploded;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _stateMachine = new IceMilkTea.Core.ImtStateMachine<Shell>(this);
        _stateMachine.AddAnyTransition<IdleState>((int)StateEventId.Idle);
        _stateMachine.AddAnyTransition<MoveState>((int)StateEventId.Move);
        _stateMachine.SetStartState<MoveState>();
        _exploded = false;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if ((_stepTime += Time.deltaTime) > 0.075f)
        {
            if (++_stepSpriteIndex >= Sprites.Count)
                _stepSpriteIndex = 0;
            _stepTime = 0;
        }

        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[_stepSpriteIndex];

        _stateMachine.Update();
    }

    /// <summary>
    /// Idle ステート
    /// </summary>
    private class IdleState : IceMilkTea.Core.ImtStateMachine<Shell>.State
    {
        protected internal override void Update()
        {
            // Attack ステートへ
            //Context.stateMachine.SendEvent((int)StateEventId.Damage);
        }
    }

    /// <summary>
    /// Move ステート
    /// </summary>
    private class MoveState : IceMilkTea.Core.ImtStateMachine<Shell>.State
    {
        protected internal override void Update()
        {
#if false
            const float g = 0.2f;
            const float width = 95f, height = 70f;

            var position = Context.transform.position;
            Context._velocityY += g * Time.deltaTime;

            position.x -= Context._velocityX * Time.deltaTime;
            position.y -= Context._velocityY;

            if (position.x > width || position.x < -width)
            {
                Context._velocityX = 0 - Context._velocityX;
            }
            if (position.y < -height)
            {
                //Context._velocityY = 0 - Context._velocityY;

                const float maxVelocity = -0.3f;
                Context._velocityY = maxVelocity;
                if (Context._velocityY < maxVelocity) Context._velocityY = maxVelocity;
            }

            Context.transform.position = position;
#endif
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Explode(int comboCount)
    {
        if (!_exploded)
        {
            GetComponent<ShellBulletEmitter>().Emit(transform.position, comboCount + 1);
            GetComponent<ScoreManager>().Scored(comboCount, transform.position);
            _exploded = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
    }
}
