using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [HideInInspector]
    public PlayerController ctr;    // 플레이어 컨트롤러

    protected override void SingletonInit()
    {
        ctr = GetComponent<PlayerController>();
    }
}
