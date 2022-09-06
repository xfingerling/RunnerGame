using System;

public static class Bank
{
    public static event Action OnBankInitializedEvent;

    public static int coins
    {
        get
        {
            CheckClass();
            return _bankInteractor.coins;
        }
    }
    public static bool isInitialized { get; private set; }

    private static BankInteractor _bankInteractor;

    public static void Initialize(BankInteractor interactor)
    {
        _bankInteractor = interactor;
        isInitialized = true;
        OnBankInitializedEvent?.Invoke();
    }

    public static bool IsEnoughCoins(int value)
    {
        CheckClass();
        return _bankInteractor.IsEnoughCoins(value);
    }

    public static void AddCoins(object sender, int value)
    {
        CheckClass();
        _bankInteractor.AddCoins(sender, value);
    }

    public static void SpendCoins(object sender, int value)
    {
        CheckClass();
        _bankInteractor.SpendCoins(sender, value);
    }

    private static void CheckClass()
    {
        if (!isInitialized)
            throw new Exception("Bank is not init yet");
    }
}
