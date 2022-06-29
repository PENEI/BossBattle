using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Slash3 : State
{
    private EnemyController enemy;
    private bool isHit = false;

    public Enemy_Attack_Slash3(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        enemy.LookPlayer();     // 플레이어쪽으로 회전
        character.state = Character.eState.Attack;
        character.Ani_Attack_Slash3();          // 애니메이션 실행
    }

    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        AnimatorStateInfo aniStateInfo = character.ani.GetCurrentAnimatorStateInfo(0);

        if (!isHit && aniStateInfo.IsName("Base Layer.Attack_Slash3") && aniStateInfo.normalizedTime >= 0.1)
        {
            isHit = true;
            enemy.isWeaponHit = true;
        }
        // 애니메이션이 전부 실행되면 종료
        stateMachine.AnimationEnd(character, enemy.idleState, "Base Layer.Attack_Slash3", 0.5f);
    }

    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        enemy.isWeaponHit = false;
        isHit = false;
    }
}
