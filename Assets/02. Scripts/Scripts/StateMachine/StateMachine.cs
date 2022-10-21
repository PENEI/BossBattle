using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // 현재 상태
    public State currentState { get; private set; }
    public Character character;

    public StateMachine(Character character)
    {
        this.character = character;
    }

    // 처음 실행 시 실행
    public void Initialize(State startingState)
    {
        currentState = startingState;
        character.state = startingState.state;
        startingState.Enter();
    }

    // 상태 전환
    public void ChangeState(State newState)
    {
        // 현재 상태를 종료
        currentState.Exit();
        // 현재 상태에 새로운 newState 저장
        currentState = newState;
        // 플레이어 enum상태 변경  
        character.state = newState.state;
        // 새로운 상태의 Enter를 실행
        newState.Enter();
    }

    /// <summary>
    /// 일정시간(time) 실행 후 상태 변환 
    /// </summary>
    /// <param name="character"></param 적용할 캐릭터>
    /// <param name="state"></param 변경할 상태>
    /// <param name="time"></param 애니메이션 재생할 비율>
    public void AutoChangeState
        (Character character, State state, float time = 0.9f)
    {
        AnimatorStateInfo aniStateInfo = 
            character.ani.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 전부 실행되면 종료
        if (aniStateInfo.normalizedTime >= time) 
        {
            ChangeState(state);
        }
    }

    /// <summary>
    /// 일정시간(time) 실행 후 상태 변환 (현재 애니메이션과 같은지 비교)
    /// </summary>
    /// <param name="character"></param 적용할 캐릭터>
    /// <param name="state"></param 변경할 상태>
    /// <param name="name"></param 현재 상태>
    /// <param name="time"></param 애니메이션 재생할 비율>
    public void AutoChangeState
        (Character character, State state, string name, float time = 0.9f)
    {
        AnimatorStateInfo aniStateInfo =
            character.ani.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 전부 실행되면 종료
        if (aniStateInfo.IsName(name) && aniStateInfo.normalizedTime >= time)
        {
            ChangeState(state);
        }
    }
}
