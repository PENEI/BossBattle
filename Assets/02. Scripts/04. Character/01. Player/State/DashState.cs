using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashState : State
{
    public float speed;             // 구르는 속도
    private Rigidbody rigid;        // 플레이어의 RigidBody;

    // 스크립트가 생성될 시 (생성자)
    public DashState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        rigid = Player.Instance.transform.GetComponentInChildren<Rigidbody>();
        speed = player.ctr.dashSpeed;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        character.state = Character.eState.Dash;
        player.ctr.isBehavior = true;      // 행동 불가 상태
        player.ctr.isNotAttack = true;    // 공격 불가 상태
        character.Ani_Dash();           // 애니메이션 실행
                                            // speed만큼 플레이어 밀기
        rigid.AddForce(player.ctr.playerObj.transform.forward * speed, ForceMode.Acceleration);
    }

    // 지속적 실행 (Update)
    public override void LogicUpdate() 
    {
        stateMachine.AnimationEnd(character, player.ctr.idleState, "Base Layer.Dash", 0.9f);
    }

    // 상태 종료 (상태 전환 시 실행)
    public override void Exit() 
    {
        player.ctr.isBehavior = false;      // 행동 가능 상태
        player.ctr.isNotAttack = false;    // 공격 가능 상태
    }
}
