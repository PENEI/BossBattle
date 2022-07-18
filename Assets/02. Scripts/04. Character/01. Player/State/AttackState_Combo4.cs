using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Combo4 : State
{
    private PlayerController ctr;
    private bool isHit = false;

    public AttackState_Combo4(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        ctr = Player.Instance.ctr;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        ctr.Ani_Attack();          // 애니메이션 실행
        ctr.state = Character.eState.Attack;
        ctr.isBehavior = true;
        isHit = true;
        ctr.isWeaponHit = true;
    }
    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        // 애니메이션 시간이 끝나면 대기 상태로 전환
        stateMachine.AnimationEnd(ctr, ctr.idleState, "Base Layer.Attack.Attack_Combo3", 0.7f);
    }
    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        ctr.isBehavior = false;
        ctr.isWeaponHit = false;
        isHit = false;
    }
}
