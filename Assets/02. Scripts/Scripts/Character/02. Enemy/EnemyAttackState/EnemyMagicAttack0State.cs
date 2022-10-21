using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicAttack0State : EnemyAttackState
{
    private GameObject skillObj;
    private int count = 5;

    public EnemyMagicAttack0State(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        if(!skillObj)
        {
            skillObj = enemy.skillDic[ESkillType.FireBall];
        }
        enemy.transform.LookAt(player.transform);
        dir = enemy.transform.rotation;
        enemy.ani.SetTrigger("Attack_CastSpell0");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        Vector3 pos = enemy.transform.position;

        // ����
        angle = 90 / (count);

        for (int i = 0; i < count; i++)
        {
            CreateFireBall(pos, ((angle * i)));
        }
    }
    public override void Escape()
    {
        base.Escape("Base Layer.Attack_CastSpell0", 0.6f, EState.Idle);
    }

    private Quaternion dir;
    private float angle;

    // ���̾ ����
    public void CreateFireBall(Vector3 _pos, float _angle, float dis = 0f)
    {
        // ȸ��, ��ġ ����
        Vector3 pos = _pos + (enemy.transform.forward * dis);
        // ������
        float correction = ((angle * (count / 2f)) - (angle / 2));
        // ź�� ���� ������ �������� ����
        Quaternion rot = dir * Quaternion.Euler(0, _angle - correction, 0);
        
        // �̻��� ����
        GameObject obj =
            SMemoryPool.Instance.MemoryPoolDic[ESkillObjType.FireBall].ActivatePoolItem(pos, rot);
    }
}