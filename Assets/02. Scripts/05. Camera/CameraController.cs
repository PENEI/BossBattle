using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 camDis;     // 메인 카메라 위치
    [SerializeField]
    private float rotationSpeed;// 오브젝트 회전 속도
    [SerializeField]
    private float camAngle;     // 메인 카메라 각도

    [Header("[각도 제한]"), SerializeField, Range(-89f, 89f)]
    private float minVerticalAngle;     // 카메라 y축 최소 회전 각도
    [SerializeField, Range(-89f, 89f)]
    private float maxVerticalAngle;     // 카메라 y축 최대 회전 각도

    private Vector3 angle;      // 회전 각도
    private Camera cam;         // 메인 카메라
    
    private void Awake()
    {
        cam = Camera.main;  // 메인 카메라 받아오기
        cam.transform.localRotation = Quaternion.Euler(Vector3.right * camAngle);   // 카메라의 각도 초기화
        cam.transform.localPosition = camDis;    // 카메라 위치 설정
    }

    private void LateUpdate()
    {
        Quaternion lookRotation;        // 회전 방향

        // 카메라 회전 계산
        if (ManualRotation())
        {
            ConstrainAngle();
            // 플레이어가 돌아야하는 회전 값 (Vecotr3)을 Quaternion으로 변환
            lookRotation = Quaternion.Euler(angle); 
        }
        else
        {
            lookRotation = transform.localRotation;
        }

        // 카메라 회전 적용
        transform.rotation = lookRotation;
    }

    // 마우스 회전 각도 계산
    private bool ManualRotation()
    {
        // 마우스 위치 x, y
        Vector3 input = new Vector3(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X"), 0);

        const float e = 0.001f; // 마우스가 움직였는지 비교하기 위한 변수

        // 카메라 회전
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            // 카메라 회전
            angle += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }
        return false;
    }
    // 카메라의 회전 제한
    private void ConstrainAngle()
    {
        // orbitAngles.x가 minVerticalAngle보다 크고, maxVerticalAngle보다 작도록 제한
        angle.x = Mathf.Clamp(angle.x, minVerticalAngle, maxVerticalAngle);

        // 마우스가 360도회전 했을 경우 다시 0도로 복구
        angle.y += angle.y < 0f ? 360 : -360;
    }
}
