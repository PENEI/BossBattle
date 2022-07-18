using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [HideInInspector]
    public PlayerController ctr;    // �÷��̾� ��Ʈ�ѷ�

    protected override void SingletonInit()
    {
        ctr = GetComponent<PlayerController>();
    }
}
