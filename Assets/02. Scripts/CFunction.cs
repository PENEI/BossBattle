using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CFunction
{
    //a와 b의 거리 구하기
    public static float GetDistance(Vector3 a, Vector3 b)
    {
        //플레이어와의 거리를 제곱 값 return
        return (a - b).sqrMagnitude;
    }
}
