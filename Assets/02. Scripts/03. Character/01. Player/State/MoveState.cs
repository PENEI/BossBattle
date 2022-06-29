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
        // 플레이어 이동 애니메이션에 입력 값 전달
        character.Ani_Movement(isMove);

        // 현재 카메라가 보는 방향
        Quaternion camRot = cam.transform.rotation;
        // 카메라가보는 방향 * 입력 이동 방향, 대각선 이동 시 직선이동 속도와 같개되도록 normalized
        Vector3 dir = (camRot * new Vector3(horizontal, 0f, vertical).normalized);
        // 진행 방향으로 moveSpeed만큼 이동
        Vector3 moveDir = dir * Time.deltaTime * player.ctr.moveSpeed;

        moveDir.y = 0;  // y축으로는 이동하지 않도록 y는 0으로 고정

        return moveDir; // 다음 이동 거리
    }
}
