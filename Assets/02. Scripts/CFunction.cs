using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CFunction
{
    //a�� b�� �Ÿ� ���ϱ�
    public static float GetDistance(Vector3 a, Vector3 b)
    {
        //�÷��̾���� �Ÿ��� ���� �� return
        return (a - b).sqrMagnitude;
    }
}
