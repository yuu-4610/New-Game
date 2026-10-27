using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;
using static UnityEngine.Rendering.DebugUI;

public class GameSceneUIManagement : MonoBehaviour
{
    [SerializeField] LevelUpBonusBoard levelUpBonusBoard;

    private void Start()
    {
        StartCoroutine(EventRegister());
    }
    private void OnDisable()
    {
        TestEventManager.Instance.testLevelUpBonusBordActive -= LevelUpBonus;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LevelUpBonus()
    {
        levelUpBonusBoard.LevelUpProsess();
    }

    private void ActiveLevelUpBonus()
    {
        
    }
    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        TestEventManager.Instance.testLevelUpBonusBordActive += LevelUpBonus;
    }
}
