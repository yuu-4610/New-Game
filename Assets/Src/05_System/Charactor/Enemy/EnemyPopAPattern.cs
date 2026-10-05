using System.IO;
using UnityEngine;

public class EnemyPopAPattern
{
    private GameObject targetObject;
    private CSVImportProcessCall csvIportProcessCall;
    private float[][] csvData;

    public EnemyPopAPattern(GameObject targetObject)
    {
        this.targetObject = targetObject;

        csvIportProcessCall = new CSVImportProcessCall();
        FileImport();
    }

    // Update is called once per frame

    public async void FileImport()
    {
        TextAsset csv = Resources.Load<TextAsset>(Path.Combine(PathHelper.ToName(ResourcePath.importCSVFileStrageLocation), ImportCSVFileName.PopPattern.ToString()));
        var text = csv.text;
        csvData = await csvIportProcessCall.GetPopPatternData(text);
    }

    public void Pop()
    {
        for (int i = 0; i < 4; i++)
        {
            ////指定エネミーを取得
            //var popObject = EnemyPrefabPool.Instance.Pop(EnemyObjectPoolName.Skeleton.ToString());

            ////プレイヤーの位置＋指定値の距離をとる
            ////[1]ー＞X座標、[2]ー＞Y座標
            //popObject.transform.position = new Vector3(targetObject.transform.position.x + csvData[i][1], targetObject.transform.position.y + csvData[i][2], 0);
        }
    }
}
