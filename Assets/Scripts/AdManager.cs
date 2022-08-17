using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdManager Instance { get { return _instance; } }
    private static AdManager _instance;

    [SerializeField] private string _gameId;
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
    [SerializeField] private bool _testMode = true;
    [SerializeField] private Button _reviveAdButton;

    public string AdUnityId => _adUnitId;

    private string _adUnitId = null;

    private void Awake()
    {
        _instance = this;

#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void LoadRewardedAd(IUnityAdsLoadListener listener)
    {
        Advertisement.Load(_adUnitId, listener);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadRewardedAd(this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);

        if (placementId.Equals(AdManager.Instance.AdUnityId))
        {
            _reviveAdButton.onClick.AddListener(ShowRewardedAd);
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
                Debug.Log("Unity Ads Rewarded Ad Completed");

                // Grant a reward.
                GameManager.Instance.GetComponent<GameStateDeath>().ResumeGame();

                // Load another ad:
                LoadRewardedAd(this);
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                break;
            default:
                break;
        }
    }
}

