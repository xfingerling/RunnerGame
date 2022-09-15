using Cinemachine;
using UnityEngine;

public class VirtualCameraGame : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _target;

    public void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;

        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();

        brain.m_DefaultBlend.m_Time = 1;

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _target = playerInteractor.player.transform;

        _virtualCamera.Follow = _target;
        _virtualCamera.LookAt = _target;
    }
}
