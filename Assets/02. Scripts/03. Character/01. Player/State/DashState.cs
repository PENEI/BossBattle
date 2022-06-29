using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashState : State
{
    public float speed;             // ������ �ӵ�
    private Rigidbody rigid;        // �÷��̾��� RigidBody;

    // ��ũ��Ʈ�� ������ �� (������)
    public DashState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        rigid = Player.Instance.transform.GetComponentInChildren<Rigidbody>();
        speed = player.ctr.dashSpeed;
    }

    // ���� ���� (start)
    public override void Enter()
    {
        character.state = Character.eState.Dash;
        player.ctr.isBehavior = true;      // �ൿ �Ұ� ����
        player.ctr.isNotAttack = true;    // ���� �Ұ� ����
        character.Ani_Dash();           // �ִϸ��̼� ����
                                            // speed��ŭ �÷��̾� �б�
        rigid.AddForce(player.ctr.playerObj.transform.forward * speed, ForceMode.Acceleration);
    }

    // ������ ���� (Update)
    public override void LogicUpdate() 
    {
        stateMachine.AnimationEnd(character, player.ctr.idleState, "Base Layer.Dash", 0.9f);
    }

    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit() 
    {
        player.ctr.isBehavior = false;      // �ൿ ���� ����
        player.ctr.isNotAttack = false;    // ���� ���� ����
    }
}
