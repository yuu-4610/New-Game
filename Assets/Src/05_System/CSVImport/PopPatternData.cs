using System.IO;
using UnityEngine;

public static class PopPatternData
{
    public static float[][] CSVDataImport(string text)
    {        
        //‚Ps–Ú‚©‚ç‡‚É—v‘f‚O`‘ã“ü
        string[] lines = text.Split('\n');
        //CSVƒf[ƒ^Ši”[•Ï”
        var data = new float[lines.Length - 1][];

        for (int i = 1; i < lines.Length; i++) // 0s–Ú‚Íƒwƒbƒ_[
        {
            //‹LÚ‚ª‚È‚¯‚ê‚ÎˆÈ~‚Ìˆ—‚ğƒXƒ‹[‚·‚é
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');
            
            int id = int.Parse(values[0]);
            float xDifference = float.Parse(values[1]);
            float yDifference = float.Parse(values[2]);

            data[i-1] = new float[] {id,  xDifference, yDifference};
        }

        return data;
    }
}
