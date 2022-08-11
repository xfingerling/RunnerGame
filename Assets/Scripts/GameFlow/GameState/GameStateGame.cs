using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private TextMeshProUGUI _fishCountText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public override void Construct()
    {
        GameManager.Instance.Motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        GameStats.Instance.OnCollectFishEvent += OnCollectFish;
        GameStats.Instance.OnScoreChangeEvent += OnScoreChange;

        _gameUI.SetActive(true);
    }

    public override void Destruct()
    {
        _gameUI.SetActive(false);

        GameStats.Instance.OnCollectFishEvent -= OnCollectFish;
        GameStats.Instance.OnScoreChangeEvent -= OnScoreChange;
    }

    public override void UpdateState()
    {
        GameManager.Instance.WorldGeneration.ScanPosition();
        GameManager.Instance.SceneryChunkGeneration.ScanPosition();
    }

    private void OnCollectFish(int fishAmount)
    {
        _fishCountText.text = fishAmount.ToString();
    }

    private void OnScoreChange(float score)
    {
        _scoreText.text = score.ToString();
    }
}
