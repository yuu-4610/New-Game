using System;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePointsPrefabPool : ObjectPoolBase
{
    [Header("対象Enumクラスに記載されている順番でアタッチ")]
    [SerializeField] StackExperiencePointObject stackExperiencePointObject;
    private Dictionary<string, Stack<GameObject>> poolObjectDictionary = new Dictionary<string, Stack<GameObject>>();
    Dictionary<string, GameObject> parentObjects = new Dictionary<string, GameObject>();
    public static ExperiencePointsPrefabPool Instance;
    private int initializeSize = 70;

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
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //-----------------プール用オブジェクトの追加、Dictionaryで管理-----------------//
        //指定したEnumの値をすべて取得
        ExperiencePointsObjectPoolName[] experiencePointkEffectArray = (ExperiencePointsObjectPoolName[])System.Enum.GetValues(typeof(ExperiencePointsObjectPoolName));
        //experiencePointsNamesの値をstring配列で管理
        string[] experiencePointsNames = Array.ConvertAll(experiencePointkEffectArray, experiencePoint => experiencePoint.ToString());

        for (int i = 0; i < experiencePointkEffectArray.Length; ++i)
        {
            var parentObject = GetOrCreatePanetObject(experiencePointsNames[i]);

            poolObjectDictionary.Add(experiencePointsNames[i], new Stack<GameObject>());

            for (int j = 0; j < initializeSize; ++j)
            {
                var experiencePointPrefab = Instantiate(stackExperiencePointObject.experiencePointsobject[i], new (0, 0, 0), Quaternion.identity, parentObject.transform);
                experiencePointPrefab.SetActive(false);
                poolObjectDictionary[experiencePointsNames[i]].Push(experiencePointPrefab);
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

    //親オブジェクトの生成処理
    private GameObject GetOrCreatePanetObject(string name)
    {
        //オブジェクトが存在しなければ新しく作成する
        if (!parentObjects.TryGetValue(name, out var parent) || parent == null)
        {
            parent = new GameObject(name);
            parentObjects[name] = parent;
        }

        return parent;
    }
}
