using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPopSystem : MonoBehaviour
{
    [SerializeField] TimeCounter timeCounter;
    private GameObject targetObject; //プレイヤーオブジェクト
    private EnemyPopAPattern aPattern;
    private CSVImportProcessCall csvImportProcessCall;
    private bool isPopTimeUpdate = default;
    private float[][] csvData;
    private int columnId = 0;
    private const int columnPopTimeSconds = 1;

    void Awake()
    {
        csvImportProcessCall = new CSVImportProcessCall();
        FileImport();
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }
    private void OnDisable()
    {
        EventManager.Instance.finishedGeneratePlayer -= Initialize;
    }

    private void Update()
    {
        if (isPopTimeUpdate)
        {
            if (timeCounter.GetTime() > csvData[columnId][columnPopTimeSconds])
            {
                //ポップアップ処理を呼び出す
                Debug.Log((int)timeCounter.GetTime());
                ++columnId;
            }
        }
    }

    // Update is called once per frame

    public void Pop()
    {
        aPattern.Pop();
    }
    private async void FileImport()
    {
        TextAsset csv = Resources.Load<TextAsset>(Path.Combine(PathHelper.ToName(ResourcePath.importCSVFileStrageLocation), ImportCSVFileName.PopTime.ToString()));
        var text = csv.text;
        csvData = await csvImportProcessCall.GetPopTimeData(text);
    }

    private void Initialize()
    {
        //プレイヤーオブジェクトの参照を取得
        targetObject = ObjectManager.Instance.GetObject(AcquisitionObjectName.Player.ToString());

        aPattern = new EnemyPopAPattern(targetObject);
    }

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.finishedGeneratePlayer += Initialize;
    }
}
