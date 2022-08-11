using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDeath : GameState
{
    [SerializeField] private float _timeToDesition = 2.5f;
    [SerializeField] private GameObject _deathUI;
    [SerializeField] private TextMeshProUGUI _highscoreText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _fishCountText;
    [SerializeField] private Image _completionCircle;

    private float _deathTime;

    public override void Construct()
    {
        GameManager.Instance.Motor.PausePlayer();

        _deathTime = Time.time;
        _completionCircle.gameObject.SetActive(true);

        if (SaveManager.Instance.save.Highscore < (int)GameStats.Instance.score)
            SaveManager.Instance.save.Highscore = (int)GameStats.Instance.score;

        SaveManager.Instance.save.Fish += GameStats.Instance.fishCollectedThisSession;
        SaveManager.Instance.Save();

        _highscoreText.text = $"Highscore: {SaveManager.Instance.save.Highscore}";
        _scoreText.text = GameStats.Instance.ScoreToText();
        _fishCountText.text = $"Total fish: x{SaveManager.Instance.save.Fish}";

        _deathUI.SetActive(true);
    }

    public override void Destruct()
    {
        _deathUI.SetActive(false);
    }

    public override void UpdateState()
    {
        float ratio = (Time.time - _deathTime) / _timeToDesition;
        _completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        _completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            _completionCircle.gameObject.SetActive(false);
        }
    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());

        GameManager.Instance.Motor.ResetPlayer();
        GameManager.Instance.WorldGeneration.ResetWorld();
        GameManager.Instance.SceneryChunkGeneration.ResetWorld();
    }

    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.Motor.RespawnPlayer();
    }
}
