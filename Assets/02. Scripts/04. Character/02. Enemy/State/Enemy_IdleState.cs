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
            random = Random.Range(1, 5);
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

        // 타겟이 탐색 범위 안에 들어올씨 전투 시작
        if (CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            State state = null;
            switch(enemy.attackKind)
            {
                case (int)EAttackKind.Move:
                    state = enemy.moveState;
                    break;
                case (int)EAttackKind.Teleport:
                    state = enemy.moveTeleport;
                    break;
                case (int)EAttackKind.SpellA:
                    state = enemy.attackCastSpell1;
                    break;
                case (int)EAttackKind.SpellB:
                    state = enemy.attackCastSpell2;
                    break;
            }
            stateMachine.ChangeState(state);
        }
    }
}
