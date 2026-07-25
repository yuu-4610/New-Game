using System.IO;
using UnityEngine;

public class CharactorClassFileTemplateContent
{
    private ImportStateActiontFileRewite importStateActionFileRewite; //StateActionテンプレートファイルの書き換えを行うクラス
    private ImportControllerFileRewrite importControllerFileRewrite; //Controllerテンプレートファイルの書き換えを行うクラス
    private ImportViewControllerFileRewrite importViewControllerFileRewrite;
    private ImportEnumFileRewrite importEnumFileRewrite;
    //private string inputTextFileStorageLocation = default; //読み込むテンプレートファイルのフルパス

    //コンストラクタ
    public CharactorClassFileTemplateContent(string inportFileStorageLocation)
    {
        //取り込むテンプレートファイルのディレクトリを引数でもらう
        var inputControllerTemplateTextFileStorageLocation = Path.Combine(Application.dataPath, inportFileStorageLocation, $"{ImporTextFileName.ImportCharactorControllerTemplate}.txt");
        var inputStateActionTemplateTextFileStorageLocation = Path.Combine(Application.dataPath, inportFileStorageLocation, $"{ImporTextFileName.ImportCharactorStateActionTemplate}.txt");
        var inputViewControllerTemplateTextFileStorageLocation = Path.Combine(Application.dataPath, inportFileStorageLocation, $"{ImporTextFileName.ImportCharactorViewControllerTemplate}.txt");
        var inputEnumTemplateTextFileStorageLocation = Path.Combine(Application.dataPath, inportFileStorageLocation, $"{ImporTextFileName.ImportCharactorEnumClassTemplate}.txt");

        importControllerFileRewrite = new ImportControllerFileRewrite(inputControllerTemplateTextFileStorageLocation);
        importStateActionFileRewite = new ImportStateActiontFileRewite(inputStateActionTemplateTextFileStorageLocation);
        importViewControllerFileRewrite = new ImportViewControllerFileRewrite(inputViewControllerTemplateTextFileStorageLocation);
        importEnumFileRewrite = new ImportEnumFileRewrite(inputEnumTemplateTextFileStorageLocation);
    }

    //StateActionクラスへの書き込み
    public string StateActionClassFileGenerate(string stateActionName, string controllerClassName, string viewControllerClassName, string variableViewClassName)
    {
        //テンプレートファイルの書き込み
        var stateActionIdleClassTextContent = importStateActionFileRewite.EnemyStateActionClassTemplateFileContentRewite(stateActionName, controllerClassName, viewControllerClassName, variableViewClassName);

        return stateActionIdleClassTextContent;
    }
    //Controllerクラスへの書き込み
    public string ControllerClassFileGenerate(string[] fileNames)
    {
        var controllerClassTextContent = importControllerFileRewrite.ControllerClassTemplateFileContentRewite(fileNames);

        return controllerClassTextContent;
    }

    //ViewControllerクラスへの書き込み
    public string ViewControllerClassFileGenerate(string[] fileNames)
    {
        var controllerClassTextContent = importViewControllerFileRewrite.ViewControllerClassTemplateFileContentRewite(fileNames);

        return controllerClassTextContent;
    }

    public string EnumClassFileRewirte(string charactorName)
    {
        var enumClassTextContent = importEnumFileRewrite.EnumTemplateFileContentRewite(charactorName);

        return enumClassTextContent;
    }
}
