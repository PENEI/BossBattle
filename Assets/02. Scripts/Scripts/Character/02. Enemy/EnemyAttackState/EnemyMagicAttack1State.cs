using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicAttack1State : EnemyAttackState
{
    private GameObject skillObj;

    public EnemyMagicAttack1State(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {

    }
    public override void Enter()
    {
        base.Enter();
        if (!skillObj)
        {
            skillObj = enemy.skillDic[ESkillType.RangeSpell];
        }
        enemy.transform.LookAt(player.transform);
        enemy.ani.SetTrigger("Attack_CastSpell1");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("\n광역 공격");
        Vector3 targetPos = player.transform.position;
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("\n광역 공격 "+i);
            // 랜덤 위치 설정
            int randomX = Random.Range(-10, 10);
            int randomZ = Random.Range(-10, 10);
            Vector3 pos = new Vector3(targetPos.x + randomX, targetPos.y, targetPos.z + randomZ);
            // 생성
            GameObject obj = SMemoryPool.Instance.MemoryPoolDic[ESkillObjType.RangeSpell].ActivatePoolItem(pos);  // 광역 스킬 생성
            
            //obj.transform.position = new Vector3(targetPos.x + randomX, targetPos.y, targetPos.z + randomZ);
        }
    }
    public override void Escape()
    {
        base.Escape("Base Layer.Attack_CastSpell1", 0.6f, EState.Idle);
    }
}
