using UnityEngine;

public class BackgroungMusic : MonoBehaviour
{
    private AudioSource _backgroundMusic;
    private UISettingsPopup _uiSettingsPopup;

    private void Start()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;

        _backgroundMusic = GetComponent<AudioSource>();
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        var uiControllerInteractor = Game.GetInteractor<UIControllerInteractor>();
        _uiSettingsPopup = uiControllerInteractor.GetView<UISettingsPopup>();
        _uiSettingsPopup.OnMusicToggleEvent += OnMusicToggle;

        if (SaveManager.instance.save.masterVolume)
            _backgroundMusic.volume = 0.1f;
    }

    private void OnMusicToggle(bool enabled)
    {
        if (enabled)
            _backgroundMusic.volume = 0.1f;
        else
            _backgroundMusic.volume = 0;
    }

    private void OnDisable()
    {
        _uiSettingsPopup.OnMusicToggleEvent -= OnMusicToggle;
    }
}
