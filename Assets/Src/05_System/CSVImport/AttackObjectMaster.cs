using UnityEngine;

public class AttackObjectMaster
{
    public static string[][] CSVDataImport(string text)
    {
        //‚Ps–Ú‚©‚ç‡‚É—v‘f‚O`‘ã“ü
        string[] lines = text.Split('\n');
        //CSVƒf[ƒ^Ši”[•Ï”
        var data = new string[lines.Length - 1][];

        for (int i = 1; i < lines.Length; i++) // 0s–Ú‚Íƒwƒbƒ_[
        {
            //‹LÚ‚ª‚È‚¯‚ê‚ÎˆÈ~‚Ìˆ—‚ğƒXƒ‹[‚·‚é
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            string objectId = values[0];
            string level = values[1];
            string explanation = values[2];

            data[i - 1] = new string[] { objectId, level, explanation };
        }

        return data;
    }
}
