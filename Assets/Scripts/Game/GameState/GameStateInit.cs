using Cinemachine;

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
        CinemachineBlendManager.SetNextBlend(new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1));
    }

    public override void UpdateState()
    {

    }
}
