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

    private void AttackEffectPop(AttackEffectObjectPoolName attackEffect)
    {
        var attackObject = attackEffectPrefabPool.Pop(attackEffect.ToString());

        //攻撃オブジェクトを渡す
        EventManager.Instance.CheckAttackObjectCollectionEvent(attackEffect.ToString(), attackObject);
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
