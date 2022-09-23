using System;

public static class Score
{
    public static event Action OnScoreInitializedEvent;

    public static int higscore
    {
        get
        {
            CheckClass();
            return _scoreInteractor.higscore;
        }
    }
    public static int scorePerSession
    {
        get
        {
            CheckClass();
            return _scoreInteractor.scorePerSession;
        }
    }

    public static float ratioScore
    {
        get
        {
            CheckClass();
            return _scoreInteractor.ratioScore;
        }
    }
    public static bool isInitialized { get; private set; }

    private static ScoreInteractor _scoreInteractor;

    public static void Initialize(ScoreInteractor interactor)
    {
        _scoreInteractor = interactor;
        isInitialized = true;
        OnScoreInitializedEvent?.Invoke();
    }

    public static void UpdateScore()
    {
        CheckClass();
        _scoreInteractor.UpdateScore();
    }

    public static void ResetScorePerSession()
    {
        _scoreInteractor.ResetScorePerSession();
    }

    private static void CheckClass()
    {
        if (!isInitialized)
            throw new Exception("Score is not init yet");
    }
}
