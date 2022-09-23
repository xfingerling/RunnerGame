using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class UIControllerInteractor : Interactor
{
    public int localID => _repository.localeID;
    public bool masterVolume => _repository.masterVolume;

    private UIControllerRepository _repository;
    private UIInterface _uiInterface;
    private View _hudView;
    private List<View> _popupViews = new List<View>();
    private View _currentView;
    private readonly Stack<View> _history = new Stack<View>();
    private bool _isActive = false;

    public override void Initialize()
    {
        base.Initialize();
        UIController.Initialize(this);

        _repository = Game.GetRepository<UIControllerRepository>();

        var uiInterfacePrefab = Resources.Load<UIInterface>("UI/[INTERFACE]");
        _uiInterface = Object.Instantiate(uiInterfacePrefab);
    }

    public override void OnStart()
    {
        base.OnStart();

        ChangeLocale(localID);
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

    public void ShowPopup<T>() where T : View
    {
        for (int i = 0; i < _popupViews.Count; i++)
        {
            if (_popupViews[i] is T)
            {
                _popupViews[i].transform.SetAsLastSibling();
                _popupViews[i].Show();
            }
        }
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

    public void SaveToggleMusic(bool enabled)
    {
        _repository.masterVolume = enabled;
        _repository.Save();
    }

    public void ChangeLocale(int localeID)
    {
        if (_isActive)
            return;

        Coroutines.StartRoutine(SetLocale(localeID));

        _repository.localeID = localeID;
        _repository.Save();
    }

    IEnumerator SetLocale(int localeID)
    {
        _isActive = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        _isActive = false;
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
