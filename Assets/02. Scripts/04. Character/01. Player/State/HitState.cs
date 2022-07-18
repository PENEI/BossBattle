using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    public HitState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {

    }

    // 상태 시작 (start)
    public override void Enter()
    {
        character.Ani_Hit();
        character.state = Character.eState.Hit;
        player.ctr.isBehavior = true;      // 행동 불가 상태
    }
    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        stateMachine.AnimationEnd(character, player.ctr.idleState, "Base Layer.Hit", 0.6f);
    }
    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        player.ctr.isBehavior = false;      // 행동 불가 상태
        player.ctr.isHit = false;
    }
}
