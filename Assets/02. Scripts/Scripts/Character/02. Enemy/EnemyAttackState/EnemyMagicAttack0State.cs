using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicAttack0State : EnemyAttackState
{
    private GameObject skillObj;
    private int count = 7;
    private Quaternion dir;     // 몬스터 회전 값
    private float angle;
    private float attackAngle = 180;// 범위공격 각도
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

        // 간격
        angle = attackAngle / (count);

        for (int i = 0; i < count; i++)
        {
            CreateFireBall(pos, (angle * i));
        }
    }
    public override void Escape()
    {
        base.Escape("Base Layer.Attack_CastSpell0", 0.6f, EState.Idle);
    }

    // 파이어볼 생성
    public void CreateFireBall(Vector3 _pos, float _angle, float dis = 1f)
    {
        // 회전, 위치 설정
        Vector3 pos = _pos + (enemy.transform.forward * dis);
        // 보정값
        float correction = angle * ((count - 1) / 2f);

        // 탄이 몬스터 주위로 퍼지도록 보정
        Quaternion rot = dir * Quaternion.Euler(0, _angle - correction, 0);
        
        // 미사일 생성
        GameObject obj =
            SMemoryPool.Instance.MemoryPoolDic[ESkillObjType.FireBall].
            ActivatePoolItem(pos, rot);
    }
}
