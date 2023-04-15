using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    private Enemy enemy;
    private int random = -1;
    private EState nextState;

    public EnemyIdleState(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {
        enemy = _character as Enemy;
    }

    public override void Enter()
    {
        character.Ani_Idle();
        // ���� ���� ���� ����
        nextState = RandomState();
    }

    public override void LogicUpdate()
    {
        Vector3 targetPos = player.transform.position;  // �÷��̾� ��ġ
        Vector3 pos = enemy.transform.position;         // ���� ��ġ
        float searchRadius = Mathf.Pow(enemy.searchRadius, 2);  // ������ Ž�� �Ÿ� ����
        float radius = CFunction.GetDistance(targetPos, pos);   // �÷��̾�� ������ ������ �Ÿ� ����

        // ������ Ž�� �Ÿ��� �÷��̾�� ������ ���� �Ÿ��� ��
        if (radius <= searchRadius) 
        {
            enemy.isBattle = true;
        }

        // ���� ���� �� ���� ����
        if(enemy.isBattle)
        {
            stateMachine.ChangeState(enemy.stateDic[nextState]);
        }
    }

    /// <summary>
    /// ���� ���� ���� ��ȯ
    /// </summary>
    /// <returns></returns>
    private EState RandomState()
    {
/*        int attackListCount = enemy.randomAttackPattern.Count;
        int tempRandom;
        do
        {
            //tempRandom = Random.Range(0, attackListCount);
            tempRandom = Random.Range(2, 4);
        } while (random == tempRandom);
        random = tempRandom;
        return enemy.randomAttackPattern[random];  // ���� ���� ����
*/
        return enemy.randomAttackPattern[2];  // ���� ���� ����
    }
}
