using System.IO;
using UnityEngine;

public class ImportEnumFileRewrite
{
    private string inputTextFileStorageLocation;

    public ImportEnumFileRewrite(string inputTextFileStorageLocation)
    {
        this.inputTextFileStorageLocation = inputTextFileStorageLocation;
    }

    public string EnumTemplateFileContentRewite(string charactorName)
    {
        //ファイルを読み込む
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        //指定値の変換
        textInput = textInput.Replace($"#{RewiteValueName.ENUMCLASS_NAME}#", $"{charactorName}");

        return textInput;
    }
}
