public class ScoreInteractor : Interactor
{
    public int higscore => _repository.highscore;
    public int scorePerSession { get; private set; }

    private ScoreRepository _repository;
    private Player _player;
    private int _scorePerCoin = 10;
    private float _distanceModifire = 1.5f;

    public override void OnCreate()
    {
        base.OnCreate();

        _repository = Game.GetRepository<ScoreRepository>();
        var playerInteractor = Game.GetInteractor<PlayerInteractor>();
        _player = playerInteractor.player;
    }
    public override void Initialize()
    {
        base.Initialize();

        Score.Initialize(this);
    }

    public void UpdateScore()
    {
        float score = _player.transform.position.z * _distanceModifire;
        score += Bank.coinsPerSession * _scorePerCoin;

        if (score > scorePerSession)
            scorePerSession = (int)score;

        SaveHighscore();
    }

    public void ResetScorePerSession()
    {
        scorePerSession = 0;
    }

    private void SaveHighscore()
    {
        if (scorePerSession > _repository.highscore)
        {
            _repository.highscore = scorePerSession;
            _repository.Save();
        }
    }
}
