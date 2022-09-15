using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _currentHatText;
    [SerializeField] private TextMeshProUGUI _completionText;
    [SerializeField] private Image _completionCircle;
    [SerializeField] private GameObject _hatButtonUIPrefab;
    [SerializeField] private Transform _hatUIContainer;

    private HatInteractor _hatInteractor;
    private Hat[] _hats;
    private bool _isInit = false;
    private int _hatCount;
    private int _unlockedHatCount;

    public override void Initialize()
    {
        Game.OnGameInitializedEvent += OnGameInitialized;

        _menuButton.onClick.AddListener(OnClickMenuButton);
    }

    private void OnGameInitialized()
    {
        Game.OnGameInitializedEvent -= OnGameInitialized;

        _hatInteractor = Game.GetInteractor<HatInteractor>();
        _hats = _hatInteractor.hats;

        _currentHatText.text = _hats[_hatInteractor.currentHatIndex].ItemName;
        FillUIShop();

        _coinText.text = $"{Bank.coins}";
        _isInit = true;
    }

    private void OnEnable()
    {
        if (_isInit)
            ResetCompletionCircle();
    }

    private void FillUIShop()
    {
        for (int i = 0; i < _hats.Length; i++)
        {
            int index = i;

            GameObject go = Instantiate(_hatButtonUIPrefab, _hatUIContainer);

            //Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));

            //Icon
            go.transform.GetChild(0).GetComponent<Image>().sprite = _hats[index].IconHat;

            //ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _hats[index].ItemName;

            //Price
            if (_hatInteractor.unlockedHatFlag[i] == 0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _hats[index].ItemPrice.ToString();
            else
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                _unlockedHatCount++;
            }
        }
    }

    private void OnHatClick(int index)
    {
        if (_hatInteractor.unlockedHatFlag[index] == 1)
        {
            _hatInteractor.SelectHat(index);
            _currentHatText.text = _hats[index].ItemName;

            SaveManager.Instance.Save();
        }
        //If we dont have it, can buy it?
        else if (_hats[index].ItemPrice <= Bank.coins)
        {
            Bank.SpendCoins(this, _hats[index].ItemPrice);
            _hatInteractor.SelectHat(index);

            _coinText.text = $"{Bank.coins}";
            _hatUIContainer.GetChild(index).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            _currentHatText.text = _hats[index].ItemName;

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

    private void OnClickMenuButton()
    {
        gameController.SetStateInit();
    }
}
