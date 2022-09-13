using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeath : View
{
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _scorePerSessionText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Button _playButton;
    [SerializeField] private Image _completionCircle;
    [SerializeField] private Button _reviveAdButton;

    private float _deathTime;

    public override void Initialize()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnEnable()
    {
        _highscoreText.text = $"{Score.higscore}";
        _scorePerSessionText.text = $"{Score.scorePerSession}";
        _coinText.text = $"{Bank.coins}";
    }

    private void OnPlayButtonClick()
    {
        gameController.SetStateInit();
    }
}
