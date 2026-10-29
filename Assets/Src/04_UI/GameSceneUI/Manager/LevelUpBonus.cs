using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpBonus
{
    private PlayerManager playerManager;
    private CSVImportProcessCall csvImportProcessCall; //CSV取得用
    private GameObject[] attackSelectButton; //複製したレベルアップボーナス用UI
    private UnityAction[] listener = new UnityAction[3];
    private readonly List<int> remainingNumbers = new List<int>(); //攻撃オブジェクト番号
    private AttackEffectObjectPoolName[] attackEffectObjectPoolNames = new AttackEffectObjectPoolName[3];
    UnityAction[] listeners = new UnityAction[3];
    private string[][] csvTextData; //テキストデータ
    private string[][] csvIconPathData; //アイコンデータ
    private int[] addNumber = new int[3];
    private int selectUINumberOfPiece;

    private TestGameSceneUIProcess testGameSceneUIProcess;

    private bool isStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public LevelUpBonus(PlayerManager playerManager, GameObject[] attackSelectButton, Transform[] objectArrangementPosition)
    {
        csvImportProcessCall = new CSVImportProcessCall();
        FileImport();
        for (int i = 0; i < 8; i++)
        {
            remainingNumbers.Add(i);
        }

        this.playerManager = playerManager;
        this.attackSelectButton = attackSelectButton;
        Initialize(objectArrangementPosition);
    }

    public void ActiveSelectButton()
    {
        var index = new int();
        //ランダムな値を取得、取得したものを削除していく
        for (int i = 0; i < selectUINumberOfPiece; i++)
        {
            index = UnityEngine.Random.Range(0, remainingNumbers.Count);
            addNumber[i] = remainingNumbers[index];
            //Debug.Log($"Removeindexs：{addNumber[i]}");
            remainingNumbers.RemoveAt(index);
        }
        LevelUpBonusProcess(addNumber);
        //消した分を追加
        for (int i = 0; i < addNumber.Length; ++i)
        {
            //Debug.Log($"Addindexs：{addNumber[i]}");
            remainingNumbers.Add(addNumber[i]);
        }
    }

    private void Initialize(Transform[] objectArrangementPosition)
    {
        selectUINumberOfPiece = objectArrangementPosition.Length;
        for (int i = 0; i < objectArrangementPosition.Length; ++i)
        {
            //座標の指定
            attackSelectButton[i].transform.position = objectArrangementPosition[i].position;
        }
    }

    private void LevelUpBonusProcess(int[] indexs)
    {
        //RestSelectUI();
        //３回行う
        for (int i = 0; i < selectUINumberOfPiece; ++i)
        {
            //存在確認
            if (!Enum.IsDefined(typeof(AttackEffectObjectPoolName), indexs[i]))
            {
                Debug.Log("定義にありません");
                continue;
            }
            //指定オブジェクトのレベル＋１を表示　＜－CSVから取得
            var level = playerManager.CollectionChek(indexs[i]);
            if (level == 9)
            {
                Debug.Log("定義にありません");
                continue;
            }
            Debug.Log($"{(AttackEffectObjectPoolName)indexs[i]}　Lev：{level}");

            //ボタンオブジェクトの子オブジェクトから指定名のオブジェクトを取得
            //アイコンを設定
            attackSelectButton[i].transform.Find(ReferenceUIObjectName.Icon.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>(csvIconPathData[(int)indexs[i]][1].Trim().Trim('\uFEFF'));
            //テキストの設定
            attackSelectButton[i].transform.Find(ReferenceUIObjectName.AttackName.ToString()).GetComponent<TextMeshProUGUI>().text = $"{csvTextData[(indexs[i] * 5) + level][1]}";
            attackSelectButton[i].transform.Find(ReferenceUIObjectName.LevelText.ToString()).GetComponent<TextMeshProUGUI>().text = $"Lev.{csvTextData[(indexs[i] * 5) + level][2]}";
            attackSelectButton[i].transform.Find(ReferenceUIObjectName.Text.ToString()).GetComponent<TextMeshProUGUI>().text = csvTextData[(indexs[i] * 5) + level][3];

            AttackEffectObjectPoolName attackEffectObjectPoolName = (AttackEffectObjectPoolName)indexs[i];
            attackEffectObjectPoolNames[i] = attackEffectObjectPoolName;
            //ボタンに引数を設定
            listeners[i] = () => SelectButtonProcess(attackEffectObjectPoolName);
            attackSelectButton[i].GetComponent<Button>().onClick.AddListener(listeners[i]);
            
            //活性化
            attackSelectButton[i].SetActive(true);
        }


    }

    private void SelectButtonProcess(AttackEffectObjectPoolName attackEffectObjectName)
    {
        playerManager.ContorllerCollectionChek(attackEffectObjectName);
    }

    public void RemoveNumberList(int index)
    {
        remainingNumbers.RemoveAt(index);
    }
    public void RestSelectUI()
    {
        Debug.Log($"リセット開始：{attackEffectObjectPoolNames.Length}");
        for (int i = 0; i < attackSelectButton.Length; i++)
        {
            Debug.Log($"攻撃：{attackEffectObjectPoolNames[i]}");
            attackSelectButton[i].GetComponent<Button>().onClick.RemoveListener(listeners[i]);
            attackSelectButton[i].SetActive(false);
        }
    }


    private async void FileImport()
    {
        var csv = Resources.Load<TextAsset>(Path.Combine(PathHelper.ToName(ResourcePath.importCSVFileStrageLocation), ImportCSVFileName.AttackObjectMaster.ToString()));
        csvTextData = await csvImportProcessCall.GetAttackObjectMaster(csv.text);

        var csv1 = Resources.Load<TextAsset>(Path.Combine(PathHelper.ToName(ResourcePath.importCSVFileStrageLocation), ImportCSVFileName.AttackObjectIconMaster.ToString()));
        csvIconPathData = await csvImportProcessCall.GetAttackObjectIconMaster(csv1.text);

        isStart = true;
    }

    private void TestInitialize()
    {
        testGameSceneUIProcess = new TestGameSceneUIProcess();
    }
    public void TestSelection(int index)
    {
        if (isStart)
        {
            //testGameSceneUIProcess.ActiveLevelUpBonusBord(index, attackSelectButton, positionObject, csvIconPathData, csvTextData);
        }
        else
        {
            Debug.Log("準備段階");
        }
    }
    
}
