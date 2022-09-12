using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : View
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _coinText;

    public override void Initialize()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _shopButton.onClick.AddListener(() => UIController.Show<UIShop>());
    }

    private void OnPlayButtonClick()
    {
        gameController.SetStateGame();
    }
}
