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
        // �ִϸ��̼� �ð��� ������ ��� ���·� ��ȯ
        stateMachine.AnimationEnd(ctr, ctr.idleState, "Base Layer.Attack.Attack_Combo3", 0.7f);
    }
    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        ctr.isBehavior = false;
        ctr.isWeaponHit = false;
        isHit = false;
    }
}
