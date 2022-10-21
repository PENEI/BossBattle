using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySkill")]
public class EnemySkillSCT : ScriptableObject
{
    public ESkillType type;         // ���� Ÿ��
    public float power;             // ���ݷ�
    public GameObject obj;          // ���� ������Ʈ
    public GameObject ImpactObj;    // �浹 �� ��µ� ������Ʈ
}
