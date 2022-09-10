using UnityEngine;

public class GameFlowInteractor : Interactor
{
    public GameFlow gameFlow { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        GameFlow gameFlowPrefab = Resources.Load<GameFlow>("GameFlow");
        gameFlow = Object.Instantiate(gameFlowPrefab);
    }
}
