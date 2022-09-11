using TMPro;
using UnityEngine;

public class GameStateGame : IGameState
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private WorldInteractor _worldInteractor;

    public void Construct(GameController gameManager)
    {
        if (_worldInteractor == null)
            Game.OnGameInitializedEvent += OnGameInitialized;

        //gameManager.ChangeCamera(GameCamera.Game);

        //GameStats.Instance.OnCollectCoinEvent += OnCollectCoin;
        //GameStats.Instance.OnScoreChangeEvent += OnScoreChange;

        //_gameUI.SetActive(true);
    }



    public void Destruct(GameController gameManager)
    {
        //_gameUI.SetActive(false);

        //GameStats.Instance.OnCollectCoinEvent -= OnCollectCoin;
        //GameStats.Instance.OnScoreChangeEvent -= OnScoreChange;
    }

    public void UpdateState(GameController gameManager)
    {
        if (_worldInteractor == null)
            return;
        _worldInteractor.UpdateLevel();
    }

    private void OnCollectCoin(int coinAmount)
    {
        _coinCountText.text = GameStats.Instance.CoinToText();
    }

    private void OnScoreChange(float score)
    {
        _scoreText.text = GameStats.Instance.ScoreToText();
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        _worldInteractor = Game.GetInteractor<WorldInteractor>();
    }
}
