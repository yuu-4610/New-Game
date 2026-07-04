using System;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public class GenerateCharactorProcess
{
    private StateActionTemplateContent stateActionTemplateContent;

    private string charactorName = default; //指定したキャラクター名で生成する
    private string charactorFolderPath = default; //キャラクター名のフォルダ
    
    private const string enumFileAndStorageLocation = @"Src\05_System\Enum\CharactorStateEnum.cs"; //追記するEnumクラス
    private string inputTextFileStorageLocation = default; //読み込むテンプレートファイルのフルパス


    public string[] fileNames { get; private set; } = default;


    private bool isFirstProcessCheck = default;
    
    public GenerateCharactorProcess(string charactorName)
    {
        this.charactorName = charactorName;
        charactorFolderPath = charactorName;

        Initialize();
    }

    private void Initialize()
    {
        stateActionTemplateContent = new StateActionTemplateContent(PathHelper.ToName(ResourcePath.importFileStorageLocation));

        //テンプレート内にある指定値の変換用
        var stateActionName = $"{charactorName}State";
        //Controllerクラスの名前を指定
        var controllerClassName = $"{charactorName}Controller";
        //ViewControllerクラスの名前を指定
        var viewControllerClassName = $"{charactorName}ViewController";
        //ViewControllerクラスの変数名を指定
        var variableViewClassName = char.ToLower(viewControllerClassName[0]) + viewControllerClassName.Substring(1);

        fileNames = new string[] { stateActionName, controllerClassName, viewControllerClassName, variableViewClassName };
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
        Directory.CreateDirectory(savePathFolder);

        //生成するControllerクラスのパスを指定
        var controllerFileName = Path.Combine(savePathFolder, $"{charactorName}Controller.cs");
        var viewControllerFileName = Path.Combine(savePathFolder, $"{charactorName}ViewController.cs");
        //CharactorStateEnum.csの指定
        var enumFileDirectory = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enumFileAndStorageLocation));

        //テンプレート内にある指定値の変換用
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

        //----------------------------Contorllerクラスの生成----------------------------//

        //テンプレートファイルの読み込み
        inputTextFileStorageLocation = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.importFileStorageLocation), $"{InporTextFileName.ImportCharactorControllerTemplate}.txt");
        //テキストファイルの書き換え
        var controllerClasstextContent = ControllerClassTemplateFileContentRewite(controllerClassName, viewControllerClassName, variableViewClassName, stateEnumName, stateActionName, variableStateActionName);

        //C#クラスの生成
        File.WriteAllText($"{controllerFileName}", controllerClasstextContent);

        //----------------------------ViewContorllerクラスの生成----------------------------//
        //テンプレートファイルの読み込み
        inputTextFileStorageLocation = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.importFileStorageLocation), $"{InporTextFileName.ImportCharactorViewControllerTemplate}.txt");
        var viewControllerClasstextContent = ViewControllerClassTemplateFileContentRewite(viewControllerClassName, charactorName);

        //C#クラスの生成
        File.WriteAllText($"{viewControllerFileName}", viewControllerClasstextContent);

        //----------------------------Enumへの書き込み----------------------------//
        //テンプレートファイルの読み込み
        inputTextFileStorageLocation = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.importFileStorageLocation), $"{InporTextFileName.ImportCharactorEnumClassTemplate}.txt");
        var enumClassTextContent = EnemyStateENumClassTemplateFileContentReWite(stateEnumName);

        //C#クラスの生成
        File.AppendAllText(enumFileDirectory, enumClassTextContent);

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
        StateActionIdleClassGenerate(savePathFolder, fileNames);


        //----------------------------StateMoveクラスの生成----------------------------//
        StateActionMoveClassGenerate(savePathFolder, fileNames);


        //----------------------------StateDieクラスの生成----------------------------//
        StateActionDieClassGenerate(savePathFolder, fileNames);


        //----------------------------StateAttackクラスの生成----------------------------//
        StateActionAttackClassGenerate(savePathFolder, fileNames);


        Debug.Log($"StateActionクラス生成完了");
        return;
    }


    //InputCharactorController.txt内にある指定値を変換する
    private string ControllerClassTemplateFileContentRewite(string controllerName, string viewControllerName, string variableViewClassName, string enumName, string stateActionName, string variableStateActionName)
    {
        //ファイルを読み込む
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        //指定値の変換
        textInput = textInput.Replace($"#{RewiteValueName.CLASS_NAME}#", $"{controllerName}");
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME}#", $"{viewControllerName}");
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME_TOLOWER_CASE}#", $"{variableViewClassName}");
        textInput = textInput.Replace($"#{RewiteValueName.ENUMCLASS_NAME}#", $"{enumName}");
        textInput = textInput.Replace($"#{RewiteValueName.STATE_NAME}#", $"{stateActionName}");
        textInput = textInput.Replace($"#{RewiteValueName.STATE_NAME_TOLOWER_CASE}#", $"{variableStateActionName}");

        return textInput;
    }
    //InputCharactorViewController.txt内にある指定値を変換する
    private string ViewControllerClassTemplateFileContentRewite(string viewControllerName, string charactorName)
    {
        //ファイルを読み込む
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        //指定値の変換
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME}#", $"{viewControllerName}");
        textInput = textInput.Replace($"#{RewiteValueName.CHARACTOR_NAME}#", $"{charactorName}");

        return textInput;
    }
    //InputCharactorEnumClass.txt内にある指定値を変換する
    private string EnemyStateENumClassTemplateFileContentReWite(string enumName)
    {
        //ファイルを読み込む
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        //指定値の変換
        textInput = textInput.Replace($"#{RewiteValueName.ENUMCLASS_NAME}#", $"{enumName}");

        return textInput;
    }



    public void StateActionIdleClassGenerate(string saveFileDirectory, string[] fileNames)
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

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateIdle.cs");

        //テンプレートファイルの読み込み
        var stateActionIdleClassTextContent = stateActionTemplateContent.ClassFileGenerate($"{fileNames[0]}Idle", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }

    public void StateActionMoveClassGenerate(string saveFileDirectory, string[] fileNames)
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

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateMove.cs");

        //テンプレートファイルの読み込み
        var stateActionIdleClassTextContent = stateActionTemplateContent.ClassFileGenerate($"{fileNames[0]}Move", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }
    public void StateActionDieClassGenerate(string saveFileDirectory, string[] fileNames)
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

        Debug.Log(fileNames);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateDie.cs");

        //テンプレートファイルの読み込み
        var stateActionIdleClassTextContent = stateActionTemplateContent.ClassFileGenerate($"{fileNames[0]}Die", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }

    public void StateActionAttackClassGenerate(string saveFileDirectory, string[] fileNames)
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

        Debug.Log(fileNames);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{charactorName}StateAttack.cs");

        //テンプレートファイルの読み込み
        var stateActionIdleClassTextContent = stateActionTemplateContent.ClassFileGenerate($"{fileNames[0]}Attack", fileNames[1], fileNames[2], fileNames[3]);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, stateActionIdleClassTextContent);
    }
}
