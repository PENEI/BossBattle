using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_CastSpell1 : State
{
    private EnemyController enemy;
    private Enemy_Attack_Scriptable scpt;   // ���ݿ� ���� ��ũ���ͺ�
    
    public Enemy_Attack_CastSpell1(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
        scpt = enemy.attack_CSpellA_Scpt;
    }

    // ���� ���� (start)
    public override void Enter()
    {
        character.state = Character.eState.Attack;
        enemy.LookPlayer();
        character.Ani_Attack_CastSpell1();          // �ִϸ��̼� ����
    }

    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        stateMachine.AnimationEnd(character, enemy.idleState, "Base Layer.Attack_CastSpell1", 0.5f);
    }

    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        Vector3 targetPos = player.transform.position;
        for (int i = 0; i < scpt.count; i++)
        {
            //GameObject obj = Instantiate(scpt.impectObj);
            GameObject obj = SMemoryPool.Instance.memoryPool_SpellA.ActivatePoolItem();
            int randomX = Random.Range(-10, 10);
            int randomZ = Random.Range(-10, 10);
            obj.transform.position = new Vector3(targetPos.x + randomX, targetPos.y, targetPos.z + randomZ);
        }
    }
}
