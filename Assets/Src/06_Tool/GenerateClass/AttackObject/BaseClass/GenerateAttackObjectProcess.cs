using System.IO;
using UnityEngine;

public class GenerateAttackObjectProcess
{
    private AttackObjectClassFileTemplateContent attackObjectClassFileTemplateContent;

    private string attackName = default; //指定したキャラクター名で生成する

    private bool isFirstProcessCheck = default;

    public string[] FileContentFileNames { get; private set; } = default; //テンプレートファイルに書き込むクラス名
    public string AttackObjectEnumNmae { get; private set; } = default;

    public GenerateAttackObjectProcess(string charactorName)
    {
        this.attackName = charactorName;

        Initialize();
    }

    private void Initialize()
    {
        //取り込むテンプレートファイルのディレクトリ
        attackObjectClassFileTemplateContent = new AttackObjectClassFileTemplateContent(PathHelper.ToName(ResourcePath.importAttackObjectFileStorageLocation));

        //Controllerクラスの名前を指定
        var className = $"{attackName}";
        //変数名
        var variableClassName = char.ToLower(className[0]) + className.Substring(1);
        //StateのEnum名を指定
        var stateEnumName = $"{attackName}";

        FileContentFileNames = new string[] { className, variableClassName };
        AttackObjectEnumNmae = stateEnumName;

    }

    public void GenerateAttackObject()
    {
        //バリデーションチェック
        if (attackName == null || attackName == "")
        {
            Debug.Log("キャラクター名の指定がありません\n処理を終了します：指定値の不備");
            isFirstProcessCheck = false;
            return;
        }
        if (Directory.Exists(Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.attackObjectClassStorateLocation), attackName)))
        {
            Debug.Log("指定されたフォルダは既に存在してます\n処理を終了します：指定フォルダの重複");
            isFirstProcessCheck = false;
            return;
        }

        //クラスファイルの作成
        GenerateAttackObjecctClass();

        Debug.Log("処理終了");
    }

    private void GenerateAttackObjecctClass()
    {
        //Controllerファイルの保存先の指定
        var savePathFolder = Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.attackObjectClassStorateLocation));


        //----------------------------クラスの生成----------------------------//
        ClassGenerate(savePathFolder, FileContentFileNames);

        //----------------------------Controllerクラスの生成----------------------------//
        ControllerClassGenerate(savePathFolder, FileContentFileNames);

        //----------------------------Processクラスの生成----------------------------//
        ProcessClassGenerate(savePathFolder, FileContentFileNames);

        //----------------------------Enumへの書き込み----------------------------//
        EnumClassGenerate(Path.Combine(Application.dataPath, PathHelper.ToName(ResourcePath.enumFileAndStorageLocation)), AttackObjectEnumNmae);


        isFirstProcessCheck = true;
        Debug.Log($"クラス生成完了");
        return;
    }

    public void ClassGenerate(string saveFileDirectory, string[] tagNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, $"{attackName}.cs");

        var ClassTextContent = attackObjectClassFileTemplateContent.ClassFileGenerate(tagNames);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, ClassTextContent);
    }

    public void ControllerClassGenerate(string saveFileDirectory, string[] tagNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(Path.Combine(saveFileDirectory, "Controller"));

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, "Controller", $"{attackName}Controller.cs");

        var ClassTextContent = attackObjectClassFileTemplateContent.ControllerClassFileGenerate(tagNames);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, ClassTextContent);
    }

    public void ProcessClassGenerate(string saveFileDirectory, string[] tagNames)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(Path.Combine(saveFileDirectory, "Process"));

        //新規作成するファイルのフルパスを指定
        var savePathFolder = Path.Combine(saveFileDirectory, "Process", $"{attackName}Process.cs");

        var ClassTextContent = attackObjectClassFileTemplateContent.ProcessClassFileGenerate(tagNames);

        //C#クラスの生成
        File.WriteAllText(savePathFolder, ClassTextContent);
    }

    public void EnumClassGenerate(string saveFileDirectory, string attackObjectEnumNmae)
    {
        //指定した先にフォルダが存在しなければ作成する
        DirectoryMessage(saveFileDirectory);

        Debug.Log($"saveFileDirectory：{saveFileDirectory}");
        var filePath = Path.Combine(saveFileDirectory, "AttackObjectPoolName.cs");
        Debug.Log(filePath);

        var content =File.ReadAllText(filePath);

        var ClassTextContent = attackObjectClassFileTemplateContent.EnumClassFileRewirte(content, attackObjectEnumNmae);

        File.WriteAllText(filePath, ClassTextContent);
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
