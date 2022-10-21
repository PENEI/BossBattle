using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public PlayerIdleState(Character character, StateMachine stateMachine, EState state) :
        base(character, stateMachine, state)
    {
        
    }
    public override void Enter()
    {
        
    }
    public override void LogicUpdate()
    {
        if (player.Attack())
        {
            stateMachine.ChangeState(player.stateDic[EState.Attack0]);
        }
        else if(player.Move())
        {
            stateMachine.ChangeState(player.stateDic[EState.Move]);
        }
    }
    public override void Exit()
    {
        
    }
}
