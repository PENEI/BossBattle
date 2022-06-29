using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Combo3 : State
{
    private PlayerController ctr;
    private bool isHit = false;

    public AttackState_Combo3(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
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
        AnimatorStateInfo aniStateInfo = ctr.ani.GetCurrentAnimatorStateInfo(0);

        // 마우스 왼쪽 입력 (공격)
        if (aniStateInfo.IsName("Base Layer.Attack.Attack_Combo2") &&
            Input.GetMouseButtonDown(0) && !ctr.isNotAttack)
        {
            stateMachine.ChangeState(ctr.attackStateCombo4);
        }

        // 애니메이션 시간이 끝나면 대기 상태로 전환
        stateMachine.AnimationEnd(ctr, ctr.idleState, "Base Layer.Attack.Attack_Combo2", 0.7f);
    }
    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        ctr.isBehavior = false;
        ctr.isWeaponHit = false;
        isHit = false;
    }
}
