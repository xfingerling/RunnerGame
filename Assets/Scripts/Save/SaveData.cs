using System;

[Serializable]
public class SaveData
{
    [NonSerialized] private const int HAT_COUNT = 16;

    public int Highscore { set; get; }
    public int Coin { set; get; }
    public DateTime LastSaveTime { set; get; }
    public int CurrentHatIndex { set; get; }
    public byte[] UnlockedHatFlag { set; get; }
    public int localeID { set; get; }
    public float masterVolume { set; get; }

    public SaveData()
    {
        Highscore = 0;
        Coin = 0;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        UnlockedHatFlag = new byte[HAT_COUNT];
        UnlockedHatFlag[0] = 1;
        localeID = 0;
    }
}
