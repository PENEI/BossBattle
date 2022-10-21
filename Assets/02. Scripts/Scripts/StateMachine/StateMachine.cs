using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // ���� ����
    public State currentState { get; private set; }
    public Character character;

    public StateMachine(Character character)
    {
        this.character = character;
    }

    // ó�� ���� �� ����
    public void Initialize(State startingState)
    {
        currentState = startingState;
        character.state = startingState.state;
        startingState.Enter();
    }

    // ���� ��ȯ
    public void ChangeState(State newState)
    {
        // ���� ���¸� ����
        currentState.Exit();
        // ���� ���¿� ���ο� newState ����
        currentState = newState;
        // �÷��̾� enum���� ����  
        character.state = newState.state;
        // ���ο� ������ Enter�� ����
        newState.Enter();
    }

    /// <summary>
    /// �����ð�(time) ���� �� ���� ��ȯ 
    /// </summary>
    /// <param name="character"></param ������ ĳ����>
    /// <param name="state"></param ������ ����>
    /// <param name="time"></param �ִϸ��̼� ����� ����>
    public void AutoChangeState
        (Character character, State state, float time = 0.9f)
    {
        AnimatorStateInfo aniStateInfo = 
            character.ani.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼��� ���� ����Ǹ� ����
        if (aniStateInfo.normalizedTime >= time) 
        {
            ChangeState(state);
        }
    }

    /// <summary>
    /// �����ð�(time) ���� �� ���� ��ȯ (���� �ִϸ��̼ǰ� ������ ��)
    /// </summary>
    /// <param name="character"></param ������ ĳ����>
    /// <param name="state"></param ������ ����>
    /// <param name="name"></param ���� ����>
    /// <param name="time"></param �ִϸ��̼� ����� ����>
    public void AutoChangeState
        (Character character, State state, string name, float time = 0.9f)
    {
        AnimatorStateInfo aniStateInfo =
            character.ani.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼��� ���� ����Ǹ� ����
        if (aniStateInfo.IsName(name) && aniStateInfo.normalizedTime >= time)
        {
            ChangeState(state);
        }
    }
}
