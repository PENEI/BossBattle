using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : State
{
    private Player p;
    public PlayerDeathState(Character character, StateMachine stateMachine, EState state) :
        base(character, stateMachine, state)
    {
        p = character as Player;
    }
    public override void Enter()
    {
        base.Enter();
        p.ani.SetTrigger("TriggerDeath");
        p.isDeath = true;
    }
}
