using UnityEngine;

public class GameFlowInteractor : Interactor
{
    public GameController gameFlow { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        GameController gameFlowPrefab = Resources.Load<GameController>("GameFlow");
        gameFlow = Object.Instantiate(gameFlowPrefab);
    }
}
