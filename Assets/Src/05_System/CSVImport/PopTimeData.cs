using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class PopTimeData
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
            float popTimeSceonds = float.Parse(values[1]);

            data[i - 1] = new float[] { id, popTimeSceonds };
        }

        return data;
    }
}
