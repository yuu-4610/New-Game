using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPuldown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] LevelUpBonus levelUpBonus;
    private readonly List<int> objectIds = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var names = new List<string>
        {
            AttackEffectObjectPoolName.Slash.ToString(),
            AttackEffectObjectPoolName.Bottom.ToString(),
            AttackEffectObjectPoolName.Ray.ToString(),
            AttackEffectObjectPoolName.Fire.ToString(),
            AttackEffectObjectPoolName.Thunder.ToString(),
            AttackEffectObjectPoolName.lightning.ToString(),
            AttackEffectObjectPoolName.MagicCircle.ToString(),
            AttackEffectObjectPoolName.MagicBom.ToString(),
        };
        objectIds.AddRange(new[] { 
            (int)AttackEffectObjectPoolName.Slash, (int)AttackEffectObjectPoolName.Bottom, (int)AttackEffectObjectPoolName.Ray,
            (int)AttackEffectObjectPoolName.Fire, (int)AttackEffectObjectPoolName.Thunder, (int)AttackEffectObjectPoolName.lightning,
            (int)AttackEffectObjectPoolName.MagicCircle, (int)AttackEffectObjectPoolName.MagicBom
        });
        dropdown.ClearOptions();
        dropdown.AddOptions(names);
        dropdown.onValueChanged.AddListener(OnSelectionChanged);
        //OnSelectionChanged(dropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelectionChanged(int index)
    {
        int selectedId = objectIds[index];
        Debug.Log($"‘I‘ð–¼: {dropdown.options[index].text}, ID: {selectedId}");
        //levelUpBonus.TestSelection(index);
    }
}
