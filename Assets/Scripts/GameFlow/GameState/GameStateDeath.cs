public class GameStateDeath : GameState
{
    public override void Construct()
    {
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

        GameManager.Instance.Motor.ResetPlayer();
        GameManager.Instance.WorldGeneration.ResetWorld();
        GameManager.Instance.SceneryChunkGeneration.ResetWorld();
    }

    private void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.Motor.RespawnPlayer();
    }
}
