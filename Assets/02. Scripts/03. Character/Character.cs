using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum eState
    { 
        None,   
        Idle,   // 대기
        Move,   // 이동
        Attack, // 공격
        Hit,    // 피격
        Dash,   // 구르기(대쉬)
        Death,  // 사망
        Max
    }
    
    [HideInInspector]
    public Animator ani;       // 애니메이션 정보
    [HideInInspector]
    public StateMachine stateMachine;    // 스테이트 머신

    public eState state;
    public float moveSpeed;     // 플레이어 이동 속도
    private float maxHp;         // 최대 체력
    public float hp;            // 현재 체력
    public float attakPower;    // 공격력

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
        character.hp -= damage;
    }
    // 사망
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
