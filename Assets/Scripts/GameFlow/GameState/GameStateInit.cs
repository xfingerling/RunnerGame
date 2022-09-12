public class GameStateInit : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        player.SetStateIdle();
        cameraInteractor.SetCameraInit();
        UIController.Show<UIMainMenu>();

        //gameManager.ChangeCamera(GameCamera.Init);

        //_scoreText.text = $"{SaveManager.Instance.save.Highscore.ToString("000000")}";
        //_coinCountText.text = $"{SaveManager.Instance.save.Coin.ToString("0000")}";

        //_menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        //_menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        //_gameManager.SetStateGame();
        //GameStats.Instance.ResetSession();
        //GetComponent<GameStateDeath>().EnableRevive();
    }

    public void OnShopClick()
    {
        //_gameManager.SetStateShop();
    }

    public override void UpdateState()
    {

    }
}
