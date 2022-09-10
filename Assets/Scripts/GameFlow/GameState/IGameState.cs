public interface IGameState
{
    public void Construct(GameFlow gameManager);
    public void Destruct(GameFlow gameManager);
    public void UpdateState(GameFlow gameManager);
}
