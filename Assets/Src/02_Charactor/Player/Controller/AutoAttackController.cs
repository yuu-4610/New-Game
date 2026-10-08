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
    [SerializeField] GameObject followObject; //このクラスをアタッチするオブジェクト
    private Dictionary<string, IAttackObject> attackObjectsIF = new Dictionary<string, IAttackObject>(); //攻撃オブジェクトを設定
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
        EventManager.Instance.registAttackEffect -= AttackRegister;
    }
    void Start()
    {
        attackObjectDistance = 1f;
        attackObjectScale = new Vector2(1.5f, 1.5f);
        attackCoolTime = 1.2f;
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

    private void AttackPermission(IAttackObject attackIF, bool permission)
    {
        attackIF.SetAttackPermission(permission);
        Debug.Log($"攻撃の許可");
    }

    private void ChangeAttackPermissions(bool attackPermission)
    {
        for (int i = 0; i < attackObjectsIF.Count; i++)
        {
            AttackPermission(attackObjectsIF[attackObjectsIF.Keys.ToArray()[i]], attackPermission);
        }
    }

    private void CollectionCheck(string attackObjectName, GameObject attackObject)
    {
        //ストック制限に達している場合
        if (attackObjectsIF.Count >= 5)
        {
            Debug.Log("既に存在している攻撃です");
            //重複チェック
            var correctionCheck = (attackObjectsIF.ContainsKey(attackObjectName)) ? true : false;
            if (correctionCheck)
            {
                //レベル上げ処理
                Debug.Log("攻撃のレベルを上げます");
                //第二予防線（万が一選択可能だった場合
                if (!attackObjectsIF[attackObjectName].IsUpLevelingPossible())
                {
                    Debug.Log("最大レベルです");
                    return;
                }
                attackObjectsIF[attackObjectName].UpLeveling();
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
            Debug.Log("ストックに空きがあります");
            //重複チェック
            var correctionCheck = (attackObjectsIF.ContainsKey(attackObjectName)) ? true : false;
            if (correctionCheck)
            {
                //レベル上げ処理
                Debug.Log("攻撃のレベルを上げます");
                //第二予防線（万が一選択可能だった場合
                if (!attackObjectsIF[attackObjectName].IsUpLevelingPossible())
                {
                    Debug.Log("最大レベルです");
                    return;
                }
                attackObjectsIF[attackObjectName].UpLeveling();
            }
            else
            {
                AttackRegister(attackObjectName, attackObject);
            }
        }
    }

    //攻撃の追加
    public void AttackRegister(string attackObjectName, GameObject attackObject)
    {
        
        //バリデーションチェックを入れる
        try
        {
            //取得したオブジェクトにアタッチしているクラスを取得する
            var attackObjectIF = attackObject.GetComponent<IAttackObject>();
            //コレクションに追加
            attackObjectsIF.Add(attackObjectName, attackObjectIF);
            //座標をセット
            attackObjectIF.SetObjectPosition(attackObjectDistance, attackObjectScale);
            //攻撃のクールタイムをセット
            attackObjectIF.InitialSetting(attackCoolTime, followObject);
            Debug.Log($"攻撃を追加しました");

            AttackPermission(attackObjectIF, true);
        }
        catch(System.Exception exceptio)
        {
            Debug.Log($"例外が発生しました：{exceptio}");
        }
    }

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.checkAttackObjectCollection += CollectionCheck;
        EventManager.Instance.registAttackEffect += AttackRegister;
        isTestAddAttack = true;
    }
}
