using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] ExperiencePointSeachGrid experiencePointSeachGrid;

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
        experiencePointSeachGrid.FindMagnetTargets(playerObject.transform.position, 10);
    }

    private void GeneratePlayer()
    {
        var player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (player == null) playerObject = player;

        //生成したプレイヤーオブジェクトの参照を登録
        ObjectManager.Instance.Register(AcquisitionObjectName.Player.ToString(), player);

        //エネミー生成処理を呼び出す
        EventManager.Instance.EnemyGenerateEvent();
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
