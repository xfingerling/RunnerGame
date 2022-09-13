using System.Collections.Generic;
using UnityEngine;

public class UIControllerInteractor : Interactor
{
    private UIInterface _uiInterface;
    private View _stratingView;
    private List<View> _views = new List<View>();
    private View[] _viewPrefabs;
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

        InitViews();
    }

    public T GetView<T>() where T : View
    {
        for (int i = 0; i < _views.Count; i++)
        {
            if (_views[i] is T tView)
                return tView;
        }

        return null;
    }

    public void Show<T>(bool remember = true) where T : View
    {
        for (int i = 0; i < _views.Count; i++)
        {
            if (_views[i] is T)
            {
                if (_currentView != null)
                {
                    if (remember)
                        _history.Push(_currentView);

                    _currentView.Hide();
                }

                _views[i].Show();
                _currentView = _views[i];
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

    private void InitViews()
    {
        _viewPrefabs = Resources.LoadAll<View>("UI/Popups");
        Transform container = _uiInterface.uiLayerPopup.transform;

        foreach (var item in _viewPrefabs)
        {
            var go = Object.Instantiate(item, container.transform);
            _views.Add(go);

            go.Initialize();
            go.Hide();
        }
    }
}
