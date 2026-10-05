using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ExperiencePointSeachGrid : MonoBehaviour
{
    public Dictionary<Vector2Int, List<GameObject>> grid { get; private set; } = new(); //オブジェクトの座標を登録
    public bool isInitialize { get; private set; } = false;
    [SerializeField] TestExperiencePointSeachGrid testExperiencePointSeachGrid; //テスト用クラス

    private float cellSize = 0.25f;
    private Vector2Int playerCell; //プレイヤーのセル
    public int cellRenge { get; private set; } //セルの上下値

    private int count;
    private bool isGenerateSqure;
    private List<GameObject> processObjects = new List<GameObject>();

    private void Start()
    {
        isInitialize = true;
    }

    //指定値を基準とし、座標の計算をする
    private Vector2Int GetCell(Vector3 position)
    {
        //Debug.Log($"int：{(int)(position.x / cellSize)}");
        //Debug.Log($"Mathf：{Mathf.FloorToInt(position.x / cellSize)}");
        //Debug.Log($"\n(X, Y) = ({(int)(position.x / cellSize)}, {(int)(position.y / cellSize)})");
        return new Vector2Int(
            (int)(position.x / cellSize),
            (int)(position.y / cellSize)
            );
    }

    public void Update()
    {
        if (!isGenerateSqure)
        {
            testExperiencePointSeachGrid.TestGridGenerate(cellSize);
            isGenerateSqure = true;
        }
    }

    public void AddExperiencePointObject(GameObject experiencePointObject)
    {
        Vector2Int cell = GetCell(experiencePointObject.transform.position);
        //上記で取得したKeyが存在しなければ、リストを追加
        if (!grid.TryGetValue(cell, out var pointObject))
        {
            pointObject = new List<GameObject>();
            grid.Add(cell, pointObject);
        }

        experiencePointObject.GetComponent<IExperiencePoint>().SetCell(cell);
        pointObject.Add(experiencePointObject);

        //Debug.Log("ポップしたので追加できた");
    }

    public void RemoveExperiencePointObject(GameObject experiencePointObject)
    {
        var cell = experiencePointObject.GetComponent<IExperiencePoint>().GetCell();

        if (!grid.TryGetValue(cell, out var pointObject))
            return;

        pointObject.Remove(experiencePointObject);

        if (pointObject.Count == 0)
            grid.Remove(cell);
        Debug.Log($"Grid登録解除した");
    }

    public void FindMagnetTargets(Vector3 playerPosition, float radius)
    {
        if (grid == null) return;
        //セル座標を取得
        var playerCell = GetCell(playerPosition);
        //Debug.Log($"現在のセル：{playerCell}");
        //if (this.playerCell == playerCell)
        //{
        //    return;
        //}
        //else
        //{
        //    this.playerCell = playerCell;
        //    Debug.Log($"現在のプレイヤーのセル{playerCell}");
            
        //}
        this.playerCell = playerCell;

        var cellRange = Mathf.CeilToInt(radius / cellSize);
        //this.cellRenge = cellRange;
        //円を求める
        var radiusSqr = radius * radius;

        //調査する範囲（x,y）
        for (int x = -cellRange; x <= cellRange; ++x)
        {
            for (int y = -cellRange; y <= cellRange; ++y)
            {
                Vector2Int cell = this.playerCell + new Vector2Int(x, y);
                //Vector2Int cell = this.playerCell;

                //登録しているDictionaryからcellをキーにオブジェクトを検索
                if (!grid.TryGetValue(cell, out var cellObject))
                    continue;

                //オブジェクトが範囲内にあれば吸収
                foreach (GameObject pointObject in cellObject)
                {
                    Vector3 delta = playerPosition - pointObject.transform.position;

                    if (delta.sqrMagnitude <= radiusSqr)
                    {
                        Debug.Log($"処理２：{pointObject}");
                        Debug.Log($"存在チェックprocessObjects：{processObjects}");
                        processObjects.Add(pointObject);

                        Debug.Log($"吸引：{processObjects.Count}");
                    }
                }
                if (processObjects != null)
                {
                    for (int i = 0; i < processObjects.Count; ++i)
                    {
                        Debug.Log($"処理３");
                        //経験値オブジェクトマネージャーに渡す（プール返却用イベント）
                        EventManager.Instance.ReturnExperiencePointEvent(processObjects[i]);
                        //RemoveExperiencePointObject(processObjects[i]);
                    }
                    //リストの中身をリセット
                    processObjects.Clear();
                }
            }
        }
    }

    
}
