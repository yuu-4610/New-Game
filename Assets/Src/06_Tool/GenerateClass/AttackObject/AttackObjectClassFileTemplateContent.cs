using System.IO;
using UnityEngine;

public class AttackObjectClassFileTemplateContent : MonoBehaviour
{
    private ImportAttackObjectFileRewite importAttackObjectFileRewite;
    private ImportAttackObjectControllerFileRewite importAttackObjectControllerFileRewite;
    private ImportAttackObjectProcessFileRewite importAttackObjectProcessFileRewite;
    private ImportAttackObjectEnumFileRewrite importAttackObjectEnumFileRewrite;

    public AttackObjectClassFileTemplateContent(string importFileStorageLocation)
    {
        //取り込むテンプレートファイルのディレクトリを引数でもらう
        var inputTemplateTextFileStorageLocation = Path.Combine(Application.dataPath, importFileStorageLocation, $"{ImporTextFileName.ImportAttackObjectTemplate}.txt");
        var inputControllerTemplateTextFileStorageLocation = Path.Combine(Application.dataPath, importFileStorageLocation, $"{ImporTextFileName.ImportAttackObjectControllerTemplate}.txt");
        var inputProcessTemplateTextFileStorageLocation = Path.Combine(Application.dataPath, importFileStorageLocation, $"{ImporTextFileName.ImportAttackObjectProcessTemplate}.txt");

        importAttackObjectFileRewite = new ImportAttackObjectFileRewite(inputTemplateTextFileStorageLocation);
        importAttackObjectControllerFileRewite = new ImportAttackObjectControllerFileRewite(inputControllerTemplateTextFileStorageLocation);
        importAttackObjectProcessFileRewite = new ImportAttackObjectProcessFileRewite(inputProcessTemplateTextFileStorageLocation);
        importAttackObjectEnumFileRewrite = new ImportAttackObjectEnumFileRewrite();
    }

    public string ClassFileGenerate(string[] tagNames)
    {
        var classTextContent = importAttackObjectFileRewite.ClassTemplateFileContentRewite(tagNames);

        return classTextContent;
    }

    public string ControllerClassFileGenerate(string[] tagNames)
    {
        var controllerClassTextContent = importAttackObjectControllerFileRewite.ControllerClassTemplateFileContentRewite(tagNames);

        return controllerClassTextContent;
    }

    public string ProcessClassFileGenerate(string[] tagNames)
    {
        var processClassTextContent = importAttackObjectProcessFileRewite.ProcessClassTemplateFileContentRewite(tagNames);

        return processClassTextContent;
    }


    public string EnumClassFileRewirte(string content, string attackObjectEnumNmae)
    {
        Debug.Log($"途中まで来た：{content}");
        var enumClassTextContent = importAttackObjectEnumFileRewrite.EnumFileContent(content, attackObjectEnumNmae);

        return enumClassTextContent;
    }
}
