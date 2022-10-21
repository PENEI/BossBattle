using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [HideInInspector]
    public EState state;
    protected Character character;      // ���� ĳ����
    protected StateMachine stateMachine;// ���� �ӽ�
    protected Player player;
    // ������
    protected State(Character _character, StateMachine _stateMachine, EState _state)
    {
        player = Player.instance;
        character = _character;
        stateMachine = _stateMachine;
        state = _state;
    }

    // ���� ���� (start)
    public virtual void Enter() { }
    // ������ ���� (Update)
    public virtual void LogicUpdate() { Escape(); }
    // ���� ���� (���� ��ȯ �� ����)
    public virtual void Exit() {  }
    // ���� ���� ����
    public virtual void Escape() { }
    public virtual void Escape(float time, EState eState) { }
    public virtual void Escape(string name, float time, EState eState) { }
}
