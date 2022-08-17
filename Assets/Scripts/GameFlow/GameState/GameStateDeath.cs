using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameStateDeath : GameState, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private float _timeToDesition = 2.5f;
    [SerializeField] private GameObject _deathUI;
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _fishCountText;
    [SerializeField] private Image _completionCircle;
    [SerializeField] private Button _reviveAdButton;

    private float _deathTime;

    public override void Construct()
    {
        GameManager.Instance.Motor.PausePlayer();

        _deathTime = Time.time;
        _deathUI.SetActive(true);

        if (SaveManager.Instance.save.Highscore < (int)GameStats.Instance.score)
            SaveManager.Instance.save.Highscore = (int)GameStats.Instance.score;

        SaveManager.Instance.save.Fish += GameStats.Instance.fishCollectedThisSession;
        SaveManager.Instance.Save();

        _highscoreText.text = $"Highscore: {SaveManager.Instance.save.Highscore}";
        _scoreText.text = GameStats.Instance.ScoreToText();
        _fishCountText.text = $"Total fish: x{SaveManager.Instance.save.Fish}";
    }

    public override void Destruct()
    {
        _deathUI.SetActive(false);
    }

    public override void UpdateState()
    {
        float ratio = (Time.time - _deathTime) / _timeToDesition;
        _completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        _completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            _completionCircle.gameObject.SetActive(false);
        }
    }

    public void EnableRevive()
    {
        _completionCircle.gameObject.SetActive(true);
        Advertisement.Load(AdManager.Instance.AdUnityId, this);
    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());

        GameManager.Instance.Motor.ResetPlayer();
        GameManager.Instance.WorldGeneration.ResetWorld();
        GameManager.Instance.SceneryChunkGeneration.ResetWorld();
    }

    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.Motor.RespawnPlayer();
    }
    private void ShowAd()
    {
        AdManager.Instance.ShowRewardedAd(this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(AdManager.Instance.AdUnityId))
        {
            _reviveAdButton.onClick.AddListener(ShowAd);
        }
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
