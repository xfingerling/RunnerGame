using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _fishCountText;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);

        _scoreText.text = $"Highscore: {SaveManager.Instance.save.Highscore.ToString("0000000")}";
        _fishCountText.text = $"Fish: {SaveManager.Instance.save.Fish.ToString("0000")}";

        _menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        _menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
        GetComponent<GameStateDeath>().EnableRevive();
    }

    public void OnShopClick()
    {
        brain.ChangeState(GetComponent<GameStateShop>());
    }
}
