using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public bool isBattle;           // 전투 상태 체크
    public bool isDeath;            // 사망 여부
    [HideInInspector]
    public GameObject rotObj;       // 회전할 오브젝트
    public NavMeshAgent agent;

    [Header("[ 범위 ]")]
    public float searchRadius;      // 탐색 범위 반지름
    public float stoppingRadius;    // 오브젝트간의 접근 거리 제한
    public float normalAttackRadius;// 평타 공격 패턴 발동 거리

    public List<EState> randomAttackPattern;        // 몬스터 공격 패턴 리스트
    public Dictionary<ESkillType, GameObject> skillDic; // 몬스터 스킬 딕셔너리
    public EnemySkillSCT fireBallSCT;               // 스킬 스크립터블
    public EnemySkillSCT RangeSpellSCT;             // 스킬 스크립터블

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
        agent = GetComponent<NavMeshAgent>();
        rotObj = transform.gameObject;
        // 딕셔너리에 상태 추가
        stateDic.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
        stateDic.Add(EState.Death, new EnemyDeathState(this, stateMachine, EState.Death));
        stateDic.Add(EState.SearchAttack, new EnemySearchAttackState(this, stateMachine, EState.SearchAttack));
        stateDic.Add(EState.Teleport, new EnemyTeleportState(this, stateMachine, EState.Teleport));
        stateDic.Add(EState.NormalAttack0, new EnemyNormalAttack0State(this, stateMachine, EState.NormalAttack0));
        stateDic.Add(EState.NormalAttack1, new EnemyNormalAttack1State(this, stateMachine, EState.NormalAttack1));
        stateDic.Add(EState.NormalAttack2, new EnemyNormalAttack2State(this, stateMachine, EState.NormalAttack2));
        stateDic.Add(EState.MagicAttack0, new EnemyMagicAttack0State(this, stateMachine, EState.MagicAttack0));
        stateDic.Add(EState.MagicAttack1, new EnemyMagicAttack1State(this, stateMachine, EState.MagicAttack1));

        skillDic = new Dictionary<ESkillType, GameObject>();
        skillDic.Add(ESkillType.FireBall, Resources.Load<GameObject>("01.Prefab/FireBall"));
        skillDic.Add(ESkillType.RangeSpell, Resources.Load<GameObject>("01.Prefab/RangeSkill"));

        SMemoryPool.Instance.MemoryPoolDic.Add(ESkillObjType.FireBall, new MemoryPool(fireBallSCT.obj));
        SMemoryPool.Instance.MemoryPoolDic.Add(ESkillObjType.RangeSpell, new MemoryPool(RangeSpellSCT.obj));
        SMemoryPool.Instance.MemoryPoolDic.Add(ESkillObjType.FireBallImpact, new MemoryPool(fireBallSCT.ImpactObj));
    }
    private void Start()
    {
        agent.speed = moveSpeed;
        agent.stoppingDistance = stoppingRadius;

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

    private void OnDrawGizmos()
    {
        CGizmo.DrawGizmosCircle(transform.position, searchRadius, Color.green);     // 플레이어 발견 범위
        CGizmo.DrawGizmosCircle(transform.position, stoppingRadius, Color.white);   // 타겟과의 점근 거리 제한
        CGizmo.DrawGizmosCircle(transform.position, normalAttackRadius, Color.red); // 콤보 공격 발동 거리
    }

    // 피격
    public override bool Hit()
    {
        return false;
    }
    // 사망
    public override bool Death()
    {
        return false;
    }
    // 이동  
    public override bool Move()
    {
        return false;
    }
    // 공격
    public override bool Attack()
    {
        return false;
    }
}
