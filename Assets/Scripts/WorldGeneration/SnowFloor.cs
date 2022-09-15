using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float offsetSpeed = 0.5f;

    private Player _player;

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.player;
    }

    private void Update()
    {
        if (_player == null)
            return;

        transform.position = Vector3.forward * _player.transform.position.z;
        _material.SetVector("_offset", new Vector2(0, -transform.position.z * offsetSpeed));
    }
}
