public class UIControllerRepository : Repository
{
    public int localeID { get; set; }
    public float masterVolume { get; set; }

    public override void Initialize()
    {
        localeID = SaveManager.instance.save.localeID;
        masterVolume = SaveManager.instance.save.masterVolume;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.instance.save.localeID = localeID;
        SaveManager.instance.save.masterVolume = masterVolume;
    }
}
