using Cinemachine;
using UnityEngine;

public class CinemachineVirtualDynamic : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    private Transform _target;

    private void Awake()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;

        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();

        brain.m_DefaultBlend.m_Time = 1;
        brain.m_ShowDebugText = true;

        _cam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _target = playerInteractor.player.transform;

        _cam.Follow = _target;
        _cam.LookAt = _target;
    }
}
