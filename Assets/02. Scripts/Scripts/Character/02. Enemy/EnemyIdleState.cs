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
        // 랜덤 공격 상태 저장
        nextState = RandomState();
    }

    public override void LogicUpdate()
    {
        Vector3 targetPos = player.transform.position;  // 플레이어 위치
        Vector3 pos = enemy.transform.position;         // 몬스터 위치
        float searchRadius = Mathf.Pow(enemy.searchRadius, 2);  // 몬스터의 탐색 거리 제곱
        float radius = CFunction.GetDistance(targetPos, pos);   // 플레이어와 몬스터의 사이의 거리 제곱

        // 몬스터의 탐색 거리와 플레이어와 몬스터의 현재 거리를 비교
        if (radius <= searchRadius) 
        {
            enemy.isBattle = true;
        }

        // 전투 상태 시 상태 변경
        if(enemy.isBattle)
        {
            stateMachine.ChangeState(enemy.stateDic[nextState]);
        }
    }

    /// <summary>
    /// 랜덤 공격 상태 반환
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
        return enemy.randomAttackPattern[random];  // 다음 공격 패턴
*/
        return enemy.randomAttackPattern[2];  // 다음 공격 패턴
    }
}
