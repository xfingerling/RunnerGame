public class GameStateDeath : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.Motor.PausePlayer();
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.SwipeDown)
            ToMenu();

        if (InputManager.Instance.SwipeUp)
            ResumeGame();
    }

    private void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }

    private void ResumeGame()
    {
        GameManager.Instance.Motor.RespawnPlayer();
        brain.ChangeState(GetComponent<GameStateGame>());
    }
}
