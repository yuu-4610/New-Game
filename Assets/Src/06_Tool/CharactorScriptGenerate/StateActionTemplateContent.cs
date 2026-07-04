using System.IO;
using UnityEngine;

public class StateActionTemplateContent
{
    private ImportFileRewite importFileRewite; //テンプレートファイルの書き換えを行うクラス
    private string inputTextFileStorageLocation = default; //読み込むテンプレートファイルのフルパス

    public StateActionTemplateContent(string inportFileStorageLocation)
    {
        //取り込むテンプレートファイルのディレクトリを引数でもらう
        inputTextFileStorageLocation = Path.Combine(Application.dataPath, inportFileStorageLocation, $"{InporTextFileName.ImportCharactorStateActionTemplate}.txt");
        importFileRewite = new ImportFileRewite(inputTextFileStorageLocation);
    }

    public string ClassFileGenerate(string stateActionName, string controllerClassName, string viewControllerClassName, string variableViewClassName)
    {
        //テンプレートファイルの書き込み
        var stateActionIdleClassTextContent = importFileRewite.EnemyStateActionClassTemplateFileContentRewite(stateActionName, controllerClassName, viewControllerClassName, variableViewClassName);

        return stateActionIdleClassTextContent;
    }
}
