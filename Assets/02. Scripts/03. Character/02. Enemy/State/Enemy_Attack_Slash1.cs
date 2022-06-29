using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Slash1 : State
{
    private EnemyController enemy;
    private bool isHit = false;

    public Enemy_Attack_Slash1(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        enemy.LookPlayer();     // 플레이어쪽으로 회전
        character.state = Character.eState.Attack;
        character.Ani_Attack_Slash1();          // 애니메이션 실행
    }

    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        AnimatorStateInfo aniStateInfo = character.ani.GetCurrentAnimatorStateInfo(0);
        if (!isHit && aniStateInfo.IsName("Base Layer.Attack_Slash1") && aniStateInfo.normalizedTime >= 0.1) 
        {
            isHit = true;
            enemy.isWeaponHit = true;
        }
        stateMachine.AnimationEnd(character, enemy.attackSlash2, "Base Layer.Attack_Slash1", 0.5f);
    }

    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        enemy.isWeaponHit = false;
        isHit = false;
    }
}
