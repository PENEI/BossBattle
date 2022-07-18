using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_CastSpell2 : State
{
    private EnemyController enemy;
    private Enemy_Attack_Scriptable scpt;   // ���ݿ� ���� ��ũ���ͺ�

    private Quaternion dir;
    private float angle;

    public Enemy_Attack_CastSpell2(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
        scpt = enemy.attack_CSpellB_Scpt;
    }

    // ���� ���� (start)
    public override void Enter()
    {
        character.state = Character.eState.Attack;
        enemy.LookPlayer();
        character.Ani_Attack_CastSpell2();          // �ִϸ��̼� ����
        dir = enemy.transform.rotation;
    }

    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        stateMachine.AnimationEnd(character, enemy.idleState, "Base Layer.Attack_CastSpell2", 0.5f);
    }

    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        Vector3 pos = enemy.transform.position;

        // ����
        angle = 90 / (scpt.count);

        for (int i = 0; i < scpt.count; i++) 
        {
            CreateFireBall(pos, ((angle * i)));
        }
    }

    // ���̾ ����
    public void CreateFireBall(Vector3 _pos, float _angle, float dis = 0f)
    {
        // �̻��� ����
        GameObject obj = SMemoryPool.Instance.memoryPool_SpellB.ActivatePoolItem();

        // ȸ�� ��, ��ġ �� ����
        obj.transform.position = _pos + (enemy.transform.forward * dis);

        // ź�� ���� ������ �������� ����
        float correction = ((angle * (scpt.count / 2f)) - (angle / 2));
        obj.transform.rotation = dir * Quaternion.Euler(0, _angle - correction, 0);
    }
}
