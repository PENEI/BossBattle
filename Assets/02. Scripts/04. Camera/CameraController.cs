using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 camDis;     // ���� ī�޶� ��ġ
    [SerializeField]
    private float rotationSpeed;// ������Ʈ ȸ�� �ӵ�
    [SerializeField]
    private float camAngle;     // ���� ī�޶� ����

    [Header("[���� ����]"), SerializeField, Range(-89f, 89f)]
    private float minVerticalAngle;     // ī�޶� y�� �ּ� ȸ�� ����
    [SerializeField, Range(-89f, 89f)]
    private float maxVerticalAngle;     // ī�޶� y�� �ִ� ȸ�� ����

    private Vector3 angle;      // ȸ�� ����
    private Camera cam;         // ���� ī�޶�
    
    private void Awake()
    {
        cam = Camera.main;  // ���� ī�޶� �޾ƿ���
        cam.transform.localRotation = Quaternion.Euler(Vector3.right * camAngle);   // ī�޶��� ���� �ʱ�ȭ
        cam.transform.localPosition = camDis;    // ī�޶� ��ġ ����
    }

    private void LateUpdate()
    {
        Quaternion lookRotation;        // ȸ�� ����

        // ī�޶� ȸ�� ���
        if (ManualRotation())
        {
            ConstrainAngle();
            // �÷��̾ ���ƾ��ϴ� ȸ�� �� (Vecotr3)�� Quaternion���� ��ȯ
            lookRotation = Quaternion.Euler(angle); 
        }
        else
        {
            lookRotation = transform.localRotation;
        }

        // ī�޶� ȸ�� ����
        transform.rotation = lookRotation;
    }

    // ���콺 ȸ�� ���� ���
    private bool ManualRotation()
    {
        // ���콺 ��ġ x, y
        Vector3 input = new Vector3(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X"), 0);

        const float e = 0.001f; // ���콺�� ���������� ���ϱ� ���� ����

        // ī�޶� ȸ��
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            // ī�޶� ȸ��
            angle += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }
        return false;
    }
    // ī�޶��� ȸ�� ����
    private void ConstrainAngle()
    {
        // orbitAngles.x�� minVerticalAngle���� ũ��, maxVerticalAngle���� �۵��� ����
        angle.x = Mathf.Clamp(angle.x, minVerticalAngle, maxVerticalAngle);

        // ���콺�� 360��ȸ�� ���� ��� �ٽ� 0���� ����
        angle.y += angle.y < 0f ? 360 : -360;
    }
}
