using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UIDeath : View, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _scorePerSessionText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Button _playButton;
    [SerializeField] private Image _completionCircle;
    [SerializeField] private Button _reviveAdButton;

    private float _timeToDesition = 5f;
    private float _deathTime;

    public override void Initialize()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _reviveAdButton.onClick.AddListener(ShowAd);
    }

    public override void Update()
    {
        base.Update();

        float ratio = (Time.time - _deathTime) / _timeToDesition;
        _completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        _completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            _completionCircle.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _deathTime = Time.time;

        _highscoreText.text = $"{Score.higscore}";
        _scorePerSessionText.text = $"{Score.scorePerSession}";
        _coinText.text = $"{Bank.coins}";
    }

    private void OnPlayButtonClick()
    {
        EnableReviveButton();
        gameController.SetStateInit();
    }

    public void EnableReviveButton()
    {
        _completionCircle.gameObject.SetActive(true);
        Advertisement.Load(AdManager.Instance.AdUnityId, this);
    }

    private void ResumeGame()
    {
        player.SetStateRespawn();
        Hide();
    }

    private void ShowAd()
    {
        AdManager.Instance.ShowRewardedAd(this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {

    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }
    public void OnUnityAdsShowStart(string placementId)
    {

    }
    public void OnUnityAdsShowClick(string placementId)
    {

    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.SKIPPED:
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                _completionCircle.gameObject.SetActive(false);

                // Grant a reward.
                ResumeGame();

                // Load another ad:
                Advertisement.Load(AdManager.Instance.AdUnityId, this);
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                break;
            default:
                break;
        }
    }
}
