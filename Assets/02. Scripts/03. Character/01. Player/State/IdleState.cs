using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        
    }

    // 상태 시작 (start)
    public override void Enter() 
    {
        character.Ani_Movement(false);
        character.state = Character.eState.Idle;
        character.Ani_Idle();          // 애니메이션 실행
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
