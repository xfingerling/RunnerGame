using UnityEngine;

public class GameStateDeath : GameStateBase
{
    public override void Construct()
    {
        base.Construct();

        SaveManager.instance.Save();
        UIController.Show<UIDeath>();
    }

    public override void Destruct()
    {
        Debug.Log("death destruct");
        Bank.ResetCoinPerSession();
        Score.ResetScorePerSession();
    }

    public override void UpdateState()
    {

    }
}
