using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    private Transform focus = default;  // Ÿ��
    [SerializeField, Range(0f, 20f)]
    private float distance;             // ī�޶�� Ÿ���� �Ÿ� x��
    [SerializeField, Range(0f, 20f)]
    private float hight;                // ī�޶�� Ÿ���� �Ÿ� y��
    [SerializeField, Min(0.1f)]
    private float focusRadius;          // ī�޶� �����ϴ� �Ÿ�
    [SerializeField, Range(0f, 1f)]
    private float focusCentering;       // ī�޶� �����ϴ� �ӵ�
    [SerializeField, Range(1f, 360f)]
    private float rotationSpeed = 90f;  // ī�޶� ȸ�� �ӵ�
    [Header("[���� ����]"), SerializeField, Range(-89f, 89f)]
    private float minVerticalAngle;     // ī�޶� y�� �ּ� ȸ�� ����
    [SerializeField, Range(-89f, 89f)]
    private float maxVerticalAngle;     // ī�޶� y�� �ִ� ȸ�� ����

    public Vector3 orbitAngles;         // ������Ʈ�� ȸ�� ��
    private Vector3 focusPoint;         // Ÿ�� ��ġ ��  




    [SerializeField, Min(0f)]
    private float alignDelay = 5f;
    float lastManualRotationTime;

    private void Awake()
    {
        focusPoint = focus.position;        //Ÿ�� ��ġ
        orbitAngles = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();     // ī�޶� ��ġ
        Quaternion lookRotation;

        // ī�޶� ȸ��
        if (ManualRotation()|| AutomaticRotation()) 
        {
            ConstrainAngle();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }

        Vector3 lookDirectionZ = (Vector3.forward * distance);   // ���� ��ǥ���� z�� ����
        Vector3 lookDirectionY = (Vector3.down * hight);           // ���� ��ǥ���� y�� ����

        // ������Ʈ ��ġ ���
        Vector3 lookPosition = focusPoint - lookDirectionZ - lookDirectionY;

        //transform.position = lookPosition;              // ������Ʈ ��ġ
        transform.rotation = lookRotation;   // ���� ī�޶� ȸ��
    }

    public void UpdateFocusPoint()
    {
        Vector3 targetPoint = focus.position;

        // ī�޶� ��������ġ - ī�޶� ��ġ
        float distance = Vector3.Distance(targetPoint, focusPoint);
        // ī�޶��� ���� �ð�
        float t = 1f;

        /* 
         * ī�޶�� �������� �Ÿ��� 0.01f �ʰ��� ����ؼ� ����
         * focusCentering�� 0���� ũ�� ����
         */
        if (distance > 0.01f && focusCentering > 0f) 
        {
            /* 
             * �����ϴ� �ð� ��� 
             * TimeScale ���� �� ī�޶� ���ڸ��� �����ƿ� ���� ����ؼ� deltaTime�� �ƴ� unscaledDeltaTime ���
             */
            t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
        }

        // ī�޶� �̵��ؾ��ϴ°Ÿ��� focusRadius���� �ָ� ī�޶� �̵�
        if (distance > focusRadius)
        {
            // ���� �����ϴ� �ð� ���
            t = Mathf.Min(t, focusRadius / distance);
        }

        // ī�޶� �ε巴�� �̵�
        focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
    }

    // ���콺 ȸ��
    private bool ManualRotation()
    {
        // ���콺 ��ġ x, y
        Vector3 input = new Vector3(Input.GetAxis("Mouse Y")*-1, Input.GetAxis("Mouse X"),0);

        const float e = 0.001f;

        // ī�޶� ȸ��
        if (input.x < -e || input.x > e || input.y < -e || input.y > e) 
        {
            // ī�޶� ȸ��
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledTime;     // �÷��� ���� ��� �ð�
            return true;
        }
        return false;
    }

    // ī�޶��� ȸ�� ����
    private void ConstrainAngle()
    {
        // orbitAngles.x�� minVerticalAngle���� ũ��, maxVerticalAngle���� �۵��� ����
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        // ���콺�� X������ 360�� ȸ�� �����ϵ��� �ϴ� ����
        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y >= 360) 
        {
            orbitAngles.y -= 360f;
        }
    }

    bool AutomaticRotation()
    {
        if (Time.unscaledTime - lastManualRotationTime < alignDelay)
        {
            return false;
        }

        return true;
    }

    // ���� ���� ����� �� ����
    private void OnValidate()
    {
        // max��  min���� �۾����� �ʵ��� ����
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

}