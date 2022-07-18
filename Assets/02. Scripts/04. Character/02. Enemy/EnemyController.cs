using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    public int attackKind = -1;    // 공격 패턴 종류

    [Header("[ 범위 ]")]
    public float searchRadius;   // 탐색 범위 반지름
    public float stoppingRadius;   // 오브젝트에 접근할때 정지하는 거리
    public float weaponAttackRadius;// 무기 공격 패턴 발동 거리

    [HideInInspector]
    protected GameObject player;    // 플레이어 오브젝트
    [HideInInspector]
    public NavMeshAgent agent;      // 길찾기 시스템

    #region -상태 변수-
    [HideInInspector]
    public Enemy_IdleState idleState;       // 대기 상태
    [HideInInspector]
    public Enemy_MovementState moveState;   // 이동 상태
    [HideInInspector]
    public Enemy_DeathState deathState;     // 사망 상태
    [HideInInspector]
    public Enemy_Attack_Slash1 attackSlash1;  // 배기1 공격 상태
    [HideInInspector]
    public Enemy_Attack_Slash2 attackSlash2;  // 배기2 공격 상태
    [HideInInspector]
    public Enemy_Attack_Slash3 attackSlash3;  // 배기3 공격 상태
    [HideInInspector]
    public Enemy_Attack_CastSpell1 attackCastSpell1;  // 마법 공격1 상태
    [HideInInspector]
    public Enemy_Attack_CastSpell2 attackCastSpell2;  // 마법 공격2 상태
    [HideInInspector]
    public Enemy_Move_Teleport moveTeleport;  // 순간 이동
    #endregion

    [Header("[스킬 공격 스크립터블]")]
    // 캐스팅 공격 A의 스크립터블(정보)
    public Enemy_Attack_Scriptable attack_CSpellA_Scpt;      
    // 캐스팅 공격 B
    public Enemy_Attack_Scriptable attack_CSpellB_Scpt;    

    #region -bool체크 변수-
    [HideInInspector]
    public bool isPlaying = false;
    [HideInInspector]
    public bool isDeath = false;    // 사망
    [HideInInspector]
    public bool isWeaponHit = false;// 무기 공격 판정
    #endregion

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        SMemoryPool.Instance.memoryPool_SpellA = new MemoryPool(attack_CSpellA_Scpt.impectObj);
        SMemoryPool.Instance.memoryPool_SpellB = new MemoryPool(attack_CSpellB_Scpt.impectObj);
    }

    protected override void Start()
    {
        base.Start();
        player = Player.Instance.gameObject;
        agent.speed = moveSpeed;
        stoppingRadius = agent.stoppingDistance;

        // 사용할 상태 생성
        NewStateBunch();

        // 초기 상태를 대기상태(idleState)로 설정
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        // Enemy의 x축 이동 값
        float velocityX = agent.velocity.x;     
        
        // Enemy의 z축 이동 값*속도
        float velocityZ = 
            agent.velocity.magnitude * moveSpeed; 
        
        // 애니메이션에 x, y축 이동 값 전달
        Ani_EnemyMove(velocityZ, velocityX);    

        // hp가 0이하면 사망 처리
        if (!isDeath && hp <= 0) 
        {
            stateMachine.ChangeState(deathState);
        }
        // --------StateMachine-----------------
        stateMachine.currentState.LogicUpdate();
        //--------------------------------------
    }

    private void OnDrawGizmos()
    {
        CGizmo.DrawGizmosCircle(transform.position, searchRadius, Color.green);     // 플레이어 발견 범위
        CGizmo.DrawGizmosCircle(transform.position, stoppingRadius, Color.white);                             // 타겟과의 점근 거리 제한
        CGizmo.DrawGizmosCircle(transform.position, weaponAttackRadius, Color.red); // 콤보 공격 발동 거리
    }

    public void LookPlayer()
    {
        transform.LookAt(player.transform);
    }

    private void NewStateBunch()
    {
        idleState = new Enemy_IdleState(this, stateMachine);
        moveState = new Enemy_MovementState(this, stateMachine);
        deathState = new Enemy_DeathState(this, stateMachine);
        attackSlash1 = new Enemy_Attack_Slash1(this, stateMachine);
        attackSlash2 = new Enemy_Attack_Slash2(this, stateMachine);
        attackSlash3 = new Enemy_Attack_Slash3(this, stateMachine);
        attackCastSpell1 = new Enemy_Attack_CastSpell1(this, stateMachine);
        attackCastSpell2 = new Enemy_Attack_CastSpell2(this, stateMachine);
        moveTeleport = new Enemy_Move_Teleport(this, stateMachine);
    }
}
