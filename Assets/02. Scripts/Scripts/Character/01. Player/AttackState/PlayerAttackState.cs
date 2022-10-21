using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    protected string stateName; // 종료할 스테이트 애니메이션 이름(String형)

    public PlayerAttackState(Character character, StateMachine stateMachine, EState state) :
        base(character, stateMachine, state)
    {
        
    }
    public override void Enter()
    {
        character.Ani_Attack();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Escape()
    {
        stateMachine.AutoChangeState(character, player.stateDic[EState.Idle]);
    }
}
