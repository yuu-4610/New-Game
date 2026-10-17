using System.IO;
using UnityEngine;

public class ImportAttackObjectFileRewite
{
    private string inputTextFileStorageLocation;
    

    public ImportAttackObjectFileRewite(string inputTextFileStorageLocation)
    {
        this.inputTextFileStorageLocation = inputTextFileStorageLocation;
    }

    public string ClassTemplateFileContentRewite(string[] tagNames)
    {
        //ファイルを読み込む
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        //指定値の変換
        textInput = textInput.Replace($"#{RewiteValueName.CLASS_NAME}#", $"{tagNames[0]}");
        textInput = textInput.Replace($"#{RewiteValueName.CLASS_VARIABLE_NAME}#", tagNames[1]);

        return textInput;
    }    
}
