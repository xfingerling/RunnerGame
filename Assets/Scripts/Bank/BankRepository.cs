public class BankRepository : Repository
{
    public int coins { get; set; }

    public override void Initialize()
    {
        coins = SaveManager.Instance.save.Coin;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.Instance.save.Coin = coins;
    }
}
