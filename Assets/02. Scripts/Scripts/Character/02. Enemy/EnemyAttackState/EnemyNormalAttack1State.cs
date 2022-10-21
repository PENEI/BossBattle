using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAttack1State : EnemyAttackState
{
    public EnemyNormalAttack1State(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {

    }
    public override void Enter()
    {
        base.Enter();
        enemy.Ani_EnemyAttack(1);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Escape()
    {
        base.Escape("Base Layer.Attack_Slash1", 0.6f, EState.NormalAttack2);
    }
}
