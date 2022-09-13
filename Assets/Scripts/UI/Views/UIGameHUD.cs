using TMPro;
using UnityEngine;

public class UIGameHUD : View
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _scorePerSessionText;

    //Internal Cooldown
    private float _lastScoreUpdate;
    private float _scoreUpdateDelta = 0.2f;

    public override void Initialize()
    {

    }

    public override void Update()
    {
        base.Update();

        if (Time.time - _lastScoreUpdate > _scoreUpdateDelta)
        {
            _lastScoreUpdate = Time.time;

            _scorePerSessionText.text = $"{Score.scorePerSession}";
            _coinText.text = $"{Bank.coinsPerSession}";
        }
    }
}
