using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollison : MonoBehaviour
{
    private PlayerController ctr;
    private EnemyController enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyController>();
        ctr = Player.Instance.ctr;
    }

    private void OnTriggerStay(Collider other)
    {
        // ���� ����� �浹 �����ʾ��� ��
        if (enemy.isWeaponHit)
        {
            // �÷��̾�� �浹 �� �÷��̾� �ִϸ��̼� ��� �� ü�� ����
            if (other.CompareTag("Player"))
            {
                // ������
                ctr.Ani_Damage_Hit(ctr, enemy.attakPower);
                enemy.isWeaponHit = false;
                if (!ctr.isHit)
                {
                    ctr.isHit = true;
                }
            }
        }
    }
}
