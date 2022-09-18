using Cinemachine;

public class GameStateShop : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        cameraInteractor.SetCameraShop();
        UIController.Show<UIShop>();
    }

    public override void Destruct()
    {
        CinemachineBlendManager.SetNextBlend(new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1));
    }

    public override void UpdateState()
    {
    }
}
