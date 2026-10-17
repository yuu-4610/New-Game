using System.IO;
using UnityEngine;

public class ImportAttackObjectControllerFileRewite
{
    private string inputControllerTextFileStorageLocation;

    public ImportAttackObjectControllerFileRewite(string inputControllerTextFileStorageLocation)
    {
        this.inputControllerTextFileStorageLocation = inputControllerTextFileStorageLocation;
    }

    public string ControllerClassTemplateFileContentRewite(string[] tagNames)
    {
        var textInput = File.ReadAllText(inputControllerTextFileStorageLocation);

        textInput = textInput.Replace($"#{RewiteValueName.CLASS_NAME}#", $"{tagNames[0]}");
        textInput = textInput.Replace($"#{RewiteValueName.CLASS_VARIABLE_NAME}#", tagNames[1]);

        return textInput;
    }
}
