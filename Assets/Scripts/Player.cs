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
    /// 
    /// </summary>
    public int MaxLife { get; private set; } = 10;

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
    /// 
    /// </summary>
    private float _blinkTime;

    /// <summary>
    /// 
    /// </summary>
    private bool _isBlink;

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
    private int _life;

    /// <summary>
    /// 
    /// </summary>
    private enum StateEventId : int
    {
        Idle,
        Damage,
        Knockout,
        Max
    }

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new IceMilkTea.Core.ImtStateMachine<Player>(this);
        _stateMachine.AddAnyTransition<IdleState>((int)StateEventId.Idle);
        _stateMachine.AddAnyTransition<DamageState>((int)StateEventId.Damage);
        _stateMachine.AddAnyTransition<KnockoutState>((int)StateEventId.Knockout);
        _stateMachine.SetStartState<IdleState>();

        _life = MaxLife;
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
            if ((_blinkTime += Time.deltaTime) > 0.2f)
            {
                _isBlink = !_isBlink;
                _blinkTime = 0;
            }

            var color = sr.color;
            color.a = _isBlink ? 0.5f : 1f;
            if ((_invincibleTime -= Time.deltaTime) < 0f)
            {
                color.a = 1f;
                _invincibleTime = 0;
            }
            sr.color = color;
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
    /// Damage ステート
    /// </summary>
    private class DamageState : IceMilkTea.Core.ImtStateMachine<Player>.State
    {
        /// <summary>
        /// 
        /// </summary>
        private GameObject _knockoutCounter;

        /// <summary>
        /// 
        /// </summary>
        private int _slapCount;

        protected internal override void Enter()
        {
            var prefab = (GameObject)Resources.Load("Prefabs/KnockoutCounter");
            _knockoutCounter = Instantiate(prefab,
                new Vector3(Context.transform.position.x, Context.transform.position.y + 16f, Context.transform.position.z),
                Quaternion.identity);
            _slapCount = 0;
        }

        protected internal override void Update()
        {
            if (_knockoutCounter)
            {
                const int baseSlapCount = 10;
                // ライフが減るごとに 5 カウントずつ増える
                int maxSlapCount = baseSlapCount + (5 * (Context.MaxLife - Context._life));

                if (Input.GetKeyUp(KeyCode.Z))
                {
                    if (++_slapCount > maxSlapCount)
                    {
                        if (--Context._life < 0) Context._life = 0;
                        Debug.Log(Context._life);
                        Context._stateMachine.SendEvent((int)StateEventId.Idle);
                        Context._invincibleTime = 2f;
                        Destroy(_knockoutCounter);
                    }
                }
            }
            else
            {
                Context.gameObject.GetComponent<ShellBulletEmitter>().Emit(Context.transform.position, 1);
                Context._stateMachine.SendEvent((int)StateEventId.Knockout);
            }
        }
    }

    /// <summary>
    /// Knockout ステート
    /// </summary>
    private class KnockoutState : IceMilkTea.Core.ImtStateMachine<Player>.State
    {
        protected internal override void Enter()
        {
            Destroy(Context.gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_stateMachine.IsCurrentState<IdleState>()) return;

        var heart = collision.gameObject.GetComponent<Heart>();
        if (heart)
        {
            Debug.Log(heart);

            heart.Use();
            if (++_life > MaxLife) _life = MaxLife;
        }

        if (_invincibleTime > 0) return;

        var shell = collision.gameObject.GetComponent<Shell>();
        if (shell)
        {
            _stateMachine.SendEvent((int)StateEventId.Damage);
        }
    }
}
