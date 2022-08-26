public interface IBaseState
{
    public void Construct(PlayerMotor motor);
    public void Destruct(PlayerMotor motor);
    public void Transition(PlayerMotor motor);
    public void ProcessMotion(PlayerMotor motor);
}
