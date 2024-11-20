using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : State
{
    public PlayerHitState(Character character, StateMachine stateMachine, EState state) :
        base(character, stateMachine, state)
    {

    }
    public override void Enter()
    {
        base.Enter();
        player.ani.SetTrigger("TriggerHit");
        player.isHit = true;
    }
}
