using UnityEditor;
using UnityEngine;

public class State<T>
{
    protected T owner;

    public State(T owner)
    {
        this.owner = owner;
    }

    //ステート遷移時一番最初に処理されるメソッド
    public virtual void Enter() { }

    //毎フレーム処理をするメソッド
    public virtual void Execute() { }

    //別ステートへ遷移時に処理されるメソッド
    public virtual void Exit() { }
}
