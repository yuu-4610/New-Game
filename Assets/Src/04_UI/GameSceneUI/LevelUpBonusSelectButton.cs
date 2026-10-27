using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBonusSelectButton : MonoBehaviour
{
    public void SelectLevelUpBonus(int attackEffectObjectPoolName)
    {
        Debug.Log($"{attackEffectObjectPoolName}");
    }
}
