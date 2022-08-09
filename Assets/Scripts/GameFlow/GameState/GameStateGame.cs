public class GameStateGame : GameState
{
    public override void Construct()
    {
        GameManager.Instance.Motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);
    }
}
