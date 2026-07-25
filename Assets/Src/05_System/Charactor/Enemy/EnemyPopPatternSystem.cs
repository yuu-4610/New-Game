using System.Collections;
using System.IO;
using UnityEngine;

public class EnemyPopPatternSystem : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    private float[][] csvData;

    void Start()
    {
        csvData = PopPatternData.CSVDataImport(ImportCSVFileName.PopPattern.ToString());

        StartCoroutine(PopCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PopCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < 4; i++)
        {
            var popObject = EnemyPrefabPool.Instance.Pop(EnemyObjectPoolName.Skeleton.ToString());

            
            popObject.transform.position = new Vector3(targetObject.transform.position.x + csvData[i][1], targetObject.transform.position.y + csvData[i][2], 0);
        }
    }
}
