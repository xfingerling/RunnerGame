public class HatRepository : Repository
{
    public int currentHatIndex { get; set; }
    public byte[] unlockedHatFlag { get; set; }

    public override void Initialize()
    {
        currentHatIndex = SaveManager.instance.save.CurrentHatIndex;
        unlockedHatFlag = SaveManager.instance.save.UnlockedHatFlag;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.instance.save.CurrentHatIndex = currentHatIndex;
        SaveManager.instance.save.UnlockedHatFlag = unlockedHatFlag;
    }
}
