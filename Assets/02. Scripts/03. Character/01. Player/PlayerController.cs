using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField]
    private float rotationSpeed;    // �÷��̾� ȸ�� �ӵ�

    #region -���� ����-
    [HideInInspector]
    public DashState dashState; public float dashSpeed;
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public MoveState moveState;
    [HideInInspector]
    public DeathState deathState;
    [HideInInspector]
    public HitState hitState;
    [HideInInspector]
    public AttackState_Combo1 attackStateCombo1;
    [HideInInspector]
    public AttackState_Combo2 attackStateCombo2;
    [HideInInspector]
    public AttackState_Combo3 attackStateCombo3;
    [HideInInspector]
    public AttackState_Combo4 attackStateCombo4;
    #endregion

    #region -boolüũ ����-
    [HideInInspector]
    public bool isDeath = false;            // ���
    [HideInInspector]
    public bool isHit = false;              // �ǰ�
    [HideInInspector]
    public bool isBehavior = false;          // �ִϸ��̼� ���� ���� ����
    [HideInInspector]
    public bool isNotAttack = false;        // ���� �Ұ� ����
    [HideInInspector]
    public bool isWeaponHit = false;        // ������ �浹 ���� ����
    #endregion

    [HideInInspector]
    public GameObject playerObj;   // �ڽİ�ü�� �ִ� �÷��̾� ������Ʈ

    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = false;     // ���콺 ��Ȱ��ȭ
        playerObj = transform.Find("Player").gameObject;    //�ڽ� ��ü �߿� "Player"�̸��� ������Ʈ ����
    }

    protected override void Start()
    {
        base.Start();
        dashState = new DashState(this, stateMachine);      // �뽬 ���� ����
        idleState = new IdleState(this, stateMachine);      // ��� ���� ����
        moveState = new MoveState(this, stateMachine);      // �̵� ���� ����
        deathState = new DeathState(this, stateMachine);    // ��� ���� ����
        hitState = new HitState(this, stateMachine);        // �ǰ� ���� ����
        attackStateCombo1 = new AttackState_Combo1(this, stateMachine);
        attackStateCombo2 = new AttackState_Combo2(this, stateMachine);
        attackStateCombo3 = new AttackState_Combo3(this, stateMachine);
        attackStateCombo4 = new AttackState_Combo4(this, stateMachine);
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        if (!isDeath && hp <= 0) 
        {
            stateMachine.ChangeState(deathState);
        }

        AnimatorStateInfo aniStateInfo = ani.GetCurrentAnimatorStateInfo(0);

        // --------StateMachine-----------------
        stateMachine.currentState.LogicUpdate();
        //--------------------------------------
        
        // ��� ���� �� �� �̻� ���� X
        if(isDeath)
        {
            return;
        }
        //--------------------------------------

        KeyDownCharacterRotation();     // �÷��̾� ȸ��

        if (!isBehavior)
        {
            // �ǰ� ��
            if (isHit)
            {
                stateMachine.ChangeState(hitState);
            }
            // ���콺 ���� �Է� (����)
            else if (Input.GetMouseButtonDown(0) && !isNotAttack)
            {
                stateMachine.ChangeState(attackStateCombo1);
            }
            // Shift�Է� �� �뽬
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                stateMachine.ChangeState(dashState);
            }
            // �̵� ���� ��� ���� ����
            else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                stateMachine.ChangeState(moveState);
            }
            
        }
    }
    
    // ����Ű�� ���� �� ȸ��
    public void KeyDownCharacterRotation()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            // ���� �̵������� Quaternion ������ ��ȯ
            Quaternion rotation = Quaternion.LookRotation(moveState.CharacterMoveDir());

            // rotation ������ rotationSpeed �ӵ��� �ε巴�� ȸ��
            playerObj.transform.rotation = 
                Quaternion.Slerp(playerObj.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
