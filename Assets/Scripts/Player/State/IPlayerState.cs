public interface IPlayerState
{
    public void Construct(Player motor);
    public void Destruct(Player motor);
    public void Transition(Player motor);
    public void ProcessMotion(Player motor);
}
