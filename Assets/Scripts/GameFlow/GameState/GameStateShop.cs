using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    [SerializeField] private GameObject _shopUI;
    [SerializeField] private TextMeshProUGUI _totalFishText;
    [SerializeField] private GameObject _hatPrefab;
    [SerializeField] private Transform _hatContainer;

    private Hat[] _hats;

    private void Awake()
    {
        _hats = Resources.LoadAll<Hat>("Hats");
        PopulateShop();
    }

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);

        _totalFishText.text = $"Fish: {SaveManager.Instance.save.Fish.ToString("0000")}";

        _shopUI.SetActive(true);
    }

    public override void Destruct()
    {
        _shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }

    private void PopulateShop()
    {
        for (int i = 0; i < _hats.Length; i++)
        {
            GameObject go = Instantiate(_hatPrefab, _hatContainer) as GameObject;
            //Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(i));
            //Icon
            go.transform.GetChild(0).GetComponent<Image>().sprite = _hats[i].IconHat;
            //ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _hats[i].ItemName;
            //Price
            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _hats[i].ItemPrice.ToString();
        }
    }

    private void OnHatClick(int i)
    {
        Debug.Log("Hat was clicked" + i);
    }
}
