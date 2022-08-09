public class GameStateInit : GameState
{
    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.Tap)
            brain.ChangeState(GetComponent<GameStateGame>());
    }
}
