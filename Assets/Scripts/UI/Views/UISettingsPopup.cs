using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISettingsPopup : View
{
    public event Action<bool> OnMusicToggleEvent;

    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Dropdown _dropdownLanguege;
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Toggle _masterAudioToggle;

    public override void Initialize()
    {
        _closeButton.onClick.AddListener(OnCloseClick);
        _dropdownLanguege.onValueChanged.AddListener(ChangeLocale);
        _masterAudioToggle.onValueChanged.AddListener(ToggleMusic);

        _dropdownLanguege.value = UIController.localeID;
        _masterAudioToggle.isOn = UIController.masterVolume;
    }

    private void ChangeLocale(int id)
    {
        UIController.ChangeLocale(id);
    }

    private void ToggleMusic(bool enabled)
    {
        if (enabled)
            _mixer.audioMixer.SetFloat("MusicVolume", 0);
        else
            _mixer.audioMixer.SetFloat("MusicVolume", -80);

        OnMusicToggleEvent?.Invoke(enabled);
        UIController.SaveToggleMusic(enabled);
    }

    private void OnCloseClick()
    {
        SaveManager.instance.Save();
        Hide();
    }
}
