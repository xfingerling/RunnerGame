public class GameStateGame : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        cameraInteractor.SetCameraGame();
        UIController.Show<UIGameHUD>();
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

    private void OnCollectCoin(int coinAmount)
    {
        //_coinCountText.text = GameStats.Instance.CoinToText();
    }

    private void OnScoreChange(float score)
    {
        //_scoreText.text = GameStats.Instance.ScoreToText();
    }
}
