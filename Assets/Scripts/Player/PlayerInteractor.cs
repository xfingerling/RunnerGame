using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player player { get; private set; }

    public override void OnCreate()
    {
        base.OnCreate();

        var playerPrefab = Resources.Load<Player>("BananaCat");
        player = Object.Instantiate(playerPrefab);
    }
}
