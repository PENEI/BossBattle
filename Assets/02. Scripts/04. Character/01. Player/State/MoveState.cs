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

    // ���� ���� (start)
    public override void Enter()
    {
        character.state = Character.eState.Move;
    }

    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        player.transform.position += CharacterMoveDir();   // �÷��̾� �̵�
    }

    // ĳ���Ͱ� �̵��� ��ġ
    public Vector3 CharacterMoveDir()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  // x��
        float vertical = Input.GetAxisRaw("Vertical");      // y��

        // ����Ű�� �Է� �ް� �ִ��� üũ
        bool isMove = ((vertical != 0) || (horizontal != 0));
        character.Ani_Movement(isMove);

        // ���� ī�޶� ���� ����
        Quaternion camRot = cam.transform.rotation;
        // Player �̵� ����
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 dir = (camRot * moveDir);
        Vector3 playerDir = dir * Time.deltaTime * player.ctr.moveSpeed;
        playerDir.y = 0;  // x������ ȸ�� ���� �ʵ��� 0�� ����

        return playerDir; // �̵� �Ÿ�
    }
}
