using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectPrefabPool : ObjectPoolBase
{
    [Header("対象Enumクラスに記載されている順番でアタッチ")]
    [SerializeField] StackAttackEffectObject stackAttackEffectObject;
    private Dictionary<string, Stack<GameObject>> poolObjectDictionary = new Dictionary<string, Stack<GameObject>>();
    public static AttackEffectPrefabPool Instance;
    public bool isInitialize { get; private set; } = default;
    private int initializeSize = 10;

    private void Awake()
    {
        //シングルトン
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        //Titleシーン（一番最初のシーン）で配置したオブジェクトを残す
        DontDestroyOnLoad(this.gameObject);

        isInitialize = false;

        Initialize();
    }
    
    private void Initialize()
    {
        //-----------------プール用オブジェクトの追加、Dictionaryで管理-----------------//
        //指定したEnumの値をすべて取得
        AttackEffectObjectPoolName[] attackEffectArray = (AttackEffectObjectPoolName[])System.Enum.GetValues(typeof(AttackEffectObjectPoolName));
        //attackEffectArrayの値をstring配列で管理
        string[] attackEffectNames = Array.ConvertAll(attackEffectArray, attackEffect => attackEffect.ToString());

        //取得したEnumの値の数だけ繰り返し処理
        for (int i = 0; i < attackEffectArray.Length; ++i)
        {
            Debug.Log($"attackEffectNames：{attackEffectNames[i]}");
            
            poolObjectDictionary.Add(attackEffectNames[i], new Stack<GameObject>());

            for (int j = 0; j < initializeSize; ++j)
            {
                var enmeyObjectPrefab = Instantiate(stackAttackEffectObject.attackPrefabObject[i]);
                enmeyObjectPrefab.SetActive(false);
                poolObjectDictionary[attackEffectNames[i]].Push(enmeyObjectPrefab);
            }
        }
    }

    public override GameObject Pop(string objectName)
    {
        if (!poolObjectDictionary.ContainsKey(objectName))
        {
            Debug.Log($"「{objectName}」は登録されていません");

            return null;
        }

        var prefab = poolObjectDictionary[objectName].Pop();
        prefab.SetActive(true);

        return prefab;
    }

    public override void Push(string returnObjectName, GameObject returnObject)
    {
        returnObject.SetActive(false);
        var prefab = poolObjectDictionary[returnObjectName];

        prefab.Push(returnObject);
    }
}
