using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    public EnemyDeathState(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {
        
    }
}
