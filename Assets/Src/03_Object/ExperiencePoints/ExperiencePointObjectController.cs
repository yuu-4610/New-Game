using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperiencePointObjectController : MonoBehaviour
{
    [SerializeField] ExperiencePointsPrefabPool experiencePointsPrefabPool; //プール管理クラス
    [SerializeField] ExperiencePointSeachGrid experiencePointSeachGrid; //グリッドシステムクラス

    private GameObject experiencePointObject; //ポップされた経験値オブジェクトの参照を一時的に保持する
    private int lowGradePointRate = 70; //確率の上限値
    private int middleGradePointRate = 90; //確率の上限値
    private int rateValue; //ランダム値

    private float testPoptime;
    private bool isTestPop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testPoptime = 0;
        isTestPop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //TestPop();
    }

    private void OnEnable()
    {
        //イベント登録
        StartCoroutine(EventRegist());
    }

    private void OnDisable()
    {
        EventManager.Instance.returnExperiencePoint -= ReturnExperiencePoint;
        EventManager.Instance.popExperiencePoint -= PopExperiencePoint;
    }

    private void PopExperiencePoint(Vector3 position)
    {
        rateValue = UnityEngine.Random.Range(0, 100);

        //rateValueの値によってポップするオブジェクトを選択し、プールから取り出す
        if (rateValue <= lowGradePointRate) experiencePointObject = experiencePointsPrefabPool.Pop(ExperiencePointsObjectPoolName.LowGradePoints.ToString());
        else if (rateValue <= middleGradePointRate) experiencePointObject = experiencePointsPrefabPool.Pop(ExperiencePointsObjectPoolName.MiddleGradePoints.ToString());

        //何かしら帰ってきたら
        if (experiencePointObject != null)
        {
            experiencePointObject.transform.position = position;
            //Gridに登録
            experiencePointSeachGrid.AddExperiencePointObject(experiencePointObject);
        }

        else
        {
            Debug.Log($"{experiencePointObject.name}のストックがありません");
        }
        experiencePointObject = null;
    }

    private void ReturnExperiencePoint(GameObject experiencePointObject)
    {
        //Gridから解除
        experiencePointSeachGrid.RemoveExperiencePointObject(experiencePointObject);

        experiencePointObject.GetComponent<IExperiencePoint>().PushProcessCheck();
    }

    //イベント登録
    private IEnumerator EventRegist()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.popExperiencePoint += PopExperiencePoint;
        EventManager.Instance.returnExperiencePoint += ReturnExperiencePoint;
    }

    private void TestPop()
    {
        testPoptime += Time.deltaTime;
        if (testPoptime > 3 && isTestPop)
        {
            isTestPop = false;
            PopExperiencePoint(new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), 0));

            testPoptime = 0;

            isTestPop = true;
        }
    }
}
