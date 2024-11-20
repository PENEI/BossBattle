using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    private Transform focus = default;  // 타겟
    [SerializeField, Range(0f, 20f)]
    private float distance;             // 카메라와 타겟의 거리 x축
    [SerializeField, Range(0f, 20f)]
    private float hight;                // 카메라와 타겟의 거리 y축
    [SerializeField, Min(0.1f)]
    private float focusRadius;          // 카메라가 감속하는 거리
    [SerializeField, Range(0f, 1f)]
    private float focusCentering;       // 카메라 감속하는 속도
    [SerializeField, Range(1f, 360f)]
    private float rotationSpeed = 90f;  // 카메라 회전 속도
    [Header("[각도 제한]"), SerializeField, Range(-89f, 89f)]
    private float minVerticalAngle;     // 카메라 y축 최소 회전 각도
    [SerializeField, Range(-89f, 89f)]
    private float maxVerticalAngle;     // 카메라 y축 최대 회전 각도

    public Vector3 orbitAngles;         // 오브젝트의 회전 값
    private Vector3 focusPoint;         // 타겟 위치 값  




    [SerializeField, Min(0f)]
    private float alignDelay = 5f;
    float lastManualRotationTime;

    private void Awake()
    {
        focusPoint = focus.position;        //타겟 위치
        orbitAngles = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();     // 카메라 위치
        Quaternion lookRotation;

        // 카메라 회전
        if (ManualRotation()|| AutomaticRotation()) 
        {
            ConstrainAngle();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else
        {
            lookRotation = transform.localRotation;
        }

        Vector3 lookDirectionZ = (Vector3.forward * distance);   // 월드 좌표에서 z축 방향
        Vector3 lookDirectionY = (Vector3.down * hight);           // 월드 좌표에서 y축 방향

        // 오브젝트 위치 계산
        Vector3 lookPosition = focusPoint - lookDirectionZ - lookDirectionY;

        //transform.position = lookPosition;              // 오브젝트 위치
        transform.rotation = lookRotation;   // 메인 카메라 회전
    }

    public void UpdateFocusPoint()
    {
        Vector3 targetPoint = focus.position;

        // 카메라가 가야할위치 - 카메라 위치
        float distance = Vector3.Distance(targetPoint, focusPoint);
        // 카메라의 감속 시간
        float t = 1f;

        /* 
         * 카메라와 목적지의 거리가 0.01f 초과면 계속해서 감속
         * focusCentering가 0보다 크면 감속
         */
        if (distance > 0.01f && focusCentering > 0f) 
        {
            /* 
             * 감속하는 시간 계산 
             * TimeScale 조정 시 카메라가 제자리에 못돌아올 때를 대비해서 deltaTime이 아닌 unscaledDeltaTime 사용
             */
            t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
        }

        // 카메라가 이동해야하는거리가 focusRadius보다 멀면 카메라가 이동
        if (distance > focusRadius)
        {
            // 다음 감속하는 시간 계산
            t = Mathf.Min(t, focusRadius / distance);
        }

        // 카메라를 부드럽게 이동
        focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
    }

    // 마우스 회전
    private bool ManualRotation()
    {
        // 마우스 위치 x, y
        Vector3 input = new Vector3(Input.GetAxis("Mouse Y")*-1, Input.GetAxis("Mouse X"),0);

        const float e = 0.001f;

        // 카메라 회전
        if (input.x < -e || input.x > e || input.y < -e || input.y > e) 
        {
            // 카메라 회전
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledTime;     // 플레이 이후 경과 시간
            return true;
        }
        return false;
    }

    // 카메라의 회전 제한
    private void ConstrainAngle()
    {
        // orbitAngles.x가 minVerticalAngle보다 크고, maxVerticalAngle보다 작도록 제한
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        // 마우스가 X축으로 360도 회전 가능하도록 하는 조건
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

    // 변수 값이 변경될 때 실행
    private void OnValidate()
    {
        // max가  min보다 작아지지 않도록 제한
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

}