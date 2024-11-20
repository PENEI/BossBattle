using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public bool isDeath;            // 사망 여부
    public bool isHit;              // 피격 여부
    public static Player instance;    // 싱글톤 패턴
    [HideInInspector]
    public GameObject rotObj; // 회전할 플레이어 오브젝트(Player오브젝트의 자식오브젝트 Player)
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

    // Player 싱글톤 생성
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

    #region -이동-
    /// <summary>
    /// 방향키 입력 시 플레이어 회전 및 이동
    /// </summary>
    public void KeyDownCharacterRotation()
    {
        // 다음 이동 위치
        Vector3 stepPos = CharacterMoveDir();
        // 다음 위치로 회전
        Quaternion rotation = Quaternion.LookRotation(stepPos);
        // 이동
        
        // rotation 방향을 rotationSpeed 속도로 부드럽게 회전
        rotObj.transform.rotation =
            Quaternion.Slerp(rotObj.transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.position += stepPos;
    }

    /// <summary>
    /// 캐릭터가 이동할 위치 및 카메라 방향에 따른 캐릭터 회전
    /// </summary>
    public Vector3 CharacterMoveDir()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  // x축
        float vertical = Input.GetAxisRaw("Vertical");      // y축

        // 방향키를 입력 받고 있는지 체크
        bool isMove = ((vertical != 0) || (horizontal != 0));
        //character.Ani_Movement(isMove);

        // 현재 카메라가 보는 방향
        Quaternion camRot = cam.transform.rotation;
        // Player 이동 방향
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 dir = (camRot * moveDir);
        Vector3 playerDir = dir * Time.deltaTime * moveSpeed;
        playerDir.y = 0;  // x축으로 회전 하지 않도록 0을 저장

        return playerDir; // 이동 거리
    }
    #endregion
}
