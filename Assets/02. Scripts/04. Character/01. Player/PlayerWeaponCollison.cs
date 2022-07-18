using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponCollison : MonoBehaviour
{
    private PlayerController ctr;
    private EnemyController enemy;

    private void Awake()
    {
        ctr = Player.Instance.ctr;
    }

    private void OnTriggerStay(Collider other)
    {
        // ���� ����� �浹 �����ʾ��� ��
        if (ctr.isWeaponHit)
        {
            Debug.Log(other.name);
            // �÷��̾�� �浹 �� �÷��̾� �ִϸ��̼� ��� �� ü�� ����
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("enemyHit");
                enemy = other.GetComponent<EnemyController>();
                // ������
                enemy.Ani_Damage_Hit(enemy, ctr.attakPower);
                ctr.isWeaponHit = false;    // ���� �浹�� �ٽ� �����ϵ��� ��ȯ
            }
        }
    }
}
