using System.IO;
using UnityEngine;

public class ImportAttackObjectEnumFileRewrite
{
    string target = "//End//";

    public ImportAttackObjectEnumFileRewrite()
    {

    }

    public string EnumFileContent(string content, string attackObjectName)
    {
        //指定ファイルの指定値前に書き込む
        var index = content.IndexOf(target);

        Debug.Log($"content：{content}");
        Debug.Log($"index：{index}");
        //ヒットすれば
        if (index >= 0)
        {
            Debug.Log("ヒット");
            content = content.Insert(index, attackObjectName+ ",\n    ");
        }

        return content;
    }
}
