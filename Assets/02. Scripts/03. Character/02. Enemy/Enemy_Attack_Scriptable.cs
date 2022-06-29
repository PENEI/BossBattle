using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy_Attack_Scriptable")]
public class Enemy_Attack_Scriptable : ScriptableObject
{
    public GameObject impectObj;    // ���� �� �߻� ����Ʈ
    [Range(1, 10)]
    public int count = 1;           // ���� Ƚ��
}
