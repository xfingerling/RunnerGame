using System.Collections.Generic;
using UnityEngine;

public class UIControllerInteractor : Interactor
{
    private UIInterface _uiInterface;
    private View _hudView;
    private List<View> _popupViews = new List<View>();
    private View _currentView;
    private readonly Stack<View> _history = new Stack<View>();

    public override void Initialize()
    {
        base.Initialize();
        UIController.Initialize(this);

        var uiInterfacePrefab = Resources.Load<UIInterface>("UI/[INTERFACE]");
        _uiInterface = Object.Instantiate(uiInterfacePrefab);
    }

    public override void OnStart()
    {
        base.OnStart();

        InitPopupViews();
        InitHUDView();
    }

    public T GetView<T>() where T : View
    {
        for (int i = 0; i < _popupViews.Count; i++)
        {
            if (_popupViews[i] is T tView)
                return tView;
        }

        return null;
    }

    public void ShowHUD()
    {
        _hudView.Show();
    }

    public void HideHUD()
    {
        _hudView.Hide();
    }

    public void Show<T>(bool remember = true) where T : View
    {
        for (int i = 0; i < _popupViews.Count; i++)
        {
            if (_popupViews[i] is T)
            {
                if (_currentView != null)
                {
                    if (remember)
                        _history.Push(_currentView);

                    _currentView.Hide();
                }

                _popupViews[i].Show();
                _currentView = _popupViews[i];
            }
        }
    }

    public void Show(View view, bool remember = true)
    {
        if (_currentView != null)
        {
            if (remember)
                _history.Push(_currentView);

            _currentView.Hide();
        }

        view.Show();

        _currentView = view;
    }

    public void ShowLast()
    {
        if (_history.Count != 0)
        {
            Show(_history.Pop(), false);
        }
    }

    public void HideAllPopups()
    {
        foreach (var view in _popupViews)
        {
            view.Hide();
        }
    }

    private void InitHUDView()
    {
        var hudPrefab = Resources.Load<View>("UI/HUD/UIGameHUD");
        Transform uiLayerHUD = _uiInterface.uiLayerHUD.transform;

        _hudView = Object.Instantiate(hudPrefab, uiLayerHUD);

        _hudView.Initialize();
        _hudView.Hide();
    }

    private void InitPopupViews()
    {
        var viewPrefabs = Resources.LoadAll<View>("UI/Popups");
        Transform uiLayerPopup = _uiInterface.uiLayerPopup.transform;

        foreach (var item in viewPrefabs)
        {
            var go = Object.Instantiate(item, uiLayerPopup.transform);
            _popupViews.Add(go);

            go.Initialize();
            go.Hide();
        }
    }
}
