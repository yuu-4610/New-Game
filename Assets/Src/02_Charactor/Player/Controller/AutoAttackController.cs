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
    private Dictionary<string, IEffectParameter> attackObjectsIF = new Dictionary<string, IEffectParameter>(); //攻撃オブジェクトを設定
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

    void AttackPermission(IEffectParameter attackIF, bool permission)
    {
        attackIF.SetAttackPermission(permission);
        Debug.Log($"攻撃の許可");
    }

    void ChangeAttackPermissions(bool attackPermission)
    {
        for (int i = 0; i < attackObjectsIF.Count; i++)
        {
            AttackPermission(attackObjectsIF[attackObjectsIF.Keys.ToArray()[i]], attackPermission);
        }
    }

    //攻撃の追加
    public void AttackRegister(string attackObjectName, GameObject attackObject)
    {
        //バリデーションチェックを入れる
        try
        {
            //取得したオブジェクトにアタッチしているクラスを取得する
            var attackObjectIF = attackObject.GetComponent<IEffectParameter>();
            //コレクションに追加
            attackObjectsIF.Add(attackObjectName, attackObjectIF);
            //座標をセット
            attackObjectIF.SetObjectPosition(attackObjectDistance, attackObjectScale);
            //攻撃のクールタイムをセット
            attackObjectIF.InitialSetting(attackCoolTime, followObject);
            Debug.Log($"攻撃の追加");

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

        EventManager.Instance.registAttackEffect += AttackRegister;
        isTestAddAttack = true;
    }
}
