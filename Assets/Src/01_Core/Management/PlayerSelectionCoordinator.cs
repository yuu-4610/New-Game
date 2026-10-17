using System.Collections;
using UnityEngine;

public class PlayerSelectionCoordinator : MonoBehaviour
{
    //プレイヤー操作の結果を各クラスへ渡す
    [SerializeField] AttackEffectPrefabPool attackEffectPrefabPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }

    private void OnDisable()
    {
        TestEventManager.Instance.testAddAttack -= AttackEffectPop;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackEffectPop(AttackEffectObjectPoolName attackObjectName)
    {
        var attackObjects = new GameObject[4];
        for(int i = 0; i < 4; ++i)
        {
            var attackObject = attackEffectPrefabPool.Pop(attackObjectName.ToString());
            attackObjects[i] = attackObject;
        }

        //攻撃オブジェクトを渡す
        EventManager.Instance.CheckAttackObjectCollectionEvent(attackObjectName, attackObjects);
    }

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        TestEventManager.Instance.testAddAttack += AttackEffectPop;
    }
}
