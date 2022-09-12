using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private TextMeshProUGUI _totalCoinText;
    [SerializeField] private TextMeshProUGUI _currentHatText;
    [SerializeField] private TextMeshProUGUI _completionText;
    [SerializeField] private Image _completionCircle;

    public override void Initialize()
    {
        _menuButton.onClick.AddListener(() => UIController.Show<UIMainMenu>());
    }
}
