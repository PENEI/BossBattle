using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {

    }

    // ���� ���� (start)
    public override void Enter()
    {
        character.Ani_Death();
        character.state = Character.eState.Death;
        player.ctr.isDeath = true;
    }
    // ������ ���� (Update)
    public override void LogicUpdate()
    {

    }
    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {

    }
}
