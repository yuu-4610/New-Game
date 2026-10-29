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

            var objectId = values[0];
            var attackName = values[1];
            var level = values[2];
            var explanation = values[3];

            data[i - 1] = new string[] { objectId, attackName, level, explanation };
        }

        return data;
    }
}
