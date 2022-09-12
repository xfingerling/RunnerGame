using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraInteractor : Interactor
{
    public CinemachineVirtualCamera activeCamera;

    private Dictionary<GameCamera, CinemachineVirtualCamera> _cameras;

    public override void Initialize()
    {
        base.Initialize();

        _cameras = new Dictionary<GameCamera, CinemachineVirtualCamera>();
        Transform container = new GameObject("[CAMERAS]").transform;

        var cameraGamePrefab = Resources.Load<CinemachineVirtualCamera>("Cameras/Camera_Game");
        var cameraInitPrefab = Resources.Load<CinemachineVirtualCamera>("Cameras/Camera_Init");
        var cameraShopPrefab = Resources.Load<CinemachineVirtualCamera>("Cameras/Camera_Shop");

        var cameraGame = Object.Instantiate(cameraGamePrefab, container);
        var cameraInit = Object.Instantiate(cameraInitPrefab, container);
        var cameraShop = Object.Instantiate(cameraShopPrefab, container);

        _cameras.Add(GameCamera.Game, cameraGame);
        _cameras.Add(GameCamera.Init, cameraInit);
        _cameras.Add(GameCamera.Shop, cameraShop);

        ActivateCameras();
    }

    public void SetCameraGame()
    {
        SetCamera(GameCamera.Game);
    }
    public void SetCameraInit()
    {
        SetCamera(GameCamera.Init);
    }
    public void SetCameraShop()
    {
        SetCamera(GameCamera.Shop);
    }

    private void SetCamera(GameCamera camera)
    {
        activeCamera = _cameras[camera];
        activeCamera.Priority = 10;

        foreach (var cam in _cameras)
        {
            if (cam.Key != camera)
                cam.Value.Priority = 0;
        }
    }

    private void ActivateCameras()
    {
        foreach (var cam in _cameras)
        {
            cam.Value.gameObject.SetActive(true);
        }
    }
}
