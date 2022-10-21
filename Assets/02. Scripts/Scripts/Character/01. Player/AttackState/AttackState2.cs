using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState2 : PlayerAttackState
{
    public AttackState2(Character character, StateMachine stateMachine, EState state) :
       base(character, stateMachine, state)
    {

    }
    public override void Enter()
    {
        stateName = this.GetType().ToString();
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.Attack())
        {
            stateMachine.ChangeState(player.stateDic[EState.Attack3]);
        }
    }
}
