using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        
    }

    // ���� ���� (start)
    public override void Enter() 
    {
        character.Ani_Movement(false);
        character.state = Character.eState.Idle;
        character.Ani_Idle();          // �ִϸ��̼� ����
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
