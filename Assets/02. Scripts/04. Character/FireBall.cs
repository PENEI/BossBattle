using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject hitObj;   // 충돌 시 효과 오브젝트
    public float speed;
    public float power;

    private PlayerController ctr;

    private void Start()
    {
        ctr = Player.Instance.ctr;
    }

    private void Update()
    {
        // 오브젝트의 Z축으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌 시 플레이어 애니메이션 재생 및 체력 감소
        if (collision.transform.CompareTag("Player")) 
        {
            // 데미지
            ctr.Ani_Damage_Hit(ctr, power);
            if(!ctr.isHit)
            {
                ctr.isHit = true;
            }
        }

        // 충돌 시 파티클 출력
        CollisionParticle(collision);

        // 메모리 풀링(오브젝트 비활성화)
        MemoryPool memoryPool = SMemoryPool.Instance.memoryPool_SpellB;
        //Destroy(gameObject);
        memoryPool.DeactivatePoolItem(gameObject);
    }

    // 충돌 시 터지는 파티클 출력
    public void CollisionParticle(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        Vector3 pos = contact.point;
        // 충돌 파티클 생성
        GameObject obj = Instantiate(hitObj, pos, rot);
    }
}
