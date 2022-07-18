using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    public int attackKind = -1;    // ���� ���� ����

    [Header("[ ���� ]")]
    public float searchRadius;   // Ž�� ���� ������
    public float stoppingRadius;   // ������Ʈ�� �����Ҷ� �����ϴ� �Ÿ�
    public float weaponAttackRadius;// ���� ���� ���� �ߵ� �Ÿ�

    [HideInInspector]
    protected GameObject player;    // �÷��̾� ������Ʈ
    [HideInInspector]
    public NavMeshAgent agent;      // ��ã�� �ý���

    #region -���� ����-
    [HideInInspector]
    public Enemy_IdleState idleState;       // ��� ����
    [HideInInspector]
    public Enemy_MovementState moveState;   // �̵� ����
    [HideInInspector]
    public Enemy_DeathState deathState;     // ��� ����
    [HideInInspector]
    public Enemy_Attack_Slash1 attackSlash1;  // ���1 ���� ����
    [HideInInspector]
    public Enemy_Attack_Slash2 attackSlash2;  // ���2 ���� ����
    [HideInInspector]
    public Enemy_Attack_Slash3 attackSlash3;  // ���3 ���� ����
    [HideInInspector]
    public Enemy_Attack_CastSpell1 attackCastSpell1;  // ���� ����1 ����
    [HideInInspector]
    public Enemy_Attack_CastSpell2 attackCastSpell2;  // ���� ����2 ����
    [HideInInspector]
    public Enemy_Move_Teleport moveTeleport;  // ���� �̵�
    #endregion

    [Header("[��ų ���� ��ũ���ͺ�]")]
    // ĳ���� ���� A�� ��ũ���ͺ�(����)
    public Enemy_Attack_Scriptable attack_CSpellA_Scpt;      
    // ĳ���� ���� B
    public Enemy_Attack_Scriptable attack_CSpellB_Scpt;    

    #region -boolüũ ����-
    [HideInInspector]
    public bool isPlaying = false;
    [HideInInspector]
    public bool isDeath = false;    // ���
    [HideInInspector]
    public bool isWeaponHit = false;// ���� ���� ����
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

        // ����� ���� ����
        NewStateBunch();

        // �ʱ� ���¸� ������(idleState)�� ����
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        // Enemy�� x�� �̵� ��
        float velocityX = agent.velocity.x;     
        
        // Enemy�� z�� �̵� ��*�ӵ�
        float velocityZ = 
            agent.velocity.magnitude * moveSpeed; 
        
        // �ִϸ��̼ǿ� x, y�� �̵� �� ����
        Ani_EnemyMove(velocityZ, velocityX);    

        // hp�� 0���ϸ� ��� ó��
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
        CGizmo.DrawGizmosCircle(transform.position, searchRadius, Color.green);     // �÷��̾� �߰� ����
        CGizmo.DrawGizmosCircle(transform.position, stoppingRadius, Color.white);                             // Ÿ�ٰ��� ���� �Ÿ� ����
        CGizmo.DrawGizmosCircle(transform.position, weaponAttackRadius, Color.red); // �޺� ���� �ߵ� �Ÿ�
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
