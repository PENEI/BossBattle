using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Slash2 : State
{
    private EnemyController enemy;
    private bool isHit = false;

    public Enemy_Attack_Slash2(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        enemy = character as EnemyController;
    }

    // ���� ���� (start)
    public override void Enter()
    {
        enemy.LookPlayer();     // �÷��̾������� ȸ��
        character.state = Character.eState.Attack;
        character.Ani_Attack_Slash2();          // �ִϸ��̼� ����
    }

    // ������ ���� (Update)
    public override void LogicUpdate()
    {
        AnimatorStateInfo aniStateInfo = character.ani.GetCurrentAnimatorStateInfo(0);
        if (!isHit&&aniStateInfo.IsName("Base Layer.Attack_Slash2") && aniStateInfo.normalizedTime >= 0.1)
        {
            enemy.isWeaponHit = true;
            isHit = true;
        }
        stateMachine.AnimationEnd(character, enemy.attackSlash3, "Base Layer.Attack_Slash2", 0.5f);
    }

    // ���� ���� (���� ��ȯ �� ����)
    public override void Exit()
    {
        enemy.isWeaponHit = false;
        isHit = false;
    }
}
