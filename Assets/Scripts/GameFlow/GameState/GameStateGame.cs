using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public override void Construct()
    {
        GameManager.Instance.Motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        GameStats.Instance.OnCollectCoinEvent += OnCollectCoin;
        GameStats.Instance.OnScoreChangeEvent += OnScoreChange;

        _gameUI.SetActive(true);
    }

    public override void Destruct()
    {
        _gameUI.SetActive(false);

        GameStats.Instance.OnCollectCoinEvent -= OnCollectCoin;
        GameStats.Instance.OnScoreChangeEvent -= OnScoreChange;
    }

    public override void UpdateState()
    {
        GameManager.Instance.WorldGeneration.ScanPosition();
        GameManager.Instance.SceneryChunkGeneration.ScanPosition();
    }

    private void OnCollectCoin(int coinAmount)
    {
        _coinCountText.text = GameStats.Instance.CoinToText();
    }

    private void OnScoreChange(float score)
    {
        _scoreText.text = GameStats.Instance.ScoreToText();
    }
}
