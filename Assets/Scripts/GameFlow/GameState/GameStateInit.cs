using TMPro;
using UnityEngine;

public class GameStateInit : IGameState
{
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinCountText;

    private GameFlow _gameManager;
    public void Construct(GameFlow gameManager)
    {
        if (gameManager == null)
            _gameManager = gameManager;

        //gameManager.ChangeCamera(GameCamera.Init);

        //_scoreText.text = $"{SaveManager.Instance.save.Highscore.ToString("000000")}";
        //_coinCountText.text = $"{SaveManager.Instance.save.Coin.ToString("0000")}";

        //_menuUI.SetActive(true);
    }

    public void Destruct(GameFlow gameManager)
    {
        //_menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        //_gameManager.SetStateGame();
        //GameStats.Instance.ResetSession();
        //GetComponent<GameStateDeath>().EnableRevive();
    }

    public void OnShopClick()
    {
        _gameManager.SetStateShop();
    }

    public void UpdateState(GameFlow gameManager)
    {

    }
}
