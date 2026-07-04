using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField] GameObject slashEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool isInitialize = default;
    private Dictionary<string, Stack<GameObject>> poolObjectNames = new Dictionary<string, Stack<GameObject>>();
    private Stack<GameObject> slashEffectPool = new();
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

        for (int i = 0; i < initializeSize; i++)
        {
            var slashEffectPrefab = Instantiate(slashEffect);
            slashEffectPrefab.SetActive(false);
            slashEffectPool.Push(slashEffectPrefab);
        }
        SetObjects();

        
    }

    private void SetObjects()
    {
        Debug.Log("登録処理開始");
        if(slashEffectPool != null) SetDictionary(poolObjectName.slashEffect, slashEffectPool);

        Debug.Log("登録処理終了");
        isInitialize = true;
    }

    public GameObject Get(poolObjectName objectName)
    {
        if (slashEffectPool.Count == 0)
        {
            var slashEffectPrefab = Instantiate(slashEffect);
            slashEffectPrefab.SetActive(false);
            slashEffectPool.Push(slashEffectPrefab);
        }

        var prefab = poolObjectNames[objectName.ToString()].Pop();
        prefab.SetActive(true);

        return prefab;
    }

    public void Return(poolObjectName returnObjectName, GameObject returnObject)
    {
        returnObject.SetActive(false);
        var prefab = poolObjectNames[returnObjectName.ToString()];

        prefab.Push(returnObject);
    }
    private void SetDictionary(poolObjectName objectName, Stack<GameObject> stackObject)
    {
        poolObjectNames.Add(objectName.ToString(), stackObject);
    }
}
