using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyParticle : MonoBehaviour
{
    public float destroyTime = 1;
    private float time = 0;

    // ��ƼŬ�� ����ǰ� destroyTime�� ���� ����
    void Update()
    {
        time += Time.deltaTime;

        if (time >= destroyTime) 
        {
            SMemoryPool.Instance.MemoryPoolDic[ESkillObjType.FireBallImpact].DeactivatePoolItem(gameObject);
            //Destroy(gameObject);
        }
    }
    private void OnDisable()
    {
        time = 0;
    }
}
