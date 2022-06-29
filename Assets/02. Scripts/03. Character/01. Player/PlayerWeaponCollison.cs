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
        // 아직 무기와 충돌 하지않았을 때
        if (ctr.isWeaponHit)
        {
            Debug.Log(other.name);
            // 플레이어와 충돌 시 플레이어 애니메이션 재생 및 체력 감소
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("enemyHit");
                enemy = other.GetComponent<EnemyController>();
                // 데미지
                enemy.Ani_Damage_Hit(enemy, ctr.attakPower);
                ctr.isWeaponHit = false;    // 무기 충돌이 다시 가능하도록 변환
            }
        }
    }
}
