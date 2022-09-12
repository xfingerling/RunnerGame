public class PlayerStateIdle : IPlayerState
{
    public void Construct(Player motor)
    {
        motor.ResetPlayer();
    }

    public void Destruct(Player motor)
    {

    }

    public void ProcessMotion(Player motor)
    {

    }

    public void Transition(Player motor)
    {

    }
}
