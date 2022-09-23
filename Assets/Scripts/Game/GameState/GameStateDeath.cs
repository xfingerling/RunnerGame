public class GameStateDeath : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        SaveManager.instance.Save();
        UIController.Show<UIDeath>();
    }

    public override void Destruct()
    {
        Bank.ResetCoinPerSession();
        Score.ResetScorePerSession();
    }

    public override void UpdateState()
    {

    }
}
