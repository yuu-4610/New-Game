using UnityEditor;
using UnityEngine;

public class GenerateAttackObjectScript : EditorWindow
{
    private string attackObjectName;

    [MenuItem("Tools/AttackObjectScriptGenerater")]
    public static void Open()
    {
        GetWindow<GenerateAttackObjectScript>("AttackObjectScriptGenerate");
    }

    private void OnGUI()
    {
        attackObjectName = EditorGUILayout.TextField("攻撃名", attackObjectName);

        GUILayout.Label("ファイルの生成");

        if (GUILayout.Button("生成", GUILayout.Width(100)))
        {
            var generateAttackObjectProcess = new GenerateAttackObjectProcess(attackObjectName);
            generateAttackObjectProcess.GenerateAttackObject();

            attackObjectName = "";
            Debug.Log($"生成しました：{attackObjectName}");
        }
    }
}
