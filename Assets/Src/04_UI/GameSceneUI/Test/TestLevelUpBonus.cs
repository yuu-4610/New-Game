using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestLevelUpBonus : MonoBehaviour
{
    [SerializeField] GameObject selectButton; //レベルアップボーナス選択用UI
    [SerializeField] GameObject[] positionObject; //配置用座標
    private CSVImportProcessCall csvImportProcessCall; //CSV取得用
    private GameObject[] attackSelectButton; //複製したレベルアップボーナス用UI
    private readonly List<int> remainingNumbers = new List<int>();
    private string[][] csvTextData; //テキストデータ
    private string[][] csvIconPathData; //アイコンデータ
    private int selectUINumberOfPiece = 3;
    private int[] addNumber = new int[3];
    private TestGameSceneUIProcess testGameSceneUIProcess;
    private bool isStart;

    void Start()
    {
        csvImportProcessCall = new CSVImportProcessCall();
        FileImport();

        var parentObject = new GameObject();
        parentObject.name = "parentObject";
        parentObject.transform.parent = this.gameObject.transform;

        var selectButtons = new GameObject[selectUINumberOfPiece];
        for (int i = 0; i < positionObject.Length; i++)
        {
            var selectButtonObject = Instantiate(selectButton, parentObject.transform);
            selectButtonObject.SetActive(false);
            selectButtons[i] = selectButtonObject;
        }
        attackSelectButton = selectButtons;
        for (int i = 0; i < 8; i++)
        {
            remainingNumbers.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveLevelUpBonusBord(int[] indexs)
    {
        //３回行う
        for (int i = 0; i < positionObject.Length; ++i)
        {
            //座標の指定
            attackSelectButton[i].transform.position = positionObject[i].transform.position;

            attackSelectButton[i].transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(csvIconPathData[(int)indexs[i]][1].Trim().Trim('\uFEFF'));
            //指定オブジェクトのレベル＋１を表示
            //var level = GetLevel()
            attackSelectButton[i].transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = $"Lev.{csvTextData[indexs[i] * 5][1]}";
            //attackSelectButton[0].transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = csvTextData[indexs[i] * 5 + level][1];
            attackSelectButton[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = csvTextData[indexs[i] * 5][2];
            //attackSelectButton[0].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = csvTextData[indexs[i] * 5 + level][2];

            attackSelectButton[i].SetActive(true);
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
            testGameSceneUIProcess.ActiveLevelUpBonusBord(index, attackSelectButton, positionObject, csvIconPathData, csvTextData);
        }
        else
        {
            Debug.Log("準備段階");
        }
    }
    private void TestSelectUI()
    {
        if (isStart)
        {
            testGameSceneUIProcess.TestSelectUI(remainingNumbers, addNumber);
            isStart = false;
        }
    }
}
