using System;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get { return _instance; } }
    private static GameStats _instance;

    public event Action<int> OnCollectCoinEvent;
    public event Action<float> OnScoreChangeEvent;

    [SerializeField] private int _pointsPerCoin = 10;
    [SerializeField] private float _distanceModifire = 1.5f;

    public float score => _score;
    public int coinCollectedThisSession => _coinCollectedThisSession;

    private float _score;
    private float _highscore;
    private int _totalCoin;
    private int _coinCollectedThisSession;
    //Internal Cooldown
    private float _lastScoreUpdate;
    private float _scoreUpdateDelta = 0.2f;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        float score = GameManager.Instance.Motor.transform.position.z * _distanceModifire;
        score += _coinCollectedThisSession * _pointsPerCoin;

        if (score > _score)
        {
            _score = score;
            if (Time.time - _lastScoreUpdate > _scoreUpdateDelta)
            {
                _lastScoreUpdate = Time.time;
                OnScoreChangeEvent?.Invoke(_score);
            }
        }
    }

    public void CollectCoin()
    {
        _coinCollectedThisSession++;
        OnCollectCoinEvent?.Invoke(_coinCollectedThisSession);
    }

    public void ResetSession()
    {
        _score = 0;
        _coinCollectedThisSession = 0;

        OnCollectCoinEvent?.Invoke(_coinCollectedThisSession);
        OnScoreChangeEvent?.Invoke(_score);
    }

    public string ScoreToText()
    {
        return _score.ToString("0000000");
    }

    public string CoinToText()
    {
        return _coinCollectedThisSession.ToString("0000");
    }
}
