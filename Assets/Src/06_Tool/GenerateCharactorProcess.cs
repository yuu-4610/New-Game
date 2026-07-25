using System;
using System.Data.SqlTypes;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public class GenerateCharactorProcess
{
    private CharactorClassFileTemplateContent charactorClassFileTemplateContent;

    private string charactorName = default; //指定したキャラクター名で生成する
    private string charactorFolderPath = default; //キャラクター名のフォルダ
    
    private const string enumFileAndStorageLocation = @"Src\05_System\Enum\CharactorStateEnum.cs"; //追記するEnumクラス
    private string inputTextFileStorageLocation = default; //読み込むテンプレートファイルのフルパス


    public string[] controllerFileContentFileNames { get; private set; } = default; //Controllerクラスに書き込むクラス名
    public string[] viewControllerFileContentFileNames { get; private set; } = default; //ViewControllerクラスに書き込むクラス名
    public string[] stateActionFileContentFileNames { get; private set; } = default; //StateActionクラスに書き込むクラス名
    public string CharactorStateName;

    private bool isFirstProcessCheck = default;
    
    public GenerateCharactorProcess(string charactorName)
    {
        this.charactorName = charactorName;
        charactorFolderPath = charactorName;

        Initialize();
    }

    private void Initialize()
    {
        charactorClassFileTemplateContent = new CharactorClassFileTemplateContent(PathHelper.ToName(ResourcePath.importFileStorageLocation));

        //Controllerクラスの名前を指定
        var controllerClassName = $"{charactorName}Controller";
        //ViewControllerクラスの名前を指定
        var viewControllerClassName = $"{charactorName}ViewController";
        //ViewControllerクラスの変数名を指定
        var variableViewClassName = char.ToLower(viewControllerClassName[0]) + viewControllerClassName.Substring(1);
        //StateのEnum名を指定
        var stateEnumName = $"{charactorName}State";
        //テンプレート内にある指定値の変換用
        var stateActionName = $"{charactorName}State";
        //StateAcionクラスの変数名を指定
        var variableStateActionName = char.ToLower(stateActionName[0]) + stateActionName.Substring(1);


        controllerFileContentFileNames = new string[] { controllerClassName, viewControllerClassName, variableViewClassName, stateEnumName, stateActionName, variableStateActionName };
        viewControllerFileContentFileNames = new string[] { viewControllerClassName , charactorName };
        stateActionFileContentFileNames = new string[] { stateActionName, controllerClassName, viewControllerClassName, variableViewClassName };
        CharactorStateName = stateEnumName;

    }

    public void GenerateCharactor()
    {
        //バリデーションチェック
        if (charactorName == null || charactorName == "")
        {
            Debug.Log("キャラクター名の指定がありません\n処理を終了します：指定値の不備");
            isFirstProcessCheck = false;
            return;
        }
        if (Directory.Exists(Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enemyClassStorageLocation), charactorName)))
        {
            Debug.Log("指定されたフォルダは既に存在してます\n処理を終了します：指定フォルダの重複");
            isFirstProcessCheck = false;
            return;
        }

        //Contorller, ViewController, Enumクラスを作成する
        GenerateEnemyControllerClass();
        //StateIdle, StateMove, StateDieクラスを作成する
        GenerateEnemyStateActionClass();

        Debug.Log("処理終了");
    }

    //Controllerファイルの作成メソッド
    private void GenerateEnemyControllerClass()
    {
        //キャラクター名のフォルダを指定
        charactorFolderPath = charactorName;

        //Controllerファイルの保存先の指定
        var savePathFolder = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enemyClassStorageLocation), charactorFolderPath, "Controller");


        //----------------------------Contorllerクラスの生成----------------------------//
        ControllerClassGenerate(savePathFolder, controllerFileContentFileNames);


        //----------------------------ViewContorllerクラスの生成----------------------------//
        //テンプレートファイルの読み込み
        ViewControllerClassGenerate(savePathFolder, viewControllerFileContentFileNames);


        //----------------------------Enumへの書き込み----------------------------//
        //テンプレートファイルの読み込み
        EnemyStateEnumClassGenerate(Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enumFileAndStorageLocation)), CharactorStateName);


        isFirstProcessCheck = true;
        Debug.Log($"Controllerクラス生成完了");
        return;
    }

    public void GenerateEnemyStateActionClass()
    {

        //Controllerファイルの保存先の指定
        var savePathFolder = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enemyClassStorageLocation), charactorFolderPath, "StateAction");
        Directory.CreateDirectory(savePathFolder);


        //----------------------------StateIdleクラスの生成----------------------------//
        StateActionIdleClassGenerate(savePathFolder, stateActionFileContentFileNames);


        //----------------------------StateMoveクラスの生成----------------------------//
        StateActionMoveClassGenerate(savePathFolder, stateActionFileContentFileNames);


        //----------------------------StateDieクラスの生成----------------------------//
        StateActionDieClassGenerate(savePathFolder, stateActionFileContentFileNames);


        //----------------------------StateAttackクラスの生成----------------------------//
        StateActionAttackClassGenerate(savePathFolder, stateActionFileContentFileNames);


        Debug.Log($"StateActionクラス生成完了");
        return;
    }


    //InputCharactorController.txt内にある指定値を変換する
    public void ControllerClassGenerate(string saveFileDirectory, string[] fileNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}Controller.cs");

        var controllerClassTextContent = charactorClassFileTemplateContent.ControllerClassFileGenerate(fileNames);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, controllerClassTextContent);
    }
    //InputCharactorViewController.txt内にある指定値を変換する
    public void ViewControllerClassGenerate(string saveFileDirectory, string[] fileNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}ViewController.cs");

        var viewControllerClassTextContent = charactorClassFileTemplateContent.ViewControllerClassFileGenerate(fileNames);

        File.WriteAllText(savePathFolder, viewControllerClassTextContent);
    }
    //InputCharactorEnumClass.txt内にある指定値を変換する
    public void EnemyStateEnumClassGenerate(string saveFileDirectory, string charactorStateName)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        var savePathFolder = Path.Combine(saveFileDirectory, $"CharactorStateEnum.cs");

        var enumClassTextContent = charactorClassFileTemplateContent.EnumClassFileRewirte(charactorStateName);

        File.AppendAllText(savePathFolder, enumClassTextContent);
    }



    public void StateActionIdleClassGenerate(string saveFileDirectory, string[] fileNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateIdle.cs");

        //テンプレートファイルの書き込み
        var stateActionIdleClassTextContent = charactorClassFileTemplateContent.StateActionClassFileGenerate($"{fileNames[0]}Idle", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }

    public void StateActionMoveClassGenerate(string saveFileDirectory, string[] fileNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateMove.cs");

        //テンプレートファイルの読み込み
        var stateActionIdleClassTextContent = charactorClassFileTemplateContent.StateActionClassFileGenerate($"{fileNames[0]}Move", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }
    public void StateActionDieClassGenerate(string saveFileDirectory, string[] fileNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        Debug.Log(fileNames);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateDie.cs");

        //テンプレートファイルの読み込み
        var stateActionIdleClassTextContent = charactorClassFileTemplateContent.StateActionClassFileGenerate($"{fileNames[0]}Die", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }

    public void StateActionAttackClassGenerate(string saveFileDirectory, string[] fileNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        Debug.Log(fileNames);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateAttack.cs");

        //テンプレートファイルの読み込み
        var stateActionIdleClassTextContent = charactorClassFileTemplateContent.StateActionClassFileGenerate($"{fileNames[0]}Attack", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }

    private void DirectoryMessage(string saveFileDirectory)
    {
        //指定した先にフォルダが存在しなければ作成する
        if (!Directory.Exists(saveFileDirectory))
        {
            Directory.CreateDirectory(saveFileDirectory);
            Debug.Log("ディレクトリを生成しました");
        }
        else
        {
            Debug.Log("ディレクトリは既に存在します");
        }
    }
}
