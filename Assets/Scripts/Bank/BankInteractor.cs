public class BankInteractor : Interactor
{
    private BankRepository _repository;

    public int coins => _repository.coins;

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

    public void AddCoins(object sender, int value)
    {
        _repository.coins += value;
        _repository.Save();
    }

    public void SpendCoins(object sender, int value)
    {
        _repository.coins -= value;
        _repository.Save();
    }
}
