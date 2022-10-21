using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyParticle : MonoBehaviour
{
    public float destroyTime = 1;
    private float time = 0;

    // 파티클이 실행되고 destroyTime초 이후 제거
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
