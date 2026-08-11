using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ExperiencePointSeachGrid : MonoBehaviour
{
    public Dictionary<Vector2Int, List<GameObject>> grid { get; private set; } = new();
    public bool isInitialize { get; private set; } = false;

    private float cellSize = 2f;
    private Vector2Int playerCell;
    public int cellRenge { get; private set; }

    private void Start()
    {
        isInitialize = true;
    }

    //指定値を基準とし、座標の計算をする
    private Vector2Int GetCell(Vector3 position)
    {
        const float cellSize = 5f;

        return new Vector2Int(
            Mathf.FloorToInt(position.x / cellSize),
            Mathf.FloorToInt(position.y / cellSize)
            );
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

        Debug.Log("追加できた");
    }

    public void RemoveExperiencePointObject(GameObject experiencePointObject)
    {
        var cell = experiencePointObject.GetComponent<IExperiencePoint>().GetCell();

        if (!grid.TryGetValue(cell, out var pointObject))
            return;

        pointObject.Remove(experiencePointObject);

        if (pointObject.Count == 0)
            grid.Remove(cell);
    }

    public void FindMagnetTargets(Vector3 playerPosition, float radius)
    {
        //セル座標を取得
        var playerCell = GetCell(playerPosition);
        Debug.Log($"現在のセル：{playerCell}");
        if (this.playerCell == playerCell)
        {
            return;
        }
        else
        {
            this.playerCell = playerCell;
        }

        var cellRange = Mathf.CeilToInt(radius / cellSize);
        this.cellRenge = cellRange;

        var radiusSqr = radius * radius;

        for (int x = -cellRange; x <= cellRange; ++x)
        {
            for (int y = -cellRange; y <= cellRange; ++y)
            {
                Vector2Int cell = playerCell + new Vector2Int(x, y);

                if (!grid.TryGetValue(cell, out var cellObject))
                    continue;

                foreach (GameObject pointObject in cellObject)
                {
                    Vector3 delta = playerPosition - pointObject.transform.position;

                    if (delta.sqrMagnitude <= radiusSqr)
                    {
                        Debug.Log("吸引");
                    }
                }
            }
        }
    }
}
