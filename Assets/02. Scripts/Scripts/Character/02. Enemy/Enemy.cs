using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public bool isBattle;           // ���� ���� üũ
    public bool isDeath;            // ��� ����
    [HideInInspector]
    public GameObject rotObj;       // ȸ���� ������Ʈ
    public NavMeshAgent agent;

    [Header("[ ���� ]")]
    public float searchRadius;      // Ž�� ���� ������
    public float stoppingRadius;    // ������Ʈ���� ���� �Ÿ� ����
    public float normalAttackRadius;// ��Ÿ ���� ���� �ߵ� �Ÿ�

    public List<EState> randomAttackPattern;        // ���� ���� ���� ����Ʈ
    public Dictionary<ESkillType, GameObject> skillDic; // ���� ��ų ��ųʸ�
    public EnemySkillSCT fireBallSCT;               // ��ų ��ũ���ͺ�
    public EnemySkillSCT RangeSpellSCT;             // ��ų ��ũ���ͺ�

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
        agent = GetComponent<NavMeshAgent>();
        rotObj = transform.gameObject;
        // ��ųʸ��� ���� �߰�
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
        CGizmo.DrawGizmosCircle(transform.position, searchRadius, Color.green);     // �÷��̾� �߰� ����
        CGizmo.DrawGizmosCircle(transform.position, stoppingRadius, Color.white);   // Ÿ�ٰ��� ���� �Ÿ� ����
        CGizmo.DrawGizmosCircle(transform.position, normalAttackRadius, Color.red); // �޺� ���� �ߵ� �Ÿ�
    }

    // �ǰ�
    public override bool Hit()
    {
        return false;
    }
    // ���
    public override bool Death()
    {
        return false;
    }
    // �̵�  
    public override bool Move()
    {
        return false;
    }
    // ����
    public override bool Attack()
    {
        return false;
    }
}
