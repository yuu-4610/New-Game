using System.Collections;
using UnityEngine;

public class LevelUpBonusBoard : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GameObject selectButtonPrefab; //レベルアップボーナス選択用UI
    [SerializeField] GameObject LevelUpBonusBoardUI;
    [SerializeField] Transform[] positionObject; //配置用座標
    private LevelUpBonus levelUpBonus;
    private TimeStopProcess timeStopProcess;
    private int selectUINumberOfPiece = 3;


    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }
    private void OnDisable()
    {
        
    }
    void Start()
    {
        timeStopProcess = new TimeStopProcess();

        //親オブジェクトの作成
        var parentObject = new GameObject();
        parentObject.name = GenerateParentObjectName.selectButtonParentObject.ToString();
        parentObject.transform.parent = this.gameObject.transform;

        //選択用ボタンの生成
        var selectButtons = new GameObject[selectUINumberOfPiece];
        for (int i = 0; i < positionObject.Length; i++)
        {
            var selectButtonObject = Instantiate(selectButtonPrefab);
            selectButtonObject.SetActive(false);
            selectButtons[i] = selectButtonObject;
            //親子関係の構築
            selectButtons[i].transform.parent = parentObject.transform;
        }

        levelUpBonus = new LevelUpBonus(playerManager, selectButtons, positionObject);

        //Editorでも設定
        LevelUpBonusBoardUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelUpProsess()
    {
        timeStopProcess.TimeStop();
        LevelUpBonusBoardUI.SetActive(true);
        levelUpBonus.ActiveSelectButton();
    }
    public void LevelUpBonusBoardInActive()
    {
        LevelUpBonusBoardUI.SetActive(false);
        levelUpBonus.RestSelectUI();
        timeStopProcess.TimeReStart();
    }
    public void RemoveAttackObjectNumberList(int number)
    {
        levelUpBonus.RemoveNumberList(number);
    }

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        
    }
}
