using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class CSVImportProcessCall
{
    public CSVImportProcessCall()
    {
        
    }

    public async Task<float[][]> GetPopPatternData(string text)
    {
        return await Task.Run(() =>
        {
            float[][] result = PopPatternData.CSVDataImport(text);

            return result;
        });
    }

    public async Task<float[][]> GetPopTimeData(string text)
    {
        return await Task.Run(() =>
        {
            float[][] result = PopTimeData.CSVDataImport(text);

            return result;
        });
    }
    public async Task<string[][]> GetAttackObjectMaster(string text)
    {
        return await Task.Run(() =>
        {
            string[][] result = AttackObjectMaster.CSVDataImport(text);

            return result;
        });
    }
    public async Task<string[][]> GetAttackObjectIconMaster(string text)
    {
        return await Task.Run(() =>
        {
            string[][] result = AttackObjectIconMaster.CSVDataImport(text);

            return result;
        });
    }
}
