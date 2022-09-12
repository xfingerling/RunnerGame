public abstract class GameStateBase
{
    protected GameController gameController;
    protected Player player;
    protected CameraInteractor cameraInteractor;
    protected WorldInteractor worldInteractor;

    public virtual void Construct()
    {
        var gameControllerInteractor = Game.GetInteractor<GameControllerInteractor>();
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        cameraInteractor = Game.GetInteractor<CameraInteractor>();
        worldInteractor = Game.GetInteractor<WorldInteractor>();
        gameController = gameControllerInteractor.gameController;
        player = playerInteractor.player;

        player.OnPlayerDeathEvent += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        player.OnPlayerDeathEvent -= OnPlayerDeath;

        gameController.SetStateDeath();
    }

    public abstract void Destruct();
    public abstract void UpdateState();
}
