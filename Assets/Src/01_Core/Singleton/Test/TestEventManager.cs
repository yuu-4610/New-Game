using System;
using UnityEngine;

public class TestEventManager : MonoBehaviour
{
    public static TestEventManager Instance { get; private set; }

    public event Action TestGridUpdate;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void TestGridUpdateEvent()
    {
        TestGridUpdate?.Invoke();
    }
}
