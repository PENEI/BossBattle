using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MovementState : State
{
    private Transform target;   // 타겟(플레이어) 위치
    private EnemyController enemy;
    
    public Enemy_MovementState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        target = player.transform;
        enemy = character.GetComponent<EnemyController>();
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        character.state = Character.eState.Move;
        
        character.Ani_Movement(true);          // 애니메이션 실행
    }
    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        enemy.agent.SetDestination(target.position);    // 추격

        // 콤보 공격 사거리에 들어올 시 상태 전환
        Vector3 targetPos = player.transform.position;
        Vector3 pos = enemy.transform.position;
        if (CFunction.GetDistance(targetPos, pos) <= Mathf.Pow(enemy.weaponAttackRadius, 2))
        {
            stateMachine.ChangeState(enemy.attackSlash1);
        }
    }
    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        enemy.agent.ResetPath();
        character.Ani_Movement(false);          // 애니메이션 실행
    }
}
