using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoAttackController : MonoBehaviour
{
    [SerializeField] GameObject followObject; //このクラスをアタッチするオブジェクト
    private Dictionary<string, GameObject> attackObject = new Dictionary<string, GameObject>(); //攻撃オブジェクトを設定
    private float attackObjectDistance = default; //追従対象との距離
    private Vector2 attackObjectScale = default; //このオブジェクトのスケール
    private float attackCoolTime = default;

    void Start()
    {
        attackObjectDistance = 1f;
        attackObjectScale = new Vector2(1.5f, 1.5f);
        attackCoolTime = 1.2f;

        //StartCoroutine(FirstRegistration());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //初回登録
    private IEnumerator FirstRegistration()
    {
        //初回攻撃登録
        while (!AttackEffectPrefabPool.Instance.isInitialize)
        {
            yield return null;
        }

        AttackRegister<Slash>(AttackEffectObjectPoolName.slashEffect);
    }

    //攻撃の追加
    public void AttackRegister<T>(AttackEffectObjectPoolName attackEffect) where T : MonoBehaviour, IEffectParameter
    {
        //攻撃用オブジェクトを追加
        attackObject.Add(attackEffect.ToString(), AttackEffectPrefabPool.Instance.Pop(attackEffect.ToString()));

        //取得したオブジェクトにアタッチしているクラスを取得する
        if (attackObject[attackEffect.ToString()].TryGetComponent<T>(out var component))
        {
            //攻撃のクールタイムをセット
            component.InitialSetting(attackCoolTime, followObject);
            component.SetObjectPosition(attackObjectDistance, attackObjectScale);
        }
    }
}
