using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
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
    private SaveState _saveData;
    private bool _isInit = false;
    private int _hatCount;
    private int _unlockedHatCount;

    private void Awake()
    {
        _saveData = SaveManager.Instance.save;
    }

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);
        _hats = Resources.LoadAll<Hat>("Hats");
        _shopUI.SetActive(true);

        _totalCoinText.text = $"Coin: {_saveData.Coin.ToString("0000")}";

        if (!_isInit)
        {
            _currentHatText.text = _hats[_saveData.CurrentHatIndex].ItemName;
            PopulateShop();

            _isInit = true;
        }

        ResetCompletionCircle();
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
            int index = i;

            GameObject go = Instantiate(_hatPrefab, _hatContainer) as GameObject;

            //Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));

            //Icon
            go.transform.GetChild(0).GetComponent<Image>().sprite = _hats[index].IconHat;

            //ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _hats[index].ItemName;

            //Price
            if (_saveData.UnlockedHatFlag[i] == 0)
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
        if (_saveData.UnlockedHatFlag[i] == 1)
        {
            _saveData.CurrentHatIndex = i;
            _currentHatText.text = _hats[i].ItemName;
            _hatLogic.SelectHat(i);

            SaveManager.Instance.Save();
        }
        //If we dont have it, can buy it?
        else if (_hats[i].ItemPrice <= SaveManager.Instance.save.Coin)
        {
            _saveData.Coin -= _hats[i].ItemPrice;
            _saveData.UnlockedHatFlag[i] = 1;
            _saveData.CurrentHatIndex = i;
            _totalCoinText.text = $"Coin: {_saveData.Coin.ToString("0000")}";
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
}
