using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePointManager : MonoBehaviour
{
    [SerializeField] ExperiencePointsPrefabPool experiencePointsPrefabPool;
    [SerializeField] ExperiencePointSeachGrid experiencePointSeachGrid;

    private GameObject experiencePointObject;
    private int lowGradePointRate = 70;
    private int middleGradePointRate = 90;
    private int rateValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void PopExperiencePoint()
    {
        rateValue = Random.Range(0, 100);

        if(rateValue <= lowGradePointRate) experiencePointObject = experiencePointsPrefabPool.Pop(ExperiencePointsObjectPoolName.LowGradePoints.ToString());
        else if (rateValue <= middleGradePointRate) experiencePointObject = experiencePointsPrefabPool.Pop(ExperiencePointsObjectPoolName.MiddleGradePoints.ToString());

        //Grid‚É“o˜^
        experiencePointSeachGrid.AddExperiencePointObject(experiencePointObject);
    }

    private void PushExperiencePoint(string pushName, GameObject experiencePointObject)
    {
        //ƒv[ƒ‹‚É–ß‚·
        experiencePointsPrefabPool.Push(pushName, experiencePointObject);

        //Grid‚©‚ç‰ðœ
        experiencePointSeachGrid.RemoveExperiencePointObject(experiencePointObject);
    }

    //ƒCƒxƒ“ƒg“o˜^
    private IEnumerator EventRegist()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.popExperiencePoint += PopExperiencePoint;
        EventManager.Instance.pushExperiencePoint += PushExperiencePoint;
    }
}
