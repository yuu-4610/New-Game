using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PathHelper
{
    /*<責務>指定された値を対応する文字列に変換する
     */
    public static string ToName(ResourcePath pathName)
    {
        switch (pathName)
        {
            case ResourcePath.BGM: return @"Audio\BGM";
            case ResourcePath.SE: return @"Audio\SE";
            case ResourcePath.enemyClassStorageLocation: return @"Src\02_Charactor\Enemy"; //エネミークラスのファイルを作成するディレクトリの先頭
            case ResourcePath.importFileStorageLocation: return @"Resources\ImportFile"; //テンプレートファイルのディレクトリ
            case ResourcePath.enumFileAndStorageLocation: return @"Src\05_System\Enum"; //追記するEnumクラス
            case ResourcePath.importCSVFileStrageLocation: return @"ImportCSV";
            default: return "";
        }
    }
}
