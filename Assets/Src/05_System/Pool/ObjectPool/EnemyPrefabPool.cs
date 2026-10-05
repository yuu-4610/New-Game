using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPrefabPool : ObjectPoolBase
{
    [Header("対象Enumクラスに記載されている順番でアタッチ")]
    [SerializeField] StackEnemyObject stackEnemyObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Dictionary<string, Stack<GameObject>> poolObjectDictionary = new Dictionary<string, Stack<GameObject>>();
    Dictionary<string, GameObject> parentObjects = new Dictionary<string, GameObject>();
    private Stack<GameObject> skeletontPool = new();
    private GameObject playerObject;
    //public bool isInitialize = default;
    private int initializeSize = 30;

    private void Initialize()
    {
        //-----------------プール用オブジェクトの追加、Dictionaryで管理-----------------//
        //指定したEnumの値をすべて取得
        EnemyObjectPoolName[] enemyArray = (EnemyObjectPoolName[])System.Enum.GetValues(typeof(EnemyObjectPoolName));
        //enemyArrayの値をstring配列で管理
        string[] enemyNames = Array.ConvertAll(enemyArray, enemy => enemy.ToString());
        //プレイヤーオブジェクトを取得
        playerObject = ObjectManager.Instance.GetObject(AcquisitionObjectName.Player.ToString());

        //取得したEnumの値の数だけ繰り返し処理
        for (int i = 0; i < enemyArray.Length; ++i)
        {
            var parentObject = GetOrCreatePanetObject(enemyNames[i]);
            //Dictonaryに追加
            poolObjectDictionary.Add(enemyNames[i], new Stack<GameObject>());

            for (int j = 0; j < initializeSize; ++j)
            {
                var enmeyObjectPrefab = Instantiate(stackEnemyObject.enemyPrefabObject[i], new (0, 0, 0), Quaternion.identity, parentObject.transform);
                enmeyObjectPrefab.SetActive(false);
                if (enmeyObjectPrefab.TryGetComponent<IEnemy>(out var enemyPrefab))
                {
                    enemyPrefab.SetTargetObject(playerObject);
                }
                poolObjectDictionary[enemyNames[i]].Push(enmeyObjectPrefab);
            }
        }

        //isInitialize = true;
    }

    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }
    private void Start()
    {
        
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

    private IEnumerator EventRegister()
    {
        while(EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.finishedGeneratePlayer += Initialize;
        EventManager.Instance.enemyObjectPush += Push;
    }

    private IEnumerator EventRegist()
    {
        while(EventManager.Instance == null)
        {
            yield return null;
        }

        
    }
}
