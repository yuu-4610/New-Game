using UnityEngine;
using System.IO;

public class ImportControllerFileRewrite
{
    private string inputTextFileStorageLocation;

    public ImportControllerFileRewrite(string inputTextFileStorageLocation)
    {
        this.inputTextFileStorageLocation = inputTextFileStorageLocation;
    }

    public string ControllerClassTemplateFileContentRewite(string[] fileNames)
    {
        //ファイルを読み込む
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        //指定値の変換
        textInput = textInput.Replace($"#{RewiteValueName.CLASS_NAME}#", $"{fileNames[0]}");
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME}#", $"{fileNames[1]}");
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME_TOLOWER_CASE}#", $"{fileNames[2]}");
        textInput = textInput.Replace($"#{RewiteValueName.ENUMCLASS_NAME}#", $"{fileNames[3]}");
        textInput = textInput.Replace($"#{RewiteValueName.STATE_NAME}#", $"{fileNames[4]}");
        textInput = textInput.Replace($"#{RewiteValueName.STATE_NAME_TOLOWER_CASE}#", $"{fileNames[5]}");

        return textInput;
    }
}
