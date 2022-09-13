using UnityEngine;

public class PlayerInteractor : Interactor
{
    public Player player { get; private set; }

    private Player _playerPrefab;

    public override void OnCreate()
    {
        base.OnCreate();

        _playerPrefab = Resources.Load<Player>("BananaCat");
        player = Object.Instantiate(_playerPrefab);
    }
}
