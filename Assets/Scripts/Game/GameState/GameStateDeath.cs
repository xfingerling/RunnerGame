using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameStateDeath : GameStateBase, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private float _timeToDesition = 2.5f;
    [SerializeField] private Image _completionCircle;
    [SerializeField] private Button _reviveAdButton;

    private float _deathTime;

    public override void Construct()
    {
        SaveManager.Instance.Save();
        UIController.Show<UIDeath>();
        //_deathTime = Time.time;
    }

    public override void Destruct()
    {
        Bank.ResetCoinPerSession();
        Score.ResetScorePerSession();
    }

    public override void UpdateState()
    {
        //float ratio = (Time.time - _deathTime) / _timeToDesition;
        //_completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        //_completionCircle.fillAmount = 1 - ratio;

        //if (ratio > 1)
        //{
        //    _completionCircle.gameObject.SetActive(false);
        //}
    }

    public void EnableRevive()
    {
        _completionCircle.gameObject.SetActive(true);
        Advertisement.Load(AdManager.Instance.AdUnityId, this);
    }

    public void ResumeGame()
    {
        //_gameManager.SetStateGame();
        //_gameManager.Motor.RespawnPlayer();
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
