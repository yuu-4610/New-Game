using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoAttackController : MonoBehaviour
{
    [SerializeField] GameObject followObject; //このクラスをアタッチするオブジェクト
    private Dictionary<string, GameObject> attackObject = new Dictionary<string, GameObject>(); //攻撃オブジェクトを設定
    private IEffectParameter effectParameter;

    void Start()
    {
        StartCoroutine(FirstRegistration());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //初回登録
    private IEnumerator FirstRegistration()
    {
        //初回攻撃登録
        while (!ObjectPool.Instance.isInitialize)
        {
            yield return null;
        }

        AttackRegister<Slash>(poolObjectName.slashEffect);
    }

    //攻撃の追加
    public void AttackRegister<T>(poolObjectName attackEffect) where T : MonoBehaviour, IEffectParameter
    {
        //攻撃用オブジェクトを追加
        attackObject.Add(attackEffect.ToString(), ObjectPool.Instance.Get(attackEffect));

        //取得したオブジェクトにアタッチしているクラスを取得する
        if (attackObject[attackEffect.ToString()].TryGetComponent<T>(out var component))
        {
            //攻撃のクールタイムをセット
            component.InitialSetting(1.0f, followObject);
        }

        
    }
}
