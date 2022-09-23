using System;

public static class UIController
{
    public static event Action OnUIInitializedEvent;

    public static int localeID => _uiControllerInteractor.localID;
    public static bool masterVolume => _uiControllerInteractor.masterVolume;
    public static bool isInitialized { get; private set; }

    private static UIControllerInteractor _uiControllerInteractor;

    public static void Initialize(UIControllerInteractor interactor)
    {
        _uiControllerInteractor = interactor;
        isInitialized = true;
        OnUIInitializedEvent?.Invoke();
    }

    public static void SaveToggleMusic(bool enabled)
    {
        CheckClass();
        _uiControllerInteractor.SaveToggleMusic(enabled);
    }

    public static T GetView<T>() where T : View
    {
        CheckClass();
        return _uiControllerInteractor.GetView<T>();
    }

    public static void ShowPopup<T>() where T : View
    {
        CheckClass();
        _uiControllerInteractor.ShowPopup<T>();
    }

    public static void Show<T>(bool remember = true) where T : View
    {
        CheckClass();
        _uiControllerInteractor.Show<T>(remember);
    }

    public static void Show(View view, bool remember = true)
    {
        CheckClass();
        _uiControllerInteractor.Show(view, remember);
    }

    public static void ShowLast()
    {
        CheckClass();
        _uiControllerInteractor.ShowLast();
    }

    public static void ShowHUD()
    {
        CheckClass();
        _uiControllerInteractor.ShowHUD();
    }

    public static void HideHUD()
    {
        CheckClass();
        _uiControllerInteractor.HideHUD();
    }

    public static void HideAllPopups()
    {
        CheckClass();
        _uiControllerInteractor.HideAllPopups();
    }

    public static void ChangeLocale(int localeID)
    {
        CheckClass();
        _uiControllerInteractor.ChangeLocale(localeID);
    }

    private static void CheckClass()
    {
        if (!isInitialized)
            throw new Exception("UIController is not init yet");
    }
}
