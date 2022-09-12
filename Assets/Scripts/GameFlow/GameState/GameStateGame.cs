public class GameStateGame : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        cameraInteractor.SetCameraGame();
        UIController.Show<UIGameHUD>();
        player.SetStateRun();


        //GameStats.Instance.OnCollectCoinEvent += OnCollectCoin;
        //GameStats.Instance.OnScoreChangeEvent += OnScoreChange;
    }



    public override void Destruct()
    {
        //GameStats.Instance.OnCollectCoinEvent -= OnCollectCoin;
        //GameStats.Instance.OnScoreChangeEvent -= OnScoreChange;
    }

    public override void UpdateState()
    {
        worldInteractor?.UpdateLevel();
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
