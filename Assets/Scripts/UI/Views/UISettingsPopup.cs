using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISettingsPopup : View
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Dropdown _dropdownLanguege;
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _masterAudioSlider;

    private float _sliderMasterVolumeValue;

    public override void Initialize()
    {
        _closeButton.onClick.AddListener(OnCloseClick);
        _dropdownLanguege.onValueChanged.AddListener(ChangeLocale);
        _masterAudioSlider.onValueChanged.AddListener(ChangeVolume);

        _dropdownLanguege.value = UIController.localeID;

        Debug.Log(UIController.masterVolume);
        ChangeVolume(UIController.masterVolume);
        _masterAudioSlider.value = UIController.masterVolume;
    }

    private void ChangeLocale(int id)
    {
        UIController.ChangeLocale(id);
    }

    private void ChangeVolume(float volume)
    {
        _mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
        _sliderMasterVolumeValue = volume;
    }

    private void OnCloseClick()
    {
        UIController.SaveVolumeValue(_sliderMasterVolumeValue);

        SaveManager.instance.Save();
        Hide();
    }
}
