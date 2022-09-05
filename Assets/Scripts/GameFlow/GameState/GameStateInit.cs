using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinCountText;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Init);

        _scoreText.text = $"{SaveManager.Instance.save.Highscore.ToString("000000")}";
        _coinCountText.text = $"{SaveManager.Instance.save.Coin.ToString("0000")}";

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
