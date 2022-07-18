using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {

    }

    // 상태 시작 (start)
    public override void Enter()
    {
        character.Ani_Death();
        character.state = Character.eState.Death;
        player.ctr.isDeath = true;
    }
    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {

    }
    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {

    }
}
