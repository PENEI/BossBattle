using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    public HitState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {

    }

    // ���� ���� (start)
    public override void Enter()
    {
        character.Ani_Hit();
        character.state = Character.eState.Hit;
        player.ctr.isBehavior = true;      // �ൿ �Ұ� ����
    }
    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        stateMachine.AnimationEnd(character, player.ctr.idleState, "Base Layer.Hit", 0.6f);
    }
    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        player.ctr.isBehavior = false;      // �ൿ �Ұ� ����
        player.ctr.isHit = false;
    }
}
