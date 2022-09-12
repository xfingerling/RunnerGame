using UnityEngine;

public class GameControllerInteractor : Interactor
{
    public GameController gameController { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        GameController gameFlowPrefab = Resources.Load<GameController>("GameFlow");
        gameController = Object.Instantiate(gameFlowPrefab);
    }
}
