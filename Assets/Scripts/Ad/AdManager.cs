using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdManager Instance { get { return _instance; } }
    private static AdManager _instance;

    [SerializeField] private string _gameId;
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
    [SerializeField] private bool _testMode = true;

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

    public void LoadRewardedAd()
    {
        Advertisement.Load(_adUnitId);
    }

    public void ShowRewardedAd(IUnityAdsShowListener showListener)
    {
        Advertisement.Show(_adUnitId, showListener);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log(message);
    }
}

