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
}
