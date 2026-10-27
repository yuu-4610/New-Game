using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectMediator : MonoBehaviour
{
    //「プレイヤー」と「ＵＩやオブジェクト」の処理を取り持つ中間クラス
    [SerializeField] AttackEffectPrefabPool attackEffectPrefabPool;
    [SerializeField] LevelUpBonusBoard levelUpBonusBoard;
    private Dictionary<AttackEffectObjectPoolName, int> attackObjectLevel = new Dictionary<AttackEffectObjectPoolName, int>();
    private AutoAttackController autoAttackController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }

    private void OnDisable()
    {
        EventManager.Instance.finishedGeneratePlayer += Initialize;
        //TestEventManager.Instance.testAddAttack -= AttackEffectPop;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Initialize()
    {
        autoAttackController = ObjectManager.Instance.GetObject(AcquisitionObjectName.Player.ToString()).GetComponent<AutoAttackController>();
    }
    public int CollectionChek(int index)
    {
        if (!Enum.IsDefined(typeof(AttackEffectObjectPoolName), index)) return 9;
        AttackEffectObjectPoolName attackEffectObjectName = (AttackEffectObjectPoolName)index;
        //登録があれば現在のレベルを返す
        if (attackObjectLevel.ContainsKey(attackEffectObjectName))
        {
            return attackObjectLevel[attackEffectObjectName];
        }
        //なければ０を返す
        else
        {
            return 0;
        }
    }

    public void ContorllerCollectionChek(AttackEffectObjectPoolName attackObjectName)
    {
        var processIndex = autoAttackController.CollectionCheck(attackObjectName);
        Debug.Log($"processIndex:{processIndex}");

        if (processIndex == 0)
        {
            //新規登録
            var attackObjects = new GameObject[4];
            for (int i = 0; i < attackObjects.Length; ++i)
            {
                var attackObject = attackEffectPrefabPool.Pop(attackObjectName.ToString());
                attackObjects[i] = attackObject;
            }
            //コレクションへの追加
            attackObjectLevel.Add(attackObjectName, 1);
            autoAttackController.AttackRegister(attackObjectName, attackObjects);
        }
        else if(processIndex == 1)
        {
            //レベルアップ
            autoAttackController.UpLeveling(attackObjectName);
            //コレクションの値の更新
            attackObjectLevel[attackObjectName] = autoAttackController.GetAttackLevel(attackObjectName) + 1;
            //最大レベルの場合、選択UIの候補から削除する
            if ((int)attackObjectLevel[attackObjectName] == 5)
            {
                levelUpBonusBoard.RemoveAttackObjectNumberList((int)attackObjectName);
            }
            }
        else if (processIndex == 2)
        {
            Debug.Log("登録できませんでした。処理を終了します");
            return;
        }

        //レベルアップボーナス選択画面を非活性化する
        levelUpBonusBoard.LevelUpBonusBoardInActive();
    }

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.finishedGeneratePlayer += Initialize;
        //TestEventManager.Instance.testAddAttack += AttackEffectPop;
    }
}
