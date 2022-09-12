using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected GameController gameController;

    private void Start()
    {
        var gameControllerInteractor = Game.GetInteractor<GameControllerInteractor>();
        gameController = gameControllerInteractor.gameController;
    }

    public abstract void Initialize();

    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void Show() => gameObject.SetActive(true);
}
