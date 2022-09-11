public interface IGameState
{
    public void Construct(GameController gameManager);
    public void Destruct(GameController gameManager);
    public void UpdateState(GameController gameManager);
}
