using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMemoryPool: MonoBehaviour
{
    [HideInInspector]
    private static SMemoryPool instance;

    [HideInInspector]
    private Dictionary<ESkillObjType, MemoryPool> memoryPoolDic;

    // ������Ƽ ȣ�� �� instance�� ���� �� �ڽ��� ����
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
            // memoryPoolDic�� ���� �� ����
            if (memoryPoolDic == null)
            {
                memoryPoolDic = new Dictionary<ESkillObjType, MemoryPool>();
            }
            return memoryPoolDic;
        }
    }
}
