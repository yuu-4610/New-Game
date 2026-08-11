using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] ExperiencePointSeachGrid experiencePointSeachGrid;

    [SerializeField] GameObject RengeObject;
    private GameObject Robject;

    private GameObject playerObject;
    

    private void Start()
    {
        Robject = Instantiate(RengeObject);
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
            experiencePointSeachGrid.FindMagnetTargets(playerObject.transform.position, 2f);
            TestMethod();
        }
    }

    private void GeneratePlayer()
    {
        var player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (playerObject == null) playerObject = player;

        //生成したプレイヤーオブジェクトの参照を登録
        ObjectManager.Instance.Register(AcquisitionObjectName.Player.ToString(), player);

        //エネミー生成処理を呼び出す
        EventManager.Instance.EnemyGenerateEvent();
    }

    private void TestMethod()
    {
        Robject.transform.localScale = new Vector3(experiencePointSeachGrid.cellRenge * 2, experiencePointSeachGrid.cellRenge * 2, 1);
        Robject.transform.position = playerObject.transform.position;
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
