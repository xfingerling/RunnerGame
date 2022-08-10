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

        _scoreText.text = $"Highscore: TBD";
        _fishCountText.text = $"Fish: TBD";

        _menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        _menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
    }

    public void OnShopClick()
    {
        //brain.ChangeState(GetComponent<GameStateShop>());
        Debug.Log("Shop");
    }
}
