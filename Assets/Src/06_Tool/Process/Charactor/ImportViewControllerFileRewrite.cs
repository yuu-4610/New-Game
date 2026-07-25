using System.IO;
using UnityEngine;


public class ImportViewControllerFileRewrite
{
    private string inputTextFileStorageLocation;

    public ImportViewControllerFileRewrite(string inputTextFileStorageLocation)
    {
        this.inputTextFileStorageLocation = inputTextFileStorageLocation;
    }

    public string ViewControllerClassTemplateFileContentRewite(string[] fileNames)
    {
        //ファイルを読み込む
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        //指定値の変換
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME}#", $"{fileNames[0]}");
        textInput = textInput.Replace($"#{RewiteValueName.CHARACTOR_NAME}#", $"{fileNames[1]}");

        return textInput;
    }
}
