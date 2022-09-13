public class BankInteractor : Interactor
{
    public int coins => _repository.coins;
    public int coinsPerSession { get; private set; }

    private BankRepository _repository;

    public override void OnCreate()
    {
        base.OnCreate();
        _repository = Game.GetRepository<BankRepository>();
    }

    public override void Initialize()
    {
        Bank.Initialize(this);
    }

    public bool IsEnoughCoins(int value)
    {
        return coins >= value;
    }

    public void AddCoins(object sender, int value = 1)
    {
        _repository.coins += value;
        coinsPerSession += value;
        _repository.Save();

    }

    public void SpendCoins(object sender, int value)
    {
        _repository.coins -= value;
        _repository.Save();
    }

    public void ResetCoinPerSession()
    {
        coinsPerSession = 0;
    }
}
