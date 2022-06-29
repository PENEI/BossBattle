using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGizmo: MonoBehaviour
{

    //������Ʈ ������ ��(���)�� Gizmo�� �׸�
    /*
     pos = ����� �׸� ��ġ
    radius = ������
    _color = ����� ����
    circleStep = �� (���� �̷�� ���� ����)
    ratioLastPt = ���ϴ� ���� ������ ��ġ
     */

    public static void DrawGizmosCircle
        (Vector3 pos, float radius, Color _color, 
        int circleStep = 20)
    {
        Gizmos.color = _color;

        float theta;
        // n������ �Ѻ� ���� ���ϱ�
        float step = (2f * Mathf.PI) / circleStep;

        Vector3 p0 = pos;
        Vector3 p1 = pos;

        // circleStep������ �� �׸���
        for (int i = 0; i < circleStep; ++i)
        {
            theta = step * i;
            // ���� �������� ��ġ A
            p0.x = pos.x + radius * Mathf.Sin(theta);
            p0.z = pos.z + radius * Mathf.Cos(theta);

            // ���� �������� ��ġ B
            theta = step * (i + 1);
            p1.x = pos.x + radius * Mathf.Sin(theta);
            p1.z = pos.z + radius * Mathf.Cos(theta);

            // A�� B�� �̾ ���� �� �׸���
            Gizmos.DrawLine(p0, p1);
        }
    }
}
