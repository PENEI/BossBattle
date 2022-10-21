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

    // �ִϸ��̼� ���� �� ���� ���� �� ���� ���� ��ȯ(�ִϸ��̼� �ð��� ��)
    public override void Escape(float time, EState eState = EState.Idle)
    {
        base.Escape();
        stateMachine.AutoChangeState(character, enemy.stateDic[eState], time);
    }
    // �ִϸ��̼� ���� �� ���� ���� �� ���� ���� ��ȯ(�ִϸ��̼� �ð��� ���� ����ִϸ��̼� ��)
    public override void Escape(string name, float time = 0.9f, EState eState = EState.Idle)
    {
        base.Escape();
        stateMachine.AutoChangeState(character, enemy.stateDic[eState], name, time);
    }
}
