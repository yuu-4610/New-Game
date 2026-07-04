using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//継承クラス可能クラスを限定する（classと列挙型を指定）
public abstract class StateMachine<T, TEnum> : MonoBehaviour where T : class where TEnum : System.IConvertible
{
    public State<T> currentState;
    public List<State<T>> stateList = new List<State<T>>();
    private bool canChangeState = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentState = null;
    }

    //前ステートのExit()を処理ー＞次ステートのEnter()を処理ー＞次Execute()を処理
    public void ChangeState(State<T> state)
    {
        try
        {
            if (currentState == state) return;
            canChangeState = false;
            if(currentState != null)
            {
                currentState.Exit();
            }
            currentState = state;
            currentState.Enter();
            canChangeState = true;
        }
        catch(Exception exception)
        {
            //例外処理
        }
    }

    
    public virtual void Update()
    {
        if (currentState != null)
        {
            if (canChangeState)
            {
                currentState.Execute();
            }
        }
    }
}
