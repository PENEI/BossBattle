using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleState : State
{
    private EnemyController enemy;

    public Enemy_IdleState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;         // Character에서 Enemy로 형변환 가능 시 변환 
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        int random;
        do
        {
            random = Random.Range(0, 4);
        } while (random == enemy.attackKind);
        enemy.attackKind = random;
        
        character.state = Character.eState.Idle;
        character.Ani_Idle();          // 애니메이션 실행
    }
    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        // 타겟이 일정범위 안에 있으면 추격
        Vector3 targetPos = player.transform.position;
        Vector3 pos = enemy.transform.position;

        // 이동 상태 전환
        if (enemy.attackKind == 0 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        // 순간 이동 상태 전환
        else if (enemy.attackKind == 1 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.moveTeleport);
        }
        // 스킬1 상태 전환
        else if (enemy.attackKind == 2 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.attackCastSpell1);
        }
        // 스킬2 상태 전환
        else if (enemy.attackKind == 3 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.attackCastSpell2);
        }
    }
}
