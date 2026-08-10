using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    private float timeCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
    }

    public float GetTime()
    {
        return timeCount;
    }
}
