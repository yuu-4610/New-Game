using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestGameSceneUIProcess
{
    private readonly List<int> remainingNumbers = new List<int>();
    private int[] addNumber = new int[3];
    public void ActiveLevelUpBonusBord(int attackEffectObjectPoolName, GameObject[] attackSelectButton, GameObject[] positionObject, string[][] csvIconPathData, string[][] csvTextData)
    {
        attackSelectButton[0].SetActive(true);
        attackSelectButton[0].transform.position = positionObject[0].transform.position;

        //アイコンの設定
        var iconObject = attackSelectButton[0].transform.Find("Icon").GetComponent<Image>();
        iconObject.sprite = Resources.Load<Sprite>(csvIconPathData[attackEffectObjectPoolName][1].Trim().Trim('\uFEFF'));

        //レベルを取得
        var tmp = attackSelectButton[0].transform.Find("Text").GetComponent<TextMeshProUGUI>();
        //テーブルの指定業から説明文を取得行
        tmp.text = csvTextData[attackEffectObjectPoolName * 5][2];

    }

    public void TestSelectUI(List<int> remainingNumbers, int[] addNumber)
    {
        for(int j = 0; j < 50; j++)
        {
            var index = new int();
            //ランダムで取得、取得したものを削除していく
            for (int i = 0; i < 3; i++)
            {
                index = Random.Range(0, remainingNumbers.Count);
                addNumber[i] = remainingNumbers[index];
                //Debug.Log($"Removeindexs：{addNumber[i]}");
                if(i == 2)
                {
                    if (addNumber[0] == addNumber[1]) Debug.Log("数が重なっています");
                    else if (addNumber[0] == addNumber[2]) Debug.Log("数が重なっています");
                    else if (addNumber[1] == addNumber[2]) Debug.Log("数が重なっています");
                    else Debug.Log("正常に取得できました。");
                }
                remainingNumbers.RemoveAt(index);
            }
            //消した分を追加
            for (int i = 0; i < addNumber.Length; ++i)
            {
                //Debug.Log($"Addindexs：{addNumber[i]}");
                remainingNumbers.Add(addNumber[i]);
            }
        }
    }
}
