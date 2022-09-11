using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIInterface : MonoBehaviour
{
    [SerializeField] private Camera _uiCamera;
    [SerializeField] private Canvas _uiLayerHUD;
    [SerializeField] private Canvas _uiLayerFXUnderPopup;
    [SerializeField] private Canvas _uiLayerPopup;
    [SerializeField] private Canvas _uiLayerFXOverPopup;
    [SerializeField] private Canvas _uiLayerLoadingScreen;
    [SerializeField] private Canvas _canvas;

    private Camera _mainCamera;

    public Canvas uiLayerHUD => _uiLayerHUD;
    public Canvas uiLayerFXUnderPopup => _uiLayerFXUnderPopup;
    public Canvas uiLayerPopup => _uiLayerPopup;
    public Canvas uiLayerFXOverPopup => _uiLayerFXOverPopup;
    public Canvas uiLayerLoadingScreen => _uiLayerLoadingScreen;
    public Canvas canvas => _canvas;

    private void Start()
    {
        Camera.main.GetComponent<UniversalAdditionalCameraData>().cameraStack.Add(_uiCamera);
    }
}
