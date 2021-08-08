using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// 現在のレベル
    /// </summary>
    public int Level { get; private set; }

    /// <summary>
    /// ステートマシン
    /// </summary>
    private IceMilkTea.Core.ImtStateMachine<LevelManager> _stateMachine;

    /// <summary>
    /// 
    /// </summary>
    private enum StateEventId : int
    {
        Play,
        GameOver,
        Max
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _stateMachine = new IceMilkTea.Core.ImtStateMachine<LevelManager>(this);
        _stateMachine.AddAnyTransition<PlayState>((int)StateEventId.Play);
        _stateMachine.AddAnyTransition<GameOverState>((int)StateEventId.GameOver);
        _stateMachine.SetStartState<PlayState>();

        Level = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        _stateMachine.Update();
    }

    /// <summary>
    /// 
    /// </summary>
    void applyLevel()
    {
        {
            var gameObject = GameObject.Find("ShellFactory");
            var shellFactory = gameObject.GetComponent<ShellFactory>();

            shellFactory.MaxShellCount = 40 + (5 * Level);
            shellFactory.ShellSpawnSpan = 0.25f - (0.02f * Level);
        }

        {
            var gameObject = GameObject.Find("ItemManager");
            var itemManager = gameObject.GetComponent<ItemManager>();

            itemManager.ItemSpawnSpan = 30f - (1 * Level);
        }
    }

    /// <summary>
    /// Play ステート
    /// </summary>
    private class PlayState : IceMilkTea.Core.ImtStateMachine<LevelManager>.State
    {
        /// <summary>
        /// 
        /// </summary>
        private float _time;

        protected internal override void Update()
        {
            if (null == GameObject.Find("Beetle"))
            {
                Context._stateMachine.SendEvent((int)StateEventId.GameOver);
            }

            if ((_time += Time.deltaTime) > 60.0f)
            {
                if (++Context.Level > 10)
                {
                    Context.Level = 10;
                }
                Context.applyLevel();
                _time = 0;
            }
        }
    }

    /// <summary>
    /// GameOver ステート
    /// </summary>
    private class GameOverState : IceMilkTea.Core.ImtStateMachine<LevelManager>.State
    {
        protected internal override void Enter()
        {
            var prefab = (GameObject)Resources.Load("Prefabs/UI/GameOverMenu");
            Instantiate(prefab, GameObject.Find("Canvas").transform);
        }

        protected internal override void Update()
        {

        }
    }
}
