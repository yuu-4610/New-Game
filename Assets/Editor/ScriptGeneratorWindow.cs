using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptGeneratorWindow : EditorWindow
{
    private string enemyName;
    private string savePathFolder;
    private bool showCreateScript = true;


    [MenuItem("Tools/Script Generator")]
    public static void Open()
    {
        GetWindow<ScriptGeneratorWindow>("Script Generator");
    }

    private void OnGUI()
    {
        enemyName = EditorGUILayout.TextField("エネミー名", enemyName);

        GUILayout.Space(15); // 15ピクセル分の縦余白

        GUILayout.Label("ファイル一式の生成");
        
        if (GUILayout.Button("生成", GUILayout.Width(100)))
        {
            var generateCharactorProcess = new GenerateCharactorProcess(enemyName);
            generateCharactorProcess.GenerateCharactor();

            enemyName = "";
            Debug.Log($"生成しました：{enemyName}");
        }

        showCreateScript = EditorGUILayout.Foldout(showCreateScript, "StateActionClassの生成");

        if (showCreateScript)
        {
            //StateIdleクラスの生成
            StateIdleClassGenerate();

            //StateIdleクラスの生成
            StateMoveClassGenerate();

            //StateDieクラスの生成
            StateDieClassGenerate();

            //StateAttackクラスの生成
            StateAttackClassGenerate();
        }
    }

    private void StateIdleClassGenerate()
    {
        GUILayout.Label("StateIdleクラスの作成", GUILayout.Width(100));
        if (GUILayout.Button("生成", GUILayout.Width(100)))
        {
            if (enemyName == null || enemyName == "")
            {
                Debug.Log("キャラクター名の指定がありません\n処理を終了します：指定値の不備");
                return;
            }
            savePathFolder = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enemyClassStorageLocation), enemyName, "StateAction");

            //生成命令クラス
            var generateCharactorProcess = new GenerateCharactorProcess(enemyName);
            generateCharactorProcess.StateActionIdleClassGenerate(savePathFolder, generateCharactorProcess.fileNames);

            enemyName = "";
            Debug.Log($"生成しました：{enemyName}StateIdle");
        }
    }

    private void StateMoveClassGenerate()
    {
        GUILayout.Label("StateMoveクラスの作成");
        if (GUILayout.Button("生成", GUILayout.Width(100)))
        {
            if (enemyName == null || enemyName == "")
            {
                Debug.Log("キャラクター名の指定がありません\n処理を終了します：指定値の不備");
                return;
            }
            savePathFolder = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enemyClassStorageLocation), enemyName, "StateAction");

            //生成命令クラス
            var generateCharactorProcess = new GenerateCharactorProcess(enemyName);
            generateCharactorProcess.StateActionMoveClassGenerate(savePathFolder, generateCharactorProcess.fileNames);

            enemyName = "";
            Debug.Log($"生成しました：{enemyName}StateMove");
        }
    }

    private void StateDieClassGenerate()
    {
        GUILayout.Label("StateDieクラスの作成");
        if (GUILayout.Button("生成", GUILayout.Width(100)))
        {
            if (enemyName == null || enemyName == "")
            {
                Debug.Log("キャラクター名の指定がありません\n処理を終了します：指定値の不備");
                return;
            }
            savePathFolder = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enemyClassStorageLocation), enemyName, "StateAction");

            //生成命令クラス
            var generateCharactorProcess = new GenerateCharactorProcess(enemyName);
            generateCharactorProcess.StateActionDieClassGenerate(savePathFolder, generateCharactorProcess.fileNames);

            enemyName = "";
            Debug.Log($"生成しました：{enemyName}StateDie");
        }
    }

    private void StateAttackClassGenerate()
    {
        GUILayout.Label("StateAttackクラスの作成", GUILayout.Width(100));
        if (GUILayout.Button("生成", GUILayout.Width(100)))
        {
            if (enemyName == null || enemyName == "")
            {
                Debug.Log("キャラクター名の指定がありません\n処理を終了します：指定値の不備");
                return;
            }
            savePathFolder = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enemyClassStorageLocation), enemyName, "StateAction");

            //生成命令クラス
            var generateCharactorProcess = new GenerateCharactorProcess(enemyName);
            generateCharactorProcess.StateActionAttackClassGenerate(savePathFolder, generateCharactorProcess.fileNames);

            enemyName = "";
            Debug.Log($"生成しました：{enemyName}StateAttack");
        }
    }
}