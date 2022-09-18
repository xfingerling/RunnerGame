public class ScoreRepository : Repository
{
    public int highscore { get; set; }

    public override void Initialize()
    {
        highscore = SaveManager.instance.save.Highscore;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.instance.save.Highscore = highscore;
    }
}
