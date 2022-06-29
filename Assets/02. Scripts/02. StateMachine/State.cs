using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Character character;      // 현재 캐릭터
    protected StateMachine stateMachine;// 상태 머신
    protected Player player;            // 플레이어

    // 생성자
    protected State(Character _character, StateMachine _stateMachine)
    {
        player = Player.Instance;
        character = _character;
        stateMachine = _stateMachine;
    }

    // 상태 시작 (start)
    public virtual void Enter() { }
    // 지속적 실행 (Update)
    public virtual void LogicUpdate() { }
    // 상태 종료 (상태 전환 시 실행)
    public virtual void Exit() { }
}
