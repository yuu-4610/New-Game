using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestExecution : MonoBehaviour
{
    [SerializeField] GameObject[] attackControlObject;
    [SerializeField] GameObject[] attackObject; //本番では引数で受け取るのでいらない
    public Dictionary<AttackEffectObjectPoolName, IAttackControl> attackObjectCollection = new Dictionary<AttackEffectObjectPoolName, IAttackControl>();
    private string[] attackEffectNames;
    private IAttackControl iAttackControl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackEffectObjectPoolName[] attackEffectArray = (AttackEffectObjectPoolName[])System.Enum.GetValues(typeof(AttackEffectObjectPoolName));
        //attackEffectArrayの値をstring配列で管理
        attackEffectNames = Array.ConvertAll(attackEffectArray, attackEffect => attackEffect.ToString());

        for (int i = 0; i < attackObject.Length; i++)
        {
            if (i == 0 || i == 1 || i == 2 || i == 7 || i == 4 || i == 5 || i == 6)
            {
                continue;
            }

            //コントローラークラスのIFの参照を取得
            Debug.Log(attackControlObject[(int)attackEffectArray[i]].name);
            var objectIF = attackControlObject[(int)attackEffectArray[i]].GetComponent<IAttackControl>();

            var attackObjects = new GameObject[4];
            for (int j = 0; j < 4; ++j)
            {
                var objects = Instantiate(attackObject[i]);

                attackObjects[j] = objects;
            }
            //各攻撃管理クラスをコレクションに追加　＝　攻撃手段を増やした
            attackObjectCollection.Add(attackEffectArray[i], objectIF);
            //各攻撃管理クラスで初期化処理を行う
            objectIF.IsAddObject(attackObjects, this.gameObject);
        }
    }

    private void UpLeveling(AttackEffectObjectPoolName attackEffectObjectPoolName)
    {
        //レベルアップ対象のクラスのオブジェクト名を取得
        var targetControlObject = attackObjectCollection[attackEffectObjectPoolName];

        if (targetControlObject.GetCurrentLevel() >= 5) return;

        targetControlObject.UpLeveling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
