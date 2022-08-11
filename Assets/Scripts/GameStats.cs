using System;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get { return _instance; } }
    private static GameStats _instance;

    public event Action<int> OnCollectFishEvent;
    public event Action<float> OnScoreChangeEvent;

    [SerializeField] private int _pointsPerFish = 10;
    [SerializeField] private float _distanceModifire = 1.5f;

    private float _score;
    private float _highscore;
    private int _totalFish;
    private int _fishCollectedThisSession;
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
        score += _fishCollectedThisSession * _pointsPerFish;

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

    public void CollectFish()
    {
        _fishCollectedThisSession++;
        OnCollectFishEvent?.Invoke(_fishCollectedThisSession);
    }

    public void ResetSession()
    {
        _score = 0;
        _fishCollectedThisSession = 0;

        OnCollectFishEvent?.Invoke(_fishCollectedThisSession);
        OnScoreChangeEvent?.Invoke(_score);
    }

    public string ScoreToText()
    {
        return _score.ToString("0000000");
    }

    public string FishToText()
    {
        return _fishCollectedThisSession.ToString("0000");
    }
}
