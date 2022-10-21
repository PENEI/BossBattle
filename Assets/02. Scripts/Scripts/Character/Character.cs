using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public EState state = EState.None;
    public float moveSpeed;         // 이동 속도
    public float rotationSpeed;     // 회전 속도
    public int power;               // 힘
    public int maxHp;               // 최대 체력
    [HideInInspector]
    public int hp;                  // 현재 체력
    [HideInInspector]
    public StateMachine stateMachine;    // 스테이트 머신
    [HideInInspector]
    public Animator ani;            // 애니메이션

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

    // 피격 
    public virtual bool Hit() { return false; }
    // 사망
    public virtual bool Death() { return false; }
    // 이동
    public virtual bool Move() { return false; }
    // 공격
    public virtual bool Attack() { return false; }


    // 이동
    public void Ani_Movement(bool isMove)
    {
        ani.SetBool("Movement", isMove);
    }
    // 대쉬
    public void Ani_Dash()
    {
        ani.SetTrigger("TriggerDash");
    }
    // 대기
    public void Ani_Idle()
    {
        ani.SetBool("Idle", true);
    }
    // 공격
    public void Ani_Attack()
    {
        ani.SetTrigger("TriggerAttack");
    }
    // 피격
    public void Ani_Hit()
    {
        ani.SetTrigger("TriggerHit");
    }
    // 피격 시 데미지 감소
    public void Ani_Damage_Hit(Character character, float damage)
    {
        
    }
    // 사망
    public void Ani_Death()
    {
        ani.SetTrigger("TriggerDeath");
    }

    // 몬스터 노말공격
    public void Ani_EnemyAttack(int n)
    {
        ani.SetTrigger("Attack_Slash" + n);
    }
}
