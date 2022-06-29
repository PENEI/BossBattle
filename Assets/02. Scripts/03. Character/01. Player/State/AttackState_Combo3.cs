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

    // ���� ���� (start)
    public override void Enter()
    {
        ctr.Ani_Attack();          // �ִϸ��̼� ����
        ctr.state = Character.eState.Attack;
        ctr.isBehavior = true;
        isHit = true;
        ctr.isWeaponHit = true;
    }
    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        AnimatorStateInfo aniStateInfo = ctr.ani.GetCurrentAnimatorStateInfo(0);

        // ���콺 ���� �Է� (����)
        if (aniStateInfo.IsName("Base Layer.Attack.Attack_Combo2") &&
            Input.GetMouseButtonDown(0) && !ctr.isNotAttack)
        {
            stateMachine.ChangeState(ctr.attackStateCombo4);
        }

        // �ִϸ��̼� �ð��� ������ ��� ���·� ��ȯ
        stateMachine.AnimationEnd(ctr, ctr.idleState, "Base Layer.Attack.Attack_Combo2", 0.7f);
    }
    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        ctr.isBehavior = false;
        ctr.isWeaponHit = false;
        isHit = false;
    }
}
