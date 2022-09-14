public class GameStateInit : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        player.SetStateIdle();
        cameraInteractor.SetCameraInit();
        UIController.Show<UIMainMenu>();
        worldInteractor.ResetWorld();
    }

    public override void Destruct()
    {
        UIController.HideAllPopups();
    }

    public void OnPlayClick()
    {
        //GameStats.Instance.ResetSession();
        //GetComponent<GameStateDeath>().EnableRevive();
    }

    public override void UpdateState()
    {

    }
}
