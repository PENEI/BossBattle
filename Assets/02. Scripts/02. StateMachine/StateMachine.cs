using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // 현재 상태
    public State currentState { get; private set; }

    // 처음 실행 시 실행
    public void Initialize(State startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    // 상태 전환
    public void ChangeState(State newState)
    {
        // 현재 상태를 종료
        currentState.Exit();
        // 현재 상태에 새로운 newState 저장
        currentState = newState; 
        // 새로운 상태의 Enter를 실행
        newState.Enter();
    }

    // 일정시간(time) 실행 후 상태 변환 
    public void AnimationEnd
        (Character character, State state, string aniState, float time = 0.9f)
    {
        AnimatorStateInfo aniStateInfo = 
            character.ani.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 전부 실행되면 종료
        if (aniStateInfo.IsName(aniState) && aniStateInfo.normalizedTime >= time) 
        {
            ChangeState(state);
        }
    }
}
