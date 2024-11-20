using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public bool isDeath;            // ��� ����
    public bool isHit;              // �ǰ� ����
    public static Player instance;    // �̱��� ����
    [HideInInspector]
    public GameObject rotObj; // ȸ���� �÷��̾� ������Ʈ(Player������Ʈ�� �ڽĿ�����Ʈ Player)
    private Camera cam;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
        InstancePlayer();
        rotObj = transform.Find("Player").gameObject;
        cam = Camera.main;
        
    }
    private void Start()
    {
        stateDic.Add(EState.Idle, new PlayerIdleState(this, stateMachine, EState.Idle));
        stateDic.Add(EState.Move, new PlayerMoveState(this, stateMachine, EState.Move));
        stateDic.Add(EState.Hit, new PlayerHitState(this, stateMachine, EState.Hit));
        stateDic.Add(EState.Death, new PlayerDeathState(this, stateMachine, EState.Death));
        stateDic.Add(EState.Attack0, new AttackState0(this, stateMachine, EState.Attack0));
        stateDic.Add(EState.Attack1, new AttackState1(this, stateMachine, EState.Attack1));
        stateDic.Add(EState.Attack2, new AttackState2(this, stateMachine, EState.Attack2));
        stateDic.Add(EState.Attack3, new AttackState3(this, stateMachine, EState.Attack3));

        stateMachine.Initialize(stateDic[EState.Idle]);
    }

    protected override void Update()
    {
        base.Update();
        if (hp <= 0 && !isDeath)
        {
            stateMachine.ChangeState(stateDic[EState.Death]);
        }
    }

    public override bool Hit()
    {
        return false;
    }
    public override bool Death()
    {
        if (hp <= 0)
        {
            return true;
        }
        return false;
    }
    public override bool Move()
    {
        if (!isDeath)
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                return true;
            }
        }
        return false;
    }
    
    public override bool Attack()
    {
        if (!isDeath)
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
        }
        return false;
    }

    // Player �̱��� ����
    private void InstancePlayer()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) 
        {
            Destroy(instance);
        }
    }

    #region -�̵�-
    /// <summary>
    /// ����Ű �Է� �� �÷��̾� ȸ�� �� �̵�
    /// </summary>
    public void KeyDownCharacterRotation()
    {
        // ���� �̵� ��ġ
        Vector3 stepPos = CharacterMoveDir();
        // ���� ��ġ�� ȸ��
        Quaternion rotation = Quaternion.LookRotation(stepPos);
        // �̵�
        
        // rotation ������ rotationSpeed �ӵ��� �ε巴�� ȸ��
        rotObj.transform.rotation =
            Quaternion.Slerp(rotObj.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.position += stepPos;
    }

    /// <summary>
    /// ĳ���Ͱ� �̵��� ��ġ �� ī�޶� ���⿡ ���� ĳ���� ȸ��
    /// </summary>
    public Vector3 CharacterMoveDir()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  // x��
        float vertical = Input.GetAxisRaw("Vertical");      // y��

        // ����Ű�� �Է� �ް� �ִ��� üũ
        bool isMove = ((vertical != 0) || (horizontal != 0));
        //character.Ani_Movement(isMove);

        // ���� ī�޶� ���� ����
        Quaternion camRot = cam.transform.rotation;
        // Player �̵� ����
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 dir = (camRot * moveDir);
        Vector3 playerDir = dir * Time.deltaTime * moveSpeed;
        playerDir.y = 0;  // x������ ȸ�� ���� �ʵ��� 0�� ����

        return playerDir; // �̵� �Ÿ�
    }
    #endregion
}
