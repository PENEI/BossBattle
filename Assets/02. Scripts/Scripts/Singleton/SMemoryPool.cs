using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMemoryPool: MonoBehaviour
{
    [HideInInspector]
    private static SMemoryPool instance;

    [HideInInspector]
    private Dictionary<ESkillObjType, MemoryPool> memoryPoolDic;

    // 프로퍼티 호출 시 instance가 없을 시 자신을 생성
    public static SMemoryPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof (SMemoryPool)) as SMemoryPool;
            }
            return instance;
        }
    }

    public Dictionary<ESkillObjType, MemoryPool> MemoryPoolDic
    {
        get
        {
            // memoryPoolDic이 없을 시 생성
            if (memoryPoolDic == null)
            {
                memoryPoolDic = new Dictionary<ESkillObjType, MemoryPool>();
            }
            return memoryPoolDic;
        }
    }
}
