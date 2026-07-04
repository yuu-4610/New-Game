using UnityEngine;
using System.IO;

public class ImportFileRewite
{
    private string inputTextFileStorageLocation;
    public ImportFileRewite(string inputTextFileStorageLocation)
    {
        this.inputTextFileStorageLocation = inputTextFileStorageLocation;
    }

    public string EnemyStateActionClassTemplateFileContentRewite(string stateActionName, string controllerClassName, string viewControllerClassName, string variableViewClassName)
    {
        //ÉtÉ@ÉCÉãÇì«Ç›çûÇﬁ
        var textInput = File.ReadAllText(inputTextFileStorageLocation);

        textInput = textInput.Replace($"#{RewiteValueName.STATE_NAME}#", stateActionName);
        textInput = textInput.Replace($"#{RewiteValueName.CLASS_NAME}#", controllerClassName);
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME}#", viewControllerClassName);
        textInput = textInput.Replace($"#{RewiteValueName.VIEWCLASS_NAME_TOLOWER_CASE}#", variableViewClassName);

        return textInput;

    }
}
