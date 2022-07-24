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
            random = Random.Range(1, 5);
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

        // Ÿ���� Ž�� ���� �ȿ� ���þ� ���� ����
        if (CFunction.GetDistance(targetPos, pos) 
            <= Mathf.Pow(enemy.searchRadius, 2))
        {
            State state = null;
            switch(enemy.attackKind)
            {
                case (int)EAttackKind.Move:
                    state = enemy.moveState;
                    break;
                case (int)EAttackKind.Teleport:
                    state = enemy.moveTeleport;
                    break;
                case (int)EAttackKind.SpellA:
                    state = enemy.attackCastSpell1;
                    break;
                case (int)EAttackKind.SpellB:
                    state = enemy.attackCastSpell2;
                    break;
            }
            stateMachine.ChangeState(state);
        }
    }
}
