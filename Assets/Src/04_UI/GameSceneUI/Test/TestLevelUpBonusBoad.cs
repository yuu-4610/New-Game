using System.Collections.Generic;
using UnityEngine;

public class TestLevelUpBonusBoad : MonoBehaviour
{
    //[SerializeField] TestLevelUpBonus testLevelUpBonus;
    private readonly List<int> remainingNumbers = new List<int>();
    private int selectUINumberOfPiece = 3;
    private int[] addNumber = new int[3];
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            remainingNumbers.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタン押下時処理
    public void TestSelectUIActive ()
    {
        var index = new int();
        //ランダムな値を取得、取得したものを削除していく
        for (int i = 0; i < selectUINumberOfPiece; i++)
        {
            index = Random.Range(0, remainingNumbers.Count);
            addNumber[i] = remainingNumbers[index];
            //Debug.Log($"Removeindexs：{addNumber[i]}");
            remainingNumbers.RemoveAt(index);
        }
        //testLevelUpBonus.ActiveLevelUpBonusBord(addNumber);
        //消した分を追加
        for (int i = 0; i < addNumber.Length; ++i)
        {
            //Debug.Log($"Addindexs：{addNumber[i]}");
            remainingNumbers.Add(addNumber[i]);
        }
    }
    public void TestBoardActiveButton()
    {
        TestEventManager.Instance.TestLevelUpBonusBordActiveEvent();
    }
}
