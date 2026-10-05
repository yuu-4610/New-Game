using UnityEngine;

public class TestExperiencePointSeachGrid : MonoBehaviour
{
    [SerializeField] GameObject[] square;
    public void TestGridGenerate(float cellSize)
    {
        GameObject parentObject = Instantiate(new GameObject());
        parentObject.name = "TestCellObjectParent";
        for (int i = 0; i < square.Length; i++)
        {
            square[i].transform.localScale = new Vector2(cellSize, cellSize);
        }
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                var index = (i % 2 == 0) ? ((int)j % 2) : ((int)(j + 1) % 2);
                var generateObject = Instantiate(square[index]);
                generateObject.transform.parent = parentObject.transform;
                generateObject.transform.position = new Vector3(((-4 + j) * cellSize) + cellSize / 2, ((4 - i) * cellSize) + cellSize / 2, 1);
            }
        }
    }
}
