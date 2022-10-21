using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell1 : MonoBehaviour
{
    public float power;                 // ������
    public LineRenderer render;         // ȭ�鿡 ���� ����
    public LineRenderer subRender;      // �帣�� �ð��� ǥ���� ����
    public float startTime = 2;         // �ߵ� �ð�
    public int pointCount = 20;         // n�������� ���� ���� ����(������ ���� ���� �� ó�� ����)
    public float radius = 0;            // ���� ���� ������
    private float subTime = 0;          // ���� Ŀ���� ���� ���� �ð� ��
    private float timer;                // ������Ʈ�� �����ǰ��� ���� �ð�
    private ParticleSystem particle;    // ��ƼŬ
    private bool isFire = false;        // ������ �ѹ����ϵ��� ����
    private Transform target;           // Ÿ�� ��ġ    
    private Player ctr;       // �÷��̾� ��Ʈ�ѷ�

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

        // ���� ����
        if (isFire)
        {
            render.enabled = false;
            subRender.enabled = false;
        }

        // ���� Ŀ���� ��
        DrawMovingSpellCircle(subRender, pointCount, radius);

        // �����ð� �� ����
        DelayParticle(particle, startTime);

        // ��ƼŬ ���� �� ����
        DestroyParticle();
    }

    // ���� ����
    private void DrawRange(LineRenderer render, int pointCount,float radius)
    {
        float step = (2 * Mathf.PI) / pointCount;   // ���� �� ������ �Ÿ�

        Vector3 pos = transform.position;
        Vector3 p0 = pos;   // ���� ������ ��ġ

        float theta = 0;    // ���� ������ �Ÿ�
        // pointCount������ �� �׸���
        for (int i = 0; i < pointCount; i++) 
        {
            theta = step * i;
            p0.x = pos.x + radius * Mathf.Sin(theta);
            p0.z = pos.z + radius * Mathf.Cos(theta);

            render.SetPosition(i, p0);
        }
    }

    // ��� �ð��� ������ �׸���
    private void DrawMovingSpellCircle(LineRenderer render, int pointCount, float time)
    {
        // �Ÿ��� ����� �ð� 1��
        float interval = radius / startTime;

        // subFireTime�� �ð��� �ߵ� �ð����� Ŀ���� �ʵ��� ����
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

    // ��ƼŬ�� ����� �� ������Ʈ ����
    private void DestroyParticle()
    {
        if (isFire && !particle.isPlaying)
        {
            SMemoryPool.Instance.MemoryPoolDic[ESkillObjType.RangeSpell].DeactivatePoolItem(gameObject);
        }
    }

    // ���� �ð� �� ��ƼŬ ���� �� �̺�Ʈ
    private void DelayParticle(ParticleSystem particle, float startTime)
    {
        // �����ǰ� fireTime���� ũ�ų� ������ �ߵ�
        if (!isFire && timer >= startTime)
        {
            isFire = true;
            particle.Play();

            // �����ȿ� Ÿ���� ���� ��
            if (CFunction.GetDistance(transform.position, target.position) <= Mathf.Pow(radius, 2))
            {
                EventParticle();
            }
        }
    }

    // ��ƼŬ �̺�Ʈ
    private void EventParticle()
    {
        // Ÿ���� �����ȿ� ������ ������
        // �÷��̾�� �浹 �� �÷��̾� �ִϸ��̼� ��� �� ü�� ����
        // ������
        /*ctr.Ani_Damage_Hit(ctr, power);
        if (!ctr.isHit)
        {
            ctr.isHit = true;
        }*/
    }
}
