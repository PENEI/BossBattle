using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private Camera cam;

    public MoveState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        cam = Camera.main;
    }

    // 상태 시작 (start)
    public override void Enter()
    {
        character.state = Character.eState.Move;
    }

    // 지속적 실행 (Update)
    public override void LogicUpdate()
    {
        player.transform.position += CharacterMoveDir();   // 플레이어 이동
    }

    // 캐릭터가 이동할 위치
    public Vector3 CharacterMoveDir()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  // x축
        float vertical = Input.GetAxisRaw("Vertical");      // y축

        // 방향키를 입력 받고 있는지 체크
        bool isMove = ((vertical != 0) || (horizontal != 0));
        character.Ani_Movement(isMove);

        // 현재 카메라가 보는 방향
        Quaternion camRot = cam.transform.rotation;
        // Player 이동 방향
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 dir = (camRot * moveDir);
        Vector3 playerDir = dir * Time.deltaTime * player.ctr.moveSpeed;
        playerDir.y = 0;  // x축으로 회전 하지 않도록 0을 저장

        return playerDir; // 이동 거리
    }
}
