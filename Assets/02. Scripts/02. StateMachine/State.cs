using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Character character;      // ���� ĳ����
    protected StateMachine stateMachine;// ���� �ӽ�
    protected Player player;            // �÷��̾�

    // ������
    protected State(Character _character, StateMachine _stateMachine)
    {
        player = Player.Instance;
        character = _character;
        stateMachine = _stateMachine;
    }

    // ���� ���� (start)
    public virtual void Enter() { }
    // ������ ���� (Update)
    public virtual void LogicUpdate() { }
    // ���� ���� (���� ��ȯ �� ����)
    public virtual void Exit() { }
}
