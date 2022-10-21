using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell1 : MonoBehaviour
{
    public float power;                 // 데미지
    public LineRenderer render;         // 화면에 보일 라인
    public LineRenderer subRender;      // 흐르는 시간을 표시할 라인
    public float startTime = 2;         // 발동 시간
    public int pointCount = 20;         // n각형으로 만들 점의 갯수(많으면 많을 수록 원 처럼 보임)
    public float radius = 0;            // 공격 범위 반지름
    private float subTime = 0;          // 점차 커지는 원을 위한 시간 값
    private float timer;                // 오브젝트가 생성되고나서 부터 시간
    private ParticleSystem particle;    // 파티클
    private bool isFire = false;        // 공격을 한번만하도록 실행
    private Transform target;           // 타겟 위치    
    private Player ctr;       // 플레이어 컨트롤러

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        render.positionCount = pointCount;
        subRender.positionCount = pointCount;
        
        target = Player.instance.transform;
    }

    private void Start()
    {
        //ctr = PlayerTest.Instance.ctr;
        DrawRange(render, pointCount, radius);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // 공격 범위
        if (isFire)
        {
            render.enabled = false;
            subRender.enabled = false;
        }

        // 점차 커지는 원
        DrawMovingSpellCircle(subRender, pointCount, radius);

        // 일정시간 후 실행
        DelayParticle(particle, startTime);

        // 파티클 실행 후 삭제
        DestroyParticle();
    }

    // 공격 범위
    private void DrawRange(LineRenderer render, int pointCount,float radius)
    {
        float step = (2 * Mathf.PI) / pointCount;   // 점과 점 사이의 거리

        Vector3 pos = transform.position;
        Vector3 p0 = pos;   // 원의 꼭지점 위치

        float theta = 0;    // 현재 지점의 거리
        // pointCount각형의 원 그리기
        for (int i = 0; i < pointCount; i++) 
        {
            theta = step * i;
            p0.x = pos.x + radius * Mathf.Sin(theta);
            p0.z = pos.z + radius * Mathf.Cos(theta);

            render.SetPosition(i, p0);
        }
    }

    // 경과 시간을 범위로 그리기
    private void DrawMovingSpellCircle(LineRenderer render, int pointCount, float time)
    {
        // 거리에 비례한 시간 1초
        float interval = radius / startTime;

        // subFireTime의 시간이 발동 시간보다 커지지 않도록 제한
        if (subTime >= time) 
        {
            subTime = time; 
        }
        else
        {
            subTime += Time.deltaTime * interval;
            DrawRange(render, pointCount, subTime);
        }
        
    }

    // 파티클이 실행된 후 오브젝트 삭제
    private void DestroyParticle()
    {
        if (isFire && !particle.isPlaying)
        {
            SMemoryPool.Instance.MemoryPoolDic[ESkillObjType.RangeSpell].DeactivatePoolItem(gameObject);
        }
    }

    // 일정 시간 후 파티클 실행 및 이벤트
    private void DelayParticle(ParticleSystem particle, float startTime)
    {
        // 생성되고 fireTime보다 크거나 같으면 발동
        if (!isFire && timer >= startTime)
        {
            isFire = true;
            particle.Play();

            // 범위안에 타겟이 있을 시
            if (CFunction.GetDistance(transform.position, target.position) <= Mathf.Pow(radius, 2))
            {
                EventParticle();
            }
        }
    }

    // 파티클 이벤트
    private void EventParticle()
    {
        // 타겟이 범위안에 있으면 데미지
        // 플레이어와 충돌 시 플레이어 애니메이션 재생 및 체력 감소
        // 데미지
        /*ctr.Ani_Damage_Hit(ctr, power);
        if (!ctr.isHit)
        {
            ctr.isHit = true;
        }*/
    }
}
