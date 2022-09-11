using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    [SerializeField] private Button _button;
    public override void Initialize()
    {
        _button.onClick.AddListener(() => UIController.Show<UIMainMenu>());
    }
}
