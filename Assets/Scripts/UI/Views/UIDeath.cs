using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeath : View
{
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private Image _completionCircle;
    [SerializeField] private Button _reviveAdButton;
    [SerializeField] private Button _playButton;

    private float _deathTime;

    public override void Initialize()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick()
    {
        gameController.SetStateInit();
    }
}
