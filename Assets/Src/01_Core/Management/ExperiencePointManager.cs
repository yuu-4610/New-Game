using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperiencePointManager : MonoBehaviour
{
    [SerializeField] ExperiencePointsPrefabPool experiencePointsPrefabPool;
    [SerializeField] ExperiencePointSeachGrid experiencePointSeachGrid;

    private GameObject experiencePointObject;
    private int lowGradePointRate = 70;
    private int middleGradePointRate = 90;
    private int rateValue;

    private float time;
    private bool isPop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        isPop = true;
    }

    // Update is called once per frame
    void Update()
    {
        TestPop();
    }

    private void OnEnable()
    {
        StartCoroutine(EventRegist());
    }

    private void OnDisable()
    {
        EventManager.Instance.pushExperiencePoint -= PushExperiencePoint;
        EventManager.Instance.popExperiencePoint -= PopExperiencePoint;
    }

    private void PopExperiencePoint(Vector3 position)
    {
        rateValue = UnityEngine.Random.Range(0, 100);

        //rateValueの値によってポップするオブジェクトを選択し、プールから取り出す
        if (rateValue <= lowGradePointRate) experiencePointObject = experiencePointsPrefabPool.Pop(ExperiencePointsObjectPoolName.LowGradePoints.ToString());
        else if (rateValue <= middleGradePointRate) experiencePointObject = experiencePointsPrefabPool.Pop(ExperiencePointsObjectPoolName.MiddleGradePoints.ToString());

        //何も返って来なかったら
        if (experiencePointObject != null)
        {
            experiencePointObject.transform.position = position;
            //Gridに登録
            experiencePointSeachGrid.AddExperiencePointObject(experiencePointObject);
        }
    }

    private void PushExperiencePoint(string pushName, GameObject experiencePointObject)
    {
        //プールに戻す
        experiencePointsPrefabPool.Push(pushName, experiencePointObject);

        //Gridから解除
        experiencePointSeachGrid.RemoveExperiencePointObject(experiencePointObject);
    }

    //イベント登録
    private IEnumerator EventRegist()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.popExperiencePoint += PopExperiencePoint;
        EventManager.Instance.pushExperiencePoint += PushExperiencePoint;
    }

    private void TestPop()
    {
        time += Time.deltaTime;
        if (time > 3 && isPop)
        {
            isPop = false;
            PopExperiencePoint(new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), 0));

            time = 0;

            isPop = true;
        }
    }
}
