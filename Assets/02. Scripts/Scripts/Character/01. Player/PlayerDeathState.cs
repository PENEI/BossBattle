using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : State
{
    public PlayerDeathState(Character character, StateMachine stateMachine, EState state) :
        base(character, stateMachine, state)
    {

    }
}
