using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private TextMeshProUGUI _fishCountText;
    [SerializeField] private TextMeshProUGUI _highscoreText;

    public override void Construct()
    {
        GameManager.Instance.Motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        _fishCountText.text = "xTBD";
        _highscoreText.text = "TBD";

        _gameUI.SetActive(true);
    }

    public override void Destruct()
    {
        _gameUI.SetActive(false);
    }

    public override void UpdateState()
    {
        GameManager.Instance.WorldGeneration.ScanPosition();
        GameManager.Instance.SceneryChunkGeneration.ScanPosition();
    }
}
