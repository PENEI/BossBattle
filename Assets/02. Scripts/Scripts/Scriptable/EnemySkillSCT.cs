using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySkill")]
public class EnemySkillSCT : ScriptableObject
{
    public ESkillType type;         // 공격 타입
    public float power;             // 공격력
    public GameObject obj;          // 공격 오브젝트
    public GameObject ImpactObj;    // 충돌 시 출력될 오브젝트
}
