using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    /*<責務>ゲーム全体で使用する処理イベントの提供
     */
    public static EventManager Instance { get; private set; }

    public event Action transitionGameToResult; //ゲーム終了判定時に発火

    public event Action<Transform, Transform, int> synthesisPieceObjectGenerate; //パズルピースが合体した時に発火

    public event Action<int> sceneTransition; //シーン遷移命令時に発火

    public event Action<string, GameObject> enemyObjectPush;

    public event Action transitionTitleToGameEvent;

    public event Action playerGenerate;

    public event Action finishedGeneratePlayer;

    public event Action<string, GameObject> pushExperiencePoint;
    public event Action<GameObject> returnExperiencePoint;

    public event Action<Vector3> popExperiencePoint;


    // Use this for initialization
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    //各シーン遷移後に行う処理
    public void SceneTransitionEvent(int sceneType)
    {
        sceneTransition?.Invoke(sceneType);
    }

    public void EnemyObjectPushEvent(string poolObjectName,GameObject pushObject)
    {
        enemyObjectPush?.Invoke(poolObjectName, pushObject);
    }

    public void PlayerGenerateEvent()
    {
        playerGenerate?.Invoke();
    }

    public void FinishedGeneratePlayerEvent()
    {
        finishedGeneratePlayer?.Invoke();
    }

    public void PopExperiencePointEvent(Vector3 position)
    {
        popExperiencePoint?.Invoke(position);
    }

    public void PushExperiencePointEvent(string objectName, GameObject pushExperiencePointObject)
    {
        pushExperiencePoint?.Invoke(objectName, pushExperiencePointObject);
    }

    public void ReturnExperiencePointEvent(GameObject pushExperiencePointObject)
    {
        returnExperiencePoint?.Invoke(pushExperiencePointObject);
    }

    //ゲームシーン遷移時に処理
    public void TransitionTitleToGameEvent()
    {
        transitionTitleToGameEvent?.Invoke();
    }


    public void Debuger(Action eventFunction)
    {
        Debug.Log("=== ObjectGenerateEvent 呼び出し元 ===");
        Debug.Log(System.Environment.StackTrace);

        Debug.Log("Invoke フレーム：" + Time.frameCount);
        Debug.Log("登録数: " + eventFunction?.GetInvocationList().Length);
    }
}
