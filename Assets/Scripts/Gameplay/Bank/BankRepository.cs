public class BankRepository : Repository
{
    public int coins { get; set; }

    public override void Initialize()
    {
        coins = SaveManager.instance.save.Coin;
    }

    public override void OnCreate()
    {

    }

    public override void OnStart()
    {

    }

    public override void Save()
    {
        SaveManager.instance.save.Coin = coins;
    }
}
