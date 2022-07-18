using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleState : State
{
    private EnemyController enemy;

    public Enemy_IdleState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;         // Character���� Enemy�� ����ȯ ���� �� ��ȯ 
    }

    // ���� ���� (start)
    public override void Enter()
    {
        int random;
        do
        {
            random = Random.Range(0, 4);
        } while (random == enemy.attackKind);
        enemy.attackKind = random;
        
        character.state = Character.eState.Idle;
        character.Ani_Idle();          // �ִϸ��̼� ����
    }
    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        // Ÿ���� �������� �ȿ� ������ �߰�
        Vector3 targetPos = player.transform.position;
        Vector3 pos = enemy.transform.position;

        // �̵� ���� ��ȯ
        if (enemy.attackKind == 0 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        // ���� �̵� ���� ��ȯ
        else if (enemy.attackKind == 1 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.moveTeleport);
        }
        // ��ų1 ���� ��ȯ
        else if (enemy.attackKind == 2 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.attackCastSpell1);
        }
        // ��ų2 ���� ��ȯ
        else if (enemy.attackKind == 3 && 
            CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            stateMachine.ChangeState(enemy.attackCastSpell2);
        }
    }
}
