public class ScoreRepository : Repository
{
    public int highscore { get; set; }

    public override void Initialize()
    {
        highscore = SaveManager.Instance.save.Highscore;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.Instance.save.Highscore = highscore;
    }
}
