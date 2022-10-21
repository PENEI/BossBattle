using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    protected Enemy enemy;
    protected string stateName;

    public EnemyAttackState(Character _character, StateMachine _stateMachine, EState _state) : 
        base(_character, _stateMachine, _state)
    {
        enemy = _character as Enemy;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }

    // 애니메이션 끝날 시 상태 종료 및 다음 상태 변환(애니메이션 시간만 비교)
    public override void Escape(float time, EState eState = EState.Idle)
    {
        base.Escape();
        stateMachine.AutoChangeState(character, enemy.stateDic[eState], time);
    }
    // 애니메이션 끝날 시 상태 종료 및 다음 상태 변환(애니메이션 시간과 현재 재생애니메이션 비교)
    public override void Escape(string name, float time = 0.9f, EState eState = EState.Idle)
    {
        base.Escape();
        stateMachine.AutoChangeState(character, enemy.stateDic[eState], name, time);
    }
}
