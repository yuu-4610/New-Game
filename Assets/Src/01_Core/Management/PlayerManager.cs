using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] ExperiencePointSeachGrid experiencePointSeachGrid;
    [SerializeField] StackAttackControllerObject[] controllerObjectList;
    //private GameObject Robject;

    private GameObject playerObject;
    

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(EventRegist());
    }

    private void OnDisable()
    {
        EventManager.Instance.playerGenerate -= GeneratePlayer;
    }

    private void Update()
    {
        if (playerObject != null)
        {
            //if(experiencePointSeachGrid != null)
            //{
            //    experiencePointSeachGrid.FindMagnetTargets(playerObject.transform.position, 1.5f);
            //    TestMethod();
            //}
        }
    }

    private void GeneratePlayer()
    {
        var attackObjects = new GameObject[controllerObjectList.Length];
        for(int i = 0; i < controllerObjectList.Length; i++)
        {
            attackObjects[i] = Instantiate(controllerObjectList[i].attackObject);
            attackObjects[i].transform.parent = this.gameObject.transform;
        }
        var player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (playerObject == null) playerObject = player;
        playerObject.transform.parent = this.gameObject.transform;

        playerObject.GetComponent<AutoAttackController>().SetControllerObject(attackObjects);

        //生成したプレイヤーオブジェクトの参照を登録
        ObjectManager.Instance.Register(AcquisitionObjectName.Player.ToString(), playerObject);

        //プレイヤー生成後に
        EventManager.Instance.FinishedGeneratePlayerEvent();
    }

    private void TestMethod()
    {
        //Robject.transform.localScale = new Vector3(experiencePointSeachGrid.cellRenge * 2, experiencePointSeachGrid.cellRenge * 2, 1);
        //Robject.transform.position = playerObject.transform.position;
    }

    private IEnumerator EventRegist()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.playerGenerate += GeneratePlayer;
    }
}
