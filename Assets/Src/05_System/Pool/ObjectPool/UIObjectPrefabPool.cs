using System;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectPrefabPool : ObjectPoolBase
{
    [SerializeField] StackUIObject stackUIObject;
    [SerializeField] Transform canvasTransform;
    private Dictionary<string, Stack<GameObject>> poolObjectDictionary = new Dictionary<string, Stack<GameObject>>();
    private Dictionary<string, GameObject> parentObjects = new Dictionary<string, GameObject>();

    private GameObject parentTopObject;
    private int initializeSize = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        parentTopObject = new GameObject("UIParentObject");
        parentTopObject.transform.parent = canvasTransform;

        Initialize();
    }
    private void Initialize()
    {
        //-----------------プール用オブジェクトの追加、Dictionaryで管理-----------------//
        //指定したEnumの値をすべて取得
        UIObjectName[] uiObjectNameArray = (UIObjectName[])System.Enum.GetValues(typeof(UIObjectName));
        //uiObjectNameArrayの値をstring配列で管理
        string[] attackEffectNames = Array.ConvertAll(uiObjectNameArray, uiObject => uiObject.ToString());

        //取得したEnumの値の数だけ繰り返し処理
        for (int i = 0; i < uiObjectNameArray.Length; ++i)
        {
            try
            {
                //親オブジェクトの取得
                var parentObject = GetOrCreatePanetObject(attackEffectNames[i]);

                poolObjectDictionary.Add(attackEffectNames[i], new Stack<GameObject>());

                for (int j = 0; j < initializeSize; ++j)
                {
                    var UIObjectPrefab = Instantiate(stackUIObject.UIPrefabObject[i], canvasTransform.position, Quaternion.identity, parentObject.transform);
                    UIObjectPrefab.SetActive(false);
                    poolObjectDictionary[attackEffectNames[i]].Push(UIObjectPrefab);
                }
            }
            catch
            {
                continue;
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
            //Canvas内のUIを纏めるオブジェクトのため、RectTransformに変換
            parent = new GameObject(name, typeof(RectTransform));
            parentObjects[name] = parent;
        }
        parent.transform.parent = parentTopObject.transform;

        return parent;
    }
}
