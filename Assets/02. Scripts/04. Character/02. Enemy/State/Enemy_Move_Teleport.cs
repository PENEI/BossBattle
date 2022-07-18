using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move_Teleport : State
{
    private EnemyController enemy;

    public Enemy_Move_Teleport(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        character.state = Character.eState.Attack;
        character.Ani_Move_Teleport();          // 애니메이션 실행
    }

    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        stateMachine.AnimationEnd(character, enemy.attackSlash3, "Base Layer.Move_Teleport");
    }

    // 상태 종료 (상태 전환 시 실행)
    public override void Exit()
    {
        // 플레이어 뒤로 이동
        Vector3 movePos = player.transform.GetChild(0).localRotation * (Vector3.forward * 3);
        enemy.transform.position = player.transform.position - movePos;
    }
}
