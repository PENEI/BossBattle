using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_CastSpell2 : State
{
    private EnemyController enemy;
    private Enemy_Attack_Scriptable scpt;   // 공격에 대한 스크립터블

    private Quaternion dir;
    private float angle;

    public Enemy_Attack_CastSpell2(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
        scpt = enemy.attack_CSpellB_Scpt;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        character.state = Character.eState.Attack;
        enemy.LookPlayer();
        character.Ani_Attack_CastSpell2();          // 애니메이션 실행
        dir = enemy.transform.rotation;
    }

    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        stateMachine.AnimationEnd(character, enemy.idleState, "Base Layer.Attack_CastSpell2", 0.5f);
    }

    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        Vector3 pos = enemy.transform.position;

        // 간격
        angle = 90 / (scpt.count);

        for (int i = 0; i < scpt.count; i++) 
        {
            CreateFireBall(pos, ((angle * i)));
        }
    }

    // 파이어볼 생성
    public void CreateFireBall(Vector3 _pos, float _angle, float dis = 0f)
    {
        // 미사일 생성
        GameObject obj = SMemoryPool.Instance.memoryPool_SpellB.ActivatePoolItem();

        // 회전 값, 위치 값 설정
        obj.transform.position = _pos + (enemy.transform.forward * dis);

        // 탄이 몬스터 주위로 퍼지도록 보정
        float correction = ((angle * (scpt.count / 2f)) - (angle / 2));
        obj.transform.rotation = dir * Quaternion.Euler(0, _angle - correction, 0);
    }
}
