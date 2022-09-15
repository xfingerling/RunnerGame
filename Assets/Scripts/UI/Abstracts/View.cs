using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected GameController gameController;
    protected Player player;

    private void Start()
    {
        var gameControllerInteractor = Game.GetInteractor<GameControllerInteractor>();
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        gameController = gameControllerInteractor.gameController;
        player = playerInteractor.player;
    }

    public abstract void Initialize();

    public virtual void Update() { }

    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void Show() => gameObject.SetActive(true);
}
