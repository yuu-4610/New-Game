using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;
    private Dictionary<string, GameObject> objects = new();

    private void Awake()
    {
        //シングルトン
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public GameObject GetOrCreate(string key, GameObject prefab)
    {
        //objects を見て既に登録されているオブジェジェクトであればそのまま返す
        //登録されていなければ第2引数の参照を元に生成 + 返す
        if (!objects.TryGetValue(key, out var obj) || obj == null)
        {
            obj = Instantiate(prefab);
            obj.name = key;
            objects[key] = obj;
        }
        return obj;
    }
    // Start is called before the first frame update
    //オブジェクトのコンポーネント参照をする
    public T GetOtherComponent<T>(string key, GameObject prefab) where T : Component
    {
        var objct = GetOrCreate(key, prefab);
        return objct.GetComponent<T>();
    }
    public GameObject GetObject(string key)
    {
        if (!objects.ContainsKey(key))
        {
            Debug.Log("登録されていません");

            return null;
        }

        return objects[key];
    }
    
    public void Register(string key, GameObject obj)
    {
        objects[key] = obj;
    }
}