using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public EState state = EState.None;
    public float moveSpeed;         // �̵� �ӵ�
    public float rotationSpeed;     // ȸ�� �ӵ�
    public int power;               // ��
    public int maxHp;               // �ִ� ü��
    [HideInInspector]
    public int hp;                  // ���� ü��
    [HideInInspector]
    public StateMachine stateMachine;    // ������Ʈ �ӽ�
    [HideInInspector]
    public Animator ani;            // �ִϸ��̼�

    public Dictionary<EState, State> stateDic;

    protected virtual void Awake()
    {
        hp = maxHp;
        ani = GetComponentInChildren<Animator>();
        stateDic = new Dictionary<EState, State>();
    }

    protected virtual void Update()
    {
        // -----------------------------------------------------
        // ------------StateMachine LogicUpdate-----------------
        // -----------------------------------------------------
        stateMachine.currentState.LogicUpdate();
        // -----------------------------------------------------
    }

    // �ǰ� 
    public virtual bool Hit() { return false; }
    // ���
    public virtual bool Death() { return false; }
    // �̵�
    public virtual bool Move() { return false; }
    // ����
    public virtual bool Attack() { return false; }


    // �̵�
    public void Ani_Movement(bool isMove)
    {
        ani.SetBool("Movement", isMove);
    }
    // �뽬
    public void Ani_Dash()
    {
        ani.SetTrigger("TriggerDash");
    }
    // ���
    public void Ani_Idle()
    {
        ani.SetBool("Idle", true);
    }
    // ����
    public void Ani_Attack()
    {
        ani.SetTrigger("TriggerAttack");
    }
    // �ǰ�
    public void Ani_Hit()
    {
        ani.SetTrigger("TriggerHit");
    }
    // �ǰ� �� ������ ����
    public void Ani_Damage_Hit(Character character, float damage)
    {
        
    }
    // ���
    public void Ani_Death()
    {
        ani.SetTrigger("TriggerDeath");
    }

    // ���� �븻����
    public void Ani_EnemyAttack(int n)
    {
        ani.SetTrigger("Attack_Slash" + n);
    }
}
