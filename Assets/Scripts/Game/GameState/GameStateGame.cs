public class GameStateGame : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        cameraInteractor.SetCameraGame();
        UIController.ShowHUD();
        player.SetStateRun();
    }



    public override void Destruct()
    {
    }

    public override void UpdateState()
    {
        worldInteractor?.UpdateLevel();
        Score.UpdateScore();
    }
}
