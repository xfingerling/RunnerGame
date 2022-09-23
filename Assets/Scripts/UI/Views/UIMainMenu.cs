using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : View
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _coinText;

    public override void Initialize()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsClick);
    }

    private void OnEnable()
    {
        _highscoreText.text = $"{Score.higscore}";
        _coinText.text = $"{Bank.coins}";
    }

    private void OnPlayButtonClick()
    {
        gameController.SetStateGame();
    }

    private void OnShopButtonClick()
    {
        gameController.SetStateShop();
    }

    private void OnSettingsClick()
    {
        UIController.ShowPopup<UISettingsPopup>();
    }
}
