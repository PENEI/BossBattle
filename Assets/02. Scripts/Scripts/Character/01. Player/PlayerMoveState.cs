using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State
{
    public PlayerMoveState(Character character, StateMachine stateMachine, EState state) :
        base(character, stateMachine, state)
    { }

    public override void Enter()
    {
        // �ִϸ��̼� ���
        character.Ani_Movement(true);
    }
    public override void LogicUpdate()
    {
        // ���� Ż�� ����
        base.LogicUpdate();
        // �÷��̾� �̵� �� ȸ��
        player.KeyDownCharacterRotation();

        if (player.Attack())
        {
            stateMachine.ChangeState(player.stateDic[EState.Attack0]);
        }
    }
    public override void Exit()
    {
        character.Ani_Movement(false);
    }
    public override void Escape()
    {
        // ����Ű ���� ���� �� ��� ���·� ��ȯ
        if(!player.Move())
        {
            stateMachine.ChangeState(player.stateDic[EState.Idle]);
        }
    }
}
