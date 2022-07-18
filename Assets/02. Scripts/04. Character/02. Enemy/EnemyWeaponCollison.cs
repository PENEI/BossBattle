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
        // 아직 무기와 충돌 하지않았을 때
        if (enemy.isWeaponHit)
        {
            // 플레이어와 충돌 시 플레이어 애니메이션 재생 및 체력 감소
            if (other.CompareTag("Player"))
            {
                // 데미지
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
