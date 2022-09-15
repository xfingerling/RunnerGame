public class HatRepository : Repository
{
    public int currentHatIndex { get; set; }
    public byte[] unlockedHatFlag { get; set; }

    public override void Initialize()
    {
        currentHatIndex = SaveManager.Instance.save.CurrentHatIndex;
        unlockedHatFlag = SaveManager.Instance.save.UnlockedHatFlag;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.Instance.save.CurrentHatIndex = currentHatIndex;
        SaveManager.Instance.save.UnlockedHatFlag = unlockedHatFlag;
    }
}
