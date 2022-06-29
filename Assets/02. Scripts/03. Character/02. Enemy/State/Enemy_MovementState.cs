using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MovementState : State
{
    private Transform target;   // Ÿ��(�÷��̾�) ��ġ
    private EnemyController enemy;
    
    public Enemy_MovementState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        target = player.transform;
        enemy = character.GetComponent<EnemyController>();
    }

    // ���� ���� (start)
    public override void Enter()
    {
        character.state = Character.eState.Move;
        
        character.Ani_Movement(true);          // �ִϸ��̼� ����
    }
    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        enemy.agent.SetDestination(target.position);    // �߰�

        // �޺� ���� ��Ÿ��� ���� �� ���� ��ȯ
        Vector3 targetPos = player.transform.position;
        Vector3 pos = enemy.transform.position;
        if (CFunction.GetDistance(targetPos, pos) <= Mathf.Pow(enemy.weaponAttackRadius, 2))
        {
            stateMachine.ChangeState(enemy.attackSlash1);
        }
    }
    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        enemy.agent.ResetPath();
        character.Ani_Movement(false);          // �ִϸ��̼� ����
    }
}
