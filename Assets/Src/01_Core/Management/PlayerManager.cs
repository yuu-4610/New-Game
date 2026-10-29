using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] ExperiencePointSeachGrid experiencePointSeachGrid;
    [SerializeField] StackAttackControllerObject[] controllerObjectList;
    [SerializeField] AttackEffectPrefabPool attackEffectPrefabPool;
    [SerializeField] LevelUpBonusBoard levelUpBonusBoard;
    //private GameObject Robject;
    private Dictionary<AttackEffectObjectPoolName, int> attackObjectLevel = new Dictionary<AttackEffectObjectPoolName, int>();
    private GameObject playerObject;
    private CSVImportProcessCall csvImportProcessCall;
    private PlayerController playerController;
    private AutoAttackController autoAttackController;
    private string[][] csvCharaMasterData;
    private string[][] csvCharaStatusData;
    private float timeCount;


    private void Start()
    {
        csvImportProcessCall = new CSVImportProcessCall();
        FileImport();
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
            timeCount += Time.deltaTime;
            if(timeCount > 2f)
            {
                playerObject.GetComponent<IPlayer>().Damageable(2f);
                timeCount = 0f;
            }
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

        autoAttackController = playerObject.GetComponent<AutoAttackController>();
        //攻撃コントロールオブジェクトを渡す
        autoAttackController.SetControllerObject(attackObjects);

        playerController = playerObject.GetComponent<PlayerController>();
        //ステータスセット
        playerController.UpLeveling(int.Parse(csvCharaStatusData[playerController.GetCurrentLevel()][2]), int.Parse(csvCharaStatusData[playerController.GetCurrentLevel()][3]));

        //生成したプレイヤーオブジェクトの参照を登録
        ObjectManager.Instance.Register(AcquisitionObjectName.Player.ToString(), playerObject);

        //プレイヤーオブジェクトの参照登録後、イベント発火
        EventManager.Instance.FinishedGeneratePlayerEvent();
    }
    public int CollectionChek(int index)
    {
        if (!Enum.IsDefined(typeof(AttackEffectObjectPoolName), index)) return 9;
        AttackEffectObjectPoolName attackEffectObjectName = (AttackEffectObjectPoolName)index;
        //登録があれば現在のレベルを返す
        if (attackObjectLevel.ContainsKey(attackEffectObjectName))
        {
            return attackObjectLevel[attackEffectObjectName];
        }
        //なければ０を返す
        else
        {
            return 0;
        }
    }
    public void ContorllerCollectionChek(AttackEffectObjectPoolName attackObjectName)
    {
        var processIndex = autoAttackController.CollectionCheck(attackObjectName);
        Debug.Log($"processIndex:{processIndex}");

        if (processIndex == 0)
        {
            //新規登録
            var attackObjects = new GameObject[4];
            for (int i = 0; i < attackObjects.Length; ++i)
            {
                var attackObject = attackEffectPrefabPool.Pop(attackObjectName.ToString());
                attackObjects[i] = attackObject;
            }
            //コレクションへの追加
            attackObjectLevel.Add(attackObjectName, 1);
            autoAttackController.AttackRegister(attackObjectName, attackObjects);
        }
        else if (processIndex == 1)
        {
            //レベルアップ
            autoAttackController.UpLeveling(attackObjectName);
            //コレクションの値の更新
            attackObjectLevel[attackObjectName] = autoAttackController.GetAttackLevel(attackObjectName) + 1;
            //最大レベルの場合、選択UIの候補から削除する
            if ((int)attackObjectLevel[attackObjectName] == 5)
            {
                levelUpBonusBoard.RemoveAttackObjectNumberList((int)attackObjectName);
            }
        }
        else if (processIndex == 2)
        {
            Debug.Log("登録できませんでした。処理を終了します");
            return;
        }

        //レベルアップボーナス選択画面を非活性化する
        levelUpBonusBoard.LevelUpBonusBoardInActive();
    }

    private void TestMethod()
    {
        //Robject.transform.localScale = new Vector3(experiencePointSeachGrid.cellRenge * 2, experiencePointSeachGrid.cellRenge * 2, 1);
        //Robject.transform.position = playerObject.transform.position;
    }
    private async void FileImport()
    {
        var csv = Resources.Load<TextAsset>(Path.Combine(PathHelper.ToName(ResourcePath.importCSVFileStrageLocation), ImportCSVFileName.CharacterMaster.ToString()));
        csvCharaMasterData = await csvImportProcessCall.GetCharaMaster(csv.text);

        var csv1 = Resources.Load<TextAsset>(Path.Combine(PathHelper.ToName(ResourcePath.importCSVFileStrageLocation), ImportCSVFileName.CharacterStatusData.ToString()));
        csvCharaStatusData = await csvImportProcessCall.GetCharaStatusData(csv1.text);
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
