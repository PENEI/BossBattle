using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleportState : EnemyAttackState
{
    public EnemyTeleportState(Character _character, StateMachine _stateMachine, EState _state) :
        base(_character, _stateMachine, _state)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        enemy.ani.SetTrigger("Move_Teleport");
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        // 플레이어 뒤로 이동
        Vector3 movePos = player.transform.GetChild(0).localRotation * (Vector3.forward * 3);
        enemy.transform.position = player.transform.position - movePos;

        enemy.transform.LookAt(player.transform);   // 플레이어 방향 회전
    }
    public override void Escape()
    {
        base.Escape("Base Layer.Move_Teleport", 0.9f, EState.NormalAttack2);
    }
}
