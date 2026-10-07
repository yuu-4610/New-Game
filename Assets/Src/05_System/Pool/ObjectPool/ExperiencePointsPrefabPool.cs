using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePointsPrefabPool : ObjectPoolBase
{
    [Header("対象Enumクラスに記載されている順番でアタッチ")]
    [SerializeField] StackExperiencePointObject stackExperiencePointObject;
    private Dictionary<string, Stack<GameObject>> poolObjectDictionary = new Dictionary<string, Stack<GameObject>>();
    private Dictionary<string, GameObject> parentObjects = new Dictionary<string, GameObject>();
    private GameObject playerObject;
    public static ExperiencePointsPrefabPool Instance;
    private int initializeSize = 70;
    private int count;

    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }

    private void Awake()
    {
        
    }

    void Start()
    {
        count = 0;
    }

    private void Initialize()
    {
        //-----------------プール用オブジェクトの追加、Dictionaryで管理-----------------//
        //指定したEnumの値をすべて取得
        ExperiencePointsObjectPoolName[] experiencePointkEffectArray = (ExperiencePointsObjectPoolName[])System.Enum.GetValues(typeof(ExperiencePointsObjectPoolName));
        //experiencePointsNamesの値をstring配列で管理
        string[] experiencePointsNames = Array.ConvertAll(experiencePointkEffectArray, experiencePoint => experiencePoint.ToString());
        //プレイヤーオブジェクトを取得
        playerObject = ObjectManager.Instance.GetObject(AcquisitionObjectName.Player.ToString());

        for (int i = 0; i < experiencePointkEffectArray.Length; ++i)
        {
            var parentObject = GetOrCreatePanetObject(experiencePointsNames[i]);

            poolObjectDictionary.Add(experiencePointsNames[i], new Stack<GameObject>());

            for (int j = 0; j < initializeSize; ++j)
            {
                var experiencePointPrefab = Instantiate(stackExperiencePointObject.experiencePointsobject[i], new (0, 0, 0), Quaternion.identity, parentObject.transform);
                experiencePointPrefab.name = $"経験値：{++count}";
                experiencePointPrefab.SetActive(false);
                if (experiencePointPrefab.TryGetComponent<IExperiencePoint>(out var enemyPrefab))
                {
                    enemyPrefab.SetTargetObject(playerObject);
                }
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
        if(poolObjectDictionary.Count == 0)
        {
            Debug.Log($"スタックがありません");

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
        Debug.Log($"プールへ返却");
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

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.finishedGeneratePlayer += Initialize;
        EventManager.Instance.pushExperiencePoint += Push;
    }
}
