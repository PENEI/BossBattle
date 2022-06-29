using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGizmo: MonoBehaviour
{

    //오브젝트 주위로 원(평면)을 Gizmo로 그림
    /*
     pos = 기즈모 그릴 위치
    radius = 반지름
    _color = 기즈모 색상
    circleStep = 각 (원을 이루는 점의 갯수)
    ratioLastPt = 원하는 원의 꼭지점 위치
     */

    public static void DrawGizmosCircle
        (Vector3 pos, float radius, Color _color, 
        int circleStep = 20)
    {
        Gizmos.color = _color;

        float theta;
        // n각형의 한변 길이 구하기
        float step = (2f * Mathf.PI) / circleStep;

        Vector3 p0 = pos;
        Vector3 p1 = pos;

        // circleStep각형의 원 그리기
        for (int i = 0; i < circleStep; ++i)
        {
            theta = step * i;
            // 원의 꼭지점의 위치 A
            p0.x = pos.x + radius * Mathf.Sin(theta);
            p0.z = pos.z + radius * Mathf.Cos(theta);

            // 원의 꼭지점의 위치 B
            theta = step * (i + 1);
            p1.x = pos.x + radius * Mathf.Sin(theta);
            p1.z = pos.z + radius * Mathf.Cos(theta);

            // A와 B를 이어서 원의 변 그리기
            Gizmos.DrawLine(p0, p1);
        }
    }
}
