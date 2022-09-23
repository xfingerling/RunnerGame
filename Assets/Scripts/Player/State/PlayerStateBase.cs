using UnityEngine;

public abstract class PlayerStateBase
{
    protected GameController gameController;
    protected Player player;
    protected CameraInteractor cameraInteractor;

    public virtual void Construct()
    {
        var gameControllerInteractor = Game.GetInteractor<GameControllerInteractor>();
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        cameraInteractor = Game.GetInteractor<CameraInteractor>();
        gameController = gameControllerInteractor.gameController;
        player = playerInteractor.player;
        Debug.Log(this);
    }
    public abstract void Destruct();
    public abstract void Transition();
    public abstract void ProcessMotion();
}
