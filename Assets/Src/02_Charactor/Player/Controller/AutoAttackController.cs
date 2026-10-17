using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoAttackController : MonoBehaviour
{
    private GameObject[] attackControllerObject;
    [SerializeField] GameObject followObject; //このクラスをアタッチするオブジェクト
    private Dictionary<AttackEffectObjectPoolName, IAttackControl> possessionAttackObjectsIF = new Dictionary<AttackEffectObjectPoolName, IAttackControl>(); //所持している攻撃オブジェクト
    private Dictionary<AttackEffectObjectPoolName, GameObject> controllerObjectCollection = new Dictionary<AttackEffectObjectPoolName, GameObject>();
    private float attackObjectDistance = default; //追従対象との距離
    private Vector2 attackObjectScale = default; //このオブジェクトのスケール
    private float attackCoolTime = default;

    private bool isTestAddAttack;

    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }
    private void OnDisable()
    {
        EventManager.Instance.checkAttackObjectCollection -= CollectionCheck;
    }
    void Start()
    {
        attackObjectDistance = 1f;
        attackObjectScale = new Vector2(1.5f, 1.5f);
        attackCoolTime = 1.2f;
        //指定したEnumの値をすべて取得
        AttackEffectObjectPoolName[] attackObjectArray = (AttackEffectObjectPoolName[])System.Enum.GetValues(typeof(AttackEffectObjectPoolName));
        //enemyArrayの値をstring配列で管理
        for(int i = 0; i < attackObjectArray.Length; ++i)
        {
            controllerObjectCollection.Add(attackObjectArray[i], attackControllerObject[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTestAddAttack)
        {
            TestEventManager.Instance.TestAddAttackEvent(AttackEffectObjectPoolName.Slash);
            isTestAddAttack = false;
        }
    }


    private void CollectionCheck(AttackEffectObjectPoolName attackObjectName, GameObject[] attackObject)
    {
        //ストック制限に達している場合
        if (possessionAttackObjectsIF.Count >= 5)
        {
            Debug.Log("既に存在している攻撃です");
            //重複チェック
            var correctionCheck = (possessionAttackObjectsIF.ContainsKey(attackObjectName)) ? true : false;
            if (correctionCheck)
            {
                //第二予防線（万が一レベルMAXの攻撃を獲得できた場合
                if (possessionAttackObjectsIF[attackObjectName].GetCurrentLevel() >= 5)
                {
                    Debug.Log("レベルが最大です");
                    return;
                }
                else
                {
                    Debug.Log("攻撃のレベルを上げます");
                    //レベルアップ処理
                    possessionAttackObjectsIF[attackObjectName].UpLeveling();
                }
            }
            else
            {
                //コレクションの入れ替えするか確認（候補：Event
                //登録処理はAttackRegisterで行う
            }
        }
        //ストックに空きがある場合
        else
        {
            Debug.Log("登録可能です");
            //重複チェック
            var correctionCheck = (possessionAttackObjectsIF.ContainsKey(attackObjectName)) ? true : false;
            if (correctionCheck)
            {
                //第二予防線（万が一レベルMAXの攻撃を獲得できた場合
                if (possessionAttackObjectsIF[attackObjectName].GetCurrentLevel() >= 5)
                {
                    Debug.Log("レベルが最大です");
                    return;
                }
                else
                {
                    Debug.Log("攻撃のレベルを上げます");
                    //レベルアップ処理
                    possessionAttackObjectsIF[attackObjectName].UpLeveling();
                }
            }
            else
            {
                //新規登録
                AttackRegister(attackObjectName, attackObject);
            }
        }
    }

    //攻撃の追加
    private void AttackRegister(AttackEffectObjectPoolName attackObjectName, GameObject[] attackObject)
    {
        
        //バリデーションチェックを入れる
        try
        {
            //コントローラーオブジェクトが登録されていれば
            if (!controllerObjectCollection.ContainsKey(attackObjectName))
            {
                Debug.Log("登録できない攻撃です");
                throw new Exception();
            }

            //IFの取得
            var attackControllerIF = controllerObjectCollection[attackObjectName].GetComponent<IAttackControl>();
            //コレクションに追加
            possessionAttackObjectsIF.Add(attackObjectName, attackControllerIF);
            //攻撃オブジェクトの初期化を行う
            attackControllerIF.IsAddObject(attackObject, this.gameObject);
            Debug.Log($"攻撃を追加しました");
        }
        catch(System.Exception exception)
        {
            Debug.Log($"例外が発生しました：{exception}");
        }
    }

    public void SetControllerObject(GameObject[] controllerObjects)
    {
        attackControllerObject = controllerObjects;
    }

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.checkAttackObjectCollection += CollectionCheck;
        //EventManager.Instance.registAttackEffect += AttackRegister;
        isTestAddAttack = true;
    }
}
