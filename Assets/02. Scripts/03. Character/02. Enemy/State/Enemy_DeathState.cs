using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathState : State
{
    private EnemyController enemy;

    public Enemy_DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
    }

    // ���� ���� (start)
    public override void Enter()
    {
        enemy.LookPlayer();     // �÷��̾������� ȸ��
        character.state = Character.eState.Death;
        character.Ani_Death();  // �ִϸ��̼� ���
        enemy.isDeath = true;   // ���
    }

    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        // �ִϸ��̼��� ���� ����Ǹ� ����
        stateMachine.AnimationEnd(character, enemy.idleState, "Base Layer.Death", 0.9f);
    }

    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        // ��� üũ
        Destroy(enemy.gameObject);
    }
}
