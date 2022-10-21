using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [HideInInspector]
    public EState state;
    protected Character character;      // 현재 캐릭터
    protected StateMachine stateMachine;// 상태 머신
    protected Player player;
    // 생성자
    protected State(Character _character, StateMachine _stateMachine, EState _state)
    {
        player = Player.instance;
        character = _character;
        stateMachine = _stateMachine;
        state = _state;
    }

    // 상태 시작 (start)
    public virtual void Enter() { }
    // 지속적 실행 (Update)
    public virtual void LogicUpdate() { Escape(); }
    // 상태 종료 (상태 전환 시 실행)
    public virtual void Exit() {  }
    // 상태 해제 조건
    public virtual void Escape() { }
    public virtual void Escape(float time, EState eState) { }
    public virtual void Escape(string name, float time, EState eState) { }
}
