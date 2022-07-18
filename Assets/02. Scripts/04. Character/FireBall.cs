using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject hitObj;   // �浹 �� ȿ�� ������Ʈ
    public float speed;
    public float power;

    private PlayerController ctr;

    private void Start()
    {
        ctr = Player.Instance.ctr;
    }

    private void Update()
    {
        // ������Ʈ�� Z������ �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� �浹 �� �÷��̾� �ִϸ��̼� ��� �� ü�� ����
        if (collision.transform.CompareTag("Player")) 
        {
            // ������
            ctr.Ani_Damage_Hit(ctr, power);
            if(!ctr.isHit)
            {
                ctr.isHit = true;
            }
        }

        // �浹 �� ��ƼŬ ���
        CollisionParticle(collision);

        // �޸� Ǯ��(������Ʈ ��Ȱ��ȭ)
        MemoryPool memoryPool = SMemoryPool.Instance.memoryPool_SpellB;
        //Destroy(gameObject);
        memoryPool.DeactivatePoolItem(gameObject);
    }

    // �浹 �� ������ ��ƼŬ ���
    public void CollisionParticle(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        Vector3 pos = contact.point;
        // �浹 ��ƼŬ ����
        GameObject obj = Instantiate(hitObj, pos, rot);
    }
}
