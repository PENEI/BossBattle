using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : State
{
    public PlayerMoveState(Character character, StateMachine stateMachine, EState state) :
        base(character, stateMachine, state)
    { }

    public override void Enter()
    {
        // 애니메이션 출력
        character.Ani_Movement(true);
    }
    public override void LogicUpdate()
    {
        // 상태 탈출 조건
        base.LogicUpdate();
        // 플레이어 이동 및 회전
        player.KeyDownCharacterRotation();

        if (player.Attack())
        {
            stateMachine.ChangeState(player.stateDic[EState.Attack0]);
        }
    }
    public override void Exit()
    {
        character.Ani_Movement(false);
    }
    public override void Escape()
    {
        // 방향키 조작 안할 시 대기 상태로 전환
        if(!player.Move())
        {
            stateMachine.ChangeState(player.stateDic[EState.Idle]);
        }
    }
}
