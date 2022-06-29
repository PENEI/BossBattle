using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField]
    private float rotationSpeed;    // 플레이어 회전 속도

    #region -상태 변수-
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

    #region -bool체크 변수-
    [HideInInspector]
    public bool isDeath = false;            // 사망
    [HideInInspector]
    public bool isHit = false;              // 피격
    [HideInInspector]
    public bool isBehavior = false;          // 애니메이션 실행 가능 여부
    [HideInInspector]
    public bool isNotAttack = false;        // 공격 불가 상태
    [HideInInspector]
    public bool isWeaponHit = false;        // 무기의 충돌 가능 여부
    #endregion

    [HideInInspector]
    public GameObject playerObj;   // 자식객체로 있는 플레이어 오브젝트

    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = false;     // 마우스 비활성화
        playerObj = transform.Find("Player").gameObject;    //자식 객체 중에 "Player"이름의 오브젝트 저장
    }

    protected override void Start()
    {
        base.Start();
        dashState = new DashState(this, stateMachine);      // 대쉬 상태 생성
        idleState = new IdleState(this, stateMachine);      // 대기 상태 생성
        moveState = new MoveState(this, stateMachine);      // 이동 상태 생성
        deathState = new DeathState(this, stateMachine);    // 사망 상태 생성
        hitState = new HitState(this, stateMachine);        // 피격 상태 생성
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
        
        // 사망 상태 시 더 이상 실행 X
        if(isDeath)
        {
            return;
        }
        //--------------------------------------

        KeyDownCharacterRotation();     // 플레이어 회전

        if (!isBehavior)
        {
            // 피격 시
            if (isHit)
            {
                stateMachine.ChangeState(hitState);
            }
            // 마우스 왼쪽 입력 (공격)
            else if (Input.GetMouseButtonDown(0) && !isNotAttack)
            {
                stateMachine.ChangeState(attackStateCombo1);
            }
            // Shift입력 시 대쉬
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                stateMachine.ChangeState(dashState);
            }
            // 이동 중일 경우 상태 변경
            else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                stateMachine.ChangeState(moveState);
            }
            
        }
    }
    
    // 방향키를 누를 시 회전
    public void KeyDownCharacterRotation()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            // 다음 이동방향을 Quaternion 값으로 변환
            Quaternion rotation = Quaternion.LookRotation(moveState.CharacterMoveDir());

            // rotation 방향을 rotationSpeed 속도로 부드럽게 회전
            playerObj.transform.rotation = 
                Quaternion.Slerp(playerObj.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
