public class PlayerStateIdle : IPlayerState
{
    public void Construct(Player motor)
    {
        motor.Anim.SetTrigger("Idle");
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
