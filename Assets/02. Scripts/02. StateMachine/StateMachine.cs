using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // ���� ����
    public State currentState { get; private set; }

    // ó�� ���� �� ����
    public void Initialize(State startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    // ���� ��ȯ
    public void ChangeState(State newState)
    {
        // ���� ���¸� ����
        currentState.Exit();
        // ���� ���¿� ���ο� newState ����
        currentState = newState; 
        // ���ο� ������ Enter�� ����
        newState.Enter();
    }

    // �����ð�(time) ���� �� ���� ��ȯ 
    public void AnimationEnd
        (Character character, State state, string aniState, float time = 0.9f)
    {
        AnimatorStateInfo aniStateInfo = 
            character.ani.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼��� ���� ����Ǹ� ����
        if (aniStateInfo.IsName(aniState) && aniStateInfo.normalizedTime >= time) 
        {
            ChangeState(state);
        }
    }
}
