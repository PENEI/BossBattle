using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum eState
    { 
        None,   
        Idle,   // ���
        Move,   // �̵�
        Attack, // ����
        Hit,    // �ǰ�
        Dash,   // ������(�뽬)
        Death,  // ���
        Max
    }
    
    [HideInInspector]
    public Animator ani;       // �ִϸ��̼� ����
    [HideInInspector]
    public StateMachine stateMachine;    // ������Ʈ �ӽ�

    public eState state;
    public float moveSpeed;     // �÷��̾� �̵� �ӵ�
    private float maxHp;         // �ִ� ü��
    public float hp;            // ���� ü��
    public float attakPower;    // ���ݷ�

    protected virtual void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        stateMachine = new StateMachine();
        maxHp = hp;
    }

    //-------------------------------
    //-----------Animation-----------
    //-------------------------------

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
        character.hp -= damage;
    }
    // ���
    public void Ani_Death()
    {
        ani.SetTrigger("TriggerDeath");
    }

    //-------------------------------
    //-----------Enemy---------------
    //-------------------------------

    public void Ani_Attack_Slash1()
    {
        ani.SetTrigger("Attack_Slash1");
    }
    public void Ani_Attack_Slash2()
    {
        ani.SetTrigger("Attack_Slash2");
    }
    public void Ani_Attack_Slash3()
    {
        ani.SetTrigger("Attack_Slash3");
    }
    public void Ani_Move_Teleport()
    {
        ani.SetTrigger("Move_Teleport");
    }
    public void Ani_Attack_CastSpell1()
    {
        ani.SetTrigger("Attack_CastSpell1");
    }
    public void Ani_Attack_CastSpell2()
    {
        ani.SetTrigger("Attack_CastSpell2");
    }
    protected void Ani_EnemyMove(float velocityZ, float velocityX)
    {
        ani.SetFloat("Horizontal", velocityX);
        ani.SetFloat("Vertical", velocityZ);
    }
}
