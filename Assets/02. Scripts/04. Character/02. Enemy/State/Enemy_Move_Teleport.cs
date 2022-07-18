using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move_Teleport : State
{
    private EnemyController enemy;

    public Enemy_Move_Teleport(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
    }

    // ���� ���� (start)
    public override void Enter()
    {
        character.state = Character.eState.Attack;
        character.Ani_Move_Teleport();          // �ִϸ��̼� ����
    }

    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        stateMachine.AnimationEnd(character, enemy.attackSlash3, "Base Layer.Move_Teleport");
    }

    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        // �÷��̾� �ڷ� �̵�
        Vector3 movePos = player.transform.GetChild(0).localRotation * (Vector3.forward * 3);
        enemy.transform.position = player.transform.position - movePos;
    }
}
