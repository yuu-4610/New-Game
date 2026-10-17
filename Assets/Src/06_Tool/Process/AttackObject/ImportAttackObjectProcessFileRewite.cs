using System.IO;
using UnityEngine;

public class ImportAttackObjectProcessFileRewite
{
    private string inputProcessTextFileStorageLocation;

    public ImportAttackObjectProcessFileRewite(string inputProcessTextFileStorageLocation)
    {
        this.inputProcessTextFileStorageLocation = inputProcessTextFileStorageLocation;
    }

    public string ProcessClassTemplateFileContentRewite(string[] tagNames)
    {
        var textInput = File.ReadAllText(inputProcessTextFileStorageLocation);

        textInput = textInput.Replace($"#{RewiteValueName.CLASS_NAME}#", $"{tagNames[0]}");
        textInput = textInput.Replace($"#{RewiteValueName.CLASS_VARIABLE_NAME}#", tagNames[1]);

        return textInput;
    }
}
