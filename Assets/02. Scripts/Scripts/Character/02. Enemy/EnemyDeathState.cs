using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    private Enemy enemy;

    public EnemyDeathState(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {
        enemy = _character as Enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.ani.SetTrigger("TriggerDeath");
        enemy.isDeath = true;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // 애니메이션이 전부 실행되면 종료
        AnimatorStateInfo aniStateInfo = 
            character.ani.GetCurrentAnimatorStateInfo(0);
        if (aniStateInfo.IsName("Base Layer.Death") && aniStateInfo.normalizedTime >= 0.95f)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
