using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchAttackState : EnemyAttackState
{
    private Vector3 targetPos;  // 타겟 위치
    //private Enemy enemy;
    public EnemySearchAttackState(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        enemy.Ani_Movement(true);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        targetPos = player.transform.position;
        float attackRadius = Mathf.Pow(enemy.normalAttackRadius, 2);
        float radius = CFunction.GetDistance(targetPos, enemy.transform.position);
        enemy.agent.SetDestination(targetPos);  // 타겟 추격
        if(radius <= attackRadius)
        {
            stateMachine.ChangeState(enemy.stateDic[EState.NormalAttack0]);
        }

    }
    public override void Exit()
    {
        base.Exit();
        enemy.Ani_Movement(false);
        enemy.agent.ResetPath();    // 제장된 목표물 위치 삭제
    }
    public override void Escape()
    {
        base.Escape();
    }
}
