using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : ItemBase
{
    /// <summary>
    /// ステートマシン
    /// </summary>
    private IceMilkTea.Core.ImtStateMachine<Life> _stateMachine;

    /// <summary>
    /// 
    /// </summary>
    private enum StateEventId : int
    {
        Idle,
        Disappear,
        Max
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        _stateMachine = new IceMilkTea.Core.ImtStateMachine<Life>(this);
        _stateMachine.AddAnyTransition<IdleState>((int)StateEventId.Idle);
        _stateMachine.AddAnyTransition<DisappearState>((int)StateEventId.Disappear);
        _stateMachine.SetStartState<IdleState>();

        _stepSpan = 0.25f;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        UpdateStep();

        _stateMachine.Update();
    }

    /// <summary>
    /// Idle ステート
    /// </summary>
    private class IdleState : IceMilkTea.Core.ImtStateMachine<Life>.State
    {
        protected internal override void Update()
        {

        }
    }

    /// <summary>
    /// Disappear ステート
    /// </summary>
    private class DisappearState : IceMilkTea.Core.ImtStateMachine<Life>.State
    {
        protected internal override void Update()
        {
            var position = Context.transform.position;
            position.y -= Mathf.Sin(Time.frameCount) * 10;
            Context.transform.position = position;
        }
    }
}
