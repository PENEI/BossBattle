using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMemoryPool : Singleton<SMemoryPool>
{
    [HideInInspector]
    public MemoryPool memoryPool_SpellA;
    [HideInInspector]
    public MemoryPool memoryPool_SpellB;
}
