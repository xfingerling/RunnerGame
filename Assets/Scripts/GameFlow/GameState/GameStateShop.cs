using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : IGameState
{
    [SerializeField] private GameObject _shopUI;
    [SerializeField] private TextMeshProUGUI _totalCoinText;
    [SerializeField] private TextMeshProUGUI _currentHatText;
    [SerializeField] private GameObject _hatPrefab;
    [SerializeField] private Transform _hatContainer;
    [SerializeField] private HatLogic _hatLogic;
    [SerializeField] private Image _completionCircle;
    [SerializeField] private TextMeshProUGUI _completionText;

    private Hat[] _hats;
    private bool _isInit = false;
    private int _hatCount;
    private int _unlockedHatCount;
    private GameFlow _gameManager;

    public void Construct(GameFlow gameManager)
    {
        if (_gameManager == null)
            _gameManager = gameManager;

        gameManager.ChangeCamera(GameCamera.Shop);
        _hats = Resources.LoadAll<Hat>("Hats");
        _shopUI.SetActive(true);

        _totalCoinText.text = $"{SaveManager.Instance.save.Coin.ToString("0000")}";

        if (!_isInit)
        {
            _currentHatText.text = _hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;
            PopulateShop();

            _isInit = true;
        }

        ResetCompletionCircle();
    }

    public void Destruct(GameFlow gameManager)
    {
        _shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        _gameManager.SetStateInit();
    }

    private void PopulateShop()
    {
        for (int i = 0; i < _hats.Length; i++)
        {
            int index = i;

            GameObject go = Object.Instantiate(_hatPrefab, _hatContainer) as GameObject;

            //Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));

            //Icon
            go.transform.GetChild(0).GetComponent<Image>().sprite = _hats[index].IconHat;

            //ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _hats[index].ItemName;

            //Price
            if (SaveManager.Instance.save.UnlockedHatFlag[i] == 0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _hats[index].ItemPrice.ToString();
            else
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                _unlockedHatCount++;
            }
        }
    }

    private void OnHatClick(int i)
    {
        if (SaveManager.Instance.save.UnlockedHatFlag[i] == 1)
        {
            SaveManager.Instance.save.CurrentHatIndex = i;
            _currentHatText.text = _hats[i].ItemName;
            _hatLogic.SelectHat(i);

            SaveManager.Instance.Save();
        }
        //If we dont have it, can buy it?
        else if (_hats[i].ItemPrice <= SaveManager.Instance.save.Coin)
        {
            SaveManager.Instance.save.Coin -= _hats[i].ItemPrice;
            SaveManager.Instance.save.UnlockedHatFlag[i] = 1;
            SaveManager.Instance.save.CurrentHatIndex = i;
            _totalCoinText.text = $"{SaveManager.Instance.save.Coin.ToString("0000")}";
            _hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            _currentHatText.text = _hats[i].ItemName;
            _hatLogic.SelectHat(i);

            SaveManager.Instance.Save();
            _unlockedHatCount++;
            ResetCompletionCircle();
        }
        //Dont have it , cant buy it
        else
            Debug.Log("Not enough coin");
    }

    private void ResetCompletionCircle()
    {
        int hatCount = _hats.Length - 1;
        int currentlyUnlockedCount = _unlockedHatCount - 1;


        _completionCircle.fillAmount = (float)currentlyUnlockedCount / (float)hatCount;
        _completionText.text = $"{currentlyUnlockedCount} / {hatCount}";
    }

    public void UpdateState(GameFlow gameManager)
    {
    }
}
