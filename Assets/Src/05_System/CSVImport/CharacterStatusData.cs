using UnityEngine;

public class CharacterStatusData
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

            Debug.Log(values.Length);
            Debug.Log(values[0]);
            Debug.Log(values[1]);
            
            var charaId = values[0];
            var level = values[1];
            var hp = values[2];
            var power = values[3];

            data[i - 1] = new string[] { charaId, level, hp, power };
        }

        return data;
    }
}
