using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathState : State
{
    private EnemyController enemy;

    public Enemy_DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        enemy.LookPlayer();     // 플레이어쪽으로 회전
        character.state = Character.eState.Death;
        character.Ani_Death();  // 애니메이션 출력
        enemy.isDeath = true;   // 사망
    }

    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        // 애니메이션이 전부 실행되면 종료
        stateMachine.AnimationEnd(character, enemy.idleState, "Base Layer.Death", 0.9f);
    }

    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        // 사망 체크
        Destroy(enemy.gameObject);
    }
}
